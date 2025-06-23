using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;



namespace Forum.Controllers
{
    public class SignupController : Controller

    {

        private readonly ApplicationDbContext contextget;

        private readonly PasswordHasher<Utilisateurs> passwordHasher;
        private readonly ILogger<SignupController> _logger;
        public SignupController(ApplicationDbContext context, ILogger<SignupController> logger, PasswordHasher<Utilisateurs> passwordHasher)
        {
            contextget = context;
            this.passwordHasher = passwordHasher;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Index(Utilisateurs modele)
        {
            if (ModelState.IsValid)
            {

                if (contextget.Utilisateurs.Any(u => u.Email == modele.Email))
                {
                    ModelState.AddModelError("Email", "Cet email est déjà utilisé");
                    return View(modele);
                }

                var newUtilisateur = new Utilisateurs
                {
                    Nom = modele.Nom,
                    Prenom = modele.Prenom,
                    Email = modele.Email,
                    Motdepasse = passwordHasher.HashPassword(null, modele.Motdepasse),
                    Role = "Admin",
                    Id_etablissement = modele.Id_etablissement,
                };

                contextget.Add(newUtilisateur);

                try
                {
                    contextget.SaveChanges();
                    HttpContext.Session.SetInt32("UserId", newUtilisateur.Id_utilisateur);
                    HttpContext.Session.SetString("UserRole", newUtilisateur.Role);


                    TempData["SuccessMessage"] = "Inscription réussie !";
                    return RedirectToAction("Etablissement", "Signup");

                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                    TempData["ErrorMessage"] = "Une erreur se présente dans le formulaire";
                    return View(modele);
                }
            }
            return View(modele);
            //return RedirectToAction("Etablissement", "Signup");
        }

        [HttpGet]
        public IActionResult Etablissement()
        {
            return View();
        }
        public IActionResult Etablissement_etape2()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Inscrire_utilisateur()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index");
            }

            var utilisateurConnecte = contextget.Utilisateurs.Find(userId);
            if (utilisateurConnecte == null || utilisateurConnecte.Id_etablissement == null)
            {
                return RedirectToAction("Index");
            }

            var users = contextget.Utilisateurs
     .Where(u => u.Id_etablissement == utilisateurConnecte.Id_etablissement
                 && (u.Role == "Etudiant" || u.Role == "Professeur"))
     .ToList();

            ViewBag.Liste_user = users;


            return View();
        }

        [HttpPost]
        public IActionResult SupprimerUtilisateur(int id)
        {
            var utilisateur = contextget.Utilisateurs.Find(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            contextget.Utilisateurs.Remove(utilisateur);
            contextget.SaveChanges();

            TempData["SuccessMessage"] = "Utilisateur supprimé avec succès.";
            return RedirectToAction("Inscrire_utilisateur");
        }


        [HttpPost]
        public IActionResult Inscrire_utilisateur(Utilisateurs modele)
        {
            if (!ModelState.IsValid)
                return View(modele);

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                ModelState.AddModelError("", "Session expirée. Veuillez vous reconnecter.");
                return View(modele);
            }

            var utilisateurConnecte = contextget.Utilisateurs.Find(userId);
            if (utilisateurConnecte?.Id_etablissement == null)
            {
                ModelState.AddModelError("", "Aucun établissement associé à votre compte.");
                return View(modele);
            }

            var nouveau_user = new Utilisateurs
            {
                Nom = modele.Nom,
                Prenom = modele.Prenom,
                Email = modele.Email,
                Motdepasse = passwordHasher.HashPassword(null, modele.Motdepasse),
                Role = modele.Role,
                Id_etablissement = utilisateurConnecte.Id_etablissement,
                Section = modele.Section,
                Matiere = modele.Matiere,
            };


            contextget.Add(nouveau_user);
            try
            {
                contextget.SaveChanges();
                ViewBag.SuccessMessage = "Utilisateur inscrit avec succès !";
                return RedirectToAction("Inscrire_utilisateur");
                
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Erreur base de données : " + ex.InnerException?.Message);
                return View(modele);
            }
        }

        [HttpPost]
        public ActionResult Etablissement(Etablissements modele)
        {
            _logger.LogInformation("Début de la méthode Etablissement");

            var userId = HttpContext.Session.GetInt32("UserId");
            _logger.LogInformation($"UserID from session: {userId}");

            if (userId == null)
            {
                _logger.LogWarning("UserID est null dans la session");
                ModelState.AddModelError("", "Utilisateur non connecté.");
                return View(modele);
            }

            var newEtablissement = new Etablissements
            {
                code_postal = modele.code_postal,
                academie = modele.academie,
                nom_etablissement = modele.nom_etablissement,
                Region = modele.Region,
            };

            _logger.LogInformation("Nouvel établissement créé, prêt à être ajouté");

            contextget.Add(newEtablissement);

            TempData["NomEtablissementCree"] = newEtablissement.nom_etablissement;


            try
            {
                _logger.LogInformation("Tentative de sauvegarde de l'établissement");
                contextget.SaveChanges();
                _logger.LogInformation($"Établissement sauvegardé avec ID: {newEtablissement.Id_etablissement}");

                var utilisateur = contextget.Utilisateurs.Find(userId);
                if (utilisateur != null)
                {
                    _logger.LogInformation($"Utilisateur trouvé: {utilisateur.Email}");
                    utilisateur.Id_etablissement = newEtablissement.Id_etablissement;
                    _logger.LogInformation("Tentative de mise à jour de l'utilisateur");
                    contextget.SaveChanges();
                    _logger.LogInformation("Utilisateur mis à jour avec l'ID établissement");
                }
                else
                {
                    _logger.LogWarning("Utilisateur non trouvé dans la base de données");
                }

                return RedirectToAction("Etablissement_etape2");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Erreur DbUpdateException lors de la sauvegarde");
                ModelState.AddModelError("", "Erreur lors de la sauvegarde. Voir les logs pour plus de détails.");
                return View(modele);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur inattendue");
                ModelState.AddModelError("", "Erreur inattendue. Voir les logs pour plus de détails.");
                return View(modele);
            }
        }

    }
}
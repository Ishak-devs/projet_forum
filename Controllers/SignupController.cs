using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Forum.Controllers
{
    public class SignupController : Controller

    {

        private readonly ApplicationDbContext contextget;

        private readonly PasswordHasher<Utilisateurs> passwordHasher;

        public SignupController(ApplicationDbContext context, PasswordHasher<Utilisateurs> passwordHasher)
        {
            contextget = context;
            this.passwordHasher = passwordHasher;
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
                    HttpContext.Session.SetString("UserId", newUtilisateur.Id_utilisateur.ToString());


                    // Redirection après succès
                    TempData["SuccessMessage"] = "Inscription réussie !";
                    return RedirectToAction("Etablissement", "Signup");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                    return View(modele);
                }
            }
            return View(modele);
            return RedirectToAction("Etablissement", "Signup");
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
            return View();
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

            var nouvelEleve = new Utilisateurs
            {
                Nom = modele.Nom,
                Prenom = modele.Prenom,
                Email = modele.Email,
                Motdepasse = passwordHasher.HashPassword(null, modele.Motdepasse),
                Role = modele.Role,
                Id_etablissement = utilisateurConnecte.Id_etablissement,
                Section = modele.Section,
            };


            contextget.Add(nouvelEleve);
            try
            {
                contextget.SaveChanges();
                TempData["SuccessMessage"] = "Élève inscrit avec succès !";
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

            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                ModelState.AddModelError("", "Utilisateur non connecté.");
                return View(modele);
            }


            var newEtablissement = new Etablissements
            {
                code_postal = modele.code_postal,
                academie = modele.academie,
                nom_etablissement = modele.nom_etablissement,
                type_etablissement = modele.type_etablissement,
                Region = modele.Region,
            };

            contextget.Add(newEtablissement);

            contextget.SaveChanges();
            try
            {
                contextget.SaveChanges();

                var utilisateur = contextget.Utilisateurs.Find(userId);
                if (utilisateur != null)
                {
                    utilisateur.Id_etablissement = newEtablissement.Id_etablissement;
                    contextget.SaveChanges();
                }

                return RedirectToAction("Etablissement_etape2");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                return View(modele);
            }
        }

    }
}
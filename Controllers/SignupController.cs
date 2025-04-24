using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity.Infrastructure;

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
                // Vérifie si l'email existe déjà
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
        public IActionResult Inscrire_Eleve()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Inscrire_Eleve(Utilisateurs modele)
        {
            if (ModelState.IsValid)
            {
                if (contextget.Utilisateurs.Any(u => u.Email == modele.Email))
                {
                    ModelState.AddModelError("Email", "Cet email est déjà utilisé");
                    return View(modele);
                }

                var nouvelEleve = new Utilisateurs
                {
                    Nom = modele.Nom,
                    Prenom = modele.Prenom,
                    Email = modele.Email,
                    Motdepasse = passwordHasher.HashPassword(null, modele.Motdepasse),
                    Role = "Étudiant",
                };

                contextget.Add(nouvelEleve);
                try
                {
                    contextget.SaveChanges();
                    TempData["SuccessMessage"] = "Élève inscrit avec succès à l'établissement !";
                    return RedirectToAction("Inscrire_Eleve");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                }
            }

            return View(modele);
        }


        [HttpPost]
        public ActionResult Etablissement(Etablissements modele)
        {
            string utilisateurId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(utilisateurId))
            {
                ModelState.AddModelError("", "Utilisateur non connecté.");
                return View(modele);
            }

            // Créer le nouvel établissement
            var newEtablissement = new Etablissements
            {
                numero_rue = modele.numero_rue,
                nom_rue = modele.nom_rue,
                code_postal = modele.code_postal,
                ville = modele.ville,
                nom_etablissement = modele.nom_etablissement,
            };

            contextget.Add(newEtablissement);

            try
            {
                contextget.SaveChanges();
                var utilisateur = contextget.Utilisateurs.Find(int.Parse(utilisateurId));
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
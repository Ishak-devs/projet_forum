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


        [HttpPost] //Méthode post pour gerer l inscription
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
                    Role = "admin",
                    Id_promo = modele.Id_promo
                };

                contextget.Add(newUtilisateur);

                try
                {
                    contextget.SaveChanges();
                    HttpContext.Session.SetString("user_id", newUtilisateur.Id_utilisateur.ToString());


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

        [HttpPost]
        public ActionResult Etablissement(Etablissements modele)
        {

            if (ModelState.IsValid)
            {
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
                    HttpContext.Session.SetString("user_id", newEtablissement.Id_etablissement.ToString());
                    return RedirectToAction("Etablissement_etape_2", "Signup");
                }
                catch (DbUpdateException ex)
                {
                    ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                    return View(modele);
                }

            }
            return View();
        }

        public ActionResult Etablissement_etape_2()
        {
            return View();
        }
    }
}
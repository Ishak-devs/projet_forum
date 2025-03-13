using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class SignupController : Controller
        
    {

        private readonly ApplicationDbContext contextget;

        //Initialisation de la valeur passwordhasher lié a l'entité Eleve
        private readonly PasswordHasher<Eleve> passwordHasher;
        private readonly PasswordHasher<Professeur> profsseurHasher;

        public SignupController(ApplicationDbContext context, PasswordHasher<Eleve> passwordHasher, PasswordHasher<Professeur> profsseurHasher) 
        {
            contextget = context ;
            //On récupere passwordhasher que nous avons initialisé pour l'utiliser dans notre class
            this.passwordHasher = passwordHasher;
            this.profsseurHasher = profsseurHasher;
        }


        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(SignupViewModel modele)
        {

            //if (ModelState.IsValid)
            //{
            if (modele.Role == "Elève")
            {

                //Récuperer l'élève à ajouter depuis le formulaire
                var newEleve = new Eleve
                {
                    Nom = modele.Nom,
                    Prenom = modele.Prenom,
                    Email = modele.Email,
                    //on hash le mot de passe
                    Password = passwordHasher.HashPassword(null, modele.Password)
                };
                contextget.Add(newEleve);
                //Sauvegarder les changements dans la base de données
                contextget.SaveChanges();
            }

            else if (modele.Role == "Professeur")
            {
                //Ajouter le nouveau professeur a la table
                var newProfesseur = new Professeur
                {
                    Nom = modele.Nom,
                    Prenom = modele.Prenom,
                    Email = modele.Email,
                    Password = profsseurHasher.HashPassword(null, modele.Password),
                    Matiere = modele.Matiere
                };
                contextget.Add(newProfesseur);
                contextget.SaveChanges();
            }
            if (ModelState.IsValid)
            {
                // Ajouter le professeur dans la table Professeur
                
                    ViewBag.Messagesuccess = "Inscription réussie !";
                    //ViewBag.SelectedRole = modele.Role; 
                    //ViewBag.SelectedMatiere = modele.Matiere;
                ModelState.Clear();
                    return View(new SignupViewModel());
                }
            else
            {
                ViewBag.MessageError = "Problème d'inscription !";
            }
            //}
            //else
            //{
            //    ViewBag.MessageError = "Il y a des erreurs dans le formulaire. Veuillez corriger les champs indiqués.";
            //}

            return View(modele);

            //    else
            //    {
            //        ModelState.Clear();

            //    }

            //}
        }
    }
}
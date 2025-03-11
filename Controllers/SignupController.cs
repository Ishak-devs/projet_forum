using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class SignupController : Controller
        
    {

        private readonly ApplicationDbContext contextget;
        public SignupController(ApplicationDbContext context) 
        {
            contextget = context ;
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(SignupViewModel modele)
        {
            if (ModelState.IsValid)
            {
                if (modele.Role == "Elève")
                {
                    //Récuperer l'élève à ajouter depuis le formulaire
                    var newEleve = new Eleve
                    {
                        Nom = modele.Nom,
                        Prenom = modele.Prenom,
                        Email = modele.Email,
                        Password = modele.Password,
                    };
                    contextget.Add(newEleve);
                    //Sauvegarder les changements dans la base de données
                    contextget.SaveChanges();
                }

                else if (modele.Role == "Professeur")
                {
                    //Ajouter le nouvel élève a la table
                    var newProfesseur = new Professeur
                    {
                        Nom = modele.Nom,
                        Prenom = modele.Prenom,
                        Email = modele.Email,
                        Password = modele.Password
                    };

                    // Ajouter le professeur dans la table Professeur
                    contextget.Add(newProfesseur);

                    ViewBag.Messagesuccess = "Inscription réussie !";
                    ModelState.Clear();
                    return View(new SignupViewModel());
                }
            }
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
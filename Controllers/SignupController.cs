using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class SignupController : Controller
    {
        private readonly ApplicationDbContext contextget;

      [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(SignupViewModel eleve, ApplicationDbContext contextget)
        {
            if (ModelState.IsValid)
            {
                //Récuperer l'élève à ajouter depuis le formulaire
                var newEleve = new Eleve
                {
                    Nom = eleve.Nom,
                    Prenom = eleve.Prenom,
                    Email = eleve.Email,
                    Password = eleve.Password,
                };
                //Ajouter le nouvel élève a la table
                contextget.Add(newEleve);
                //Sauvegarder les changements dans la base de données
                contextget.SaveChanges();


                ViewBag.Messagesuccess = "Inscription réussie !";
                ModelState.Clear();
                return View(new SignupViewModel());
            }
            //else
            //{
            //    ViewBag.MessageError = "Il y a des erreurs dans le formulaire. Veuillez corriger les champs indiqués.";
            //}

            return View(eleve);


            //    else
            //    {
            //        ModelState.Clear();

            //    }

            //}
        }
    }
}

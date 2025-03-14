using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class SignupController : Controller

    {

        private readonly ApplicationDbContext contextget;

        private readonly PasswordHasher<Eleve> passwordHasher;
        private readonly PasswordHasher<Professeur> profsseurHasher;

        public SignupController(ApplicationDbContext context, PasswordHasher<Eleve> passwordHasher, PasswordHasher<Professeur> profsseurHasher)
        {
            contextget = context;
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
                if (modele.Role == "Elève")
                {
                    if 
                        (!string.IsNullOrEmpty(modele.Nom) &&
                         !string.IsNullOrEmpty(modele.Prenom) &&
                         !string.IsNullOrEmpty(modele.Email) &&
                         !string.IsNullOrEmpty(modele.Password) &&
                         !string.IsNullOrEmpty(modele.Role))
                    {
                        {
                            //Récuperer l'élève à ajouter depuis le formulaire
                            var newEleve = new Eleve
                            {
                                Nom = modele.Nom,
                                Prenom = modele.Prenom,
                                Email = modele.Email,
                                Password = passwordHasher.HashPassword(null, modele.Password) //on hash le mot de passe
                            };
                            contextget.Add(newEleve);
                            contextget.SaveChanges();
                            ViewBag.Messagesuccess = "Inscription de l'élève réussie !";
                            ModelState.Clear();
                            return View(new SignupViewModel());

                        }
                    }


                    else if (modele.Role == "Professeur")
                    {
                    if (ModelState.IsValid)
                    {
                       

                        {
                            {

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
                                ViewBag.Messagesuccess = "Inscription du professeur réussie !";
                            }
                        }


                        ModelState.Clear();
                        return View(new SignupViewModel());
                    }

                    ViewBag.MessageError = "Il y a des erreurs dans le formulaire.";
                    return View(modele);

                }
            }
            return View();

        }
    }
}
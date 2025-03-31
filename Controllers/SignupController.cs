using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity.Infrastructure;

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
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost] //Méthode post pour gerer l inscription
        public ActionResult Index(SignupViewModel modele)
        {

            bool isInvalid = !ModelState.IsValid;

            if (isInvalid)
            {


                if (modele.Role == "Elève")
                {
                    var newEleve = new Eleve //Nouvelle eleve a ajouté
                    {
                        Nom = modele.Nom,
                        Prenom = modele.Prenom,
                        Email = modele.Email,
                        Password = passwordHasher.HashPassword(null, modele.Password) //On hash le mdp
                    };
                    contextget.Add(newEleve);
                    try
                    {
                        contextget.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                        return View(modele);
                    }
                    //contextget.SaveChanges(); //Update database
                    ViewBag.Messagesuccess = "Inscription de l'élève réussie !";
                }


                else if (modele.Role == "Professeur")
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
                    try
                    {
                        contextget.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        ModelState.AddModelError("", "Erreur d'enregistrement : " + ex.Message);
                        return View(modele);
                    }
                    //contextget.SaveChanges();
                    //ViewBag.Messagesuccess = "Inscription du professeur réussie !";
                }
            }

            // :)
            return View(new SignupViewModel());
        }
    }
}
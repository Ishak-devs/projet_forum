using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using System.Data.Entity;
//using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class AuthentificationController : Controller
    {

        private readonly ApplicationDbContext contextget; //Initialisation de contextget

        public AuthentificationController(ApplicationDbContext context)
        {
            contextget = context; //On stock le context dans contextget
        }

        [HttpGet]
        public IActionResult Authentification()
        {

            return View();
        }


        [HttpPost]
        public async Task<ActionResult> AuthentificationAsync(LoginViewModel eleve)
        {
            //if (ModelState.IsValid)
            //{
                var utilisateurExiste = await contextget.Eleves //Initialisation de la variable utilisateurExiste
                 .AnyAsync(e => e.Email == eleve.Email && e.Password == eleve.Password); //Méthode async pour verifier les données


                //Si les données sont ok
                if (utilisateurExiste)
                {
                    ViewBag.Messagesuccess = "Vous êtes connecté !";
                    ModelState.Clear();
                    return View();
                    //return View(new LoginViewModel());
                }
               
            //}
            else
            {
                ViewBag.MessageError = "Il y a des erreurs dans le formulaire. Veuillez corriger les champs indiqués.";
            }

            return View(eleve);


            //    else
            //    {
            

            //    }

            //}
        }
    }
}

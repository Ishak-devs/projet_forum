using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
//using System.Data.Entity;
//using Microsoft.AspNetCore.Identity;
using System.Linq;

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
        public async Task<IActionResult> Authentification(LoginViewModel modele)
        {
            //if (ModelState.IsValid)
            //{
            if (modele.Role == "Elève")
            {
                var ElèveExiste = await contextget.Eleves
                .AnyAsync(e => e.Email == modele.Email && e.Password == modele.Password); ; //Méthode async pour verifier les données


                //Si les données sont ok
                if (ElèveExiste)
                {
                    ViewBag.Messagesuccess = "Vous êtes connecté en tant qu'élève !";
                    ModelState.Clear();
                    return View();
                    //return View(new LoginViewModel());
                }

                //}
                else
                {
                    ViewBag.MessageError = "Problème dans la requete.";
                }
            }
            if (modele.Role == "Professeur")
            {
                var ProfesseurExiste = await contextget.Professeurs
                .AnyAsync(e => e.Email == modele.Email && e.Password == modele.Password); ; //Méthode async pour verifier les données


                //Si les données sont ok
                if (ProfesseurExiste)
                {
                    ViewBag.Messagesuccess = "Vous êtes connecté en tant que professeur !";
                    ModelState.Clear();
                    return View();
                    //return View(new LoginViewModel());
                }

                //}
                else
                {
                    ViewBag.MessageError = "Problème dans la requete.";
                }
            }



                return View(modele);


            //    else
            //    {
            

            //    }

            //}
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
//using System.Data.Entity;
//using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class AuthentificationController : Controller
    {
       

        private readonly ApplicationDbContext contextget; //creation to contextget
        private readonly PasswordHasher<Eleve> elevePasswordHasher;
        private readonly PasswordHasher<Professeur> professeurPasswordHasher;

        public AuthentificationController(ApplicationDbContext context)
        {
            contextget = context; //we keep our context on contextget
            elevePasswordHasher = new PasswordHasher<Eleve>();
            professeurPasswordHasher = new PasswordHasher<Professeur>();
        }

        [HttpGet]
        public IActionResult Authentification()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Authentification(LoginViewModel modele)
        {
            if (ModelState.IsValid)
            {
                if (modele.Role == "Elève")
                {
                    var Eleve = await contextget.Eleves
                    .FirstOrDefaultAsync(e => e.Email == modele.Email);

                    if (Eleve == null)
                    {
                        ViewBag.Message_error_email = "L'email saisi n'est pas reconnu" ;
                    }

                        //.AnyAsync(e => e.Email == modele.Email && e.Password == modele.Password); ; //check in table


                        //Si les données sont ok
                        if (Eleve != null && elevePasswordHasher.VerifyHashedPassword(null, Eleve.Password, modele.Password) == PasswordVerificationResult.Success)
                    {
                        ViewBag.Messagesuccess = "Vous êtes connecté en tant qu'élève !";
                        ModelState.Clear();
                        return View();
                        //return View(new LoginViewModel());
                    }

                    //}
                    else
                    {
                        ViewBag.MessageError = "Vérifiez votre mot de passe.";
                    }
                }





                if (modele.Role == "Professeur")
                {
                    var Professeur = await contextget.Professeurs
                    .FirstOrDefaultAsync(p => p.Email == modele.Email);

                    if (Professeur == null)
                    {
                        ViewBag.Message_error_email = "L'email saisi n'est pas reconnu";
                    }
                    //.AnyAsync(e => e.Email == modele.Email && e.Password == modele.Password); ; //check for professeur


                    //Si les données sont ok
                    if (Professeur != null && elevePasswordHasher.VerifyHashedPassword(null, Professeur.Password, modele.Password) == PasswordVerificationResult.Success)
                    {
                        ViewBag.Messagesuccess = "Vous êtes connecté en tant qu'élève !";
                        ModelState.Clear();
                        return View();
                        //return View(new LoginViewModel());
                    }

                    //}
                    else
                    {
                        ViewBag.MessageError = "Vérifiez votre mot de passe.";
                    }
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

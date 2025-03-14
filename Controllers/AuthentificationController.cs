using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
//using System.Data.Entity;
//using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace Forum.Controllers

{
    public class AuthentificationController : Controller
    {
       

        private readonly ApplicationDbContext contextget; //creation to contextget
        private readonly PasswordHasher<Eleve> elevePasswordHasher;
        private readonly PasswordHasher<Professeur> professeurPasswordHasher;

        public AuthentificationController(ApplicationDbContext context)
        {
            contextget = context; 
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

                        //.AnyAsync(e => e.Email == modele.Email && e.Password == modele.Password); ; 


                        //Si les données sont ok
                        if (Eleve != null && elevePasswordHasher.VerifyHashedPassword(null, Eleve.Password, modele.Password) == PasswordVerificationResult.Success)
                    {
                        //Stockage de l'id dans une vraible de session
                        HttpContext.Session.SetString("Eleve_id", Eleve.Id.ToString());
                        //Console.WriteLine("Eleve ID stored in session: " + HttpContext.Session.GetString("Eleve_id"));
                        //ViewBag.Messagesuccess = "Vous êtes connecté en tant qu'élève !";
                        //ViewBag.SessionEleveId = HttpContext.Session.GetString("Eleve_id");

                        ModelState.Clear();
                        return RedirectToAction("Eleve", "Dashboard");
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
                    //.AnyAsync(e => e.Email == modele.Email && e.Password == modele.Password); ; 


                    //Si les données sont ok
                    if (Professeur != null && professeurPasswordHasher.VerifyHashedPassword(null, Professeur.Password, modele.Password) == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("Prof_id", Professeur.Id.ToString());
                        Console.WriteLine("Prof_id dans la session: " + HttpContext.Session.GetString("Prof_id"));
                        //ViewBag.Messagesuccess = "Vous êtes connecté en tant que professeur !";
                        //ViewBag.SessionProf_id = HttpContext.Session.GetString("Prof_id");
                        ModelState.Clear();
                        return RedirectToAction("Professeur", "Dashboard");
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

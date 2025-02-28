using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class SignupController : Controller
    {

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Signup(SignupViewModel eleve)
        {
            if (ModelState.IsValid)
            {
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

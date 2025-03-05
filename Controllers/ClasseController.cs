using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class ClasseController : Controller
    {

        [HttpGet]
        public IActionResult Creeclasse()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Creeclasse(ClasseViewModel classeview)
        {
            if (ModelState.IsValid)
            {

                ViewBag.Messagesuccess = "Vous êtes connecté !";
                ModelState.Clear();
                return View(new ClasseViewModel());
            }
            //else
            //{
            //    ViewBag.MessageError = "Il y a des erreurs dans le formulaire. Veuillez corriger les champs indiqués.";
            //}

            return View(classeview);


            //    else
            //    {


            //    }

            //}
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using AspNetCoreGeneratedDocument;

namespace Forum.Controllers
{
    public class AuthentificationController : Controller
    {
        //[Route ("Signup")]
        [HttpGet]
        public IActionResult Authentification()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Authentification(Elève eleve)
        {

            ViewBag.Messagesuccess = "confirmation";
                

            return View(eleve);


            //    if (!ModelState.IsValid)
            //    {
            //        return View(elève);
            //    }
            //    else
            //    {
            //        ModelState.Clear();

            //    }

            //}
        }
    }
}

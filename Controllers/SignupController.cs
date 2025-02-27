using Microsoft.AspNetCore.Mvc;
using Forum.Models;

namespace Forum.Controllers
{
    public class SignupController : Controller
    {
        //[Route ("Signup")]
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        //VERIFICATIONS DES CHAMPS

        [HttpPost]
        public ActionResult Signup(Elève elève)
        {
            if (!ModelState.IsValid)
            {
                return View(elève);
            }

            ViewBag.message_ok = "Inscription réussie.";
            return View(new Elève());
        }
    }
}


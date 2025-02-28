using Microsoft.AspNetCore.Mvc;
using Forum.Models;

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
        public ActionResult Authentification(Elève elève)
        {
            if (!ModelState.IsValid)
            {
                return View(elève);
            }

            ModelState.Clear();
            return RedirectToAction("Index");
        }
    }
}


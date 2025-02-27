using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class AuthentificationController : Controller
    {
        [HttpGet]
        public IActionResult Authentification()
        {
            return View();
        }

        //VERIFICATIONS DES CHAMPS
        [HttpPost]
        public ActionResult Authentification(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
            {
                ViewBag.Erroremail = "L'email est obligatoire";
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.Errorpassword = "Mot de passe obligatoire";
            }
            return View();
        }
    }
}
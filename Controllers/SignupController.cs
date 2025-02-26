using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class SignupController : Controller
    {
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        //VERIFICATIONS DES CHAMPS
        [HttpPost]
        public ActionResult Signup(string nom, string prénom,string email, string password)
        {
            nom = Request.Form["nom"];
            prénom = Request.Form["prénom"];
            email = Request.Form["email"];
            password = Request.Form["password"];

            if (string.IsNullOrEmpty(nom))
            {
                ViewBag.Errornom = "Le Nom est obligatoire";
            }
            if (string.IsNullOrEmpty(prénom))
            {
                ViewBag.Errorprénom = "Le prénom est obligatoire";
            }
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
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


        [HttpPost]
        public ActionResult Signup(Elève elève)
        {
            if (!ModelState.IsValid)
            {
                return View(elève);
            }

            ModelState.Clear();
            //ViewBag.message_ok = "Inscription réussie.";
            //TempData["message_ok"] = "inscription réussie";
            return RedirectToAction ("Index");
            //return View(new Elève());
        }
    }
}


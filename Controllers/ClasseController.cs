using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Forum.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext contextpost;

        public ClasseController(ApplicationDbContext context)
        {
            contextpost = context;
        }


        [HttpGet]
        public IActionResult Creeclasse()
        {
            var Eleves = contextpost.Eleves
            .Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nom
            })
            .ToList();

            var model = new ClasseViewModel
            {
                Eleves = new SelectList(Eleves, "Value", "Text")
            };

            return View(model);
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




using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext contextget;

        public ClasseController(ApplicationDbContext context)
        {
            contextget = context;
        }


        [HttpGet]
        public IActionResult Creeclasse()
        {
            var profId = HttpContext.Session.GetString("Prof_id");
            if (string.IsNullOrEmpty(profId))
            {
                return RedirectToAction("Index", "Authentification"); 
            }

            var Eleves = contextget.Eleves
            .Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Nom + "  " + e.Prenom

            })

            .ToList();  //Méthode tolist pour convertir un tableau en liste


            var model = new ClasseViewModel
            {
                Eleves = new SelectList(Eleves, "Value", "Text"),
                Eleveschoisis = new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Creeclasse(ClasseViewModel classeview, string ajout_eleve)
        {
            var profId = HttpContext.Session.GetString("Prof_id");
            if (string.IsNullOrEmpty(profId))
            {
                return RedirectToAction("Index", "Authentification"); 
            }
            var Eleves = contextget.Eleves
           .Select(e => new SelectListItem
           {
               Value = e.Id.ToString(),
               Text = e.Nom + "  " + e.Prenom

           })

           .ToList();
            if (classeview.Eleveschoisis == null)
            {
                classeview.Eleveschoisis = new List<int>();
            }

            if (ajout_eleve == "ajouter" && classeview.id_eleve.HasValue) //On vérifie si un élève à été séléctionné.
            {
                if (!classeview.Eleveschoisis.Contains(classeview.id_eleve.Value))
                {
                    classeview.Eleveschoisis.Add(classeview.id_eleve.Value);
                    ViewBag.Message_eleve = "Elève ajouté !";
                }
                else
                {
                    ViewBag.Message_eleve = "Cet élève est déjà dans la liste !";
                }

            }
                classeview.Eleves = new SelectList(Eleves, "Value", "Text");

                //foreach (var eleveId in classeview.Eleveschoisis)
                //{
                //    var newDetailsClasse = new Details_classe
                //    {
                //        //Id_classe = Details_classe.Id, 
                //        id_eleve = eleveId,
                //        id_professeur = int.Parse(profId)
                //    };

                //    contextget.Details_classe.Add(newDetailsClasse);
                //}

                //contextget.SaveChanges();

                return View(classeview);

            }
            //return View(classeview);
        }
    }




using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System;

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
        public ActionResult Creeclasse(ClasseViewModel classeview, string ajout_eleve, string action)
        {
            var profId = HttpContext.Session.GetString("Prof_id");
            if (string.IsNullOrEmpty(profId))
            {
                return RedirectToAction("Index", "Authentification"); 
            }
                 if (classeview.Eleveschoisis == null)
            {
                classeview.Eleveschoisis = new List<int>();
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
            if (action == "creer" && !string.IsNullOrEmpty(classeview.Classe))
            {

                if (contextget.Classes.Any(c => c.classe == classeview.Classe))
                {
                    ModelState.AddModelError("Classe", "Une classe avec ce nom existe déjà");
                }

                else
                {
                    var nouvelleClasse = new Classe
                    {
                        classe = classeview.Classe
                    };

                    contextget.Classes.Add(nouvelleClasse);
                    contextget.SaveChanges();

                    foreach (var eleveId in classeview.Eleveschoisis.Distinct())
                    {
                        var detailClasse = new Details_classe
                        {
                            Id_classe = nouvelleClasse.Id_classe,
                            id_professeur = int.Parse(profId),
                            id_eleve = eleveId
                        };

                        contextget.Details_classe.Add(detailClasse);
                    }

                    contextget.SaveChanges();
                    TempData["SuccessMessage"] = "Classe créée avec succès !";
                }
            }
            TempData["Eleveschoisis"] = classeview.Eleveschoisis;
            classeview.Eleves = new SelectList(Eleves, "Value", "Text");

                return View(classeview);

            }
        }
    }
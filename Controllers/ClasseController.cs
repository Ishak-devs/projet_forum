


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
            var Eleves = contextget.Eleves
           .Select(e => new SelectListItem
           {
               Value = e.Id.ToString(),
               Text = e.Nom + "  " + e.Prenom

           })

           .ToList();

            //Récupération de l'id du professeur connecté souhaitant crée une classe
            var profId = HttpContext.Session.GetString("Prof_id");

            if (ajout_eleve == "ajouter" && classeview.id_eleve.HasValue) //On vérifie si un élève à été séléctionné.
            {
                if (classeview.Eleveschoisis == null) //Vérification de l'état de Eleve choisis avant de créer une liste.
                {
                    classeview.Eleveschoisis = new List<int>(); //Création d'une liste d'élève ajouté au groupe de classe dans le modele.
                    ViewBag.Message_eleve = "Elève ajouté !";


                    if (!classeview.Eleveschoisis.Contains(classeview.id_eleve.Value)) //Vérification de l'existence de l'id dans la liste afin d'éviter les doublons.
                    {
                        classeview.Eleveschoisis.Add(classeview.id_eleve.Value); //Ajouter un élève à Eleveschoisis grace à la méthode add.

                    }

                }
                classeview.Eleves = new SelectList(Eleves, "Value", "Text");



                foreach (var eleveId in classeview.Eleveschoisis)
                {
                    var newDetailsClasse = new Details_classe
                    {
                        //Id_classe = Details_classe.Id, 
                        id_eleve = eleveId,        
                        id_professeur = int.Parse(profId) 
                    };

                    contextget.Details_classe.Add(newDetailsClasse); 
                }

                //contextget.SaveChanges();

                return View(classeview);

            }
            return View(classeview);
        }
    }
    }


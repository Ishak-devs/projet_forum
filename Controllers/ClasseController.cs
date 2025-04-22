using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Forum.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Creeclasse()
        {
            var userId = HttpContext.Session.GetString("Id_user");
            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Vous devez vous connecter pour accéder à cette page.";
                return RedirectToAction("Index", "Authentification");
            }

            var eleves = _context.Utilisateurs
                .Where(u => u.Role == "Eleve")
                .Select(e => new SelectListItem
                {
                    Value = e.Id_utilisateur.ToString(),
                    Text = $"{e.Nom} {e.Prenom}"
                })
                .ToList();

            var model = new ClasseViewModel
            {
                Eleves = new SelectList(eleves, "Value", "Text"),
                Eleveschoisis = new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Creeclasse(ClasseViewModel classeview, string ajout_eleve, string action, string supprimer)
        {
            var userId = HttpContext.Session.GetString("Id_user");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Authentification");
            }

            // Initialisation si null
            classeview.Eleveschoisis ??= new List<int>();

            // Recharger la liste des élèves pour la vue
            var eleves = _context.Utilisateurs
                .Where(u => u.Role == "Eleve")
                .Select(e => new SelectListItem
                {
                    Value = e.Id_utilisateur.ToString(),
                    Text = $"{e.Nom} {e.Prenom}"
                })
                .ToList();

            // Gestion de l'ajout d'élève
            if (ajout_eleve == "ajouter" && classeview.Eleves.HasValue)
            {
                if (!classeview.Eleveschoisis.Contains(classeview.Id_user.Value))
                {
                    classeview.Eleveschoisis.Add(classeview.Nom_classe.Value);
                    ViewBag.Message_eleve = "Elève ajouté !";
                }
                else
                {
                    ViewBag.Message_eleve = "Cet élève est déjà dans la liste !";
                }
            }

            // Gestion de la suppression d'élève
            if (!string.IsNullOrEmpty(supprimer) && int.TryParse(supprimer, out int eleveIdASupprimer))
            {
                classeview.Eleveschoisis.Remove(eleveIdASupprimer);
                ViewBag.Message_eleve = "Élève supprimé avec succès !";
            }

            // Création de la classe
            if (action == "creer")
            {
                if (string.IsNullOrEmpty(classeview))
                {
                    ModelState.AddModelError("ClasseNom", "Le nom de la classe est requis");
                }
                else
                {
                    // Création de la classe
                    var nouvelleClasse = new Promos
                    {
                        Nom_promo = classeview.Nom_classe,
                        Id_user = int.Parse(userId)
                    };

                    _context.Promos.Add(nouvelleClasse);
                    _context.SaveChanges();

                    // Ajout des élèves à la classe
                    foreach (var eleveId in classeview.Eleveschoisis.Distinct())
                    {
                        var user = _context.Utilisateurs.Find(eleveId);
                        if (user != null)
                        {
                           
                        }
                    }

                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Classe créée avec succès !";
                    return RedirectToAction("Creeclasse");
                }
            }

            // Recharger le modèle pour la vue
            classeview.Eleves = new SelectList(eleves, "Value", "Text");
            return View(classeview);
        }
    }
}
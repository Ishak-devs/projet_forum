using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

            // Gestion de la suppression d'élève
            if (!string.IsNullOrEmpty(supprimer) && int.TryParse(supprimer, out int eleveIdASupprimer))
            {
                classeview.Eleveschoisis.Remove(eleveIdASupprimer);
                ViewBag.Message_eleve = "Élève supprimé avec succès !";
            }

            // Création de la classe
            if (action == "creer")
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
            return View();
        }

        }
    }
using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using LinqToDB;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext contextget; // Remplacez par votre DbContext

        public DashboardController(ApplicationDbContext context)
        {
            contextget = context;
        }

        public IActionResult Eleve()
        {
            var eleveId = HttpContext.Session.GetString("Eleve_id");

            if (!string.IsNullOrEmpty(eleveId))
            {
                int id = int.Parse(eleveId);

                var eleve = contextget.Eleves
                    .FirstOrDefault(p => p.Id == id);

                if (eleve != null)
                {
                    ViewBag.SessionEleve_id = eleveId;
                    ViewBag.EleveNom = eleve.Nom;
                    ViewBag.ElevePrenom = eleve.Prenom;

                    var amis = contextget.Amis
        .Where(a => a.EleveDemandeurId == id)
        .Select(a => new AmisViewModel
        {
            AmiId = a.EleveAmiId,
            Accepted = a.Accepted,
            Nom = contextget.Eleves.FirstOrDefault(e => e.Id == a.EleveDemandeurId).Nom,
            Prenom = contextget.Eleves.FirstOrDefault(e => e.Id == a.EleveDemandeurId).Prenom
        })
        .ToList();
                    ViewBag.Amis = amis;
                }
            }

            return View();
        }

        public IActionResult Professeur()
        {
            var profId = HttpContext.Session.GetString("Prof_id");

            if (!string.IsNullOrEmpty(profId))
            {
                int id = int.Parse(profId);

                var professeur = contextget.Professeurs
                    .FirstOrDefault(p => p.Id == id);

                if (professeur != null)
                {
                    ViewBag.SessionProf_id = profId;
                    ViewBag.ProfNom = professeur.Nom;
                    ViewBag.ProfPrenom = professeur.Prenom;
                }
            }

            return View();
        }
    }
    }

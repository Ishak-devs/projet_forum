using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext contextget;

        public DashboardController(ApplicationDbContext context)
        {
            contextget = context;
        }

        public IActionResult Index()
        {
            string? Role = HttpContext.Session.GetInt32("UserRole");
            if (Role == "Admin")
                return View("Admin");
            else if (Role == "Professeur") 
                return View("Professeur");
            else if (Role == "Etudiant");
            return View("Etudiant");
        }
                
        public IActionResult Eleve()
        {
            var eleveId = HttpContext.Session.GetInt32("Etudiant");

          

                var eleve = contextget.Utilisateurs
                    .FirstOrDefault(p => p.Id_utilisateur == id);

                if (eleve != null)
                {
                    ViewBag.SessionEleve_id = eleveId;
                    ViewBag.EleveNom = eleve.Nom;
                    ViewBag.ElevePrenom = eleve.Prenom;
                }
            }

            return View();
        }
        public IActionResult Admin()
        {
            return View();


        }
        public IActionResult Schoolspace()
        {
            return View();
        }

        public IActionResult Professeur()
        {
            var profId = HttpContext.Session.GetInt32("Professeur");
                var professeur = contextget.Utilisateurs
                    .FirstOrDefault(p => p.Id_utilisateur == id);

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

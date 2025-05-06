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

            var classes = contextget.Classes.ToList();
            string? Role = HttpContext.Session.GetString("UserRole").ToString();
            if (Role == "Admin")
                return View("Admin", classes);
            else if (Role == "Professeur")
                return View("Professeur");
            else if (Role == "Etudiant")
                return View("Etudiant");
            return new EmptyResult();

        }

        public IActionResult Eleve()
        {
            var eleveId = HttpContext.Session.GetInt32("Etudiant");

            if (eleveId.HasValue)
            {
                var eleve = contextget.Utilisateurs
                    .FirstOrDefault(p => p.Id_utilisateur == eleveId.Value);

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
            var classes = contextget.Classes.ToList();
            return View(classes);
        }


        public IActionResult Schoolspace()
        {
            return View();
        }

        public IActionResult Professeur()
        {
            var profId = HttpContext.Session.GetInt32("Professeur");

            if (profId.HasValue)
            {
                var professeur = contextget.Utilisateurs
                    .FirstOrDefault(p => p.Id_utilisateur == profId.Value);

                if (professeur != null)
                {
                    ViewBag.SessionProf_id = profId;
                    ViewBag.ProfNom = professeur.Nom;
                    ViewBag.ProfPrenom = professeur.Prenom;
                }
            }

            return RedirectToAction("Index");
        }
        
    }
}

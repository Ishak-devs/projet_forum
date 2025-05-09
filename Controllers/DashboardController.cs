using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var classes = _context.Classes.ToList();
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
                var eleve = _context.Utilisateurs
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

            var classes = _context.Classes.ToList();

            var Prof_principal = classes.Select(c => new
            {

                Classe = c,
                ProfPrincipal = _context.Utilisateurs
                    .FirstOrDefault(u => u.Id_utilisateur == c.ProfesseurPrincipalId)
            }).ToList();

            var effectifsParClasse = _context.Utilisateurs
                .Where(u => u.Role == "Etudiant" && u.Id_classe != null)
                .GroupBy(u => u.Id_classe)
                .ToDictionary(g => g.Key, g => g.Count());

            ViewBag.EffectifsParClasse = effectifsParClasse;
            ViewBag.prof_principal = Prof_principal;

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
                var professeur = _context.Utilisateurs
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

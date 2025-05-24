using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
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

        public IActionResult Etudiant()
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
                .GroupBy(u => u.Id_classe.Value)
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

        public IActionResult AjouterAlerte()
        {
            var classes = _context.Classes.ToList();
            return View(classes);
        }


        [HttpPost]
        public IActionResult AjouterAlerte(string message, string alertType, int? Id_classe)
        {


            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                TempData["ErrorMessage"] = "Vous devez être connecté pour envoyer une alerte.";
                return RedirectToAction("Login", "Account");
            }


            var nouvelleAlerte = new Alertes
            {
                Id_utilisateur = userId.Value,
                Message = message,
                Titre = alertType,
                Id_classe = Id_classe.Value
            };

            try
            {
                _context.Alertes.Add(nouvelleAlerte);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "L'alerte a été envoyée avec succès.";
                return RedirectToAction("Admin");
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'envoi de l'alerte.");
                TempData["ErrorMessage"] = "Une erreur est survenue lors de l'envoi de l'alerte.";
                return RedirectToAction("Admin");
            }
        
        }
        
        public IActionResult VoirAlertes()
        {
            var alertes = _context.Alertes
                .Include(a => a.User) 
                .Include(a => a.Classe) 
                .OrderByDescending(a => a.Id_alerte) 
                .ToList();

            return View(alertes);
        }
    }
}
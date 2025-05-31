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

        public async Task<IActionResult> Etudiant()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var etudiant = await _context.Utilisateurs
                .Include(e => e.Classes)
                .FirstOrDefaultAsync(e => e.Id_utilisateur == userId);

            var notes = await _context.Notes
                .Include(n => n.Modules)
                .Where(n => n.Id_utilisateur == userId)
                .OrderByDescending(n => n.Date_note)
                .Take(3)
                .ToListAsync();

            float moyenne = (float)(notes.Any() ? notes.Average(n => n.Note) : 0);

            ViewBag.Moyenne = moyenne;
            ViewBag.Classe = etudiant?.Classes?.Nom_classe ?? "Classe inconnue";
            ViewBag.Notes = notes;
            ViewBag.EleveNom = etudiant?.Nom;
            ViewBag.ElevePrenom = etudiant?.Prenom;

            return View();
        }


        public async Task<IActionResult> AdminAsync()
        {

            var classes = _context.Classes.ToList();

          

            var effectifs = _context.Utilisateurs
                     .Where(u => u.Role == "Etudiant" && u.Id_classe != null)
                     .GroupBy(u => u.Id_classe.Value)
                     .ToDictionary(g => g.Key, g => g.Count());


            var alertes = _context.Alertes
                .Include(a => a.Classe)  
                .Include(a => a.User)     
                .OrderByDescending(a => a.Id_alerte)
                .Take(5) 
                .ToList();

            ViewBag.EffectifsParClasse = effectifs;
            ViewBag.Alertes = alertes;

            return View(classes);
        }

        public IActionResult Schoolspace()
        {
            return View();
        }

        public IActionResult Professeur()
        {
            var profId = HttpContext.Session.GetInt32("UserId");

            if (profId.HasValue)
            {
                var professeur = _context.Utilisateurs
                    .Include(u => u.Classes)
                        .ThenInclude(c => c.Utilisateurs) // récupère les utilisateurs de la classe
                    .FirstOrDefault(u => u.Id_utilisateur == profId.Value);

                if (professeur != null)
                {
                    ViewBag.SessionProf_id = profId;
                    ViewBag.ProfNom = professeur.Nom;
                    ViewBag.ProfPrenom = professeur.Prenom;

                    var classe = professeur.Classes;

                    // récupère uniquement les étudiants de la classe
                    var eleves = classe?.Utilisateurs
                        .Where(u => u.Role == "Etudiant")
                        .ToList();

                    var alertes = _context.Alertes
                        .Where(a => a.Id_utilisateur == profId.Value)
                        .ToList();

                    ViewBag.Classe = classe;
                    ViewBag.Eleves = eleves;
                    ViewBag.Alertes = alertes;

                    return View("Professeur");
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
                _context.Alertes.Add(nouvelleAlerte); //add on db
                _context.SaveChanges(); //save on db

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
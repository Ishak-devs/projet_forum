using System.Diagnostics;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Forum.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ModulesController> _logger;

        public ModulesController(ApplicationDbContext context, ILogger<ModulesController> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.ToListAsync();
            ViewBag.Classes = classes;
            var utilisateurs = await _context.Utilisateurs.ToListAsync();
            ViewBag.Utilisateurs = utilisateurs;
            return new EmptyResult();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmploiDuTempsParClasse(int classeId)
        {
            var modules = await _context.Modules
                .Include(m => m.Utilisateurs)
                .Where(m => m.Id_classe == classeId)
                .Select(m => new
                {
                    m.Id_module,
                    m.Theme_module,
                    m.Horaire,
                    Professeur = m.Utilisateurs.Prenom + " " + m.Utilisateurs.Nom
                })
                .ToListAsync();

            return Json(modules);
        }


        [HttpGet]
        public async Task<IActionResult> GetProfByClasse(int id)
        {
            var modules = await _context.Utilisateurs
                .Where(m => m.Role == "Professeur")
                .Select(m => new { m.Id_utilisateur, m.Prenom, m.Nom, m.Matiere })
                .ToListAsync();

            return Json(modules);
        }
        [HttpGet]
        public async Task<IActionResult> Ajouter_emplois_du_temps()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Authentification");
            }

            var classes = await _context.Classes.ToListAsync();
            var utilisateurs = await _context.Utilisateurs
                .Where(u => u.Role == "Professeur")
                .ToListAsync();

            ViewBag.Classes = classes;
            ViewBag.Utilisateurs = utilisateurs;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ajouter_emplois_du_temps([FromBody] List<EmploiDuTempsData> timetableData)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return Json(new { success = false, message = "Non authentifié" });
                }

                if (timetableData == null || !timetableData.Any())
                {
                    return Json(new { success = false, message = "Aucune donnée reçue" });
                }
                //

                var modules = timetableData.Select(t => new Modules
                {
                    Id_classe = t.ClassId,
                    Theme_module = $"{t.Course}",
                    Horaire = $"{t.Day} {t.Hour} {t.HourEnd}",
                    Id_utilisateur = t.ProfId,

                }).ToList();

                await _context.Modules.AddRangeAsync(modules);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Emploi du temps enregistré avec {Count} cours", modules.Count);
                return Json(new { success = true, message = "Emploi du temps enregistré avec succès" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'enregistrement de l'emploi du temps");
                return Json(new { success = false, message = $"Erreur: {ex.Message}" });
            }
        }
    }
}
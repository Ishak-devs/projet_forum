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
            var classes = await _context.Promos.ToListAsync();
            ViewBag.Classes = classes;
            var utilisateurs = await _context.Utilisateurs.ToListAsync();
            ViewBag.Utilisateurs = utilisateurs;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmploiDuTempsParClasse(int id)
        {
            try
            {
                var modules = await _context.Modules
    .Where(m => m.Id_classe == id)
    .ToListAsync();


                var emploiDuTemps = modules.Select(m =>
                {
                    var parts = m.Horaire.Split(' ');
                    return new
                    {
                        jour = parts[0],
                        heureDebut = parts[1],
                        heureFin = parts[2],
                        theme = m.Theme_module,
                        //profId = m.Id_utilisateur,
                        //professeur = m.Utilisateurs.Prenom + " " + m.Utilisateurs.Nom
                    };
                }).ToList();

                return Json(emploiDuTemps);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de la récupération de l'emploi du temps");
                return Json(new { error = ex.Message });
            }
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

            var classes = await _context.Promos.ToListAsync();
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
            _logger.LogInformation("POST Ajouter_emplois_du_temps reçu");

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

                // Supprimer les anciens modules pour cette classe
                var classId = timetableData.First().ClassId;
                var existingModules = await _context.Modules
                    .Where(m => m.Id_classe == classId)
                    .ToListAsync();

                if (existingModules.Any())
                {
                    _context.Modules.RemoveRange(existingModules);
                    await _context.SaveChangesAsync();
                }

                // Ajouter les nouveaux modules
                var modules = timetableData.Select(t => new Modules
                {
                    Id_classe = t.ClassId,
                    Theme_module = t.Course,
                    Horaire = $"{t.Day} {t.Hour} {t.HourEnd}",
                    //Id_utilisateur = t.ProfId
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

    public class EmploiDuTempsData
    {
        public int ClassId { get; set; }
        public string Day { get; set; }
        public string Hour { get; set; }
        public string HourEnd { get; set; }
        public string Course { get; set; }
        public int ProfId { get; set; }
    }
}
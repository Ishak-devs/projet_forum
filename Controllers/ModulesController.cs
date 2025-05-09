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
         

            ViewBag.Classes = classes;


            return View();
        }

        [HttpPost]
        public IActionResult Ajouter_emplois_du_temps(int classId, string theme_module, string horaire)
        {
            try
            {
                var module = new Modules
                {
                    Id_classe = classId,
                    Theme_module = theme_module,
                    Horaire = horaire
                };

                _context.Modules.Add(module);
                _context.SaveChanges();

                _logger.LogInformation("Module ajouté avec succès: {Theme} pour la classe {ClassId}", theme_module, classId);
                TempData["SuccessMessage"] = "Ajout réussie !";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur lors de l'ajout du module. ClassId: {ClassId}, Theme: {Theme}, Horaire: {Horaire}",
                    classId, theme_module, horaire);
                TempData["ErrorMessage"] = "Erreur lors de l'ajout du module";
            }

            return RedirectToAction("Ajouter_emplois_du_temps");
        }
    }
}
using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Forum.Controllers
{
    public class ProfesseurController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProfesseurController> _logger;

        public ProfesseurController(ApplicationDbContext context, ILogger<ProfesseurController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AjouterDevoir()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var professeur = await _context.Utilisateurs
                .Include(u => u.Promos)
                .FirstOrDefaultAsync(u => u.Id_utilisateur == userId);

            if (professeur?.Id_promos == null)
            {
                TempData["ErrorMessage"] = "Vous n'êtes pas assigné à une classe";
                return RedirectToAction("Index", "Dashboard");
            }

            var classeId = await _context.Utilisateurs
                .Where(u => u.Id_utilisateur == userId)
                .Select(u => u.Id_promos)
                .FirstOrDefaultAsync();

            
            var modules = await _context.Modules
                .Where(m => m.Id_promos == classeId)
                .ToListAsync();


            ViewBag.Modules = new SelectList(modules, "Id_module", "Theme_module");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AjouterDevoir(Ressources devoir, IFormFile fichier)
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            try
            {
                if (string.IsNullOrEmpty(devoir.Intitulé))
                {
                    ModelState.AddModelError("Intitulé", "L'intitulé est requis");
                    return View(devoir);
                }

                var professeur = await _context.Utilisateurs
                    .Include(u => u.Promos)
                    .FirstOrDefaultAsync(u => u.Id_utilisateur == userId);

                if (professeur == null)
                {
                    TempData["ErrorMessage"] = "Professeur non trouvé";
                    return RedirectToAction("Index");
                }

                devoir.Id_utilisateur = userId ?? 0;
                devoir.Date_devoir = DateTime.Now;
                //devoir.Id_classe = professeur.Id_classe.Value;


                if (fichier != null && fichier.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fichier.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fichier.CopyToAsync(fileStream);
                    }


                    devoir.FichierPath = Path.Combine("uploads", uniqueFileName).Replace("\\", "/");
                }

                _context.Ressources.Add(devoir);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Devoir ajouté avec succès";
                return RedirectToAction("AjouterDevoir");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur ajout devoir");
                TempData["ErrorMessage"] = "Une erreur est survenue";
                return View(devoir);
            }
        }



        public IActionResult MesClasses()
        {
            var profId = HttpContext.Session.GetInt32("UserId");

            if (!profId.HasValue)
                return RedirectToAction("Index", "Authentification");

            var professeur = _context.Utilisateurs.FirstOrDefault(u => u.Id_utilisateur == profId.Value);

            if (professeur == null || !professeur.Id_promos.HasValue)
                return View(new List<Promos>());

            var classe = _context.Promos
                .Where(c => c.Id_promos == professeur.Id_promos.Value)
                .ToList();

            return View(classe);
        }
        [HttpGet]
        public IActionResult SaisirNotes()
        {
            var profId = HttpContext.Session.GetInt32("UserId");

            if (!profId.HasValue)
                return RedirectToAction("Index", "Authentification");

            var professeur = _context.Utilisateurs.FirstOrDefault(u => u.Id_utilisateur == profId.Value);

            if (professeur == null || professeur.Id_promos == null)
                return NotFound("Classe non trouvée pour ce professeur.");

            var eleves = _context.Utilisateurs
                .Where(e => e.Id_promos == professeur.Id_promos && e.Role == "Etudiant")
                .ToList();

            var modules = _context.Modules.ToList();

            ViewBag.Eleves = eleves;
            ViewBag.Modules = modules;

            return View();
        }

        [HttpPost]
        public IActionResult SaisirNotes(int Id_utilisateur, int Id_module, double Note)
        {
            var nouvelleNote = new Evaluations
            {
                Id_utilisateur = Id_utilisateur,
                Id_module = Id_module,
                Note = Note,
                Date_note = DateTime.Now
            };

            _context.Evaluations.Add(nouvelleNote);
            _context.SaveChanges();

            TempData["Message"] = "Note enregistrée avec succès !";
            return RedirectToAction("SaisirNotes");
        }

        public IActionResult EmploiDuTemps()
        {
            var profId = HttpContext.Session.GetInt32("UserId");

            if (!profId.HasValue)
                return RedirectToAction("Index", "Authentification");

            var professeur = _context.Utilisateurs.FirstOrDefault(u => u.Id_utilisateur == profId.Value);


            var edt = _context.Modules
                .Where(m => m.Id_promos == professeur.Id_promos)
                .Include(m => m.Promos)
                .ToList();

            return View(edt);
        }

    }
}
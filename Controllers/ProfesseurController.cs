using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Forum.Controllers
{
    public class ProfesseurController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfesseurController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MesClasses()
        {
            var profId = HttpContext.Session.GetInt32("UserId");

            if (!profId.HasValue)
                return RedirectToAction("Index", "Authentification");

            var professeur = _context.Utilisateurs.FirstOrDefault(u => u.Id_utilisateur == profId.Value);

            if (professeur == null || !professeur.Id_classe.HasValue)
                return View(new List<Classes>()); 

            var classe = _context.Classes
                .Where(c => c.Id_classe == professeur.Id_classe.Value)
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

            if (professeur == null || professeur.Id_classe == null)
                return NotFound("Classe non trouvée pour ce professeur.");

            var eleves = _context.Utilisateurs
                .Where(e => e.Id_classe == professeur.Id_classe && e.Role == "Etudiant")
                .ToList();

            var modules = _context.Modules.ToList();

            ViewBag.Eleves = eleves;
            ViewBag.Modules = modules;

            return View();
        }

        [HttpPost]
        public IActionResult SaisirNotes(int Id_utilisateur, int Id_module, double Note)
        {
            var nouvelleNote = new Notes
            {
                Id_utilisateur = Id_utilisateur,
                Id_module = Id_module,
                Note = Note,
                Date_note = DateTime.Now
            };

            _context.Notes.Add(nouvelleNote);
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

            if (professeur == null || professeur.Id_classe == null)
                TempData["Message"] = "Professeur non trouvé ou non connecté.";
            return RedirectToAction("Professeur", "Dashboard");


            var edt = _context.Modules
                .Where(m => m.Id_classe == professeur.Id_classe)
                .Include(m => m.Classes)
                .ToList();

            return View(edt);
        }

    }
}
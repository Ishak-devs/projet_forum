using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{

    public class EtudiantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EtudiantController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> EspacePerso()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Index", "Authentification");

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

            return View();
        }


        public async Task<IActionResult> Emplois_du_temps()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var etudiant = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id_utilisateur == userId);

            if (etudiant == null)
                return NotFound("Étudiant non trouvé");

            var modules = await _context.Modules
                .Where(m => m.Id_classe == etudiant.Id_classe)
                .ToListAsync();

            return View(modules);
        }

        public async Task<IActionResult> MesNotes()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var notes = await _context.Notes
                .Include(n => n.Modules)
                .Where(n => n.Id_utilisateur == userId)
                .ToListAsync();

            return View(notes);
        }
        public async Task<IActionResult> MesDevoirs()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var devoirs = await _context.Devoirs
                .Include(d => d.Modules)
                .Where(d => d.Id_utilisateur == userId)
                .OrderBy(d => d.Modules.Theme_module)
                .ThenBy(d => d.Date_devoir)
                .ToListAsync();

            return View(devoirs);
        }
        [HttpPost]
        public async Task<IActionResult> RendreDevoir(int Id_devoir, IFormFile fichier)
        {
            if (fichier == null || fichier.Length == 0)
                return BadRequest("Fichier manquant.");

            var devoir = await _context.Devoirs.FindAsync(Id_devoir);
            if (devoir == null)
                return NotFound();

            var fileName = Path.GetFileName(fichier.FileName);
            var filePath = Path.Combine("wwwroot/uploads", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fichier.CopyToAsync(stream);
            }

            devoir.FichierPath = "/uploads/" + fileName;
            await _context.SaveChangesAsync();

            return RedirectToAction("MesDevoirs");
        }


    }

}

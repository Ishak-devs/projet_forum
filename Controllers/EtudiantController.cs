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

    }

}

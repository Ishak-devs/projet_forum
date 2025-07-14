using Forum.Models;
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
        //public async Task<IActionResult> EspacePerso()
        //{
        //    int? userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId == null) return RedirectToAction("Index", "Authentification");

        //    var etudiant = await _context.Utilisateurs
        //        .Include(e => e.Classes)
        //        .FirstOrDefaultAsync(e => e.Id_utilisateur == userId);

        //    var notes = await _context.Notes
        //        .Include(n => n.Modules)
        //        .Where(n => n.Id_utilisateur == userId)
        //        .OrderByDescending(n => n.Date_note)
        //        .Take(3)
        //        .ToListAsync();

        //    float moyenne = (float)(notes.Any() ? notes.Average(n => n.Note) : 0);


        //    ViewBag.Moyenne = moyenne;
        //    ViewBag.Classe = etudiant?.Classes?.Nom_classe ?? "Classe inconnue";
        //    ViewBag.Notes = notes;

        //    return View();
        //}

        //public async Task<IActionResult> Etudiant()
        //{
        //    int? userId = HttpContext.Session.GetInt32("UserId");
        //    if (userId == null) return RedirectToAction("Index", "Authentification");

        //    var etudiant = await _context.Utilisateurs
        //        .Include(e => e.Classes)
        //        .FirstOrDefaultAsync(e => e.Id_utilisateur == userId);

        //    var notes = await _context.Notes
        //        .Include(n => n.Modules) // Ensure Modules are loaded
        //        .Where(n => n.Id_utilisateur == userId)
        //        .OrderByDescending(n => n.Date_note)
        //        .Take(3)
        //        .ToListAsync();

        //    float moyenne = (float)(notes.Any() ? notes.Average(n => n.Note) : 0);

        //    ViewBag.Moyenne = moyenne;
        //    ViewBag.Classe = etudiant?.Classes?.Nom_classe ?? "Classe inconnue";
        //    ViewBag.Notes = notes;

        //    return View();
        //}


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
                .Where(m => m.Id_promos == etudiant.Id_promos)
                .ToListAsync();

            return View(modules);
        }

        public async Task<IActionResult> MesNotes()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var notes = await _context.Evaluations
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

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Id_utilisateur == userId);

            if (utilisateur == null || utilisateur.Id_promos == null)
                return View(new List<Ressources>());

            var devoirs = await _context.Ressources
                .Include(r => r.Utilisateurs) 
                .Where(r => r.Utilisateurs.Id_promos == utilisateur.Id_promos)
                .OrderBy(r => r.Date_devoir)
                .ToListAsync();





            return View(devoirs);
        }


        public async Task<IActionResult> TelechargerFichier(int id)
        {
            var devoir = await _context.Ressources.FindAsync(id);

            if (devoir == null || string.IsNullOrEmpty(devoir.FichierPath))
                return NotFound();

            var cheminComplet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", devoir.FichierPath);

            if (!System.IO.File.Exists(cheminComplet))
                return NotFound();

            var typeMime = "application/octet-stream"; // Tu peux adapter le MIME selon l’extension
            var nomFichier = Path.GetFileName(cheminComplet);
            var fileBytes = await System.IO.File.ReadAllBytesAsync(cheminComplet);

            return File(fileBytes, typeMime, nomFichier);
        }



        [HttpPost]
        public async Task<IActionResult> RendreDevoir(int Id_devoir, IFormFile fichier)
        {
            if (fichier == null || fichier.Length == 0)
                return BadRequest("Fichier manquant.");

            var devoir = await _context.Ressources.FindAsync(Id_devoir);
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
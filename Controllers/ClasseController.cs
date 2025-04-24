using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Forum.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasseController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AjouterPromo()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Authentification");
            }

            var admin = _context.Utilisateurs
                .Include(u => u.Etablissements)
                .FirstOrDefault(u => u.Id_utilisateur == userId);

            if (admin == null || admin.Id_etablissement == null)
            {
                TempData["ErrorMessage"] = "Admin ou établissement non trouvé";
                return RedirectToAction("Index", "Dashboard");
            }

            var utilisateursEtablissement = _context.Utilisateurs
                .Where(u => u.Id_etablissement == admin.Id_etablissement)
                .ToList();

            var Etudiant = utilisateursEtablissement.Where(u => u.Role == "Etudiant").ToList();
            var professeurs = utilisateursEtablissement.Where(u => u.Role == "Professeur").ToList();

            ViewBag.Etudiant = Etudiant;
            ViewBag.Professeurs = professeurs;

            var roles = new List<string> { "Etudiant", "Professeur" };
            ViewBag.Roles = new SelectList(roles);

            return View();
        }

    }
}
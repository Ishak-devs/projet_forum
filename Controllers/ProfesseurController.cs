using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Http;
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
                return View(new List<Classes>()); // retourne une liste vide

            var classe = _context.Classes
                .Where(c => c.Id_classe == professeur.Id_classe.Value)
                .ToList();

            return View(classe);
        }
    }
}

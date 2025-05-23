using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class ProfesseurController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DashboardController> _logger;

        public ProfesseurController(ApplicationDbContext context, ILogger<DashboardController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult MesClasses()
        {
            var profId = HttpContext.Session.GetInt32("Professeur");

            if (!profId.HasValue)
                return RedirectToAction("Index", "Authentification");

            var mesClasses = _context.Classes
                .Where(c => c.ProfesseurPrincipalId == profId.Value)
                .ToList();

            return View(mesClasses);
        }
     


    }
}
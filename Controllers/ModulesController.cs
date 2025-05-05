using System.Diagnostics;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Forum.Controllers
{
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ModulesController(ApplicationDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> GetModulesByClasse(int id)
        {
            var modules = await _context.Modules
                .Where(m => m.Id_classe == id)
                .Select(m => new { m.Id_module, m.Theme_module })
                .ToListAsync();

            return Json(modules);
        }


        public async Task<IActionResult> Ajouter_emplois_du_temps()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Authentification");
            }
            var classes = await _context.Classes.ToListAsync();
            var utilisateurs = await _context.Utilisateurs.ToListAsync();
            var modules = await _context.Modules.ToListAsync();

            ViewBag.Classes = classes;
            ViewBag.Utilisateurs = utilisateurs;
            ViewBag.Modules = modules;

            return View();
        }
    }
}

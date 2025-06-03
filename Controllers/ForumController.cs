using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Forum.Models;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext contextget;

        public ForumController(ApplicationDbContext context)
        {
            contextget = context;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Authentification");
            }

            var classes = await contextget.Classes.ToListAsync();
            var utilisateurs = await contextget.Utilisateurs.ToListAsync();

            var utilisateur = utilisateurs.FirstOrDefault(u => u.Id_utilisateur == userId);
            if (utilisateur != null && utilisateur.Id_classe != null)
            {
                HttpContext.Session.SetInt32("ClassId", (int)utilisateur.Id_classe);
            }

            ViewBag.Classes = classes;
            ViewBag.Users = utilisateurs.Distinct().ToList();

            return View();
        }
    }
}
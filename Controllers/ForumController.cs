using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

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
            var userId = HttpContext.Session.GetString("Id_user");

            if (!string.IsNullOrEmpty(userId) && int.TryParse(userId, out int userIdInt))
            {
                var eleveClasses = await contextget.Promos
                    .Where(dc => dc.Id_promo == userIdInt)
                    .ToListAsync();

                if (eleveClasses.Any())
                {
                    ViewBag.Classes = eleveClasses;
                    ViewBag.UserRole = "Eleve";
                    return View();
                }

                var profClasses = await contextget.Utilisateurs
                    .Where(u => u.Id_utilisateur == userIdInt && u.Role == "Professeur")
                    .ToListAsync();

                if (profClasses.Any())
                {
                    ViewBag.Classes = profClasses;
                    ViewBag.UserRole = "Professeur";
                    return View();
                }

                ViewBag.Classes = null;
                ViewBag.UserRole = "Utilisateur";
                return View();
            }

            return RedirectToAction("Index", "Authentification");
        }
    }
}
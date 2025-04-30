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
            var Utilisateurs = await contextget.Utilisateurs.ToListAsync();

            ViewBag.Classes = classes;
            ViewBag.Users = Utilisateurs;

            return View();
        }
    }
    }

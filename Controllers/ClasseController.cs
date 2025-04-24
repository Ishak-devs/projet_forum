using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
            var userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Message"] = "Vous devez être connecté pour accéder à cette page.";
                return RedirectToAction("Index", "Signup"); 
            }

            return View();
        }

    }
}
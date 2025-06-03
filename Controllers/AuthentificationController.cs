using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Forum.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly ApplicationDbContext contextget;
        private readonly PasswordHasher<Utilisateurs> PasswordHasher;

        public AuthentificationController(ApplicationDbContext context)
        {
            contextget = context;
            PasswordHasher = new PasswordHasher<Utilisateurs>();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel modele)
        {
            if (ModelState.IsValid)
            {
                var utilisateur = await contextget.Utilisateurs
                    .FirstOrDefaultAsync(u => u.Email == modele.Email);

                if (utilisateur != null)
                {
                    var result = PasswordHasher.VerifyHashedPassword(
                        utilisateur,
                        utilisateur.Motdepasse,
                        modele.Password);

                    if (result == PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("UserRole", utilisateur.Role);
                        HttpContext.Session.SetInt32("UserId", utilisateur.Id_utilisateur);
                        HttpContext.Session.SetString("Username", utilisateur.Prenom);

                        ModelState.Clear();

                        if (utilisateur.Role == "Admin")
                            return RedirectToAction("Admin", "Dashboard");
                        else if (utilisateur.Role == "Professeur")
                            return RedirectToAction("Professeur", "Dashboard");
                        else
                            return RedirectToAction("Etudiant", "Dashboard");
                    }
                }

                ViewBag.MessageError = "Email ou mot de passe incorrect.";
            }

            return View(modele);
        }
    }
}
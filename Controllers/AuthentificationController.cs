using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace Forum.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Utilisateurs> _passwordHasher;
        private const int MaxLoginAttempts = 5;
        private const int LockoutDurationMinutes = 15;

        public AuthentificationController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Utilisateurs>();
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
            if (!ModelState.IsValid)
                return View(modele);

            var utilisateur = await _context.Utilisateurs
                .FirstOrDefaultAsync(u => u.Email == modele.Email);

            if (utilisateur == null)
            {
                ViewBag.MessageError = "Email ou mot de passe incorrect.";
                return View(modele);
            }

            // Vérifier blocage
            if (utilisateur.bloquage)
            {
                if (utilisateur.bloquage_temps.HasValue &&
                    utilisateur.bloquage_temps.Value.AddMinutes(LockoutDurationMinutes) > DateTime.Now)
                {
                    // Compte bloqué, renvoyer vers page d'erreur
                    return RedirectToAction("ErreurPage");
                }
                else
                {
                    // Fin blocage, débloquer compte
                    utilisateur.bloquage = false;
                    utilisateur.bloquage_temps = null;
                    await _context.SaveChangesAsync();
                }
            }

            var result = _passwordHasher.VerifyHashedPassword(
                utilisateur,
                utilisateur.Motdepasse,
                modele.Password);

            if (result == PasswordVerificationResult.Success)
            {
                // Connexion réussie, reset blocage et compteur erreurs en session
                utilisateur.bloquage = false;
                utilisateur.bloquage_temps = null;
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove("erreur_saisie_" + modele.Email);

                HttpContext.Session.SetString("UserRole", utilisateur.Role);
                HttpContext.Session.SetInt32("UserId", utilisateur.Id_utilisateur);
                HttpContext.Session.SetString("Usernsame", utilisateur.Prenom);

                bool hasForumAccess = utilisateur.Role == "Admin" || utilisateur.Id_promos != null;
                HttpContext.Session.SetString("UserHasForumAccess", hasForumAccess ? "true" : "false");

                ModelState.Clear();

                if (utilisateur.Role == "Admin" && utilisateur.Etablissements == null)
                {
                    return RedirectToAction("Etablissement", "Signup");
                }
                else if (utilisateur.Role == "Admin")
                {
                    return RedirectToAction("Admin", "Dashboard");
                }
                else if (utilisateur.Role == "Professeur")
                {
                    return RedirectToAction("Professeur", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Etudiant", "Dashboard");
                }
            }
            else
            {
                // Mot de passe incorrect : incrémenter compteur en session
                int erreurs = HttpContext.Session.GetInt32("erreur_saisie_" + modele.Email) ?? 0;
                erreurs++;
                HttpContext.Session.SetInt32("erreur_saisie_" + modele.Email, erreurs);

                if (erreurs >= MaxLoginAttempts)
                {
                    // Bloquer compte
                    utilisateur.bloquage = true;
                    utilisateur.bloquage_temps = DateTime.Now;
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove("erreur_saisie_" + modele.Email);

                    return RedirectToAction("ErreurPage");
                }

                ViewBag.MessageError = "Email ou mot de passe incorrect.";
                return View(modele);
            }
        }

        public IActionResult ErreurPage()
        {
            return View();
        }
    }
}

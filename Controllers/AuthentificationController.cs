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
        private const int MaxLoginAttempts = 5;

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
            // Vérifier si l'utilisateur a dépassé le nombre maximum de tentatives
            int erreur_saisie = HttpContext.Session.GetInt32("erreur_saisie") ?? 0;
            if (erreur_saisie >= MaxLoginAttempts)
            {
                return View("ErreurPage");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel modele)
        {
            // Vérifier d'abord si l'utilisateur n'a pas déjà dépassé le nombre maximum de tentatives
            int erreur_saisie = HttpContext.Session.GetInt32("erreur_saisie") ?? 0;
            if (erreur_saisie >= MaxLoginAttempts)
            {
                return View("ErreurPage");
            }

            if (ModelState.IsValid)
            {
                var utilisateur = await contextget.Utilisateurs
                    .Include(u => u.Etablissements)
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

                        // Vérifie s'il a une classe
                        bool hasForumAccess = utilisateur.Role == "Admin" || utilisateur.Id_promos != null;
                        HttpContext.Session.SetString("UserHasForumAccess", hasForumAccess ? "true" : "false");

                        ModelState.Clear();
                        HttpContext.Session.Remove("erreur_saisie");

                        // Vérifier si c'est un admin sans établissement
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

                    ViewBag.MessageError = "Email ou mot de passe incorrect.";
                    erreur_saisie++;
                    HttpContext.Session.SetInt32("erreur_saisie", erreur_saisie);

                    if (erreur_saisie >= MaxLoginAttempts)
                    {
                        return View("ErreurPage");
                    }
                }
                else
                {
                    ViewBag.MessageError = "Email ou mot de passe incorrect.";
                    erreur_saisie++;
                    HttpContext.Session.SetInt32("erreur_saisie", erreur_saisie);

                    if (erreur_saisie >= MaxLoginAttempts)
                    {
                        return View("ErreurPage");
                    }
                }

                return View(modele);
            }

            return View(modele);
        }
    }
}
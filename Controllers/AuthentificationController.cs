using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
namespace Forum.Controllers

{
    public class AuthentificationController : Controller
    {
       

        private readonly ApplicationDbContext contextget; //creation to contextget
        private readonly PasswordHasher<Eleve> elevePasswordHasher;
        private readonly PasswordHasher<Professeur> professeurPasswordHasher;

        public AuthentificationController(ApplicationDbContext context)
        {
            contextget = context; 
            elevePasswordHasher = new PasswordHasher<Eleve>();
            professeurPasswordHasher = new PasswordHasher<Professeur>();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Eleve_id") != null) 
            {

                return RedirectToAction("Eleve", "Dashboard");
            }
            if (HttpContext.Session.GetString("Prof_id") != null)
            {
                return RedirectToAction("Professeur", "Dashboard");
            }
                return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel modele)
        {
            if (ModelState.IsValid)
            {
                // Vérifier si l'utilisateur est un élève
                var Eleve = await contextget.Eleves.FirstOrDefaultAsync(e => e.Email == modele.Email);

                // Vérifier si l'utilisateur est un professeur
                var Professeur = await contextget.Professeurs.FirstOrDefaultAsync(p => p.Email == modele.Email);

                // Si aucun utilisateur n'est trouvé
                if (Eleve == null && Professeur == null)
                {
                    ViewBag.Message_error_email = "L'email saisi n'est pas reconnu";
                    return View(modele);
                }

                // Vérification du mot de passe pour un élève
                if (Eleve != null && elevePasswordHasher.VerifyHashedPassword(null, Eleve.Password, modele.Password) == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("Eleve_id", Eleve.Id.ToString());
                    ModelState.Clear();
                    return RedirectToAction("Eleve", "Dashboard");
                }

                // Vérification du mot de passe pour un professeur
                if (Professeur != null && professeurPasswordHasher.VerifyHashedPassword(null, Professeur.Password, modele.Password) == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("Prof_id", Professeur.Id.ToString());
                    Console.WriteLine("Prof_id dans la session: " + HttpContext.Session.GetString("Prof_id"));
                    ModelState.Clear();
                    return RedirectToAction("Professeur", "Dashboard");
                }

                // Si le mot de passe est incorrect
                ViewBag.MessageError = "Vérifiez votre mot de passe.";
            }

            return View(modele);
        }

    }
}
        


using Microsoft.AspNetCore.Mvc;
using Forum.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Forum.Controllers
{
    public class ClasseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClasseController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes.ToListAsync();
            ViewBag.Classes = classes;
            return new EmptyResult();
        }

        [HttpGet]
        public IActionResult Ajouterclasse()
        {
            

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Authentification");
            }

            var admin = _context.Utilisateurs
                .Include(u => u.Etablissements)
                .FirstOrDefault(u => u.Id_utilisateur == userId);

            if (admin == null || admin.Id_etablissement == null)
            {
                TempData["ErrorMessage"] = "Admin ou établissement non trouvé";
                return RedirectToAction("Index", "Dashboard");
            }

            var utilisateursEtablissement = _context.Utilisateurs
                .Where(u => u.Id_etablissement == admin.Id_etablissement)
                .ToList();

            var Etudiant = utilisateursEtablissement.Where(u => u.Role == "Etudiant").ToList();
            var professeurs = utilisateursEtablissement.Where(u => u.Role == "Professeur").ToList();

            ViewBag.Etudiant = Etudiant;
            ViewBag.Professeurs = professeurs;

            var roles = new List<string> { "Etudiant", "Professeur" };
            ViewBag.Roles = new SelectList(roles);

            return View();
        }


        [HttpPost]
        public IActionResult Ajouterclasse(string Nom_classe, List<int> selectedEleves, List<int> selectedProfs)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index", "Authentification");
            }

            var admin = _context.Utilisateurs
                .Include(u => u.Etablissements)
                .FirstOrDefault(u => u.Id_utilisateur == userId);

            if (admin == null || admin.Id_etablissement == null)
            {
                TempData["ErrorMessage"] = "Admin ou établissement non trouvé";
                return RedirectToAction("Index", "Dashboard");
            }

            try
            {
                var Nouvelle_Classe = new Classes
                {
                    Nom_classe = Nom_classe,
                };

                _context.Classes.Add(Nouvelle_Classe);
                _context.SaveChanges();

                if (selectedEleves != null && selectedEleves.Any())
                {
                    foreach (var eleveId in selectedEleves)
                    {
                        var eleve = _context.Utilisateurs.Find(eleveId);
                        if (eleve != null)
                        {
                            eleve.Id_classe = Nouvelle_Classe.Id_classe;
                        }
                    }
                }

                if (selectedProfs != null && selectedProfs.Any())
                {
                    foreach (var profId in selectedProfs)
                    {
                        var prof = _context.Utilisateurs.Find(profId);
                        if (prof != null)
                        {
                            prof.Id_classe = Nouvelle_Classe.Id_classe;
                        }
                    }
                }

                _context.SaveChanges();

                TempData["SuccessMessage"] = $"La promotion {Nom_classe} a été créée avec succès !";
                return RedirectToAction("Ajouterclasse");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erreur lors de la création de la classe : {ex.Message}";
                return RedirectToAction("Ajouterclasse");
            }
        }
      

    }
}
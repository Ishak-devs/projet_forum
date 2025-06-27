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
        public async Task<IActionResult> Ajouterclasse()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
                return RedirectToAction("Index", "Authentification");

            var admin = await _context.Utilisateurs
                .Include(u => u.Etablissements)
                .FirstOrDefaultAsync(u => u.Id_utilisateur == userId);

            if (admin == null || admin.Id_etablissement == null)
            {
                TempData["ErrorMessage"] = "Admin ou établissement non trouvé";
                return RedirectToAction("Index", "Dashboard");
            }

            var utilisateursEtablissement = await _context.Utilisateurs
                .Where(u => u.Id_etablissement == admin.Id_etablissement)
                .ToListAsync();

            var etudiants = utilisateursEtablissement.Where(u => u.Role == "Etudiant").ToList();
            var professeurs = utilisateursEtablissement.Where(u => u.Role == "Professeur").ToList();

            ViewBag.Etudiant = etudiants;
            ViewBag.Professeurs = professeurs;
            ViewBag.Roles = new SelectList(new List<string> { "Etudiant", "Professeur" });


            var classes = await _context.Classes
                .Where(c => c.Id_etablissement == admin.Id_etablissement)
                .ToListAsync();

            ViewBag.Classes = classes;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SupprimerClasse(int id)
        {
            var classe = await _context.Classes.FindAsync(id);
            if (classe == null)
            {
                TempData["ErrorMessage"] = "Classe non trouvée.";
                return RedirectToAction("Ajouterclasse");
            }

            // On enlève les liens avec les utilisateurs
            var utilisateurs = _context.Utilisateurs.Where(u => u.Id_classe == id);
            foreach (var user in utilisateurs)
            {
                user.Id_classe = null;
            }

            _context.Classes.Remove(classe);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Classe supprimée avec succès.";
            return RedirectToAction("Ajouterclasse");
        }


        [HttpPost]
        public IActionResult Ajouterclasse(string Nom_classe, List<int> selectedEleves, List<int> selectedProfs, int? ProfesseurPrincipalId)
        {
            {
                var userId = HttpContext.Session.GetInt32("UserId");
                if (userId == null)
                {
                    return RedirectToAction("Index", "Authentification");
                }

                var admin = _context.Utilisateurs
       .Include(u => u.Classes)
           .ThenInclude(c => c.Etablissement)
       .FirstOrDefault(u => u.Id_utilisateur == userId && u.Role == "Admin");

                if (admin == null || admin.Classes == null || admin.Classes.Etablissement == null)
                {
                    TempData["ErrorMessage"] = "Admin, classe ou établissement non trouvé";
                    return RedirectToAction("Index", "Dashboard");
                }


                try
                {
                    var Nouvelle_Classe = new Classes
                    {
                        Nom_classe = Nom_classe,
                        Id_etablissement = admin.Id_etablissement
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
}
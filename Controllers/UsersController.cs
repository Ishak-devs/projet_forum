using System.Security.Claims;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UsersController : Controller
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext contextget)
    {
        _context = contextget;
    }

    public async Task<IActionResult> Index(string searchString)
    {
        var viewModel = new UserSearchViewModel
        {
            SearchString = searchString // Récupération de la saisie
        };

        // Vérifier si la chaîne de recherche n'est pas vide ou nulle
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            // Convertit un string de plusieurs mots en liste de mots
            var keywords = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // Élèves
            var elevesQuery = _context.Eleves.AsQueryable();

            if (keywords.Length == 1)
            {
                // Si 1 mot, recherche par nom ou prénom
                elevesQuery = elevesQuery.Where(e =>
                    e.Nom.ToLower().Equals(keywords[0].ToLower()) ||
                    e.Prenom.ToLower().Equals(keywords[0].ToLower()));
            }
            else if (keywords.Length == 2)
            {
                // Si 2 mots, recherche par combinaison nom/prénom
                elevesQuery = elevesQuery.Where(e =>
                    (e.Nom.ToLower().Equals(keywords[0].ToLower()) && e.Prenom.ToLower().Equals(keywords[1].ToLower())) ||
                    (e.Nom.ToLower().Equals(keywords[1].ToLower()) && e.Prenom.ToLower().Equals(keywords[0].ToLower()))
                );
            }

            viewModel.Eleves = await elevesQuery.ToListAsync();

            // Professeurs
            var profsQuery = _context.Professeurs.AsQueryable();

            if (keywords.Length == 1)
            {
                // Si 1 mot, recherche par nom ou prénom
                profsQuery = profsQuery.Where(p =>
                    p.Nom.ToLower().Equals(keywords[0].ToLower()) ||
                    p.Prenom.ToLower().Equals(keywords[0].ToLower()));
            }
            else if (keywords.Length == 2)
            {
                // Si 2 mots, recherche par combinaison nom/prénom
                profsQuery = profsQuery.Where(p =>
                    (p.Nom.ToLower().Equals(keywords[0].ToLower()) && p.Prenom.ToLower().Equals(keywords[1].ToLower())) ||
                    (p.Nom.ToLower().Equals(keywords[1].ToLower()) && p.Prenom.ToLower().Equals(keywords[0].ToLower()))
                );
            }

            viewModel.Professeurs = await profsQuery.ToListAsync();
        }
        else
        {
            // Si la chaîne de recherche est vide ou nulle, renvoyer des listes vides
            viewModel.Eleves = new List<Eleve>();
            viewModel.Professeurs = new List<Professeur>();
        }

        return View(viewModel);
    }

    [HttpPost]
    [HttpPost]
    public async Task<IActionResult> ModifierStatutAmi(int idAmi, bool accepter)
    {
        var lienAmitie = await _context.Amis.FirstOrDefaultAsync(a => a.Id == idAmi);
        if (lienAmitie == null)
        {
            TempData["ErrorMessage"] = "Relation non trouvée.";
            return RedirectToAction("Index");
        }

        if (accepter)
        {
            lienAmitie.Accepted = true;
        }
        else
        {
            _context.Amis.Remove(lienAmitie);
        }

        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = accepter ? "Demande acceptée." : "Demande refusée.";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> AddFriend(int userId, string userType)
    {
        try
        {
            // Récupérer l'ID depuis la session
            var eleveId = HttpContext.Session.GetString("Eleve_id");
            var profId = HttpContext.Session.GetString("Prof_id");

            if (string.IsNullOrEmpty(eleveId) && string.IsNullOrEmpty(profId))
            {
                return RedirectToAction("Index", "Authentification");
            }

            int currentUserId = !string.IsNullOrEmpty(eleveId) ? int.Parse(eleveId) : int.Parse(eleveId);

            // Vérifier si la relation existe déjà
            var existingFriend = await _context.Amis
                .FirstOrDefaultAsync(a => a.EleveDemandeurId == currentUserId && a.EleveAmiId == userId);

            if (existingFriend != null)
            {
                TempData["Message"] = "Cet utilisateur est déjà dans votre liste d'amis";
                return RedirectToAction("Index", new { searchString = "" });
            }

            // Crée la nouvelle relation 
            var newFriend = new Amis
            {
                EleveDemandeurId = currentUserId,
                EleveAmiId = userId,
                Accepted = false,
            };

            _context.Amis.Add(newFriend);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Demande d'amis envoyé";
            return RedirectToAction("Index", new { searchString = "" });
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Erreur lors de l'ajout : {ex.Message}";
            return RedirectToAction("Index", new { searchString = "" });
        }
    }
}

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
}

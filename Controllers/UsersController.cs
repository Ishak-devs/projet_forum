//using System.Data.Entity; //Pour utilisation de to list asynchrone
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
            SearchString = searchString //Recuperation de la saisie 
        };

        // Convertit un string de plusieurs mots en liste de mots
        var keywords = searchString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        
        // Élèves
        
        var elevesQuery = _context.Eleves.AsQueryable();
        
        // Si 1 mot
        if (keywords.Length == 1)
        {
            elevesQuery = elevesQuery.Where(e => e.Nom.ToLower().Equals(searchString) || e.Prenom.ToLower().Equals(searchString));
        }

        // Si 2 mots, on imagine que l'utilisateur donne le nom et prénom de la personne qu'il cherche
        if (keywords.Length == 2)
        {
            elevesQuery = elevesQuery.Where(e =>
                (e.Nom.ToLower().Equals(keywords[0]) && e.Prenom.ToLower().Equals(keywords[1])) ||
                (e.Nom.ToLower().Equals(keywords[1]) && e.Prenom.ToLower().Equals(keywords[0]))
            );
        }
        
        viewModel.Eleves = await elevesQuery.ToListAsync();
        
        
        // Professeurs
        var profsQuery = _context.Professeurs.AsQueryable();
        
        // Si 1 mot
        if (keywords.Length == 1)
        {
            profsQuery = profsQuery.Where(p => p.Nom.ToLower().Equals(searchString) || p.Prenom.ToLower().Equals(searchString));
        }

        // Si 2 mots, on imagine que l'utilisateur donne le nom et prénom de la personne qu'il cherche
        if (keywords.Length == 2)
        {
            profsQuery = profsQuery.Where(p =>
                (p.Nom.ToLower().Equals(keywords[0]) && p.Prenom.ToLower().Equals(keywords[1])) ||
                (p.Nom.ToLower().Equals(keywords[1]) && p.Prenom.ToLower().Equals(keywords[0]))
            );
        }
        
        viewModel.Professeurs = await profsQuery.ToListAsync();

        return View(viewModel);
    }
}

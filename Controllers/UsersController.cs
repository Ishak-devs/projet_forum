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
            SearchString = searchString
        };


        var elevesQuery = _context.Eleves.AsQueryable();
        if (!string.IsNullOrEmpty(searchString))
        {
            elevesQuery = elevesQuery.Where(e => e.Nom.Contains(searchString) || e.Prenom.Contains(searchString));
        }
        viewModel.Eleves = await elevesQuery.ToListAsync();

        var profsQuery = _context.Professeurs.AsQueryable();
        if (!string.IsNullOrEmpty(searchString))
        {
            profsQuery = profsQuery.Where(p => p.Nom.Contains(searchString) || p.Prenom.Contains(searchString));
        }
        viewModel.Professeurs = await profsQuery.ToListAsync();

        return View(viewModel);
    }
}

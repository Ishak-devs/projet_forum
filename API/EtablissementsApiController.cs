using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EtablissementsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EtablissementsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetByRegion(string region)
    {
        var etablissements = _context.Etablissements
            .Where(e => e.Region == region)
            .Select(e => new { e.Id_etablissement, e.Nom_etablissement }) 
            .ToList();

        return Ok(etablissements);
    }
}

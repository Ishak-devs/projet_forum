//using Microsoft.AspNetCore.Mvc;

//namespace Forum.Controllers
//{
//    public class ForumController : Controller
//    {
//        private readonly ApplicationDbContext contextget;

//        public ForumController(ApplicationDbContext context)
//        {
//            contextget = context;
//        }
//        public IActionResult Index()
//        {

//            ViewBag.ClassId = 1;
//            return View();
//        }
//    }
//}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext contextget;

        public ForumController(ApplicationDbContext context)
        {
            contextget = context;
        }

        public async Task<IActionResult> Index()
        {
            var eleveId = HttpContext.Session.GetString("Eleve_id"); //Stockage id
            if (!string.IsNullOrEmpty(eleveId) && int.TryParse(eleveId, out int eleveIdInt)) //Si eleve connecté et id est bien un int
            {
                var classes = await contextget.Details_classe

                    .Where(dc => dc.id_eleve == eleveIdInt) //stockage de l'id de la classe
                    .Include(dc => dc.Classe) // Charge les détails de la classe
                    .ToListAsync();

                if (classes.Any()) //Si l'id de la classe est pas null
                {
                    ViewBag.Classes = classes; //Stockage de l'id de la classe dans un viewbag
                    ViewBag.UserRole = "Eleve"; //Stockage du role
                    return View(); //Retourne la vue
                }
                else
                {
                    ViewBag.Classes =  null; // L'élève n'a pas de classe, mais il peut quand même accéder au forum
                    ViewBag.UserRole = "Eleve";
                    return View();
                }
            }

            else
            {

                var profId = HttpContext.Session.GetString("Prof_id");
                if (!string.IsNullOrEmpty(profId) && int.TryParse(profId, out int profIdInt))
                {

                    var classes = await contextget.Details_classe
                        .Where(dc => dc.id_professeur == profIdInt)
                        .ToListAsync();

                    if (classes.Any())
                    {
                        ViewBag.Classes = classes;
                        ViewBag.UserRole = "Professeur";
                        return View();
                    }
                    else
                    {
                        ViewBag.Classes =  null; // Le Professeur n'a pas de classe, mais il peut quand même accéder au forum
                        ViewBag.UserRole = "Professeur";
                        return View();
                    }
                }
            }
            return RedirectToAction("Index", "Authentification");
        }
    }
}
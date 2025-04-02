using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        private readonly ApplicationDbContext contextget;

        public ForumController(ApplicationDbContext context)
        {
            contextget = context;
        }
        public IActionResult Index()
        {

            ViewBag.ClassId = 1;
            return View();
        }
    }
}
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace Forum.Controllers
//{
//    public class ForumController : Controller
//    {
//        private readonly ApplicationDbContext contextget;

//        public ForumController(ApplicationDbContext context)
//        {
//            contextget = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            var eleveId = HttpContext.Session.GetString("Eleve_id"); //Stockage id
//            if (!string.IsNullOrEmpty(eleveId) && int.TryParse(eleveId, out int eleveIdInt)) //Si eleve connecté et id est bien un int
//            {
//                var classeDetails = await contextget.Details_classe
//                    .FirstOrDefaultAsync(dc => dc.id_eleve == eleveIdInt); //stockage de l'id de la classe

//                if (classeDetails != null) //Si l'id de la classe est pas null
//                {
//                    ViewBag.ClassId = classeDetails.Id_classe; //Stockage de l'id de la classe dans un viewbag
//                    ViewBag.UserRole = "Eleve"; //Stockage du role
//                    return View(); //Retourne la vue
//                }

//                return RedirectToAction("Index", "Authentification"); //Si l'eleve n est pas connecté on le renvoie a la page d'authentification
//            }

//            var profId = HttpContext.Session.GetString("Prof_id"); 
//            if (!string.IsNullOrEmpty(profId) && int.TryParse(profId, out int profIdInt))
//            {

//                var classeDetails = await contextget.Details_classe
//                    .FirstOrDefaultAsync(dc => dc.id_professeur == profIdInt);

//                if (classeDetails != null)
//                {
//                    ViewBag.ClassId = classeDetails.Id_classe;
//                    ViewBag.UserRole = "Professeur";
//                    return View();
//                }

//                return RedirectToAction("Index", "Authentification");
//            }

//            return RedirectToAction("Index", "Authentification");
//        }
//    }
//}
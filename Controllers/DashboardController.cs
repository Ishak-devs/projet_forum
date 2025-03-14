using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Eleve()
        {
            var eleveId = HttpContext.Session.GetString("Eleve_id");
            ViewBag.SessionEleve_id = HttpContext.Session.GetString("Eleve_id");
            return View();
        }
        public IActionResult Professeur()
        {
            var profId = HttpContext.Session.GetString("Prof_id");
            ViewBag.SessionProf_id = HttpContext.Session.GetString("Prof_id");
            return View();
        }
    }
}

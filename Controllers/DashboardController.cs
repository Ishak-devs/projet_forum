using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Eleve()
        {
            return View();
        }
        public IActionResult Professeur()
        {
            return View();
        }
    }
}

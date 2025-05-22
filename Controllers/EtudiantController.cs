using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class EtudiantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class NavbarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

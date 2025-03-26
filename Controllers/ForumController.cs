using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ClassId = 1;
            return View();
        }
    }
}

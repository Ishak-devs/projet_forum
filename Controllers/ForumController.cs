using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class ForumController : Controller
    {
        public IActionResult Forum()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Forum.Controllers
{
    public class EtablissementsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

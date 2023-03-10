using Microsoft.AspNetCore.Mvc;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

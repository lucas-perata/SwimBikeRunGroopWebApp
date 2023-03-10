using Microsoft.AspNetCore.Mvc;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

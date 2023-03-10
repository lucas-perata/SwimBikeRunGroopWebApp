using Microsoft.AspNetCore.Mvc;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class TrainingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SwimBikeRunGroopWebApp.Data;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var races = _context.Races.ToList();
            return View(races);
        }

        public IActionResult Detail(int id)
        {
            var race = _context.Races.Where(r => r.Id == id).FirstOrDefault();
            return View(race);
        }
    }
}

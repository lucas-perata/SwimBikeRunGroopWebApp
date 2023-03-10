using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var trainings = _context.Trainings.ToList();
            return View(trainings);
        }

        public IActionResult Detail(int id)
        {
            var training = _context.Trainings.Where(t => t.Id == id).Include(c => c.Club).FirstOrDefault(); 
            return View(training);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.Repository;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _trainingRepository;
        private readonly ApplicationDbContext _context;
        public TrainingController(ITrainingRepository trainingRepository, ApplicationDbContext context)
        {
            _trainingRepository = trainingRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Training> trainings = await _trainingRepository.GetAll();
            return View(trainings);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Training training = await _trainingRepository.GetById(id);
            return View(training);
        }

        public IActionResult Create()
        {
            PopulateClubsDropDownList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Training training)
        {
           
                if (!ModelState.IsValid)
                {
                PopulateClubsDropDownList(training.ClubId);

                return View(training);
  
                }
            _trainingRepository.Add(training);
            return RedirectToAction("Index");

        }

        private void PopulateClubsDropDownList(object selectedClub = null)
        {
            var clubsQuery = from d in _context.Clubs
                                   orderby d.Title
                                   select d;
            ViewBag.ClubId = new SelectList(clubsQuery.AsNoTracking(), "ClubId", "Title", selectedClub);
        }

    }
}

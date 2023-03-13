using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.Repository;
using SwimBikeRunGroopWebApp.ViewModels;

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

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var training = await _trainingRepository.GetById(id);
            if (training == null) return View("Error");
            var trainingVM = new EditTrainingViewModel
            {
                Title = training.Title,
                ClubId = training.ClubId,
                Club = training.Club,
                DistanceFromStart = training.DistanceFromStart,
                StartAddress = training.StartAddress,
                AveragePace = training.AveragePace,         
            };
            return View(trainingVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, EditTrainingViewModel trainingVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit training");
                return View("Edit", trainingVM);
            }


            var userTraining = await _trainingRepository.GetByIdNoTracking(id);

            if (userTraining == null) return View("Error");

            var training = new Training
            {
                Id = id,
                Title = trainingVM.Title,
                ClubId = trainingVM.ClubId,
                Club = trainingVM.Club,
                DistanceFromStart = trainingVM.DistanceFromStart,
                StartAddress = trainingVM.StartAddress,
                AveragePace = trainingVM.AveragePace,
            };

            _trainingRepository.Update(training);

            return RedirectToAction("Index");
        }
    }
}

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
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ApplicationDbContext _context;
        public TrainingController(ITrainingRepository trainingRepository, ApplicationDbContext context, IHttpContextAccessor contextAccessor)
        {
            _trainingRepository = trainingRepository;
            _context = context;
            _contextAccessor = contextAccessor;
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

        public async Task<IActionResult> Create()
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var adminClub = await _trainingRepository.AdminClub(curUserId);
            var createTrainingViewModel = new CreateTrainingViewModel { AppUserId = curUserId, ClubId = adminClub.ClubId };
            return View(createTrainingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingViewModel createTrainingVM)
        {      
            if (ModelState.IsValid)
            {
                var training = new Training
                {
                    Title = createTrainingVM.Title,
                    ClubId = createTrainingVM.ClubId,
                    DistanceFromStart = createTrainingVM.DistanceFromStart,
                    StartAddress = createTrainingVM.StartAddress,
                    AveragePace = createTrainingVM.AveragePace,
                    AppUserId = createTrainingVM.AppUserId
                };
                _trainingRepository.Add(training);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Problems creating a training");
            }

            return View(createTrainingVM);
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id, Training training)
        {
            var trainingToDelete = await _trainingRepository.GetById(id); 

             _trainingRepository.Delete(trainingToDelete);

            return RedirectToAction("Index");
        }
    }
}

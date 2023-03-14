using Microsoft.AspNetCore.Mvc;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.ViewModels;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;

        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Race> races = await _raceRepository.GetAll();
            return View(races); 
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceRepository.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(Race race)
        {
            if (!ModelState.IsValid)
            {
                return View(race);
            }

            _raceRepository.Add(race);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _raceRepository.GetByIdAsync(id);
            if (race == null) return View("Error");
            var raceVM = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                Location = race.Location,
                SportCategory = race.SportCategory,
            };
            return View(raceVM);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit race");
                return View("Edit", raceVM); 
            }

            var userRace = await _raceRepository.GetByIdAsyncNoTracking(id);

            if (userRace == null) return View("Error");

            var race = new Race
            {
                Id = id,
                Title = raceVM.Title,
                Description = raceVM.Description,
                Location = raceVM.Location,
                SportCategory = raceVM.SportCategory,
            };

            _raceRepository.Update(race);

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id, Race race)
        {
            var raceToDelete = await _raceRepository.GetByIdAsync(id);
            _raceRepository.Delete(raceToDelete);

            return RedirectToAction("Index");
        }
    }
}

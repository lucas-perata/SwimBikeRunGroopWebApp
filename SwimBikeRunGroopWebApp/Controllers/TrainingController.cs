using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _trainingRepository;
        public TrainingController(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
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
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Data.Enum;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.ViewModels;
using System;
using System.Dynamic;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubRepository.GetByIdAsync(id);
            return View(club);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);

                var club = new Club
                {
                    Title = clubVM.Title,
                    Strava = clubVM.Strava,
                    IsWomensOnly = clubVM.IsWomensOnly,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    ClubCategory = clubVM.ClubCategory,
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        Province = clubVM.Address.Province,
                    }
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(clubVM);
        }
    }
}

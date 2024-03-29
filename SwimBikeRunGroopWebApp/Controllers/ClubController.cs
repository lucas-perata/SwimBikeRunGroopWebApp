﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IHttpContextAccessor _contextAccessor;
        public ClubController(IClubRepository clubRepository, IPhotoService photoService, IHttpContextAccessor contextAccessor)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
           

            if (User.Identity.IsAuthenticated)
            {
                var curUserId = _contextAccessor.HttpContext.User.GetUserId();
                var hasClub = _clubRepository.UserIsInClub(id, curUserId);
                Club club = await _clubRepository.GetByIdAsync(id);
                var model = new DetailViewModel
                {
                    hasClub = hasClub,
                    club = club,
                };
                return View(model);
            }
            else
            {
                Club club = await _clubRepository.GetByIdAsync(id);
                var model = new DetailViewModel
                {
                    club = club,
                };
                return View(model);
            }
        }

        public IActionResult Create() 
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var createClubViewModel = new CreateClubViewModel { AppUserId = curUserId };
            return View(createClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (_clubRepository.UserHasClub(_contextAccessor.HttpContext.User.GetUserId()) == false)
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
                        AppUserId = clubVM.AppUserId,
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
            }
            else
            {
                ModelState.AddModelError("", "Users can create only one club");
                return RedirectToAction("Index");
            }
            return View(clubVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null) return View("Error");
            var clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                Strava = club.Strava,
                IsWomensOnly = club.IsWomensOnly,
                URL = club.Image,
                ClubCategory = club.ClubCategory,
                AddressId = club.AddressId,
                Address = club.Address
            };
            return View(clubVM); 
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", clubVM);
            }

            var userClub = await _clubRepository.GetByIdAsyncNoTracking(id);

            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(clubVM);
            }

            if (!string.IsNullOrEmpty(userClub.Image))
            {
                _ = _photoService.DeletePhotoAsync(userClub.Image);
            }

            var club = new Club
            {
                ClubId = id,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Strava = clubVM.Strava,
                ClubCategory = clubVM.ClubCategory,
                Image = photoResult.Url.ToString(),
                AddressId = clubVM.AddressId,
                Address = clubVM.Address,
            };


            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id, Club club)
        {
            var clubToDelete = await _clubRepository.GetByIdAsync(id);
            _clubRepository.Delete(clubToDelete); 

            return RedirectToAction("Index");   
        }

        [HttpPost]

        public async Task<IActionResult> JoinClub(int clubId)
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();

            if (_clubRepository.UserIsInClub(clubId, curUserId) == false)
            {
                var userClub = new UserClub
                {
                    AppUserId = curUserId,
                    ClubId = clubId,
                };
                _clubRepository.AddUserToClub(userClub);
                return RedirectToAction("Detail", new { id = clubId });
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        [HttpPost]
        public async Task<IActionResult> LeaveClub(int clubId)
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();

            if (_clubRepository.UserIsInClub(clubId, curUserId) == true)
            {
                var relationToDelete = await _clubRepository.GetUserClubById(clubId, curUserId);
                _clubRepository.DeleteUserOfClub(relationToDelete);
            }

            return RedirectToAction("Detail", new { id = clubId });
        }
    }
}

using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.ViewModels;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPhotoService _photoService;
        public DashboardController(IDashboardRepository dashboardRepository, IHttpContextAccessor contextAccessor,
            IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _contextAccessor = contextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdit(AppUser user, EditUserDashboardViewModel editVM, ImageUploadResult photoResult)
        {
            user.Id = editVM.Id;
            user.Pace = editVM.Pace;
            user.RunWeekly = editVM.RunWeekly;
            user.Bike = editVM.Bike;
            user.BikeWeekly = editVM.BikeWeekly;
            user.Swim = editVM.Swim;
            user.SwimWeekly = editVM.SwimWeekly;
            user.Province = editVM.Province;
            user.Town = editVM.Town;
            user.City = editVM.City;
            user.ProfileImageUrl = photoResult.Url.ToString();
        }
        public async Task<IActionResult> Index()
        {
            var userRaces = await _dashboardRepository.GetAllUserRaces();
            var userClubs = await _dashboardRepository.GetAllUserClubs();
            var userTrainings = await _dashboardRepository.GetAllUserTrainings();

            var dashboardViewModel = new DashboardViewModel()
            {
                Races = userRaces,
                Clubs = userClubs,
                Trainings = userTrainings
            };

            return View(dashboardViewModel);
        }

        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId(); 
            var user = await _dashboardRepository.GetUserById(curUserId);
            
            if (user == null) return View("Error");

            var editUserDashboardViewModel = new EditUserDashboardViewModel()
            {
                Id = user.Id,
                ProfileImageUrl = user.ProfileImageUrl,
                Town = user.Town,
                City = user.City,
                Province = user.Province, 
                Pace = user.Pace,
                RunWeekly = user.RunWeekly,
                Swim = user.Swim, 
                SwimWeekly = user.SwimWeekly,
                Bike = user.Bike, 
                BikeWeekly = user.BikeWeekly,
            };
            return View(editUserDashboardViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardViewModel editVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile");
            }

            var user = await _dashboardRepository.GetByIdNoTracking(editVM.Id);

            if (user == null)
            {
                return View("Error");
            }

            if (user.ProfileImageUrl == "" || user.ProfileImageUrl == null)
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editVM); 
                }
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                MapUserEdit(user, editVM, photoResult);

                _dashboardRepository.Update(user);

                return RedirectToAction("Index");
            }
        }
    }
}

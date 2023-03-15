using Microsoft.AspNetCore.Mvc;
using SwimBikeRunGroopWebApp.Interfaces;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.ViewModels;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;   
        }
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach(var user in users)
            {
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Pace = user.Pace,
                    RunWeekly = user.RunWeekly,
                    Swim =  user.Swim,
                    SwimWeekly = user.SwimWeekly,
                    Bike = user.Bike,
                    BikeWeekly = user.BikeWeekly,
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]

        public async Task<IActionResult> Delete(string id, AppUser user)
        {
            var userToDelete = await _userRepository.GetUserByIdAsync(id);
            _userRepository.Delete(userToDelete);

            return RedirectToAction("Index");
        }
    }
}

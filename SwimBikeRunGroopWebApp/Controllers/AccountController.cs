using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SwimBikeRunGroopWebApp.Data;
using SwimBikeRunGroopWebApp.Models;
using SwimBikeRunGroopWebApp.ViewModels;

namespace SwimBikeRunGroopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; 
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //User found, check the password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                };
                // Password wrong
                TempData["Error"] = "Wrong password. Please, try again";
                return View(loginViewModel);
            }
            // User not found
            TempData["Error"] = "Email does not exist. Please, try again"; 
            return View(loginViewModel);
        }

        public IActionResult Register()
        {
            var response = new RegisterViewModel(); 
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is registered";
                return View();
            }

            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            }; 
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterAdmin()
        {
            var response = new RegisterAdminViewModel();
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> RegisterAdmin(RegisterAdminViewModel registerAdminViewModel)
        {
            if (!ModelState.IsValid) return View(registerAdminViewModel);

            var user = await _userManager.FindByEmailAsync(registerAdminViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is registered";
                return RedirectToAction("Account", "Login");
            }

            var newUserAdmin = new AppUser()
            {
                Email = registerAdminViewModel.EmailAddress,
                UserName = registerAdminViewModel.EmailAddress,
            };
            var newUserResponse = await _userManager.CreateAsync(newUserAdmin, registerAdminViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUserAdmin, UserRoles.Admin);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RegisterSuperAdmin()
        {
            var response = new RegisterAdminViewModel();
            return View(response);
        }

        [HttpPost]

        public async Task<IActionResult> RegisterSuperAdmin(RegisterAdminViewModel registerAdminViewModel)
        {
            if (!ModelState.IsValid) return View(registerAdminViewModel);

            var user = await _userManager.FindByEmailAsync(registerAdminViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is registered";
                return RedirectToAction("Account", "Login");
            }

            var newUserAdmin = new AppUser()
            {
                Email = registerAdminViewModel.EmailAddress,
                UserName = registerAdminViewModel.EmailAddress,
            };
            var newUserResponse = await _userManager.CreateAsync(newUserAdmin, registerAdminViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUserAdmin, UserRoles.SuperAdmin);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}

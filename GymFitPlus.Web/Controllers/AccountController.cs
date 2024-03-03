using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Web.Attributes;
using GymFitPlus.Web.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GymFitPlus.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }


        [HttpGet]
        [AllowAnonymous]
        [UserIsAuthenticated]
        public IActionResult Login()
        {
            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [UserIsAuthenticated]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction(nameof(Dashboard));
                }
                else
                {
                    _logger.LogInformation("Invalid login attempt.");
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [UserIsAuthenticated]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [UserIsAuthenticated]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new()
                {
                    UserName = model.Username,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction(nameof(RegisterUserInfo));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterUserInfo()
        {
            var model = new RegisterUserInfoFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserInfo(RegisterUserInfoFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(User.Id().ToString());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.BirthDate = model.BirthDate;
            user.Gender = model.Gender;
            user.FacebookUrl = model.FacebookUrl;
            user.InstagramUrl = model.InstagramUrl;
            user.YouTubeUrl = model.YouTubeUrl;
            user.PhoneNumber = model.PhoneNumber;

            if (model.Image != null && model.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    model.Image[0].CopyTo(memoryStream);
                    user.Image = memoryStream.ToArray();
                }
            }

            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Dashboard));
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {

            var currentUser = await _userManager.Users
                .AsNoTracking()
                .Where(x => x.Id == User.Id())
                .Select(x => new UserInfoViewModel()
                {
                    FullName = $"{x.FirstName} {x.LastName}",
                    Age = DateTime.Today.Year - x.BirthDate.Year,
                    Gander = x.Gender.ToString(),
                    Image = x.Image,
                    FacebookUrl = x.FacebookUrl,
                    InstagramUrl = x.InstagramUrl,
                    YouTubeUrl = x.YouTubeUrl
                })
                .FirstOrDefaultAsync();


            return View(currentUser);
        }
    }
}



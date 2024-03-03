using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.AccountViewModels;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Web.Attributes;
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
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountController> logger,
            IAccountService accountService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _accountService = accountService;
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
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.RegisterUserAsync(model);

                    if (result.Succeeded)
                    {
                        await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: true, lockoutOnFailure: false);

                        return RedirectToAction(nameof(RegisterUserInfo));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(model);
            }
            catch (Exception)
            {
                //TODO Custom Erro pages
                return RedirectToAction();
            }
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
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.RegisterUserInfoAsync(model, User.Id().ToString());

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Dashboard));
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                return View(model);
            }
            catch (Exception)
            {
                //TODO Custom Erro pages
                return RedirectToAction();
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //[HttpGet]
        //public async Task<IActionResult> Dashboard()
        //{

        //    var currentUser = await _userManager.Users
        //        .AsNoTracking()
        //        .Where(x => x.Id == User.Id())
        //        .Select(x => new UserInfoViewModel()
        //        {
        //            FullName = $"{x.FirstName} {x.LastName}",
        //            Age = DateTime.Today.Year - x.BirthDate.Year,
        //            Gander = x.Gender.ToString(),
        //            Image = x.Image,
        //            FacebookUrl = x.FacebookUrl,
        //            InstagramUrl = x.InstagramUrl,
        //            YouTubeUrl = x.YouTubeUrl
        //        })
        //        .FirstOrDefaultAsync();


        //    return View(currentUser);
        //}
    }
}



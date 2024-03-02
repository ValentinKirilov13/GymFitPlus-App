﻿using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Web.Models.AccountViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            var model = new LoginViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
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
        public IActionResult Register()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Dashboard));
            }

            var model = new RegisterViewModel();

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
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

                    return RedirectToAction(nameof(Dashboard));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            
            var currentUser = await _userManager.Users
                .AsNoTracking()
                .Where(x => x.Id == GetUserId())
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


            return Json(currentUser);
        }
    }
}



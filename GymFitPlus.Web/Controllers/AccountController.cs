using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.AccountViewModels;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Security.Claims;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

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
        [UserIsNotAuthenticated]
        public IActionResult LogInSignUp()
        {
            return View("LogIn_SignUp");
        }

        [HttpPost]
        [AllowAnonymous]
        [UserIsNotAuthenticated]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
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
                    }
                }
                ViewBag.LoginModel = model;
                return View("LogIn_SignUp");
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [UserIsNotAuthenticated]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _accountService.RegisterUserAsync(model);

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User have successfully registered.");

                        await _signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: true, lockoutOnFailure: false);

                        _logger.LogInformation("User logged in.");
                        return RedirectToAction(nameof(RegisterUserInfo), new { register = true });
                    }
                    foreach (var error in result.Errors)
                    {
                        _logger.LogInformation("Invalid register attempt!");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                ViewBag.RegisterModel = model;
                ViewBag.Register = bool.Parse("true");
                return View("LogIn_SignUp");
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> RegisterUserInfo(bool register)
        {
            try
            {
                var model = await _accountService.GetUserInfoForEdit(User.Id().ToString());

                if (register)
                {
                    ViewBag.RegState = "SecondPartRegistration";
                    TempData["RegState"] = true;
                }

                return View(model);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserInfo(RegisterUserInfoFormViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _accountService.RegisterUserInfoAsync(model, User.Id().ToString());

                    if (result.Succeeded)
                    {
                        if (TempData["RegState"] != null)
                        {
                            TempData.Remove("RegState");
                            return RedirectToAction(nameof(StatisticController.Index), "Statistic");
                        }
                        else
                        {
                            return RedirectToAction(nameof(Dashboard));
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        _logger.LogInformation("Invalid full register attempt!");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(model);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                UserInfoViewModel currentUser = await _accountService.GetUserForDashboardAsync(User.Id());

                return View(currentUser);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }
    }
}



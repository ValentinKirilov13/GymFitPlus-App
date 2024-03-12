using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.AccountViewModels;
using GymFitPlus.Infrastructure.Data.Models;
using GymFitPlus.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
                        return RedirectToAction(nameof(RegisterUserInfo));
                    }
                    else
                    {
                        _logger.LogInformation("Invalid login attempt.");
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }

                return View("LogIn_SignUp",model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                //TODO Custom Erro pages
                return RedirectToAction();
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
                        return RedirectToAction(nameof(RegisterUserInfo));
                    }
                    foreach (var error in result.Errors)
                    {
                        _logger.LogInformation("Invalid register attempt!");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                ViewBag.Register = bool.Parse("true");
                return View("LogIn_SignUp", model);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);

                //TODO Custom Erro pages
                return RedirectToAction();
            }
        }

        [HttpGet]
        public async Task<IActionResult> RegisterUserInfo()
        {
            try
            {
                if (await _accountService.IsCurrentUserFullRegisteredAsync(User.Id()))
                {
                    return RedirectToAction(nameof(Dashboard));
                }

                var model = new RegisterUserInfoFormViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                //TODO Custom Erro pages
                return RedirectToAction();
            }         
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUserInfo(RegisterUserInfoFormViewModel model)
        {
            try
            {
                if (await _accountService.IsCurrentUserFullRegisteredAsync(User.Id()))
                {
                    return RedirectToAction(nameof(Dashboard));
                }

                if (ModelState.IsValid && model.BirthDate != DateTime.Today && model.Gender != default)
                {
                    IdentityResult result = await _accountService.RegisterUserInfoAsync(model, User.Id().ToString());

                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User have successfully full registered.");
                        return RedirectToAction(nameof(Dashboard));
                    }
                    foreach (var error in result.Errors)
                    {
                        _logger.LogInformation("Invalid full register attempt!");
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                
                if (model.BirthDate == DateTime.Today)
                {
                    ModelState.AddModelError("BirthDate", string.Format(RequiredErrorMessage, "BirthDate"));
                }

                if(model.Gender == default)
                {
                    ModelState.AddModelError("Gender", string.Format(RequiredErrorMessage, "Gender"));
                }
                
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                if (ex is NullReferenceException)
                {
                    // Handle FileNotFoundException
                    return RedirectToAction();
                }
                else
                {
                    //TODO Custom Erro pages
                    return RedirectToAction();
                }             
            }           
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
            try
            {
                UserInfoViewModel currentUser = await _accountService.GetUserForDashboardAsync(User.Id());

                return View(currentUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                //TODO Custom Erro pages
                return RedirectToAction();
            }            
        }
    }
}



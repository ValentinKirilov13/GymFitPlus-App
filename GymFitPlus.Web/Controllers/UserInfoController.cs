using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.UserInfoViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Controllers
{
    public class UserInfoController : BaseController
    {
        private readonly IUserInfoServices services;

        public UserInfoController(IUserInfoServices services)
        {
            this.services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string? currUserId = GetUserId();

            if (currUserId == null)
            {
                return BadRequest();
            }

            var model = await services.GetUserInfoByIdAsync(currUserId);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new UserInfoViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInfoViewModel viewModel)
        {
            string? currUserId = GetUserId();

            if (currUserId == null)
            {
                return BadRequest();
            }

            await services.CreateUserAsync(viewModel, currUserId);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            string? currUserId = GetUserId();

            if (currUserId == null)
            {
                return BadRequest();
            }

            var model = await services.GetUserInfoByIdAsync(currUserId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserInfoViewModel viewModel)
        {
            string? currUserId = GetUserId();

            if (currUserId == null)
            {
                return BadRequest();
            }

            await services.EditUserAsync(viewModel, currUserId);

            return RedirectToAction(nameof(Index));
        }
    }
}

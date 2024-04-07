using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminBaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<UserViewModel> viewModel = await _accountService.GetAllUsersAsync();

            return View(viewModel);
        }
    }
}

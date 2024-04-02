using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.StatisticViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymFitPlus.Web.Controllers
{
    public class StatisticController : BaseController
    {
        private readonly IStatisticService _statisticService;
        private readonly ILogger<StatisticController> _logger;

        public StatisticController(IStatisticService statisticService, ILogger<StatisticController> logger)
        {
            _statisticService = statisticService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new UserStatsViewModel();

            return View(nameof(ChangeStats), viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStats()
        {
            var viewModel = await _statisticService.GetUserLastAllStatsAsync(User.Id());

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStats(UserStatsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            viewModel.UserId = User.Id();
            viewModel.DateOfМeasurements = DateTime.Today;

            await _statisticService.UpdateUserStatsAsync(viewModel);

            return RedirectToAction(nameof(AccountController.Dashboard), "Account");
        }

        [HttpGet]
        public async Task<IActionResult> GetStats(string statsType)
        {
            var viewModel = await _statisticService.GetUserConcreteStatsAsync(statsType, User.Id());
       
            return Json(viewModel);
        }
    }
}

using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.StatisticViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

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

            ViewBag.RegState = "ThirdStateOfRegister";

            return View(nameof(ChangeStats), viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStats()
        {
            try
            {
                var viewModel = await _statisticService.GetUserLastAllStatsAsync(User.Id());

                return View(viewModel);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return View(new UserStatsViewModel());
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStats(UserStatsViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                viewModel.UserId = User.Id();
                viewModel.DateOfМeasurements = DateTime.Today;

                bool result = await _statisticService.UpdateUserStatsAsync(viewModel);

                if (result)
                {
                    TempData["UserMessageSuccess"] = "Successfully updated statistics";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(AccountController.Dashboard), "Account");
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
        public async Task<IActionResult> GetStats(string statsType)
        {
            try
            {
                var viewModel = await _statisticService.GetUserConcreteStatsAsync(statsType, User.Id());

                return Json(viewModel);
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

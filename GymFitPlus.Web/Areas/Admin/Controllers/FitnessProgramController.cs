using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class FitnessProgramController : AdminBaseController
    {
        private readonly IFitnessProgramService _fitnessProgramService;
        private readonly ILogger<FitnessProgramController> _logger;

        public FitnessProgramController(IFitnessProgramService fitnessProgramService, ILogger<FitnessProgramController> logger)
        {
            _fitnessProgramService = fitnessProgramService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(QueryFitnessProgramViewModel viewModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(viewModel.Username))
                {
                    TempData["Username"] = viewModel.Username;

                    viewModel.UserDeletedFitnessPrograms = await _fitnessProgramService.GetUserAllDeletedFitnessProgramsAsync(viewModel.Username);
                }

                return View(viewModel);
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
        public async Task<IActionResult> RestoreFitnessProgram(int id)
        {
            try
            {
                await _fitnessProgramService.RestoreFitnessProgramAsync(id);

                return RedirectToAction(nameof(Index), new { username = TempData["Username"] });
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

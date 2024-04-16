using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Controllers
{
    public class WorkoutController : BaseController
    {
        private readonly IWorkoutService _workoutService;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IWorkoutService workoutService,
                                 ILogger<WorkoutController> logger)
        {
            _workoutService = workoutService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int skip = 0, int take = 0)
        {
            try
            {
                if (take > 0)
                {
                    IEnumerable<WorkoutAllViewModel> model = await _workoutService.GetAllWorkoutsAsync(User.Id());

                    model = model.OrderByDescending(x => x.Date).Skip(skip).Take(take);
                    return Json(model);
                }

                return View();
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
        public async Task<IActionResult> Details(int workoutId)
        {
            try
            {
                WorkoutDetailViewModel model = await _workoutService.GetByIdWorkoutAsync(workoutId, User.Id());

                return Json(model);
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
        public async Task<IActionResult> CreateWorkout(WorkoutDetailViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["FitnessProgramId"] != viewModel.FitnessProgramId)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                if (!ModelState.IsValid)
                {
                    List<string> errors = ModelState.Values
                     .SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList();

                    TempData["WorkoutModelErrors"] = errors;
                    TempData.Put("WorkoutModel", viewModel);
                    return RedirectToAction(nameof(Details), "FitnessProgram", new { id = viewModel.FitnessProgramId, startWorkout = true });
                }

                viewModel.UserId = User.Id();
                viewModel.Date = DateTime.Today;

                bool result = await _workoutService.CreateWorkoutAsync(viewModel);

                if (result)
                {
                    TempData["UserMessageSuccess"] = $"Successfully finished workout";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> DeleteWorkout(WorkoutDetailViewModel viewModel)
        {
            try
            {
                if (viewModel.UserId != User.Id())
                {
                    return Unauthorized();
                }

                bool result = await _workoutService.DeleteWorkoutAsync(viewModel, User.Id());

                if (result)
                {
                    TempData["UserMessageSuccess"] = $"Successfully deleted workout";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(Index));
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

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
            if (take > 0)
            {
                IEnumerable<WorkoutAllViewModel> model = await _workoutService.GetAllWorkoutsAsync(User.Id());

                model = model.OrderByDescending(x => x.Date).Skip(skip).Take(take);
                return Json(model);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int workoutId)
        {
            WorkoutDetailViewModel model = await _workoutService.GetByIdWorkoutAsync(workoutId, User.Id());

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout(WorkoutDetailViewModel viewModel)
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

            await _workoutService.CreateWorkoutAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteWorkout(WorkoutDetailViewModel viewModel)
        {
            if (viewModel.UserId != User.Id())
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }

            await _workoutService.DeleteWorkoutAsync(viewModel, User.Id());

            return RedirectToAction(nameof(Index));
        }
    }
}

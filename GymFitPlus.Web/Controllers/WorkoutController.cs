using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Index()
        {
            IEnumerable<WorkoutAllViewModel> model = await _workoutService.GetAllWorkoutsAsync(User.Id());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int workoutId)
        {
            WorkoutDetailViewModel model = await _workoutService.GetByIdWorkoutAsync(workoutId,User.Id());
 
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkout(WorkoutDetailViewModel viewModel)
        {
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
                return Forbid();
            }

            await _workoutService.DeleteWorkoutAsync(viewModel, User.Id());

            return RedirectToAction(nameof(Index));
        }
    }
}

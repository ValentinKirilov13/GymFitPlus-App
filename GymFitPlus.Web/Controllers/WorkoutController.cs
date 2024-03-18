using GymFitPlus.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Controllers
{
    public class WorkoutController : BaseController
    {
        private readonly IWorkoutService _workoutService;
        private readonly ILogger<WorkoutController> _logger;

        public WorkoutController(IWorkoutService workoutService, ILogger<WorkoutController> logger)
        {
            _workoutService = workoutService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

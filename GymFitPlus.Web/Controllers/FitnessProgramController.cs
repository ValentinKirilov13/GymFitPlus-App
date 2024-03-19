using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace GymFitPlus.Web.Controllers
{
    public class FitnessProgramController : BaseController
    {
        private readonly IFitnessProgramService _fitnessProgramService;
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<FitnessProgramController> _logger;

        public FitnessProgramController(
            IFitnessProgramService fitnessProgramService,
            IExerciseService exerciseService,
            ILogger<FitnessProgramController> logger)
        {
            _fitnessProgramService = fitnessProgramService;
            _exerciseService = exerciseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(bool startWorkout = false)
        {
            IEnumerable<FitnessProgramFormViewModel> allPrograms = await _fitnessProgramService.AllFitnessProgramsAsync(User.Id());

            if (startWorkout)
            {
                return View("StartWorkout", allPrograms);
            }

            return View(allPrograms);
        }

        [HttpGet]
        public IActionResult AddProgram()
        {
            var model = new FitnessProgramFormViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProgram(FitnessProgramFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _fitnessProgramService.AddFitnessProgramAsync(viewModel, User.Id());

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, bool startWorkout = false)
        {
            if (TempData["Succssed"] != null)
            {
                string? succeeded = TempData["Succssed"]?.ToString();

                if (succeeded != null)
                {
                    ViewBag.Success = bool.Parse(succeeded);
                }
            }

            var program = await _fitnessProgramService.FindFitnessProgramByIdAsync(id);

            if (startWorkout)
            {
                return View("StartWorkoutDashboard", program);
            }

            return View(program);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FitnessProgramDetailViewModel viewModel)
        {
            await _fitnessProgramService.DeleteFitnessProgramAsync(viewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> EditProgramName(FitnessProgramDetailViewModel viewModel)
        {
            TempData["Succssed"] = false;

            if (ModelState.IsValid)
            {
                TempData["Succssed"] = await _fitnessProgramService.EditFitnessProgramAsync(viewModel);
                TempData["Action"] = "edited program name";
            }
            else
            {
                var errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                            .ToList();

                StringBuilder sb = new StringBuilder();

                foreach (var error in errors)
                {
                    sb.AppendLine(error);
                }

                TempData["Errors"] = sb.ToString().TrimEnd();
            }

            return RedirectToAction("Details", new { id = viewModel.Id });
        }



        [HttpGet]
        public async Task<IActionResult> AddExerciseToProgram(int programId, int exerciseCount, int exerciseId = -1)
        {
            var model = new FitnessProgramExercisesInfoViewModel();
            model.FitnessProgramId = programId;
            model.ExerciseId = exerciseId;
            model.Order = ++exerciseCount;

            var exercisesIdsNotToGet = await _fitnessProgramService.GetAllExerciseFromProgramAsync(programId);
            model.Exercises = await _exerciseService.GetAllExerciseForProgramAsync(exercisesIdsNotToGet);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseToProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var exercisesIdsNotToGet = await _fitnessProgramService.GetAllExerciseFromProgramAsync(viewModel.FitnessProgramId);
                viewModel.Exercises = await _exerciseService.GetAllExerciseForProgramAsync(exercisesIdsNotToGet);
                return View(viewModel);
            }

            TempData["Succssed"] = await _fitnessProgramService.AddExerciseToProgramAsync(viewModel);

            TempData["Action"] = "added exercise in program";

            return RedirectToAction("Details", new { id = viewModel.FitnessProgramId });
        }

        [HttpPost]
        public async Task<IActionResult> EditExerciseFromProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            TempData["Succssed"] = await _fitnessProgramService.EditExerciseFromProgramAsync(viewModel);

            TempData["Action"] = "edited exercise";

            return RedirectToAction("Details", new { id = viewModel.FitnessProgramId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveExerciseFromProgram(int exerciseId, int programId)
        {
            TempData["Succssed"] = await _fitnessProgramService.RemoveExerciseFromProgramAsync(exerciseId, programId);

            TempData["Action"] = "removed exercise";

            return RedirectToAction("Details", new { id = programId });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProgramsAjax(int withoutExerciseId)
        {
            var programs = await _fitnessProgramService.GetAllFitnessProgramsFilltered(User.Id(), withoutExerciseId);

            return Json(programs);
        }
    }
}

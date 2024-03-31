using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using System.Text;

namespace GymFitPlus.Web.Controllers
{
    public class FitnessProgramController : BaseController
    {
        private readonly IFitnessProgramService _fitnessProgramService;
        private readonly ILogger<FitnessProgramController> _logger;

        public FitnessProgramController(
            IFitnessProgramService fitnessProgramService,
            ILogger<FitnessProgramController> logger)
        {
            _fitnessProgramService = fitnessProgramService;
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
            var program = await _fitnessProgramService.FindFitnessProgramByIdAsync(id);

            if (startWorkout)
            {
                TempData["FitnessProgramId"] = program.Id;

                if (TempData.Get<WorkoutDetailViewModel>("WorkoutModel") != null && TempData["WorkoutModelErrors"] != null)
                {
                    if (TempData["WorkoutModelErrors"] is string[] errors && errors.Any())
                    {
                        foreach (var error in errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }

                    ViewBag.ValidationError = true;
                    ViewBag.WorkoutModel = TempData.Get<WorkoutDetailViewModel>("WorkoutModel");
                }
                else
                {
                    ViewBag.WorkoutModel = new WorkoutDetailViewModel() { FitnessProgramId = program.Id };
                }

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
        public async Task<IActionResult> GetAllProgramsAjax(int withoutExerciseId)
        {
            var programs = await _fitnessProgramService.GetAllFitnessProgramsFilltered(User.Id(), withoutExerciseId);

            return Json(programs);
        }
    }
}

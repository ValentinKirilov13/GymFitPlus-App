using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> Index()
        {
            IEnumerable<FitnessProgramFormViewModel> allPrograms = await _fitnessProgramService.AllFitnessProgramsAsync(User.Id());

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
        public async Task<IActionResult> Details(int id)
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

            return View(program);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FitnessProgramDetailViewModel viewModel)
        {
            await _fitnessProgramService.DeleteFitnessProgramAsync(viewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddExerciseToProgram(int programId, int exerciseCount)
        {
            var model = new FitnessProgramExercisesInfoViewModel();
            model.FitnessProgramId = programId;
            model.Order = ++exerciseCount;
            model.Exercises = await _exerciseService.GetAllExerciseForProgramAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseToProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Exercises = await _exerciseService.GetAllExerciseForProgramAsync();
                return View(viewModel);
            }

            TempData["Succssed"] = await _fitnessProgramService.AddExerciseToProgramAsync(viewModel);


            return RedirectToAction("Details", new { id = viewModel.FitnessProgramId });
        }

        [HttpPost]
        public async Task<IActionResult> EditExerciseInProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = true;

                return View("Details", await _fitnessProgramService.FindFitnessProgramByIdAsync(viewModel.FitnessProgramId));
                //return ViewComponent("EditExerciseInProgramComponent", new
                //{
                //    exerciseId = viewModel.ExerciseId,
                //    programId = viewModel.FitnessProgramId
                //});
            }

            ViewBag.Errors = false;

            await _fitnessProgramService.EditFitnessProgramExercise(viewModel);


            return RedirectToAction("Details", new { id = viewModel.FitnessProgramId });
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var program = await _fitnessProgramService.FindFitnessProgramByIdAsync(id);
            return View(program);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FitnessProgramFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _fitnessProgramService.EditFitnessProgram(model);

                return Json(new { success = true, id = model.Id });
            }

            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return Json(new { success = false, errors });
        }


    }
}

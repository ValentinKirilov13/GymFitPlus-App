using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GymFitPlus.Web.Controllers
{
    public class FitnessProgramController : BaseController
    {
        private readonly IFitnessProgramService _fitnessProgramService;

        public FitnessProgramController(IFitnessProgramService fitnessProgramService)
        {
            _fitnessProgramService = fitnessProgramService;
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

                ViewBag.Success = bool.Parse(succeeded);
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
        public async Task<IActionResult> AddExerciseToProgram(int programId)
        {
            var model = new FitnessProgramExercisesInfoViewModel();
            model.FitnessProgramId = programId;
            model.Exercises = await _fitnessProgramService.GetAllExerciseForProgramAsync();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseToProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Exercises = await _fitnessProgramService.GetAllExerciseForProgramAsync();
                return View(viewModel);
            }

            TempData["Succssed"] = await _fitnessProgramService.AddExerciseToProgramAsync(viewModel);


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
                // Process the valid data
                // ...

                return Json(new { success = true, id = model.Id });
            }

            // If ModelState is not valid, return the validation errors
            var errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );

            return Json(new { success = false, errors });
        }
    }
}

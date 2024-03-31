﻿using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Mvc;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Controllers
{
    public class ExerciseController : BaseController
    {
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IExerciseService exerciseService, ILogger<ExerciseController> logger)
        {
            _exerciseService = exerciseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllExercisesQueryModel query)
        {
            IEnumerable<ExerciseAllViewModel> model = await _exerciseService.AllExerciseAsync(query);

            ViewBag.Query = query;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ExerciseDetailViewModel model = await _exerciseService.FindExerciseByIdAsync(id);
            
            return View(model);
        }

        [HttpGet]
        public IActionResult AddExercise()
        {
            ExerciseDetailViewModel model = new ExerciseDetailViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise(ExerciseDetailViewModel viewModel)
        {
            if (viewModel.MuscleGroup == default)
            {
                ModelState.AddModelError(nameof(viewModel.MuscleGroup), string.Format(RequiredErrorMessage, "Muscle group"));
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _exerciseService.AddExerciseAsync(viewModel);


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditExercise(int id)
        {
            ExerciseDetailViewModel model = await _exerciseService.FindExerciseByIdAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditExercise(int id, ExerciseDetailViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (viewModel.MuscleGroup == default)
            {
                ModelState.AddModelError(nameof(viewModel.MuscleGroup), string.Format(RequiredErrorMessage, "Muscle group"));
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            await _exerciseService.EditExerciseAsync(viewModel);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExercise(int id, ExerciseDetailViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            await _exerciseService.DeleteExerciseAsync(viewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddExerciseToProgram(int programId, int exerciseCount, int exerciseId = -1)
        {
            var model = new FitnessProgramExercisesInfoViewModel
            {
                FitnessProgramId = programId,
                ExerciseId = exerciseId,
                Order = ++exerciseCount,
                Exercises = await _exerciseService.GetAllExerciseForProgramAsync(programId)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddExerciseToProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Exercises = await _exerciseService.GetAllExerciseForProgramAsync(viewModel.FitnessProgramId);
                return View(viewModel);
            }

            await _exerciseService.AddExerciseToProgramAsync(viewModel);
          
            return RedirectToAction(nameof(FitnessProgramController.Details), 
                                    "FitnessProgram", 
                                    new { id = viewModel.FitnessProgramId });
        }

        [HttpPost]
        public async Task<IActionResult> EditExerciseFromProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                await _exerciseService.EditExerciseFromProgramAsync(viewModel);
            }
            else
            {
                Dictionary<string, string> errors = new();

                foreach (var error in ModelState)
                {
                    foreach (var item in error.Value.Errors)
                    {
                        errors.Add(error.Key, item.ErrorMessage);
                    }
                }
            
                TempData["FitnessProgramExerciseViewModelErrors"] = errors;
            }
           
            return RedirectToAction(nameof(FitnessProgramController.Details),
                                   "FitnessProgram",
                                   new { id = viewModel.FitnessProgramId });
        }

        [HttpPost]
        public async Task<IActionResult> RemoveExerciseFromProgram(int exerciseId, int programId)
        {
            await _exerciseService.RemoveExerciseFromProgramAsync(exerciseId, programId);

            return RedirectToAction(nameof(FitnessProgramController.Details),
                                   "FitnessProgram",
                                   new { id = programId });
        }
    }
}

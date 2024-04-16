using GymFitPlus.Core.Contracts;
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
            try
            {
                IEnumerable<ExerciseAllViewModel> model = await _exerciseService.AllExerciseAsync(query);

                ViewBag.Query = query;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                ExerciseDetailViewModel model = await _exerciseService.FindExerciseByIdAsync(id);

                return View(model);
            }
            catch (NullReferenceException ex)
            {
                _logger.LogError("{Message:}", $"{NullReferenceErrorMessage} {ex.Message}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}",ex.Message);
                return BadRequest();
            }
        }
     
        [HttpGet]
        public async Task<IActionResult> AddExerciseToProgram(int programId, int exerciseCount, int exerciseId = -1)
        {
            try
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
        public async Task<IActionResult> AddExerciseToProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    viewModel.Exercises = await _exerciseService.GetAllExerciseForProgramAsync(viewModel.FitnessProgramId);
                    return View(viewModel);
                }

                bool result = await _exerciseService.AddExerciseToProgramAsync(viewModel);

                if (result)
                {
                    TempData["UserMessageSuccess"] = "Successfully added exercise to program";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(FitnessProgramController.Details),
                                        "FitnessProgram",
                                        new { id = viewModel.FitnessProgramId });
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
        public async Task<IActionResult> EditExerciseFromProgram(FitnessProgramExercisesInfoViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = await _exerciseService.EditExerciseFromProgramAsync(viewModel);

                    if (result)
                    {
                        TempData["UserMessageSuccess"] = $"Successfully edited exercise {viewModel.ExerciseName}";
                    }
                    else
                    {
                        TempData["UserMessageError"] = "Аn error occurred, please try again later";
                    }
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
                    TempData["ModalToShow"] = $"element{viewModel.FitnessProgramId}{viewModel.ExerciseId}edit";
                    TempData["FitnessProgramExerciseViewModelErrors"] = errors;
                }

                return RedirectToAction(nameof(FitnessProgramController.Details),
                                       "FitnessProgram",
                                       new { id = viewModel.FitnessProgramId });
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
        public async Task<IActionResult> RemoveExerciseFromProgram(int exerciseId, int programId)
        {
            try
            {
                bool result = await _exerciseService.RemoveExerciseFromProgramAsync(exerciseId, programId);

                if (result)
                {
                    TempData["UserMessageSuccess"] = "Successfully deleted exercise";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(FitnessProgramController.Details),
                                       "FitnessProgram",
                                       new { id = programId });
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

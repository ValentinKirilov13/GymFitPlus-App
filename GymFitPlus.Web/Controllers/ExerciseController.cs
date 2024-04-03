using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RoleConstants;


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
        [Authorize(Roles = AdminRole)]
        public IActionResult AddExercise()
        {
            ExerciseDetailViewModel model = new ExerciseDetailViewModel();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = AdminRole)]
        public async Task<IActionResult> AddExercise(ExerciseDetailViewModel viewModel)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
        }

        [HttpGet]
        [Authorize(Roles = AdminRole)]
        public async Task<IActionResult> EditExercise(int id)
        {
            try
            {
                ExerciseDetailViewModel model = await _exerciseService.FindExerciseByIdAsync(id);

                TempData["ExerciseId"] = model.Id;

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
        [Authorize(Roles = AdminRole)]
        public async Task<IActionResult> EditExercise(ExerciseDetailViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["ExerciseId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
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
        [Authorize(Roles = AdminRole)]
        public async Task<IActionResult> DeleteExercise(ExerciseDetailViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["ExerciseId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                await _exerciseService.DeleteExerciseAsync(viewModel.Id);

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

                await _exerciseService.AddExerciseToProgramAsync(viewModel);

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
                await _exerciseService.RemoveExerciseFromProgramAsync(exerciseId, programId);

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

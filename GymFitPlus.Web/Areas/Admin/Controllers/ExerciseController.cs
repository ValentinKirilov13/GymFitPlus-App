using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using Microsoft.AspNetCore.Mvc;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class ExerciseController : AdminBaseController
    {
        private readonly IExerciseService _exerciseService;
        private readonly ILogger<ExerciseController> _logger;

        public ExerciseController(IExerciseService exerciseService, ILogger<ExerciseController> logger)
        {
            _exerciseService = exerciseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(bool deleted)
        {
            try
            {
                IEnumerable<ExerciseAllViewModel> viewModel = await _exerciseService.AllExerciseForAdminAsync(deleted);

                ViewBag.IsDeleted = deleted;

                return View(viewModel);
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
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }
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

        [HttpPost]
        public async Task<IActionResult> RestoreExercise(ExerciseDetailViewModel viewModel)
        {
            try
            {
                await _exerciseService.RestoreExerciseAsync(viewModel.Id);

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
    }
}

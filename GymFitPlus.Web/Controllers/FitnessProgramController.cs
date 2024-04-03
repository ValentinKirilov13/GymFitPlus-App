using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
using System.Text;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

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
            try
            {
                IEnumerable<FitnessProgramFormViewModel> allPrograms = await _fitnessProgramService.AllFitnessProgramsAsync(User.Id());

                if (startWorkout)
                {
                    return View("StartWorkout", allPrograms);
                }

                return View(allPrograms);
            }
            catch (Exception ex)
            {
                _logger.LogError("{Message:}", ex.Message);
                return BadRequest();
            }         
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
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                await _fitnessProgramService.AddFitnessProgramAsync(viewModel, User.Id());

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
        public async Task<IActionResult> Details(int id, bool startWorkout = false)
        {
            try
            {
                var program = await _fitnessProgramService.FindFitnessProgramByIdAsync(id);

                TempData["FitnessProgramId"] = program.Id;

                if (startWorkout)
                {                   
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
        public async Task<IActionResult> Delete(FitnessProgramDetailViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["FitnessProgramId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                await _fitnessProgramService.DeleteFitnessProgramAsync(viewModel.Id);

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
        public async Task<IActionResult> EditProgramName(FitnessProgramDetailViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["FitnessProgramId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _fitnessProgramService.EditFitnessProgramAsync(viewModel);
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
        public async Task<IActionResult> GetAllProgramsAjax(int withoutExerciseId)
        {
            try
            {
                var programs = await _fitnessProgramService.GetAllFitnessProgramsFilltered(User.Id(), withoutExerciseId);

                return Json(programs);
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

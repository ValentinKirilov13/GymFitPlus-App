using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.FitnessProgramViewModels;
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Security.Claims;
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

                bool result = await _fitnessProgramService.AddFitnessProgramAsync(viewModel, User.Id());

                if (result)
                {
                    TempData["UserMessageSuccess"] = $"Successfully was created fitness program {viewModel.Name}";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

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
                        if (TempData["WorkoutModelErrors"] is string[] errorsWorkout && errorsWorkout.Any())
                        {
                            foreach (var error in errorsWorkout)
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

                if (TempData["NameErrors"] != null)
                {
                    if (TempData["NameErrors"] is Dictionary<string,string> errors && errors.Any())
                    {
                        foreach (var error in errors)
                        {
                            ModelState.AddModelError(error.Key, error.Value);
                        }
                    }                  
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
                 
                bool result = await _fitnessProgramService.DeleteFitnessProgramAsync(viewModel.Id);

                if (result)
                {
                    TempData["UserMessageSuccess"] = $"Successfully deleted fitness program {viewModel.Name}";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

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
                    bool result = await _fitnessProgramService.EditFitnessProgramAsync(viewModel);

                    if (result)
                    {
                        TempData["UserMessageSuccess"] = $"Successfully change name to {viewModel.Name}";
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

                    TempData["ModalToShow"] = "ProgramNameModal";
                    TempData["NameErrors"] = errors;
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

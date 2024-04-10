using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.RecipeViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class RecipeController : AdminBaseController
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(bool deleted)
        {
            try
            {
                IEnumerable<RecipesAllViewModel> viewModel = await _recipeService.AllRecipeForAdminAsync(deleted);

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
                RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id, favourite: false, User.Id());

                if (TempData["NoteError"] != null)
                {
                    string? errorMessage = TempData["NoteError"]?.ToString();

                    if (errorMessage != null)
                    {
                        ModelState.AddModelError(nameof(model.Note), errorMessage);
                    }
                }

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
        public IActionResult AddRecipe()
        {
            RecipeDetailsViewModel model = new RecipeDetailsViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeDetailsViewModel viewModel)
        {
            try
            {
                if (viewModel.Category == default)
                {
                    ModelState.AddModelError(nameof(viewModel.Category), string.Format(RequiredErrorMessage, "Recipe type"));
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                await _recipeService.AddRecipeAsync(viewModel);

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
        public async Task<IActionResult> EditRecipe(int id)
        {
            try
            {
                RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id, false, User.Id());

                TempData["RecipeId"] = model.Id;

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
        public async Task<IActionResult> EditRecipe(RecipeDetailsViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["RecipeId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                if (viewModel.Category == default)
                {
                    ModelState.AddModelError(nameof(viewModel.Category), string.Format(RequiredErrorMessage, "Recipe type"));
                }

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                await _recipeService.EditRecipeAsync(viewModel);

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
        public async Task<IActionResult> DeleteRecipe(RecipeDetailsViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["RecipeId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                await _recipeService.DeleteRecipeAsync(viewModel.Id);

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
        public async Task<IActionResult> RestoreRecipe(RecipeDetailsViewModel viewModel)
        {
            try
            {
                await _recipeService.RestoreRecipeAsync(viewModel.Id);

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

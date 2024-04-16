using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.RecipeViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using static GymFitPlus.Core.ErrorMessages.ErrorMessages;

namespace GymFitPlus.Web.Controllers
{
    public class RecipeController : BaseController
    {
        private readonly IRecipeService _recipeService;
        private readonly ILogger<RecipeController> _logger;

        public RecipeController(IRecipeService recipeService, ILogger<RecipeController> logger)
        {
            _recipeService = recipeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] AllRecipesQueryModel query, bool favourite)
        {
            try
            {
                IEnumerable<RecipesAllViewModel> model = await _recipeService.AllRecipesAsync(query, favourite, User.Id());

                ViewBag.IsFavourite = favourite;

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
        public async Task<IActionResult> Details(int id, bool favourite)
        {
            try
            {
                RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id, favourite, User.Id());

                ViewBag.IsFavourite = favourite;

                TempData["RecipeId"] = model.Id;

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

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(RecipeDetailsViewModel viewModel)
        {
            try
            {
                bool result = await _recipeService.AddRecipeToFavouriteAsync(viewModel, User.Id());

                if (result)
                {
                    TempData["UserMessageSuccess"] = $"Successfully added recipe {viewModel.Name} to favourite";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(Index), new { favourite = true });
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
        public async Task<IActionResult> EditFavouriteRecipe(RecipeDetailsViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["RecipeId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                if (ModelState["Note"] != null && ModelState["Note"]?.ValidationState == ModelValidationState.Valid)
                {
                    bool result = await _recipeService.EditFavouriteRecipeAsync(viewModel, User.Id());

                    if (result)
                    {
                        TempData["UserMessageSuccess"] = $"Successfully edited recipe {viewModel.Name} from favourite";
                    }
                    else
                    {
                        TempData["UserMessageError"] = "Аn error occurred, please try again later";
                    }
                }
                else
                {
                    TempData["NoteError"] = ModelState["Note"]?.Errors[0].ErrorMessage;
                }

                return RedirectToAction(nameof(Details), new { favourite = true, id = viewModel.Id });
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
        public async Task<IActionResult> DeleteFromFavorite(RecipeDetailsViewModel viewModel)
        {
            try
            {
                if ((int?)TempData["RecipeId"] != viewModel.Id)
                {
                    _logger.LogError("{Message:}", TryToEditNotChoosenOne);
                    return BadRequest();
                }

                bool result = await _recipeService.DeleteRecipeFromFavouriteAsync(viewModel, User.Id());

                if (result)
                {
                    TempData["UserMessageSuccess"] = $"Successfully deleted recipe {viewModel.Name} from favourite";
                }
                else
                {
                    TempData["UserMessageError"] = "Аn error occurred, please try again later";
                }

                return RedirectToAction(nameof(Index), new { favourite = true });
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

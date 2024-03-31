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
            IEnumerable<RecipesAllViewModel> model = await _recipeService.AllRecipesAsync(query, favourite, User.Id());

            ViewBag.IsFavourite = favourite;

            ViewBag.Query = query;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, bool favourite)
        {
            RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id, favourite, User.Id());

            ViewBag.IsFavourite = favourite;

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

        [HttpGet]
        public IActionResult AddRecipe()
        {
            RecipeDetailsViewModel model = new RecipeDetailsViewModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe(RecipeDetailsViewModel viewModel)
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

        [HttpGet]
        public async Task<IActionResult> EditRecipe(int id)
        {
            RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id, false, User.Id());

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRecipe(int id, RecipeDetailsViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
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

        [HttpPost]
        public async Task<IActionResult> DeleteRecipe(int id, RecipeDetailsViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            await _recipeService.DeleteRecipeAsync(viewModel.Id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorite(RecipeDetailsViewModel viewModel)
        {
            await _recipeService.AddRecipeToFavouriteAsync(viewModel, User.Id());

            return RedirectToAction(nameof(Index), new { favourite = true });
        }

        [HttpPost]
        public async Task<IActionResult> EditFavouriteRecipe(RecipeDetailsViewModel viewModel)
        {
            if (ModelState["Note"] != null && ModelState["Note"]?.ValidationState == ModelValidationState.Valid)
            {
                await _recipeService.EditFavouriteRecipeAsync(viewModel, User.Id());
            }
            else
            {
                TempData["NoteError"] = ModelState["Note"]?.Errors[0].ErrorMessage;
            }


            return RedirectToAction(nameof(Details), new { favourite = true, id = viewModel.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromFavorite(RecipeDetailsViewModel viewModel)
        {
            await _recipeService.DeleteRecipeFromFavouriteAsync(viewModel, User.Id());

            return RedirectToAction(nameof(Index), new { favourite = true });
        }
    }
}

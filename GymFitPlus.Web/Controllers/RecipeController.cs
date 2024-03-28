using GymFitPlus.Core.Contracts;
using GymFitPlus.Core.ViewModels.RecipeViewModels;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Index([FromQuery] AllRecipesQueryModel query)
        {
            IEnumerable<RecipesAllViewModel> model = await _recipeService.AllRecipesAsync(query);

            ViewBag.Query = query;

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id);

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
            RecipeDetailsViewModel model = await _recipeService.FindRecipeByIdAsync(id);

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

        [HttpGet]
        public async Task<IActionResult> AllFavoriteRecipes()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> AddToFavorite(RecipeDetailsViewModel viewModel)
        {
            return View();
        }
    }
}

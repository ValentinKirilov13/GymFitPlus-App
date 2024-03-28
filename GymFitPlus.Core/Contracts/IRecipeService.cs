using GymFitPlus.Core.ViewModels.RecipeViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipesAllViewModel>> AllRecipesAsync(AllRecipesQueryModel query);
        Task<RecipeDetailsViewModel> FindRecipeByIdAsync(int id);
        Task<bool> AddRecipeAsync(RecipeDetailsViewModel viewModel);
        Task<bool> EditRecipeAsync(RecipeDetailsViewModel viewModel);
        Task<bool> DeleteRecipeAsync(int id);
    }
}
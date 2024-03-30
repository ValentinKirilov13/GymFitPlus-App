using GymFitPlus.Core.ViewModels.RecipeViewModels;

namespace GymFitPlus.Core.Contracts
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipesAllViewModel>> AllRecipesAsync(AllRecipesQueryModel query, bool favourite, Guid userId);
        Task<RecipeDetailsViewModel> FindRecipeByIdAsync(int id, bool favourite, Guid userId);
        Task<bool> AddRecipeAsync(RecipeDetailsViewModel viewModel);
        Task<bool> EditRecipeAsync(RecipeDetailsViewModel viewModel);
        Task<bool> DeleteRecipeAsync(int id);
        Task<bool> AddRecipeToFavouriteAsync(RecipeDetailsViewModel viewModel, Guid userId);
        Task<bool> EditFavouriteRecipeAsync(RecipeDetailsViewModel viewModel, Guid userId);
    }
}
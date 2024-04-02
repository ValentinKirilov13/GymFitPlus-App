using GymFitPlus.Infrastructure.Enums;

namespace GymFitPlus.Core.ViewModels.RecipeViewModels
{
    public class AllRecipesQueryModel
    {
        public int CurrentPage { get; set; } = 1;

        public int RecipesPerPage { get; set; } = 3;

        public RecipeType Category { get; set; }

        public string SearchTerm { get; set; } = string.Empty;

        public Sorting Sorting { get; set; }

        public int TotalRecipesCount { get; set; }

        public bool IsFavourite { get; set; }
    }
}

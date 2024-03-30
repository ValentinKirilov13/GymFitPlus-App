using GymFitPlus.Core.ViewModels.RecipeViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class SearchRecipesComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AllRecipesQueryModel query)
        {
            return await Task.FromResult<IViewComponentResult>(View(query));
        }
    }
}

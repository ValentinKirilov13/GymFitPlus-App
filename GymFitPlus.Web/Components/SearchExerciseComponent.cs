using GymFitPlus.Core.ViewModels.ExerciseViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class SearchExerciseComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AllExercisesQueryModel model)
        {
            return await Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}

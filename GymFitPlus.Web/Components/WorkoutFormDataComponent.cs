
using GymFitPlus.Core.ViewModels.WorkoutViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class WorkoutFormDataComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(WorkoutDetailViewModel viewModel)
        {
            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    } 
}

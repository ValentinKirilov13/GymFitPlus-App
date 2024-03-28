using GymFitPlus.Core.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class RegisterComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new RegisterViewModel();

            return await Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}

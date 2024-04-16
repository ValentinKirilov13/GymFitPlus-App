using GymFitPlus.Core.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class LoginComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(LoginViewModel viewModel)
        {
            viewModel ??= new LoginViewModel();

            return await Task.FromResult<IViewComponentResult>(View(viewModel));
        }
    }
}

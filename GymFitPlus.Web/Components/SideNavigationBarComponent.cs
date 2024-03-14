using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Components
{
    public class SideNavigationBarComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult<IViewComponentResult>(View());
        }
    }
}

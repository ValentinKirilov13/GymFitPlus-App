using GymFitPlus.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GymFitPlus.Web.Attributes
{
    public class UserIsAuthenticatedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity != null && context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToActionResult(nameof(AccountController.Dashboard), "Account",null);
            }

            base.OnActionExecuting(context);
        }
    }
}

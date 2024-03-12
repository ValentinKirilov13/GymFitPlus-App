using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymFitPlus.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGetAsync()
        {
            return RedirectToAction("LogInSignUp", "Account");
        }
    }
}

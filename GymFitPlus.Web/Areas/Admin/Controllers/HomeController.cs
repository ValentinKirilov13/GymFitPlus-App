using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

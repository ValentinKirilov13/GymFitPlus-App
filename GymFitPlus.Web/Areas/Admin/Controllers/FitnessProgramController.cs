using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class FitnessProgramController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

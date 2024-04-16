using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public HomeController()
        {               
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

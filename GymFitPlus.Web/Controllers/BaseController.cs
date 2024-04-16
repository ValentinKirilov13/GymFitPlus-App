using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymFitPlus.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {       
    }
}

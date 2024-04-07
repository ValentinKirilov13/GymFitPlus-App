using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static GymFitPlus.Infrastructure.Constants.DataConstants.RoleConstants;
using static GymFitPlus.Infrastructure.Constants.DataConstants.AreaConstants;

namespace GymFitPlus.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Roles = AdminRole)]
    public class AdminBaseController : Controller
    { }
}

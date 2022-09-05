using Microsoft.AspNetCore.Mvc;
using PharmApp.UI.Helpers;

namespace PharmApp.UI.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
    public class BaseController : Controller
    {
    }
}

using Microsoft.AspNetCore.Mvc;
using PharmApp.UI.Helpers;

namespace PharmApp.UI.Areas.User.Controllers
{
    [CustomAuthorize(Roles = "User")]
    [Area("User")]
    public class BaseController : Controller
    {
        
    }
}

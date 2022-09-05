using Microsoft.AspNetCore.Mvc;
using PharmApp.UI.Helpers;

namespace PharmApp.UI.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

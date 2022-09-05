using Microsoft.AspNetCore.Mvc;

namespace PharmApp.UI.Areas.User.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

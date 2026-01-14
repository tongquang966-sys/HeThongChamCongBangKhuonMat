using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        // ================== TRANG CHẤM CÔNG ==================
        public IActionResult Index()
        {
            return View();
        }
    }
}

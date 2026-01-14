using Microsoft.AspNetCore.Mvc;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaceTestController : Controller
    {
        private readonly AiFaceService _ai;

        public FaceTestController(AiFaceService ai)
        {
            _ai = ai;
        }

        // ================== UPLOAD FORM ==================
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        // ================== HANDLE UPLOAD ==================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Vui lòng chọn ảnh khuôn mặt");
                return View();
            }

            var embedding = await _ai.EncodeFaceAsync(file);

            ViewBag.Length = embedding.Length;
            return View("Result");
        }
    }
}

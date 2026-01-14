using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        // ===== GLOBAL SEARCH (ADMIN) =====
        [HttpGet]
        public async Task<IActionResult> Global(string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return Json(new List<object>());

            q = q.Trim();

            // ================== NHÂN VIÊN ==================
            var nhanViens = await _context.NhanViens
                .Where(x =>
                    x.HoTen.Contains(q) ||
                    x.Email.Contains(q))
                .Select(x => new
                {
                    type = "NhanVien",
                    label = "👤 " + x.HoTen,
                    sub = x.Email,
                    url = "/Admin/NhanVien/Details/" + x.Id
                })
                .Take(5)
                .ToListAsync();

            // ================== CHẤM CÔNG ==================
            var chamCongs = await _context.LichSuChamCongs
                .Include(x => x.NhanVien)
                .Where(x =>
                    x.MaCa.Contains(q) ||
                    x.TrangThai.Contains(q) ||
                    x.NhanVien!.HoTen.Contains(q) ||
                    x.Ngay.ToString().Contains(q)
                )
                .OrderByDescending(x => x.Ngay)
                .Select(x => new
                {
                    type = "ChamCong",
                    label = "📅 " + x.NhanVien!.HoTen,
                    sub = x.Ngay.ToString("dd/MM/yyyy") + " | Ca " + x.MaCa,
                    url = "/Admin/NhanVien/Details/" + x.NhanVienId + "#chamcong"
                })
                .Take(5)
                .ToListAsync();

            var result = nhanViens
                .Concat(chamCongs)
                .Take(10);

            return Json(result);
        }
    }
}

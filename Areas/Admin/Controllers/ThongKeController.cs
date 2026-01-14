using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ThongKeController : Controller
    {
        private readonly AppDbContext _context;

        public ThongKeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var lichSu = _context.LichSuChamCongs
                .Include(x => x.NhanVien)
                .OrderByDescending(x => x.Ngay)
                .ToList();

            var data = lichSu.Select(x => new BangChamCongVM
            {
                HoTen = x.NhanVien != null ? x.NhanVien.HoTen : "Không xác định",
                NgayCong = x.Ngay,
                GioVao = x.GioVao ?? TimeSpan.Zero,
                GioRa = x.GioRa,
                CaLam = x.TenCa,
                TrangThai = MapTrangThai(x)
            }).ToList();

            // ✅ TRỎ ĐÚNG VIEW TRONG ADMIN
            return View("~/Areas/Admin/Views/ChamCong/LichSu.cshtml", data);
        }

        private static string MapTrangThai(LichSuChamCong x)
        {
            if (x.TrangThai == "DuCong")
                return "Đủ công";

            if (x.TrangThai == "NuaCong")
                return "Nửa công";

            if (x.DiTre)
                return "Đi trễ";

            if (x.VeSom)
                return "Về sớm";

            return "Chưa chấm công";
        }
    }
}

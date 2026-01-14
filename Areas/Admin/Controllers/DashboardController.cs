using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? month, int? year, string? ca)
        {
            int m = month ?? DateTime.Now.Month;
            int y = year ?? DateTime.Now.Year;
            var today = DateTime.Today;

            // ===== Lấy dữ liệu chấm công trong tháng/năm đã chọn =====
            var chamCong = _context.LichSuChamCongs
                .Include(x => x.NhanVien)
                .Where(x => x.Ngay.Month == m && x.Ngay.Year == y);

            if (!string.IsNullOrEmpty(ca))
                chamCong = chamCong.Where(x => x.MaCa == ca);

            var list = chamCong.ToList();

            // ===== Tạo DashboardVM =====
            var vm = new DashboardVM
            {
                TongNhanVien = _context.NhanViens.Count(),

                DiTre = list.Count(x => x.DiTre),
                DungGio = list.Count(x => !x.DiTre),

                CaSang = list.Count(x => x.MaCa == "SANG"),
                CaChieu = list.Count(x => x.MaCa == "CHIEU"),
                CaToi = list.Count(x => x.MaCa == "TOI")
            };

            // ===== BIỂU ĐỒ THEO NGÀY =====
            var groupDay = list.GroupBy(x => x.Ngay.Day).OrderBy(g => g.Key);
            foreach (var g in groupDay)
            {
                vm.NgayLabels.Add(g.Key.ToString());
                vm.SoLuotChamCong.Add(g.Count());
            }

            // ===== TOP NHÂN VIÊN ĐÚNG GIỜ =====
            vm.TopNhanViens = list
                .Where(x => !x.DiTre)
                .GroupBy(x => x.NhanVien!.HoTen)
                .Select(g => new TopNhanVienVM
                {
                    HoTen = g.Key,
                    SoLanDungGio = g.Count()
                })
                .OrderByDescending(x => x.SoLanDungGio)
                .Take(5)
                .ToList();

            // ===== CẢNH BÁO ĐI TRỄ =====
            vm.CanhBaoDiTre = list
                .Where(x => x.DiTre)
                .GroupBy(x => x.NhanVien!.HoTen)
                .Where(g => g.Count() >= 3)
                .Select(g => $"{g.Key} đi trễ {g.Count()} lần")
                .ToList();

            // ===== NHÂN VIÊN CHƯA CHẤM CÔNG HÔM NAY =====
            var daCham = _context.LichSuChamCongs
                .Where(x => x.Ngay == today)
                .Select(x => x.NhanVienId)
                .ToList();

            vm.ChuaChamCong = _context.NhanViens
                .Include(nv => nv.CaLamViec)
                .Where(nv => !daCham.Contains(nv.Id))
                .ToList();  // ✅ Lấy trực tiếp NhanVien

            ViewBag.Month = m;
            ViewBag.Year = y;
            ViewBag.Ca = ca;

            return View(vm);
        }
    }
}

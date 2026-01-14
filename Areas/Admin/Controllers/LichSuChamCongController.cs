using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.ViewModels;
using ClosedXML.Excel;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LichSuChamCongController : Controller
    {
        private readonly AppDbContext _context;

        public LichSuChamCongController(AppDbContext context)
        {
            _context = context;
        }

        // ===== LỊCH SỬ CHẤM CÔNG =====
        public IActionResult Index()
        {
            var data = (
                from ls in _context.LichSuChamCongs
                join nv in _context.NhanViens
                    on ls.NhanVienId equals nv.Id
                orderby ls.ThoiGianChamCong descending
                select new BangChamCongVM
                {
                    MaNV = nv.Id,
                    HoTen = nv.HoTen,
                    NgayCong = ls.Ngay,
                    GioVao = ls.GioVao ?? TimeSpan.Zero,
                    GioRa = ls.GioRa ?? TimeSpan.Zero,
                    TrangThai = ls.TrangThai,
                    CaLam = ls.MaCa
                }
            ).ToList();

            return View(data);
        }

        // ===== EXPORT EXCEL =====
        [HttpGet]
        public IActionResult ExportExcel()
        {
            var data = _context.LichSuChamCongs
                .Include(x => x.NhanVien)
                .OrderByDescending(x => x.Ngay)
                .ToList();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Lịch sử chấm công");

            ws.Cell(1, 1).Value = "STT";
            ws.Cell(1, 2).Value = "Nhân viên";
            ws.Cell(1, 3).Value = "Ngày";
            ws.Cell(1, 4).Value = "Giờ vào";
            ws.Cell(1, 5).Value = "Giờ ra";
            ws.Cell(1, 6).Value = "Ca làm";
            ws.Cell(1, 7).Value = "Trạng thái";

            ws.Range("A1:G1").Style.Font.Bold = true;

            int row = 2, stt = 1;
            foreach (var item in data)
            {
                ws.Cell(row, 1).Value = stt++;
                ws.Cell(row, 2).Value = item.NhanVien?.HoTen;
                ws.Cell(row, 3).Value = item.Ngay.ToString("dd/MM/yyyy");
                ws.Cell(row, 4).Value = item.GioVao?.ToString(@"hh\:mm");
                ws.Cell(row, 5).Value = item.GioRa?.ToString(@"hh\:mm");
                ws.Cell(row, 6).Value = item.MaCa;
                ws.Cell(row, 7).Value = item.TrangThai;
                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);

            return File(
                stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"LichSuChamCong_{DateTime.Now:yyyyMMddHHmm}.xlsx"
            );
        }
    }
}

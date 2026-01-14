using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.User.Controllers
{
    [Area("User")]
    public class ChamCongController : Controller
    {
        private readonly FaceRecognitionService _faceService;
        private readonly AppDbContext _context;

        public ChamCongController(
            FaceRecognitionService faceService,
            AppDbContext context)
        {
            _faceService = faceService;
            _context = context;
        }

        // ===== TRANG QUÉT MẶT =====
        [HttpGet]
        public IActionResult QuetMat() => View();

        // ===== NHẬN ẢNH BASE64 =====
        [HttpPost]
        [RequestSizeLimit(10_000_000)]
        public async Task<IActionResult> QuetMatBase64([FromBody] ImageBase64Dto dto)
        {
            if (string.IsNullOrEmpty(dto.ImageBase64))
                return Json(new { success = false, message = "Không nhận được ảnh" });

            var base64Data = dto.ImageBase64.Split(',')[1];
            var imageBytes = Convert.FromBase64String(base64Data);

            var aiResult = await _faceService.RecognizeAsync(imageBytes);

            if (aiResult == null || !aiResult.success || string.IsNullOrEmpty(aiResult.employeeId))
            {
                return Json(new
                {
                    success = false,
                    message = aiResult?.message ?? "❌ Không nhận diện được khuôn mặt"
                });
            }

            if (!int.TryParse(aiResult.employeeId, out int empId))
                return Json(new { success = false, message = "❌ ID nhân viên không hợp lệ" });

            var nv = await _context.NhanViens
                .Include(x => x.CaLamViec)
                .FirstOrDefaultAsync(x => x.Id == empId);

            if (nv == null)
                return Json(new { success = false, message = "❌ Không tìm thấy nhân viên" });

            if (nv.CaLamViec == null)
                return Json(new { success = false, message = "⚠️ Nhân viên chưa được gán ca làm" });

            var ca = nv.CaLamViec;
            var today = DateTime.Today;
            var now = DateTime.Now.TimeOfDay;

            var record = await _context.LichSuChamCongs
                .FirstOrDefaultAsync(x => x.NhanVienId == nv.Id && x.Ngay == today);

            // ===== GIỜ VÀO =====
            if (record == null)
            {
                var gioChoPhep = ca.GioBatDau.Add(TimeSpan.FromMinutes(ca.ChoPhepTrePhut));
                var soPhutTre = now > gioChoPhep
                    ? (int)(now - ca.GioBatDau).TotalMinutes
                    : 0;

                record = new LichSuChamCong
                {
                    NhanVienId = nv.Id,
                    Ngay = today,
                    GioVao = now,
                    ThoiGianChamCong = DateTime.Now,
                    MaCa = ca.TenCa,
                    TenCa = ca.TenCa,
                    DiTre = soPhutTre > 0,
                    SoPhutTre = soPhutTre,
                    TrangThai = "Đang làm"
                };

                _context.LichSuChamCongs.Add(record);
            }
            // ===== GIỜ RA =====
            else if (record.GioRa == null)
            {
                record.GioRa = now;
                record.ThoiGianChamCong = DateTime.Now;

                var gioChoVe = ca.GioKetThuc.Subtract(TimeSpan.FromMinutes(ca.ChoPhepSomPhut));
                var soPhutVeSom = now < gioChoVe
                    ? (int)(gioChoVe - now).TotalMinutes
                    : 0;

                record.VeSom = soPhutVeSom > 0;
                record.SoPhutVeSom = soPhutVeSom;

                var tongGioLam = (record.GioRa - record.GioVao)?.TotalHours ?? 0;

                record.TrangThai =
                    tongGioLam >= ca.SoGioCong ? "Đủ công" :
                    tongGioLam >= ca.SoGioCong / 2 ? "Nửa công" :
                    "Không đủ công";
            }
            else
            {
                return Json(new
                {
                    success = false,
                    message = $"Nhân viên {nv.HoTen} đã chấm công xong hôm nay"
                });
            }

            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = $"✅ {nv.HoTen} – Ca {ca.TenCa}",
                confidence = aiResult.confidence
            });
        }
    }

    public class ImageBase64Dto
    {
        public string ImageBase64 { get; set; } = string.Empty;
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly AppDbContext _context;
        private readonly AttendanceService _attendanceService;

        public NhanVienController(
            AppDbContext context,
            AttendanceService attendanceService)
        {
            _context = context;
            _attendanceService = attendanceService;
        }

        // ===================== DANH SÁCH =====================
        public async Task<IActionResult> Index()
        {
            var list = await _context.NhanViens.ToListAsync();
            return View(list);
        }

        // ===================== DETAILS =====================
        public async Task<IActionResult> Details(int id)
        {
            var nv = await _context.NhanViens
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nv == null)
                return NotFound();

            var lichSu = await _context.LichSuChamCongs
                .Where(x => x.NhanVienId == id)
                .OrderByDescending(x => x.Ngay)
                .ToListAsync();

            var vm = _attendanceService.BuildDetails(nv, lichSu);

            return View(vm);
        }

        // ===================== CREATE =====================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            NhanVien nv,
            IFormFile AnhKhuonMat)
        {
            if (!ModelState.IsValid)
                return View(nv);

            _context.NhanViens.Add(nv);
            await _context.SaveChangesAsync();

            if (AnhKhuonMat != null && AnhKhuonMat.Length > 0)
            {
                var webFolder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/nhanvien");

                if (!Directory.Exists(webFolder))
                    Directory.CreateDirectory(webFolder);

                var fileName = $"{nv.Id}.jpg";
                var webPath = Path.Combine(webFolder, fileName);

                using (var stream = new FileStream(webPath, FileMode.Create))
                {
                    await AnhKhuonMat.CopyToAsync(stream);
                }

                nv.AnhNhanVien = "/uploads/nhanvien/" + fileName;

                // ===== COPY SANG AI SERVER =====
                var aiFolder = Path.Combine(
                    @"D:\attendance-face\AI_Server\face_db",
                    nv.Id.ToString());

                if (!Directory.Exists(aiFolder))
                    Directory.CreateDirectory(aiFolder);

                System.IO.File.Copy(
                    webPath,
                    Path.Combine(aiFolder, "1.jpg"),
                    true);

                _context.NhanViens.Update(nv);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ===================== EDIT =====================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null)
                return NotFound();

            return View(nv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            NhanVien model,
            IFormFile? anhMoi)
        {
            if (id != model.Id)
                return NotFound();

            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null)
                return NotFound();

            nv.HoTen = model.HoTen;
            nv.Email = model.Email;
            nv.SoDienThoai = model.SoDienThoai;
            nv.ChucVu = model.ChucVu;

            if (anhMoi != null && anhMoi.Length > 0)
            {
                var uploads = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot/uploads/nhanvien");

                if (!Directory.Exists(uploads))
                    Directory.CreateDirectory(uploads);

                var fileName = $"{nv.Id}.jpg";
                var filePath = Path.Combine(uploads, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await anhMoi.CopyToAsync(stream);
                }

                nv.AnhNhanVien = "/uploads/nhanvien/" + fileName;
            }

            _context.Update(nv);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ===================== DELETE =====================
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv == null)
                return NotFound();

            return View(nv);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nv = await _context.NhanViens.FindAsync(id);
            if (nv != null)
            {
                _context.NhanViens.Remove(nv);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // ===================== GÁN CA =====================
        [HttpGet]
        public async Task<IActionResult> GanCa(int id)
        {
            var nv = await _context.NhanViens
                .Include(x => x.CaLamViec)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (nv == null) return NotFound();

            ViewBag.CaLamViecs = await _context.CaLamViecs
                .Where(x => x.IsActive)
                .ToListAsync();

            return View(nv);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult GanCa(int id, int CaLamViecId)
        {
            if (CaLamViecId == 0)
            {
                return RedirectToAction("GanCa", new { id });
            }

            var nv = _context.NhanViens.Find(id);
            if (nv == null) return NotFound();

            nv.CaLamViecId = CaLamViecId;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}

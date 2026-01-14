using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CaLamViecController : Controller
    {
        private readonly AppDbContext _context;

        public CaLamViecController(AppDbContext context)
        {
            _context = context;
        }

        // ======= PHẦN DƯỚI GIỮ NGUYÊN 100% =======


        // ================== LIST ==================
        public async Task<IActionResult> Index()
        {
            var data = await _context.CaLamViecs
                .OrderBy(x => x.TenCa)
                .ToListAsync();

            return View(data);
        }

        // ================== CREATE ==================
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CaLamViec model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.CaLamViecs.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================== EDIT ==================
        public async Task<IActionResult> Edit(int id)
        {
            var ca = await _context.CaLamViecs.FindAsync(id);
            if (ca == null) return NotFound();

            return View(ca);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CaLamViec model)
        {
            if (id != model.Id) return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            _context.Update(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // ================== DELETE ==================
        public async Task<IActionResult> Delete(int id)
        {
            var ca = await _context.CaLamViecs.FindAsync(id);
            if (ca == null) return NotFound();

            return View(ca);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ca = await _context.CaLamViecs.FindAsync(id);
            if (ca != null)
            {
                ca.IsActive = false; // ❗ không xoá cứng
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class ChamCongService
    {
        private readonly AppDbContext _context;

        public ChamCongService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<LichSuChamCong?> XuLyChamCongAsync(int nhanVienId)
        {
            var nv = await _context.NhanViens
                .FirstOrDefaultAsync(x => x.Id == nhanVienId);

            if (nv == null) return null;

            var now = DateTime.Now;
            var today = now.Date;

            var gioBatDau = today.AddHours(8);
            var gioKetThuc = today.AddHours(17);

            var lichSu = await _context.LichSuChamCongs
                .FirstOrDefaultAsync(x =>
                    x.NhanVienId == nhanVienId &&
                    x.Ngay == today);

            if (lichSu == null)
            {
                lichSu = new LichSuChamCong
                {
                    NhanVienId = nhanVienId,
                    Ngay = today,
                    GioVao = now.TimeOfDay,
                    ThoiGianChamCong = now,
                    MaCa = "HC",
                    TenCa = "Hành chính"
                };

                if (now > gioBatDau)
                {
                    lichSu.DiTre = true;
                    lichSu.SoPhutTre = (int)(now - gioBatDau).TotalMinutes;
                    lichSu.TrangThai = "Đi trễ";
                }
                else
                {
                    lichSu.TrangThai = "Đủ công";
                }

                _context.LichSuChamCongs.Add(lichSu);
            }
            else if (lichSu.GioRa == null)
            {
                lichSu.GioRa = now.TimeOfDay;
                lichSu.ThoiGianChamCong = now;

                if (now < gioKetThuc)
                {
                    lichSu.VeSom = true;
                    lichSu.SoPhutVeSom = (int)(gioKetThuc - now).TotalMinutes;
                    lichSu.TrangThai = "Về sớm";
                }
            }

            await _context.SaveChangesAsync();
            return lichSu;
        }
    }
}

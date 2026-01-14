using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AttendanceService
    {
        public NhanVienDetailsVM BuildDetails(
            NhanVien nv,
            List<LichSuChamCong> lichSu)
        {
            var vm = new NhanVienDetailsVM
            {
                NhanVien = nv,
                LichSuChamCong = lichSu
            };

            TimeSpan gioBatDau = new TimeSpan(8, 0, 0);
            TimeSpan gioKetThuc = new TimeSpan(17, 0, 0);
            double gioCongChuan = 8;

            foreach (var cc in lichSu)
            {
                if (!cc.GioVao.HasValue || !cc.GioRa.HasValue)
                    continue;

                var gioLam = (cc.GioRa.Value - cc.GioVao.Value).TotalHours;

                vm.TongGioLam += gioLam;
                vm.TongNgayLam++;

                if (cc.GioVao.Value > gioBatDau.Add(TimeSpan.FromMinutes(15)))
                    vm.SoNgayDiMuon++;

                if (cc.GioRa.Value < gioKetThuc)
                    vm.SoNgayVeSom++;

                if (gioLam < gioCongChuan)
                    vm.SoNgayKhongDuCong++;
            }

            // Giả sử 22 ngày công / tháng
            vm.SoNgayNghi = Math.Max(0, 22 - vm.TongNgayLam);

            return vm;
        }
    }
}

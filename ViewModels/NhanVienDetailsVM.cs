using WebApp.Models;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class NhanVienDetailsVM
    {
        public NhanVien NhanVien { get; set; } = null!;

        public List<LichSuChamCong> LichSuChamCong { get; set; }
            = new List<LichSuChamCong>();

        public double TongGioLam { get; set; }
        public int TongNgayLam { get; set; }

        public int SoNgayDiMuon { get; set; }
        public int SoNgayVeSom { get; set; }
        public int SoNgayKhongDuCong { get; set; }
        public int SoNgayNghi { get; set; }
    }
}

using WebApp.Models;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class DashboardVM
    {
        // ===== THỐNG KÊ TỔNG =====
        public int TongNhanVien { get; set; }
        public int DiTre { get; set; }
        public int DungGio { get; set; }

        // ===== THEO CA =====
        public int CaSang { get; set; }
        public int CaChieu { get; set; }
        public int CaToi { get; set; }

        // ===== BIỂU ĐỒ =====
        public List<string> NgayLabels { get; set; } = new();
        public List<int> SoLuotChamCong { get; set; } = new();

        // ===== TOP NHÂN VIÊN =====
        public List<TopNhanVienVM> TopNhanViens { get; set; } = new();

        // ===== CẢNH BÁO =====
        public List<string> CanhBaoDiTre { get; set; } = new();

        // ===== NHÂN VIÊN CHƯA CHẤM CÔNG =====
        public List<NhanVien> ChuaChamCong { get; set; } = new();
    }

    public class TopNhanVienVM
    {
        public string HoTen { get; set; } = string.Empty;
        public int SoLanDungGio { get; set; }
    }
}

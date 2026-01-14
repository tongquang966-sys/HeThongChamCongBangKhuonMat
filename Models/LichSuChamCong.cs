using System;

namespace WebApp.Models
{
    public class LichSuChamCong
    {
        public int Id { get; set; }

        // ===== NHÂN VIÊN =====
        public int NhanVienId { get; set; }
        public NhanVien? NhanVien { get; set; }

        // ===== NGÀY =====
        public DateTime Ngay { get; set; }

        // ===== GIỜ THỰC TẾ =====
        public TimeSpan? GioVao { get; set; }
        public TimeSpan? GioRa { get; set; }

        public DateTime ThoiGianChamCong { get; set; }

        // ===== CA LÀM =====
        public string MaCa { get; set; } = "HC";   // 🔥 QUAN TRỌNG
        public string TenCa { get; set; } = "Hành chính";

        // ===== TRẠNG THÁI =====
        public string TrangThai { get; set; } = "DuCong";
        // DuCong | NuaCong | Nghi | ChuaChamCong

        // ===== PHÂN TÍCH CÔNG =====
        public bool DiTre { get; set; }
        public bool VeSom { get; set; }

        public int SoPhutTre { get; set; }
        public int SoPhutVeSom { get; set; }

        public string? GhiChu { get; set; }
    }
}

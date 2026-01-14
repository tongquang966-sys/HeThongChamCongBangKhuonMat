namespace WebApp.Models
{
    public class NhanVien
    {
        public int Id { get; set; }                  // Id cơ sở dữ liệu
        public string MaNV { get; set; } = string.Empty;  // Mã nhân viên hiển thị
        public string HoTen { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string SoDienThoai { get; set; } = string.Empty;
        public string ChucVu { get; set; } = string.Empty; // Dùng làm Phòng ban
        public string? ImagePath { get; set; }
        public string? AnhNhanVien { get; set; }

        // Ca làm việc
        public int? CaLamViecId { get; set; }
        public CaLamViec? CaLamViec { get; set; }

        // Face recognition
        public byte[]? FaceEmbedding { get; set; }

        // Trạng thái dashboard
        public bool DaChamCongHienTai { get; set; } = false; // đã chấm công hôm nay
        public int SoLanDungGio { get; set; } = 0;          // thống kê đúng giờ
        public int SoLanDiTre { get; set; } = 0;            // thống kê đi trễ

        // ===== Thêm alias để View dễ dùng =====
        public string PhongBan => ChucVu;                   // alias
        public string CaLam => CaLamViec?.TenCa ?? "";      // alias từ navigation
    }
}

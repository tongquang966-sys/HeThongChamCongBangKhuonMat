namespace WebApp.ViewModels
{
    public class BangChamCongVM
    {
        public int MaNV { get; set; }
        public string HoTen { get; set; } = string.Empty;

        public DateTime NgayCong { get; set; }

        public TimeSpan GioVao { get; set; }
        public TimeSpan? GioRa { get; set; }   // 👈 PHẢI nullable

        public string TrangThai { get; set; } = string.Empty;
        public string CaLam { get; set; } = string.Empty;
    }
}

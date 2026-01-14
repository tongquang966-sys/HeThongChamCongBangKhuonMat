namespace WebApp.Models
{
    public class CaLamViec
    {
        public int Id { get; set; }

        public string TenCa { get; set; } = null!; // HC, Ca Sáng, Ca Tối

        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }

        public double SoGioCong { get; set; } // 8, 4, 10...

        public int ChoPhepTrePhut { get; set; } = 15;
        public int ChoPhepSomPhut { get; set; } = 0;

        public bool IsActive { get; set; } = true;
    }
}

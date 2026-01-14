using WebApp.Models;

namespace WebApp.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.CaLamViecs.Any()) return;

            context.CaLamViecs.AddRange(
                new CaLamViec
                {
                    TenCa = "Hành chính",
                    GioBatDau = new TimeSpan(8, 0, 0),
                    GioKetThuc = new TimeSpan(17, 0, 0),
                    SoGioCong = 8
                },
                new CaLamViec
                {
                    TenCa = "Ca sáng",
                    GioBatDau = new TimeSpan(7, 0, 0),
                    GioKetThuc = new TimeSpan(11, 0, 0),
                    SoGioCong = 4
                },
                new CaLamViec
                {
                    TenCa = "Ca tối",
                    GioBatDau = new TimeSpan(18, 0, 0),
                    GioKetThuc = new TimeSpan(22, 0, 0),
                    SoGioCong = 4
                }
            );

            context.SaveChanges();
        }
    }
}

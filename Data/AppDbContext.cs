using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<LichSuChamCong> LichSuChamCongs { get; set; }
        public DbSet<CaLamViec> CaLamViecs { get; set; }
         

    }
    
}

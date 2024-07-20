using Microsoft.EntityFrameworkCore;

namespace Demo_buoi4.Models
{
    public class SanPhamDbContext: DbContext
    {
        public SanPhamDbContext(DbContextOptions<SanPhamDbContext> options) : base(options) 
        {
        }

        public DbSet<SanPham> SanPham { get; set;}
    }
}

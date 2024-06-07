
using Microsoft.EntityFrameworkCore;
using WebAPI_DoAn.Models.Domain;
namespace WebAPI_DoAn.Data
{
    public class ShopDbContext :DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
        public DbSet<shop> shops { get; set; }
        public DbSet<NhanVien> nhanViens { get; set; }
        public DbSet<NhanVienShop> nhanVienShops { get; set; }
        public DbSet<LoaiSanPham> loaiSanPhams { get; set; }

    }
}

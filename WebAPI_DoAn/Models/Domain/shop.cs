using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_DoAn.Models.Domain
{
    public class shop
    {
        [Key]
        public int Idsp { get; set; }
        public string? TenSP { get; set; }
        public string? ImageUrl { get; set; }
        public int GiaTien { get; set; }
        public string? MoTa { get; set; }
        public bool IsTrendingProduct { get; set; }
        public LoaiSanPham? loaiSanPham { get; set; }
        public int? IdLoaiSP { get; set; }
        public List<NhanVienShop> nhanvienshop { get; set; }

    }
}

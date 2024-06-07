using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_DoAn.Models.Domain
{
    public class NhanVien
    {
        [Key]
        public int IdNV { get; set; }
        public string? ImgNV { get; set; }
        public string? TenNV {get; set;}
        public string? Email { get; set;}
        public string? SDT { get; set;}
        public string? DiaChi { get; set;}
        public List<NhanVienShop> nhanVienShops { get; set; }
    }
}

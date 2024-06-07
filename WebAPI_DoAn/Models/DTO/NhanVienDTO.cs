using WebAPI_DoAn.Models.Domain;

namespace WebAPI_DoAn.Models.DTO
{
    public class NhanVienDTO
    {
        public int Id { get; set; }
        public string? ImgNV { get; set; }
        public string? TenNV { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }

    }
    public class NhanVienNoIdDTO
    {
        public string? ImgNV { get; set; }
        public string? TenNV { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }
    }
    public class NhanVienWithShopAndLoaiSPDTO
            {
                public string? ImgNV { get; set; }
                public string? TenNV { get; set; }
                public string? Email { get; set; }
                public string? SDT { get; set; }
                public string? DiaChi { get; set; }
        public List<NhanVienSHop> NhanVienSHop { set; get; }
            }
    public class NhanVienSHop
    {
        public string TenSP { get; set; }
        public List<string> NhanVienShop { get; set; }
    }
}

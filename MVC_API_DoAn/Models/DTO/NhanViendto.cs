namespace MVC_API_DoAn.Models.DTO
{
    public class NhanViendto
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
}

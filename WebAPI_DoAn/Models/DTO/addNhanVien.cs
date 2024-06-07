using System.ComponentModel.DataAnnotations;
using WebAPI_DoAn.Models.Domain;

namespace WebAPI_DoAn.Models.DTO
{
    public class addNhanVien
    {
        [Required]
        [MinLength(2)]
        public string? ImgNV { get; set; }
        public string? TenNV { get; set; }
        public string? Email { get; set; }
        public string? SDT { get; set; }
        public string? DiaChi { get; set; }
    }
}

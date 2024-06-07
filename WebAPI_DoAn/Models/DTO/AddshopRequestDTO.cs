using System.ComponentModel.DataAnnotations;

namespace WebAPI_DoAn.Models.DTO
{
    public class AddshopRequestDTO
    {
        [Required]
        [MinLength(2)]
        public string? TenSP { get; set; }
        public string? ImageUrl { get; set; }
        public int GiaTien { get; set; }
        public string? MoTa { get; set; }
        public bool IsTrendingProduct { get; set; }
    }
}

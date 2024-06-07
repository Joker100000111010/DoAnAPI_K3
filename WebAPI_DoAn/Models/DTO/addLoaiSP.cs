using System.ComponentModel.DataAnnotations;
using WebAPI_DoAn.Models.Domain;

namespace WebAPI_DoAn.Models.DTO
{
    public class addLoaiSP
    {
        [Required]
        [MinLength(2)]
        public string? LoaiSP { get; set; }
        public string? ImgLSP { get; set; }
    }
}

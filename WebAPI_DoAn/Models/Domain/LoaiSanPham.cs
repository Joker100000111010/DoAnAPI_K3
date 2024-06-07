using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI_DoAn.Models.Domain
{
    public class LoaiSanPham
    {
        [Key]
        public int IdLoaiSP { get; set; }
        public string LoaiSP { get; set;}
        public string? ImgLSP { get; set;}
        public List<shop>? shops { get; set; }
    }
}

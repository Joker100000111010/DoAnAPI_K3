using System.ComponentModel.DataAnnotations;

namespace WebAPI_DoAn.Models.Domain
{
    public class NhanVienShop
    {
        [Key]
        public int NhanVienShopId { get; set; }
        public shop? shop { get; set; }
        public int ? Idsp { get; set; }
        public NhanVien? NhanVien { get; set; }
        public int? IdNV { get; set; }
    }
}

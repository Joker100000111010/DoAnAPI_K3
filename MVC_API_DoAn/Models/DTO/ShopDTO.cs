namespace MVC_API_DoAn.Models.DTO
{
    public class ShopDTO
    {
        public int Id { get; set; }
        public string? TenSP { get; set; }
        public string? ImageUrl { get; set; }
        public int GiaTien { get; set; }
        public string? MoTa { get; set; }
        public bool IsTrendingProduct { get; set; }
    }
}
    
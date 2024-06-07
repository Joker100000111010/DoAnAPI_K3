using WebAPI_DoAn.Models.Domain;
using WebAPI_DoAn.Models.DTO;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebAPI_DoAn.Models.Repositories
{
    public interface IShopRepository
    {
        List<ShopWithAuthorAndPublisherDTO> GetAllshop(string? filterOn = null, string?
          filterQuery = null, string? sortBy = null,
         bool isAscending = true, int pageNumber = 1, int pageSize = 1000);
        ShopWithAuthorAndPublisherDTO GetshopById(int id);
        AddshopRequestDTO AddShop(AddshopRequestDTO addShopRequestDTO);
        AddshopRequestDTO? UpdateShopById(int id, AddshopRequestDTO ShopDTO);
        shop? DeleteShopById(int id);
        IEnumerable<ShopWithAuthorAndPublisherDTO> SearchProducts(string searchQuery);
    }
}

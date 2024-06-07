using static System.Reflection.Metadata.BlobBuilder;
using System;
using WebAPI_DoAn.Data;
using WebAPI_DoAn.Models.Domain;
using WebAPI_DoAn.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_DoAn.Models.Repositories
{
    public class SQLShopRepository : IShopRepository
    {
        private readonly ShopDbContext _dbContext;
        public SQLShopRepository(ShopDbContext dpContext)
        {
            _dbContext = dpContext;
        }
        public List<ShopWithAuthorAndPublisherDTO> GetAllshop(string? filterOn = null, string?
        filterQuery = null,
         string? sortBy = null, bool isAscending = true, int pageNumber = 1, int
        pageSize = 1000)
        {
            var allBooks = _dbContext.shops.Select(shop => new
           ShopWithAuthorAndPublisherDTO()
            {
                Id = shop.Idsp,
                TenSP = shop.TenSP,
                ImageUrl = shop.ImageUrl,
                GiaTien = shop.GiaTien,
                MoTa = shop.MoTa,
                IsTrendingProduct = shop.IsTrendingProduct,
            }).AsQueryable();
            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false &&
           string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = allBooks.Where(x => x.TenSP.Contains(filterQuery));
                }
            }
            //sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = isAscending ? allBooks.OrderBy(x => x.TenSP) :
                   allBooks.OrderByDescending(x => x.TenSP);
                }
            }
            //pagination
            var skipResults = (pageNumber - 1) * pageSize;
            return allBooks.Skip(skipResults).Take(pageSize).ToList();
        }
        public ShopWithAuthorAndPublisherDTO GetshopById(int id)
        {
            var bookWithDomain = _dbContext.shops.Where(n => n.Idsp == id);
            //Map Domain Model to DTOs
            var bookWithIdDTO = bookWithDomain.Select(shop => new ShopWithAuthorAndPublisherDTO()
            {
                Id = shop.Idsp,
                TenSP = shop.TenSP,
                ImageUrl = shop.ImageUrl,
                GiaTien = shop.GiaTien,
                MoTa = shop.MoTa,
                IsTrendingProduct = shop.IsTrendingProduct,
            }).FirstOrDefault();
            return bookWithIdDTO;
        }

        public AddshopRequestDTO AddShop(AddshopRequestDTO addshopRequestDTO)
        {
            var shopDomainModel = new shop
            {
                TenSP = addshopRequestDTO.TenSP,
                ImageUrl = addshopRequestDTO.ImageUrl,
                GiaTien = addshopRequestDTO.GiaTien,
                MoTa = addshopRequestDTO.MoTa,
                IsTrendingProduct = addshopRequestDTO.IsTrendingProduct,
            };
            _dbContext.shops.Add(shopDomainModel);
            _dbContext.SaveChanges();
            /*foreach (var id in shopDomainModel.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BooksId = shopDomainModel.BooksId,
                    AuthorsId = id
                };
                _dbContext.Books_Author.Add(_book_author);
                _dbContext.SaveChanges();
            } */
            return addshopRequestDTO;
        }

        public AddshopRequestDTO? UpdateShopById(int id, AddshopRequestDTO ShopDTO)
        {
            // Tìm bản ghi hiện có trong cơ sở dữ liệu
            var shopDomain = _dbContext.shops.FirstOrDefault(n => n.Idsp == id);

            if (shopDomain == null)
            {
                // Trường hợp không tìm thấy bản ghi với id tương ứng
                return null;
            }

            // Cập nhật các thuộc tính của bản ghi hiện có
            shopDomain.TenSP = ShopDTO.TenSP;
            shopDomain.ImageUrl = ShopDTO.ImageUrl;
            shopDomain.GiaTien = ShopDTO.GiaTien;
            shopDomain.MoTa = ShopDTO.MoTa;
            shopDomain.IsTrendingProduct = ShopDTO.IsTrendingProduct;

            // Debug: Kiểm tra giá trị trước khi lưu
            Console.WriteLine($"Updated shop: {shopDomain.TenSP}, {shopDomain.ImageUrl}, {shopDomain.GiaTien}, {shopDomain.MoTa}, {shopDomain.IsTrendingProduct}");

            // Lưu các thay đổi
            _dbContext.SaveChanges();

            return ShopDTO;
        }

       /* public AddshopRequestDTO? UpdateShopById(int id, AddshopRequestDTO ShopDTO)
        {
            var shopDomain = _dbContext.shops.FirstOrDefault(n => n.Idsp == id);
            if (shopDomain != null)
            {
                shopDomain.TenSP = ShopDTO.TenSP;
                shopDomain.ImageUrl = ShopDTO.ImageUrl;
                shopDomain.GiaTien = ShopDTO.GiaTien;
                shopDomain.MoTa = ShopDTO.MoTa;
                shopDomain.IsTrendingProduct = ShopDTO.IsTrendingProduct;
            }
            var existingAuthors = _dbContext.shops.Where(a => a.Idsp == id).ToList();
            _dbContext.shops.RemoveRange(existingAuthors);

            /*     foreach (var authorId in bookDTO.AuthorIds)
                 {
                     var _book_author = new Book_Author
                     {
                         BooksId = id,
                         AuthorsId = authorId
                     };
                     _dbContext.Books_Author.Add(_book_author);
                 } 
            var _book_author = new shop
            {
                Idsp = id,
            };
            _dbContext.shops.Add(_book_author);
            _dbContext.SaveChanges();
            return ShopDTO;
        } */

        public shop? DeleteShopById(int id)
        {
            var bookDomain = _dbContext.shops.FirstOrDefault(n => n.Idsp == id);
            if (bookDomain != null)
            {
                _dbContext.shops.Remove(bookDomain);
                _dbContext.SaveChanges();
            }
            return bookDomain;
        }
        public IEnumerable<ShopWithAuthorAndPublisherDTO> SearchProducts(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                // Trả về tất cả sản phẩm nếu không có từ khóa tìm kiếm
                return _dbContext.shops.Select(shop => new ShopWithAuthorAndPublisherDTO
                {
                    Id = shop.Idsp,
                    TenSP = shop.TenSP,
                    GiaTien = shop.GiaTien,
                    ImageUrl = shop.ImageUrl
                    // Các thuộc tính khác của ShopDTO
                }).ToList();
            }

            return _dbContext.shops
                           .Where(p => p.TenSP.Contains(searchQuery) || p.MoTa.Contains(searchQuery))
                           .Select(shop => new ShopWithAuthorAndPublisherDTO
                           {
                               Id = shop.Idsp,
                               TenSP = shop.TenSP,
                               GiaTien = shop.GiaTien,
                               ImageUrl = shop.ImageUrl
                               // Các thuộc tính khác của ShopDTO
                           }).ToList();
        }


    }
}

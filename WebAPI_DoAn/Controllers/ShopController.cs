using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using WebAPI_DoAn.Data;
using WebAPI_DoAn.Models.DTO;
using WebAPI_DoAn.Models.Repositories;

namespace WebAPI_DoAn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ShopController : ControllerBase
    {
        private readonly ShopDbContext _dbContext;
        private readonly ILogger<ShopController> _logger;
        private readonly IShopRepository _shopRepository;

        public ShopController(ShopDbContext dbContext, IShopRepository shopRepository, ILogger<ShopController> logger)
        {
            _dbContext = dbContext;
            _shopRepository = shopRepository;
            _logger = logger;
        }

        [HttpGet("get-all-books")]
        [Authorize(Roles = "Read,Write")]
        // [Authorize(Roles = "read")]
        public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
                                    [FromQuery] string? sortBy, [FromQuery] bool isAscending,
                                    [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 100)
        {
            _logger.LogInformation("GetAll Book Action method was invoked");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is an error log");

            var allBooks = _shopRepository.GetAllshop(filterOn, filterQuery, sortBy, isAscending, pageNumber, pageSize);
            _logger.LogInformation($"Finished GetAllBook request with data {JsonSerializer.Serialize(allBooks)}");
            return Ok(allBooks);
        }

        [HttpGet("get-book-by-id/{id}")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult GetshopById([FromRoute] int id)
        {
            var bookWithIdDTO = _shopRepository.GetshopById(id);
            return Ok(bookWithIdDTO);
        }

        [HttpPost("add-book")]
        [Authorize(Roles = "Write")]
        public IActionResult AddShop([FromBody] AddshopRequestDTO addloaiSP)
        {
            if (ModelState.IsValid)
            {
                var bookAdd = _shopRepository.AddShop(addloaiSP);
                return Ok(bookAdd);
            }

            if (!ValidateAddshop(addloaiSP))
            {
                return BadRequest(ModelState);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("update-book-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdateShopById(int id, [FromBody] AddshopRequestDTO ShopDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updateShop = _shopRepository.UpdateShopById(id, ShopDTO);
            return Ok(updateShop);
        }

        [HttpDelete("delete-book-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeleteShopById(int id)
        {
            var deleteBook = _shopRepository.DeleteShopById(id);
            return Ok(deleteBook);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Read,Write")]
        public IActionResult SearchProducts([FromQuery] string query)
        {
            _logger.LogInformation("SearchProducts action method was invoked");
            var products = _shopRepository.SearchProducts(query);
            if (products == null || !products.Any())
            {
                _logger.LogWarning("No products found for the search query: {query}", query);
                return NotFound();
            }
            _logger.LogInformation($"Found {products.Count()} products for the search query: {query}");
            return Ok(products);
        }

        private bool ValidateAddshop(AddshopRequestDTO addBookRequestDTO)
        {
            if (addBookRequestDTO == null)
            {
                ModelState.AddModelError(nameof(addBookRequestDTO), $"Please add book data");
                return false;
            }
            if (string.IsNullOrEmpty(addBookRequestDTO.MoTa))
            {
                ModelState.AddModelError(nameof(addBookRequestDTO.MoTa), $"{nameof(addBookRequestDTO.MoTa)} cannot be null");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
    }
}

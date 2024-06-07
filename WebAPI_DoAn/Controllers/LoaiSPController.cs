using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_DoAn.Data;
using WebAPI_DoAn.Models.DTO;
using WebAPI_DoAn.Models.Repositories;

namespace WebAPI_DoAn.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LoaiSPController : ControllerBase
    {
        private readonly ShopDbContext _dbContext;
        private readonly ILoaiSPRepository _loaiSPRepository;
        public LoaiSPController(ShopDbContext dbContext, ILoaiSPRepository loaiSPRepository)
        {
            _dbContext = dbContext;
            _loaiSPRepository = loaiSPRepository;
        }
        [HttpGet("get-all-author")]
        public IActionResult GetAllLoaiSP()
        {
            var allAuthors = _loaiSPRepository.GetAllLoaiSP();
            return Ok(allAuthors);
        }
        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetLoaiSPById(int id)
        {
            var authorWithId = _loaiSPRepository.GetLoaiSPById(id);
            return Ok(authorWithId);
        }
        [HttpPost("add-author")]
        public IActionResult AddLoaiSP([FromBody] addLoaiSP
       addAuthorRequestDTO)
        {
            var authorAdd = _loaiSPRepository.AddLoaiSP(addAuthorRequestDTO);
            return Ok();
        }
        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateLoaiSPById(int id, [FromBody] addLoaiSP
       authorDTO)
        {
            var authorUpdate = _loaiSPRepository.UpdateLoaiSPById(id, authorDTO);
            return Ok(authorUpdate);
        }
        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteLoaiSPById(int id)
        {
            var authorDelete = _loaiSPRepository.DeleteLoaiSPById(id);
            return Ok();
        }

    }
}

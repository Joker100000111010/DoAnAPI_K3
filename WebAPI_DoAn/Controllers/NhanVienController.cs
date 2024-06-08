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
    public class NhanVienController : ControllerBase
    {
        private readonly ShopDbContext _dbContext;
        private readonly INhanVienRepository _nhanVienRepository;
        public NhanVienController(ShopDbContext dbContext, INhanVienRepository nhanVienRepository)
        {
            _dbContext = dbContext;
            _nhanVienRepository = nhanVienRepository;
        }

        [HttpGet("get-all-author")]
        public IActionResult GetAllNhanVien()
        {
            var allAuthors = _nhanVienRepository.GetAllNhanVien();
            return Ok(allAuthors);
        }
        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetNhanVienById(int id)
        {
            var authorWithId = _nhanVienRepository.GetNhanVienById(id);
            return Ok(authorWithId);
        }

        [HttpPost("add-author")]
        [Authorize(Roles = "Write")]
        public IActionResult AddNhanVien([FromBody] addNhanVien addNhanVien)
        {
            var authorAdd = _nhanVienRepository.AddNhanVien(addNhanVien);
            return Ok();
        }
        [HttpPut("update-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdateNhanVienById(int id, [FromBody] NhanVienNoIdDTO
       NhanVienNoIdDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var NhanVienUpdate = _nhanVienRepository.UpdateNhanVienById(id, NhanVienNoIdDTO);
            return Ok(NhanVienUpdate);
        }
        [HttpDelete("delete-author-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeleteNhanVienById(int id)
        {
            var authorDelete = _nhanVienRepository.DeleteNhanVienById(id);
            return Ok();
        }
    }
}

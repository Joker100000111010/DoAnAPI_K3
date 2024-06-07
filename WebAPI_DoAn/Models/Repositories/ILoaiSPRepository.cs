using WebAPI_DoAn.Models.Domain;
using WebAPI_DoAn.Models.DTO;

namespace WebAPI_DoAn.Models.Repositories
{
    public interface ILoaiSPRepository
    {
        List<LoaiSPDTO> GetAllLoaiSP();
        addLoaiSP GetLoaiSPById(int id);
        addLoaiSP AddLoaiSP(addLoaiSP addloaiSP);
        addLoaiSP? UpdateLoaiSPById(int id, addLoaiSP LoaiSPDTO);
        LoaiSanPham? DeleteLoaiSPById(int id);
    }
}

using WebAPI_DoAn.Models.Domain;
using WebAPI_DoAn.Models.DTO;

namespace WebAPI_DoAn.Models.Repositories
{
    public interface INhanVienRepository
    {
        List<NhanVienDTO> GetAllNhanVien();
        NhanVienNoIdDTO GetNhanVienById(int id);
        addNhanVien AddNhanVien(addNhanVien addNhanVien);
        NhanVienNoIdDTO UpdateNhanVienById(int id, NhanVienNoIdDTO NhanVienNoIdDTO);
        NhanVien? DeleteNhanVienById(int id);
    }
}

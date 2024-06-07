using Microsoft.EntityFrameworkCore;
using WebAPI_DoAn.Data;
using WebAPI_DoAn.Models.Domain;
using WebAPI_DoAn.Models.DTO;

namespace WebAPI_DoAn.Models.Repositories
{
    public class SQLNhanVienRepository: INhanVienRepository
    {
        private readonly  ShopDbContext _dbContext;
        public SQLNhanVienRepository(ShopDbContext context)
        {
            _dbContext = context;
        }
        public List<NhanVienDTO> GetAllNhanVien()
        {
            //Get Data From Database -Domain Model
            var allNhanVienDomain = _dbContext.nhanViens.ToList();
            //Map domain models to DTOs
            var allPublisherDTO = new List<NhanVienDTO>();
            foreach (var NhanVienDomain in allNhanVienDomain)
            {
                allPublisherDTO.Add(new NhanVienDTO()
                {
                    Id = NhanVienDomain.IdNV,
                    ImgNV = NhanVienDomain.ImgNV,
                    TenNV = NhanVienDomain.TenNV,
                    Email = NhanVienDomain.Email,
                    SDT = NhanVienDomain.SDT,
                    DiaChi = NhanVienDomain.DiaChi,

                });
            }
            return allPublisherDTO;
        }
        public NhanVienNoIdDTO GetNhanVienById(int id)
        {
            // get book Domain model from Db
            var NhanVienWithIdDomain = _dbContext.nhanViens.FirstOrDefault(x => x.IdNV ==
           id);
            if (NhanVienWithIdDomain != null)
            { //Map Domain Model to DTOs
                var NhanVienNoIdDTO = new NhanVienNoIdDTO
                {
                    ImgNV= NhanVienWithIdDomain.ImgNV,
                    TenNV = NhanVienWithIdDomain.TenNV,
                    Email = NhanVienWithIdDomain.Email,
                    SDT = NhanVienWithIdDomain.SDT,
                    DiaChi = NhanVienWithIdDomain.DiaChi,
                };
                return NhanVienNoIdDTO;
            }
            return null;
        }
        public addNhanVien AddNhanVien(addNhanVien addNhanVien)
        {
            var NhanVienDomainModel = new NhanVien
            {
                ImgNV = addNhanVien.ImgNV,
                TenNV = addNhanVien.TenNV,
                Email = addNhanVien.Email,
                SDT = addNhanVien.SDT,
                DiaChi = addNhanVien.DiaChi,
            };
            //Use Domain Model to create Book
            _dbContext.nhanViens.Add(NhanVienDomainModel);
            _dbContext.SaveChanges();
            return addNhanVien;
        }
        public NhanVienNoIdDTO UpdateNhanVienById(int id, NhanVienNoIdDTO NhanVienNoIdDTO)
        {
            var NhanVienDomain = _dbContext.nhanViens.FirstOrDefault(n => n.IdNV == id);
            if (NhanVienDomain != null)
            {
                NhanVienDomain.ImgNV = NhanVienNoIdDTO.ImgNV;
                NhanVienDomain.TenNV = NhanVienNoIdDTO.TenNV;
                NhanVienDomain.Email = NhanVienNoIdDTO.Email;
                NhanVienDomain.SDT = NhanVienNoIdDTO.SDT;
                NhanVienDomain.DiaChi = NhanVienNoIdDTO.DiaChi;
    
            }
            Console.WriteLine($"Updated shop: {NhanVienDomain.ImgNV}, {NhanVienDomain.TenNV}, {NhanVienDomain.Email}, {NhanVienDomain.SDT},{NhanVienDomain.DiaChi}");
            _dbContext.SaveChanges();
            return NhanVienNoIdDTO;
        }
        public NhanVien? DeleteNhanVienById(int id)
        {
            var NhanVienDomain = _dbContext.nhanViens.FirstOrDefault(n => n.IdNV == id);
            if (NhanVienDomain != null)
            {
                _dbContext.nhanViens.Remove(NhanVienDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}

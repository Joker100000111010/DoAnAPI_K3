using System;
using WebAPI_DoAn.Data;
using WebAPI_DoAn.Models.Domain;
using WebAPI_DoAn.Models.DTO;

namespace WebAPI_DoAn.Models.Repositories
{
    public class SQLLoaiSPRepository:ILoaiSPRepository
    {
        private readonly ShopDbContext _dbContext;
        public SQLLoaiSPRepository(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<LoaiSPDTO> GetAllLoaiSP()
        {
            //Get Data From Database -Domain Model 
            var allLoaiSpDomain = _dbContext.loaiSanPhams.ToList();
            //Map domain models to DTOs 
            var allLoaiSPDTO = new List<LoaiSPDTO>();
            foreach (var LoaiSPDomain in allLoaiSpDomain)
            {
                allLoaiSPDTO.Add(new LoaiSPDTO()
                {
                    Id = LoaiSPDomain.IdLoaiSP,
                    LoaiSP = LoaiSPDomain.LoaiSP,
                    ImgLSP = LoaiSPDomain.ImgLSP
                });
            }
            //return DTOs 
            return allLoaiSPDTO;
        }
        public addLoaiSP GetLoaiSPById(int id)
        {
            // get book Domain model from Db
            var LoaiSPWithIdDomain = _dbContext.loaiSanPhams.FirstOrDefault(x => x.IdLoaiSP == id);
            if (LoaiSPWithIdDomain == null)
            {
                return null;
            }
            //Map Domain Model to DTOs 
            var authorNoIdDTO = new addLoaiSP
            {
                LoaiSP = LoaiSPWithIdDomain.LoaiSP,
                ImgLSP = LoaiSPWithIdDomain.ImgLSP
            };
            return authorNoIdDTO;
        }
        public addLoaiSP AddLoaiSP(addLoaiSP addloaiSP)
        {
            var LoaiSPDomainModel = new LoaiSanPham
            {
                LoaiSP = addloaiSP.LoaiSP,
                ImgLSP = addloaiSP.ImgLSP,
            };
            _dbContext.loaiSanPhams.Add(LoaiSPDomainModel);
            _dbContext.SaveChanges();
            return addloaiSP;
        }
        public addLoaiSP? UpdateLoaiSPById(int id, addLoaiSP LoaiSPDTO)
        {
            var authorDomain = _dbContext.loaiSanPhams.FirstOrDefault(n => n.IdLoaiSP == id);
            if (authorDomain != null)
            {
                authorDomain.LoaiSP = LoaiSPDTO.LoaiSP;
                authorDomain.ImgLSP = LoaiSPDTO.ImgLSP;
                _dbContext.SaveChanges();
            }
            return LoaiSPDTO;
        }
        public LoaiSanPham? DeleteLoaiSPById(int id)
        {
            var authorDomain = _dbContext.loaiSanPhams.FirstOrDefault(n => n.IdLoaiSP == id);
            if (authorDomain != null)
            {
                _dbContext.loaiSanPhams.Remove(authorDomain);
                _dbContext.SaveChanges();
            }
            return null;
        }
    }
}

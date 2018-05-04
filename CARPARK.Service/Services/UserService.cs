using CARPARK.Data.Model;
using CARPARK.Data.Repository;
using CARPARK.Data.UnitofWork;
using CARPARK.DTO.EntitisDTO;
using CARPARK.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Services
{
    public class UserService : IUserService
    {

        private readonly IRepository<Uye> _uyeRepo;
        private readonly IUnitofWork _uow;
        private UyeDTO _uyeDTO;

        public UserService(UnitofWork uow)
        {
            _uow = uow;
            _uyeRepo = _uow.GetRepository<Uye>();
            _uyeDTO = new UyeDTO();
        }

        public int Insert(UyeDTO uye)
        {
            try
            {
                var liste = AutoMapper.Mapper.DynamicMap<Uye>(uye);
                _uyeRepo.Insert(liste);
                _uow.SaveChanges();
                return liste.UyeID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public void Update(UyeDTO uye)
        {
            try
            {
                var uyeEntity = _uyeRepo.Find(Convert.ToInt32(uye.UyeID));
                _uyeRepo.Update(uyeEntity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public UyeDTO User(int id)
        {
            try
            {
                var uyeEntity = (from e in _uyeRepo.GetAll()
                                 where e.UyeID == id
                                 select new UyeDTO
                                 {
                                     UyeID = e.UyeID,
                                     KullaniciAdi = e.KullaniciAdi,
                                     Parola = e.Parola,
                                     Eposta = e.Eposta
                                 }).SingleOrDefault();
                return uyeEntity;
            }
            catch (Exception)
            {
                return new UyeDTO();
            }
        }
    }
}

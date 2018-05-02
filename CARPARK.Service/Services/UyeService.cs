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
    public class UyeService : IUyeService
    {

        private readonly IRepository<Uye> _uyeRepo;
        private readonly IUnitofWork _uow;
        private UyeDTO _uyeDTO;

        public UyeService(UnitofWork uow)
        {
            _uow = uow;
            _uyeRepo = _uow.GetRepository<Uye>();
            _uyeDTO = new UyeDTO();
        }
        public UyeDTO Uye(int id)
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

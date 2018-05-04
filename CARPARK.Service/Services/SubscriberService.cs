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
    public class SubscriberService : ISubscriberService
    {
        private readonly IRepository<Abone> _aboneRepo;
        private readonly IUnitofWork _uow;
        private AboneDTO _aboneDTO;
        public SubscriberService(UnitofWork uow)
        {
            _uow = uow;
            _aboneRepo = _uow.GetRepository<Abone>();
            _aboneDTO = new AboneDTO();
        }

        public void Delete(int abnID)
        {
            try
            {
                var entity = _aboneRepo.Find(abnID);
                entity.Durum = false;
                _aboneRepo.Update(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<AboneDTO> GetAllSubscriber()
        {
            try
            {
                var liste = (from a in _aboneRepo.GetAll()
                             where a.Durum == true
                             orderby a.AboneID descending
                             select new AboneDTO
                             {
                                 AboneID = a.AboneID,
                                 Adi = a.Adi,
                                 Soyad = a.Soyad,
                                 TCNO = a.TCNO,
                                 Durum = a.Durum,
                                 CepTel = a.CepTel,
                                 KayitTarihi = a.KayitTarihi,
                                 AracID = a.AracID,
                                 YetkiID = a.YetkiID,
                                 UyeID = a.UyeID
                             }).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<AboneDTO>();
            }
        }

        public void Insert(AboneDTO abone)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AboneDTO Subscriber(int id)
        {
            try
            {
                var abone = (from a in _aboneRepo.GetAll()
                             where a.AboneID == id
                             select new AboneDTO
                             {
                                 AboneID = a.AboneID,
                                 Adi = a.Adi,
                                 Soyad = a.Soyad,
                                 TCNO = a.TCNO,
                                 Durum = a.Durum,
                                 CepTel = a.CepTel,
                                 KayitTarihi = a.KayitTarihi,
                                 AracID = a.AracID,
                                 YetkiID = a.YetkiID,
                                 UyeID=a.UyeID
                             }).SingleOrDefault();
                return abone;
            }
            catch (Exception)
            {
                return new AboneDTO();
            }
        }

        public void Update(AboneDTO abone)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

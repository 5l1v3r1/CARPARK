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
        private readonly IRepository<AboneGiriCikis> _giriscikisRepo;
        private readonly IRepository<AboneOdeme> _odemeRepo;
        private readonly IUnitofWork _uow;
        private AboneDTO _aboneDTO;
        private AboneGirisCikisDTO _giriscikisDTO;
        private AboneOdemeDTO _odemeDTO;
        public SubscriberService(UnitofWork uow)
        {
            _uow = uow;
            _aboneRepo = _uow.GetRepository<Abone>();
            _aboneDTO = new AboneDTO();
            _giriscikisRepo = _uow.GetRepository<AboneGiriCikis>();
            _giriscikisDTO = new AboneGirisCikisDTO();
            _odemeRepo = _uow.GetRepository<AboneOdeme>();
            _odemeDTO = new AboneOdemeDTO();
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
                                 UyeID = a.UyeID,
                                 Islem = a.Islem
                             }).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<AboneDTO>();
            }
        }

        public List<AboneOdemeDTO> GetAllSubscriberPayment(int aboneID)
        {
            try
            {
                var liste = (from o in _odemeRepo.GetAll()
                             where o.AboneID == aboneID
                             orderby o.OdemeID descending
                             select new AboneOdemeDTO
                             {
                                 OdemeID = o.OdemeID,
                                 OdemeTarihi = o.OdemeTarihi,
                                 Tutar = o.Tutar,
                                 AboneID = o.AboneID
                             }).OrderBy(x => x.OdemeID).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<AboneOdemeDTO>();
            }
        }

        public void Insert(AboneDTO abone, int uyeID, int aracID)
        {
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<Abone>(abone);
                entity.Durum = true;
                entity.KayitTarihi = DateTime.Now;
                entity.YetkiID = 3;
                entity.UyeID = uyeID;
                entity.AracID = aracID;
                entity.KayitTarihi = DateTime.Now;
                entity.Islem = "Giriş Yaptı";
                _aboneRepo.Insert(entity);
                _uow.SaveChanges();
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
                                 UyeID = a.UyeID,
                                 Islem = a.Islem
                             }).SingleOrDefault();
                return abone;
            }
            catch (Exception)
            {
                return new AboneDTO();
            }
        }

        public void SubscriberPaymentInsert(AboneOdemeDTO odeme)
        {
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<AboneOdeme>(odeme);
                _odemeRepo.Insert(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AboneOdemeDTO SubscriberPayment(int id)
        {
            try
            {
                var odeme = (from o in _odemeRepo.GetAll()
                             where o.OdemeID == id
                             select new AboneOdemeDTO
                             {
                                 OdemeID = o.OdemeID,
                                 OdemeTarihi = o.OdemeTarihi,
                                 Tutar = o.Tutar,
                                 AboneID = o.AboneID
                             }).SingleOrDefault();
                return odeme;

            }
            catch (Exception)
            {
                return new AboneOdemeDTO();
            }
        }

        public void Update(AboneDTO abone)
        {
            try
            {
                var aboneEntity = _aboneRepo.Find(abone.AboneID);
                AutoMapper.Mapper.DynamicMap(abone, aboneEntity);
                _aboneRepo.Update(aboneEntity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AboneDTO GetSubscriber(int aracId)
        {
            try
            {
                var abone = (from a in _aboneRepo.GetAll()
                             where a.AracID == aracId
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
                                 UyeID = a.UyeID,
                                 Islem = a.Islem,
                             }).SingleOrDefault();
                return abone;
            }
            catch (Exception)
            {
                return new AboneDTO();
            }
        }

        public bool SubscriberInputOutput(AboneGirisCikisDTO abone)
        {
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<AboneGiriCikis>(abone);
                _giriscikisRepo.Insert(entity);
                _uow.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);

            }
        }

        public List<AboneGirisCikisDTO> GetSubscriberAllInputOutput(int abooneId)
        {
            try
            {
                var aboneList = (from a in _giriscikisRepo.GetAll()
                                 where a.AboneId == abooneId
                                 orderby a.TimeId descending
                                 select new AboneGirisCikisDTO
                                 {
                                     AboneId = a.AboneId,
                                     Durum = a.Durum,
                                     Tarih = a.Tarih,
                                     TimeId = a.TimeId
                                 }).OrderByDescending(x => x.TimeId).ToList();
                return aboneList;
            }
            catch (Exception)
            {
                return new List<AboneGirisCikisDTO>();
            }
        }
    }
}

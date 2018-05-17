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
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Musteri> _musteriRepo;
        private readonly IRepository<MusteriPark> _parkRepo;
        private readonly IRepository<MusteriYikama> _yikamaRepo;
        private readonly IRepository<Arac> _aracRepo;
        private readonly IRepository<AracMarka> _markaRepo;
        private readonly IRepository<AracModel> _modelRepo;
        private readonly IUnitofWork _uow;
        private MusteriDTO _musteriDTO;
        private MusteriParkDTO _parkDTO;
        private MusteriYikamaDTO _yikamaDTO;
        public CustomerService(UnitofWork uow)
        {
            _uow = uow;
            _musteriRepo = _uow.GetRepository<Musteri>();
            _parkRepo = _uow.GetRepository<MusteriPark>();
            _yikamaRepo = _uow.GetRepository<MusteriYikama>();
            _aracRepo = _uow.GetRepository<Arac>();
            _markaRepo = _uow.GetRepository<AracMarka>();
            _modelRepo = _uow.GetRepository<AracModel>();
            _musteriDTO = new MusteriDTO();
            _parkDTO = new MusteriParkDTO();
            _yikamaDTO = new MusteriYikamaDTO();
        }

        public MusteriDTO Customer(int musteriID)
        {
            try
            {
                MusteriDTO musteri = (from m in _musteriRepo.GetAll()
                                      join a in _aracRepo.GetAll() on m.AracID equals a.AracID
                                      join mk in _markaRepo.GetAll() on a.MarkaID equals mk.MarkaID
                                      join md in _modelRepo.GetAll() on a.ModelID equals md.ModelID
                                      where m.MusteriID == musteriID
                                      select new MusteriDTO
                                      {
                                          MusteriID = m.MusteriID,
                                          HizmetTuru = m.HizmetTuru,
                                          Aciklama = m.Aciklama,
                                          Plaka = a.Plaka,
                                          Marka = mk.Marka,
                                          MarkaID = mk.MarkaID,
                                          Model = md.Model,
                                          ModelID = md.ModelID,
                                          Tutar = m.Tutar,
                                          Durum = m.Durum
                                      }).FirstOrDefault();
                return musteri;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CustomerInsert(MusteriDTO musteri,int aracID)
        {
            try
            {
                var musteriEntity = AutoMapper.Mapper.DynamicMap<Musteri>(musteri);
                musteriEntity.AracID = aracID;
                musteriEntity.Durum = true;
                _musteriRepo.Insert(musteriEntity);
                _uow.SaveChanges();
                return musteriEntity.MusteriID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<MusteriDTO> CustomerList()
        {
            try
            {
                var liste = (from m in _musteriRepo.GetAll()
                             join a in _aracRepo.GetAll() on m.AracID equals a.AracID
                             join mk in _markaRepo.GetAll() on a.MarkaID equals mk.MarkaID
                             join md in _modelRepo.GetAll() on a.ModelID equals md.ModelID
                             select new MusteriDTO
                             {
                                 MusteriID = m.MusteriID,
                                 HizmetTuru = m.HizmetTuru,
                                 Aciklama = m.Aciklama,
                                 Plaka = a.Plaka,
                                 Marka = mk.Marka,
                                 MarkaID = mk.MarkaID,
                                 Model = md.Model,
                                 ModelID = md.ModelID,
                                 Tutar = m.Tutar,
                                 Durum = m.Durum
                             }).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<MusteriDTO>();
            }
        }

        public MusteriParkDTO CustomerPark(int musteriID)
        {
            try
            {
                var park = (from p in _parkRepo.GetAll()
                            where p.MusteriID == musteriID
                            select new MusteriParkDTO
                            {
                                MusteriID = p.MusteriID,
                                GirisTarihi = p.GirisTarihi,
                                CikisTarihi = p.CikisTarihi,
                                ParkID = p.ParkID
                            }).SingleOrDefault();
                return park;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CustomerParkInsert(MusteriParkDTO park, int musteriID)
        {
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<MusteriPark>(park);
                entity.MusteriID = musteriID;
                entity.GirisTarihi = DateTime.Now;
                _parkRepo.Insert(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MusteriYikamaDTO CustomerWashing(int musteriID)
        {
            try
            {
                var yikama = (from y in _yikamaRepo.GetAll()
                              where y.MusteriID == musteriID
                              select new MusteriYikamaDTO
                              {
                                  MusteriID = y.MusteriID,
                                  YikamaID = y.YikamaID,
                                  TeslimSaati = y.TeslimSaati,
                                  YikamaTuru = y.YikamaTuru
                              }).SingleOrDefault();
                return yikama;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CustomerWashingInsert(MusteriYikamaDTO yikama,int musteriID)
        {
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<MusteriYikama>(yikama);
                entity.MusteriID = musteriID;
                _yikamaRepo.Insert(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

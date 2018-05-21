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
using System.Web.Mvc;

namespace CARPARK.Service.Services
{
    public class CarService : ICarService
    {
        private readonly IRepository<Arac> _aracRepo;
        private readonly IRepository<AracMarka> _markaRepo;
        private readonly IRepository<AracModel> _modelRepo;
        private readonly IUnitofWork _uow;
        private AracDTO _aracDTO;
        private AracMarkaDTO _markaDTO;
        private AracModelDTO _modelDTO;
        public CarService(UnitofWork uow)
        {
            _uow = uow;
            _aracRepo = _uow.GetRepository<Arac>();
            _markaRepo = _uow.GetRepository<AracMarka>();
            _modelRepo = _uow.GetRepository<AracModel>();
            _aracDTO = new AracDTO();
            _markaDTO = new AracMarkaDTO();
            _modelDTO = new AracModelDTO();
        }

        public AracDTO Car(int id)
        {
            try
            {
                var aracEntity = (from a in _aracRepo.GetAll()
                                  where a.AracID == id
                                  select new AracDTO
                                  {
                                      AracID = a.AracID,
                                      Plaka = a.Plaka,
                                      MarkaID = a.MarkaID,
                                      ModelID = a.ModelID
                                  }).SingleOrDefault();
                return aracEntity;
            }
            catch (Exception)
            {
                return new AracDTO();
            }
        }

        public List<SelectListItem> GetAllBrand()
        {
            try
            {
                var liste = (from m in _markaRepo.GetAll()
                             select new SelectListItem
                             {
                                 Text = m.Marka,
                                 Value = m.MarkaID.ToString()
                             }).ToList();
                liste.Insert(0, new SelectListItem { Text = "Marka Seçiniz", Value = "", Selected = true });
                return liste;
            }
            catch (Exception)
            {
                return new List<SelectListItem>();
            }
        }

        public List<SelectListItem> GetAllModel(int markaID)
        {
            try
            {
                var liste = (from md in _modelRepo.GetAll()
                             where md.MarkaID == markaID
                             orderby md.ModelID descending
                             select new SelectListItem
                             {
                                 Value = md.ModelID.ToString(),
                                 Text = md.Model
                             }).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<SelectListItem>();
            }
        }

        public int Insert(AracDTO arac)
        {
            try
            {
                var liste = AutoMapper.Mapper.DynamicMap<Arac>(arac);
                _aracRepo.Insert(liste);
                _uow.SaveChanges();
                return liste.AracID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AracModelDTO Model(int modelID)
        {
            try
            {
                var model = (from md in _modelRepo.GetAll()
                             where md.ModelID == modelID
                             select new AracModelDTO
                             {
                                 ModelID = md.ModelID,
                                 Model = md.Model,
                                 MarkaID = md.MarkaID
                             }).SingleOrDefault();
                return model;
            }
            catch (Exception)
            {
                return new AracModelDTO();
            }
        }

        public AracMarkaDTO Brand(int markaID)
        {
            try
            {
                var marka = (from mk in _markaRepo.GetAll()
                             where mk.MarkaID == markaID
                             select new AracMarkaDTO
                             {
                                 MarkaID = mk.MarkaID,
                                 Marka = mk.Marka
                             }).SingleOrDefault();
                return marka;
            }
            catch (Exception)
            {
                return new AracMarkaDTO();
            }
        }

        public List<AracDTO> GetAllCarList()
        {
            try
            {
                var list = (from a in _aracRepo.GetAll()
                            orderby a.AracID descending
                            select new AracDTO
                            {
                                AracID = a.AracID,
                                MarkaID = a.MarkaID,
                                ModelID = a.ModelID,
                                Plaka = a.Plaka
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<AracDTO>();
            }
        }
    }
}

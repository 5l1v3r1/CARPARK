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
        private readonly IRepository<KaraListe> _karaRepo;
        private readonly IUnitofWork _uow;
        private AracDTO _aracDTO;
        private AracMarkaDTO _markaDTO;
        private AracModelDTO _modelDTO;
        private KaraListeDTO _karaDTO;
        public CarService(UnitofWork uow)
        {
            _uow = uow;
            _aracRepo = _uow.GetRepository<Arac>();
            _markaRepo = _uow.GetRepository<AracMarka>();
            _modelRepo = _uow.GetRepository<AracModel>();
            _karaRepo = _uow.GetRepository<KaraListe>();
            _aracDTO = new AracDTO();
            _markaDTO = new AracMarkaDTO();
            _modelDTO = new AracModelDTO();
            _karaDTO = new KaraListeDTO();
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

        public int GetCar(string plaka)
        {
            try
            {
                if (_aracRepo.GetAll().Count() > 0)
                {
                    var aracEntity = (from a in _aracRepo.GetAll()
                                      where a.Plaka == plaka
                                      select new AracDTO
                                      {
                                          AracID = a.AracID,
                                          Plaka = a.Plaka,
                                          MarkaID = a.MarkaID,
                                          ModelID = a.ModelID
                                      }).SingleOrDefault();

                    if (aracEntity == null)
                    {
                        return 0;
                    }

                    return aracEntity.AracID;
                }
                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }

        public AracMarkaDTO GetCarBrand(string brandName)
        {
            try
            {
                var aracEntity = (from a in _markaRepo.GetAll()
                                  where a.Marka.Contains(brandName.Trim())
                                  select new AracMarkaDTO
                                  {
                                      MarkaID = a.MarkaID,
                                      Marka = a.Marka
                                  }).SingleOrDefault();
                return aracEntity;
            }
            catch (Exception)
            {
                return new AracMarkaDTO();
            }
        }

        public AracModelDTO GetCarModel(int brandId, string modelName)
        {
            try
            {
                var aracEntity = (from a in _modelRepo.GetAll()
                                  where a.MarkaID == brandId && a.Model.Contains(modelName.Trim())
                                  select new AracModelDTO
                                  {
                                      MarkaID = a.MarkaID,
                                      ModelID = a.ModelID,
                                      Model = a.Model
                                  }).SingleOrDefault();
                return aracEntity;
            }
            catch (Exception)
            {
                return new AracModelDTO();
            }
        }

        public int CarBrandInsert(AracMarkaDTO brand)
        {
            try
            {
                var liste = AutoMapper.Mapper.DynamicMap<AracMarka>(brand);
                _markaRepo.Insert(liste);
                _uow.SaveChanges();
                return liste.MarkaID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int CarModelInsert(AracModelDTO model)
        {
            try
            {
                var liste = AutoMapper.Mapper.DynamicMap<AracModel>(model);
                _modelRepo.Insert(liste);
                _uow.SaveChanges();
                return liste.ModelID;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<DTO.EntitisDTO.KaraListeDTO> GetBlackList()
        {
            try
            {
                var list = (from a in _karaRepo.GetAll()
                            select new KaraListeDTO
                            {
                                AracID = a.AracID,
                                Aciklama = a.Aciklama,
                                ID = a.ID
                            }).ToList();
                return list;
            }
            catch (Exception)
            {
                return new List<KaraListeDTO>();
            }
        }

        public void BlackListCarInsert(KaraListeDTO kara)
        {
            try
            {
                var liste = AutoMapper.Mapper.DynamicMap<KaraListe>(kara);
                _karaRepo.Insert(liste);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool GetBlackListCarControl(int aracId)
        {
            try
            {
                var aracEntity = (from a in _karaRepo.GetAll()
                                  where a.AracID == aracId
                                  select new KaraListeDTO
                                  {
                                      AracID = a.AracID,
                                      Aciklama = a.Aciklama,
                                      ID = a.ID
                                  }).SingleOrDefault();
                if (aracEntity != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

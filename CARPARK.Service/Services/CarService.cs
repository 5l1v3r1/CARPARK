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

        public List<AracMarkaDTO> GetAllBrand()
        {
            try
            {
                var liste = (from b in _markaRepo.GetAll()
                             orderby b.MarkaID descending
                             select new AracMarkaDTO
                             {
                                 MarkaID = b.MarkaID,
                                 Marka = b.Marka
                             }).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<AracMarkaDTO>();
            }
        }

        public List<AracModelDTO> GetAllModel()
        {
            try
            {
                var liste = (from b in _modelRepo.GetAll()
                             orderby b.ModelID descending
                             select new AracModelDTO
                             {
                                 MarkaID = b.MarkaID,
                                 ModelID = b.ModelID,
                                 Model = b.Model
                             }).ToList();
                return liste;
            }
            catch (Exception)
            {
                return new List<AracModelDTO>();
            }
        }
    }
}

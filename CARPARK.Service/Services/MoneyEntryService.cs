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
    public class MoneyEntryService : IMoneyEntryService
    {
        private readonly IRepository<Gelirler> _gelirRepo;
        private readonly IUnitofWork _uow;
        private GelirlerDTO _gelirDTO;
        public MoneyEntryService(UnitofWork uow)
        {
            _uow = uow;
            _gelirRepo = _uow.GetRepository<Gelirler>();
            _gelirDTO = new GelirlerDTO();
        }
        public void Insert(GelirlerDTO gelir)
        {
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<Gelirler>(gelir);
                _gelirRepo.Insert(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

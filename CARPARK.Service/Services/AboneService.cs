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
    public class AboneService : IAboneService
    {
        private readonly IRepository<Abone> _aboneRepo;
        private readonly IUnitofWork _uow;
        private AboneDTO _aboneDTO;
        public AboneService(UnitofWork uow)
        {
            _uow = uow;
            _aboneRepo = _uow.GetRepository<Abone>();
            _aboneDTO = new AboneDTO();
        }
    }
}

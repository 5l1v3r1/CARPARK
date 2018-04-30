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
    public class AracService : IAracService
    {
        private readonly IRepository<Arac> _aracRepo;
        private readonly IUnitofWork _uow;
        private AracDTO _aracDTO;

        public AracService(UnitofWork uow)
        {
            _uow = uow;
            _aracRepo = _uow.GetRepository<Arac>();
            _aracDTO = new AracDTO();
        }

        public List<AracDTO> AracListesi()
        {
            try
            {
                var aracListe = (from u in _aracRepo.GetAll()
                                 select new AracDTO
                                 {
                                     AracID = u.AracID,
                                     MarkaID = Convert.ToInt32(u.MarkaID),
                                     ModelID = Convert.ToInt32(u.ModelID),
                                     Plaka = u.Plaka
                                 }).ToList();
                return aracListe;
            }
            catch (Exception)
            {
                return new List<AracDTO>();
            }
        }
    }
}

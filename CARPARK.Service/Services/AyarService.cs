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
    public class AyarService : IAyarlarService
    {
        private readonly IRepository<Ayarlar> _ayarRepo;
        private readonly IUnitofWork _uow;
        private AyarlarDTO _ayarDTO;
        public AyarService(UnitofWork uow)
        {
            _uow = uow;
            _ayarRepo = _uow.GetRepository<Ayarlar>();
            _ayarDTO = new AyarlarDTO();
        }

        public AyarlarDTO GetSettings(int id)
        {
            var liste = (from m in _ayarRepo.GetAll()
                         where m.AyarId == id
                         select new AyarlarDTO
                         {
                             AyarId = m.AyarId,
                             KamyonetKapasite = Convert.ToInt32(m.KamyonetKapasite),
                             OtomobilKapasite= Convert.ToInt32(m.OtomobilKapasite),
                             ToplamKapasite= Convert.ToInt32(m.ToplamKapasite),
                             ResimKlasorYolu=m.ResimKlasorYolu
                         }).SingleOrDefault();
            return liste;
        }
    }
}

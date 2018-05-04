using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
    public interface ISubscriberService
    {
        /// <summary>
        /// Görderilen id ile Abone veri seti döndürülür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AboneDTO Subscriber(int id);

        /// <summary>
        /// Butun Aboneleri getirir.
        /// </summary>
        /// <returns></returns>
        List<AboneDTO> GetAllSubscriber();

        /// <summary>
        /// Abone ekleme işlemi yapar.
        /// </summary>
        /// <param name="abone"></param>
        void Insert(AboneDTO abone);

        /// <summary>
        /// Abone bilgilerini günceller.
        /// </summary>
        /// <param name="abone"></param>
        void Update(AboneDTO abone);

        /// <summary>
        /// Abone silme durum false yapar.
        /// </summary>
        /// <param name="abnID"></param>
        void Delete(int abnID);
    }
}

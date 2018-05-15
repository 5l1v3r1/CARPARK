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
        /// <param name="uyeID"></param>
        /// <param name="aracID"></param>
        void Insert(AboneDTO abone,int uyeID, int aracID);

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

        /// <summary>
        /// Tum abone ödemelerini liste olarak döndürür.
        /// </summary>
        /// <returns></returns>
        List<AboneOdemeDTO> GetAllSubscriberPayment(int aboneID);

        /// <summary>
        /// Gelen ID'ye göre ilgili ödeme kaydını döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AboneOdemeDTO SubscriberPayment(int id);

        /// <summary>
        /// Abone ödeme kayıtları ekler.
        /// </summary>
        /// <param name="odeme"></param>
        void SubscriberPaymentInsert(AboneOdemeDTO odeme);
    }
}

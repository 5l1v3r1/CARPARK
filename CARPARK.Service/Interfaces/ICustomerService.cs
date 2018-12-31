using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CARPARK.Service.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Musteri listesi getirir.
        /// </summary>
        /// <returns></returns>
        List<MusteriDTO> CustomerList();

        /// <summary>
        /// Musteri Id'ye göre müsteri bilgileri dödürür.
        /// </summary>
        /// <param name="musteriID"></param>
        /// <returns></returns>
        MusteriDTO Customer(int musteriID);

        /// <summary>
        /// Musteri Id'ye göre musteri park bilgilerini getirir.
        /// </summary>
        /// <param name="musteriID"></param>
        /// <returns></returns>
        MusteriParkDTO CustomerPark(int musteriID);

        /// <summary>
        /// Muster Id'ye göre müsteri yıkama bilgisini getirir.
        /// </summary>
        /// <param name="musteriID"></param>
        /// <returns></returns>
        MusteriYikamaDTO CustomerWashing(int musteriID);

        /// <summary>
        /// Musteri ekleme işlemi yapar.
        /// </summary>
        /// <param name="musteri"></param>
        /// <param name="aracID"></param>
        /// <returns></returns>
        int CustomerInsert(MusteriDTO musteri, int aracID);

        /// <summary>
        /// Musteri arac park kayıt işlemi yapar.
        /// </summary>
        /// <param name="park"></param>
        /// <param name="musteriID"></param>
        void CustomerParkInsert(MusteriParkDTO park,int musteriID);
        
        /// <summary>
        /// Musteri arac yıkama kayıt işlemi yapar.
        /// </summary>
        /// <param name="yikama"></param>
        /// <param name="musteriID"></param>
        void CustomerWashingInsert(MusteriYikamaDTO yikama, int musteriID);


    }
}

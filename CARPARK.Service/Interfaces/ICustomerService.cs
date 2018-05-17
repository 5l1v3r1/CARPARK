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

        MusteriDTO Customer(int musteriID);

        MusteriParkDTO CustomerPark(int musteriID);

        MusteriYikamaDTO CustomerWashing(int musteriID);

        int CustomerInsert(MusteriDTO musteri, int aracID);

        void CustomerParkInsert(MusteriParkDTO park,int musteriID);

        void CustomerWashingInsert(MusteriYikamaDTO yikama, int musteriID);
    }
}

using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CARPARK.Service.Interfaces
{
    public interface ICarService
    {
        /// <summary>
        /// Arac marka listesi döndürür.
        /// </summary>
        /// <returns></returns>
        List<SelectListItem> GetAllBrand();

        /// <summary>
        /// Arac model listesi döndürür.
        /// </summary>
        /// <returns></returns>
        List<SelectListItem> GetAllModel(int markaID);

        /// <summary>
        /// Arac ekleme işlemi yapar.Geriye arac ıd döndürür.
        /// </summary>
        /// <param name="arac"></param>
        /// <returns></returns>
        int Insert(AracDTO arac);

        /// <summary>
        /// Arac marka ekleme işlemi yapar.
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        int CarBrandInsert(AracMarkaDTO brand);

        /// <summary>
        /// Arac model ekleme işlemi yapar.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int CarModelInsert(AracModelDTO model);

        /// <summary>
        /// Id'ye göre arac getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        AracDTO Car(int id);

        /// <summary>
        /// ID'ye göre marka ismi döndürür.
        /// </summary>
        /// <param name="markaID"></param>
        /// <returns></returns>
        AracMarkaDTO Brand(int markaID);

        /// <summary>
        /// ID'ye göre model ismi döndürür.
        /// </summary>
        /// <param name="modelID"></param>
        /// <returns></returns>
        AracModelDTO Model(int modelID);

        /// <summary>
        /// Arac tablosunu bir listeye atarak geri döndürür.
        /// </summary>
        /// <returns></returns>
        List<AracDTO> GetAllCarList();

        /// <summary>
        /// Plaka verisine göre arac id döndürür.
        /// </summary>
        /// <param name="plaka"></param>
        /// <returns></returns>
        int GetCar(string plaka);

        /// <summary>
        /// Marka ismine göre arac marka verisini döndürür.
        /// </summary>
        /// <param name="brandName"></param>
        /// <returns></returns>
        AracMarkaDTO GetCarBrand(string brandName);

        /// <summary>
        /// Marka id' si ve model adına göre arac model verisinin döndürür.
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="modelName"></param>
        /// <returns></returns>
        AracModelDTO GetCarModel(int brandId, string modelName);

        /// <summary>
        /// Kara listede bulunan araçları listeler.
        /// </summary>
        /// <returns></returns>
        List<KaraListeDTO> GetBlackList();

        /// <summary>
        /// Kara listeye araç ekler.
        /// </summary>
        /// <param name=""></param>
        void BlackListCarInsert(KaraListeDTO kara);

        /// <summary>
        /// Plaka şartına göre kara listede  araç kontrolu yapar.
        /// </summary>
        /// <param name="plaka"></param>
        /// <returns></returns>
        bool GetBlackListCarControl(int aracId);

    }
}

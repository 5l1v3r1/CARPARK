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
    }
}

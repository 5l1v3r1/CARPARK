using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
    public interface IPersonelService
    {
        /// <summary>
        /// Görderilen id ile personel veri seti döndürülür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PersonelDTO Personel(int id);

        /// <summary>
        /// Butun personelleri getirir.
        /// </summary>
        /// <returns></returns>
        List<PersonelUyeDTO> GetAllPersonel();

        /// <summary>
        /// Personel ekleme işlemi yapar.
        /// </summary>
        /// <param name="personel"></param>
        void Insert(PersonelUyeDTO personel);

        /// <summary>
        /// Personel bilgilerini günceller.
        /// </summary>
        /// <param name="personel"></param>
        void Update(PersonelUyeDTO personel);

        /// <summary>
        /// Personel silme durum false yapar.
        /// </summary>
        /// <param name="perID"></param>
        void Delete(int  perID);
        
    }
}

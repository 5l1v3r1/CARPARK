using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
    public interface IStaffService
    {
        /// <summary>
        /// Görderilen id ile personel veri seti döndürülür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        PersonelDTO Staff(int id);

        /// <summary>
        /// Butun personelleri getirir.
        /// </summary>
        /// <returns></returns>
        List<PersonelDTO> GetAllStaff();

        /// <summary>
        /// Personel ekleme işlemi yapar.
        /// </summary>
        /// <param name="personel"></param>
        void Insert(PersonelDTO personel, int uyeID);

        /// <summary>
        /// Personel bilgilerini günceller.
        /// </summary>
        /// <param name="personel"></param>
        void Update(PersonelDTO personel);

        /// <summary>
        /// Personel silme durum false yapar.
        /// </summary>
        /// <param name="perID"></param>
        void Delete(int perID);

    }
}

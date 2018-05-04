using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// ID'YE göre uye entitiy döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UyeDTO User(int id);

        /// <summary>
        /// Uye ekleme işlemi yapar.
        /// </summary>
        /// <param name="uye"></param>
        /// <returns></returns>
        int Insert(UyeDTO uye);

        /// <summary>
        /// Uye guncelleme işlemi yapar.
        /// </summary>
        /// <param name="uye"></param>
        void Update(UyeDTO uye);
    }
}

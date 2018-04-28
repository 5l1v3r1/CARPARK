using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
   public interface ILoginService
    {
        /// <summary>
        /// KullaniciAdi ve sifreye göre veri getirir.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UyeDTO GetUserInformation(string userName, string password);
    }
}

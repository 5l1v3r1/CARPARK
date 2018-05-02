using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
    public interface IUyeService
    {
        /// <summary>
        /// ID'YE göre uye entitiy döndürür.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UyeDTO Uye(int id);
    }
}

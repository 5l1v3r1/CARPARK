using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
   public interface IMoneyEntryService
    {
        void Insert(GelirlerDTO gelir);
    }
}

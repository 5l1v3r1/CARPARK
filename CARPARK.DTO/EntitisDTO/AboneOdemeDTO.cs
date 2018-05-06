using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
    public class AboneOdemeDTO
    {
        public int OdemeID { get; set; }
        public decimal ?Tutar { get; set; }
        public DateTime ?OdemeTarihi { get; set; }
        public int ?AboneID { get; set; }
    }
}

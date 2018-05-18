using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
   public class GelirlerDTO
    {
        public int GelirID { get; set; }
        public string GelirTuru { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public decimal Tutar { get; set; }
        public int AracID { get; set; }
    }
}

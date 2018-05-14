using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
    public class MusteriYikamaDTO
    {
        public int YikamaID { get; set; }
        public string YikamaTuru { get; set; }
        public DateTime? TeslimSaati { get; set; }
        public int? MusteriID { get; set; }
    }
}

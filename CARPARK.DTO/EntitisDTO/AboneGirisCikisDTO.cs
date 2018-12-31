using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
   public class AboneGirisCikisDTO
    {
        public int? TimeId { get; set; }
        public DateTime? Tarih { get; set; }
        public bool? Durum { get; set; }
        public int? AboneId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
    public class MusteriDTO
    {
        public int MusteriID { get; set; }
        public string HizmetTuru { get; set; }
        public string Aciklama { get; set; }
        public int MarkaID { get; set; }
        public string Marka { get; set; }
        public int ModelID { get; set; }
        public string Model { get; set; }
        public bool? Durum { get; set; }
        public decimal ?Tutar { get; set; }
        public string Plaka { get; set; }
    }
}

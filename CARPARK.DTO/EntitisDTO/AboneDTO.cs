using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
    public class AboneDTO
    {
        public int AboneID { get; set; }
        public string Adi { get; set; }
        public string Soyad { get; set; }
        public string TCNO { get; set; }
        public string CepTel { get; set; }
        public DateTime ?KayitTarihi { get; set; }
        public bool ?Durum { get; set; }
        public int ?AracID { get; set; }
        public int ?UyeID { get; set; }
        public int ?YetkiID { get; set; }
    }
}

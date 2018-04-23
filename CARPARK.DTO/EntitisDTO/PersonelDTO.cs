using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
    public class PersonelDTO
    {
        public int PersonelID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int TCNo { get; set; }
        public string Fotograf { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public int UyeID { get; set; }
        public int YetkiID { get; set; }
        public bool Durum { get; set; }

    }
}

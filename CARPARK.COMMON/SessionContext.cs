using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.COMMON
{
   public class SessionContext
    {
        public int UyeID { get; set; }
        public string KullaniciAdi { get; set; }
        public string Parola { get; set; }
        public string Eposta { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisDTO
{
    public class AyarlarDTO
    {
        public int AyarId { get; set; }
        public int ToplamKapasite { get; set; }
        public int OtomobilKapasite { get; set; }
        public int KamyonetKapasite { get; set; }
        public string ResimKlasorYolu { get; set; }
    }
}

using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisModelViewDTO
{
    public class AboneViewModel
    {
        public AboneDTO Abone { get; set; }
        public UyeDTO Uye { get; set; }
        public AracDTO Arac { get; set; }
        public AracMarkaDTO Marka { get; set; }
        public AracModelDTO Model { get; set; }
        public AracViewModel AracModel { get; set; }
        public List<AboneOdemeDTO> AboneOdeme { get; set; }

    }
}

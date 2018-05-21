using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisModelViewDTO
{
    public class MusteriViewModel
    {
        public MusteriDTO Musteri { get; set; }
        public MusteriParkDTO Park { get; set; }
        public MusteriYikamaDTO Yikama { get; set; }
        public AracViewModel AracModel { get; set; }
        public string CustomerPartial { get; set; }
        public List<MusteriDTO> MusteriListe { get; set; }

    }
}

using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisModelViewDTO
{
    public class BlackListViewModel
    {
        public int ID { get; set; }
        public AracDTO Arac { get; set; }
        public string Aciklama { get; set; }
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }

    }
}

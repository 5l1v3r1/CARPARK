using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.DTO.EntitisModelViewDTO
{
    public class PersonelViewModel
    {
        public PersonelDTO Personel { get; set; }
        public List<PersonelDTO> PersonelListesi { get; set; }
        public List<AracDTO> AracListesi { get; set; }
        public UyeDTO Uye { get; set; }

    }
}

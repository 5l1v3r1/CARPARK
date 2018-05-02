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
        public List<PersonelUyeDTO> PersonelListesi { get; set; }
        public UyeDTO Uye { get; set; }

    }
}

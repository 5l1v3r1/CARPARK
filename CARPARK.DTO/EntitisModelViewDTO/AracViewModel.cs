using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CARPARK.DTO.EntitisModelViewDTO
{
   public class AracViewModel
    {
        public AracViewModel()
        {
            //Türetilmeyen bir liste oldugu için baslangıcta bır item uretılmıstır.
            this.ModelListesi = new List<SelectListItem>();
            ModelListesi.Add(new SelectListItem { Text = "Model Seçiniz", Value = "" });
        }

        public List<SelectListItem> MarkaListesi { get; set; }
        public List<SelectListItem> ModelListesi { get; set; }
    }
}

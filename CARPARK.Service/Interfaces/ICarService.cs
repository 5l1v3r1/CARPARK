using CARPARK.DTO.EntitisDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Interfaces
{
    public interface ICarService
    {
        List<AracMarkaDTO> GetAllBrand();

        List<AracModelDTO> GetAllModel();
    }
}

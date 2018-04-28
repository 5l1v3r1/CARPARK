using CARPARK.Data.Model;
using CARPARK.Data.Repository;
using CARPARK.Data.UnitofWork;
using CARPARK.DTO.EntitisDTO;
using CARPARK.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARPARK.Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<Uye> _uyeRepo;
        private readonly IUnitofWork _uow;
        private UyeDTO _uyeDTO;
        public LoginService(UnitofWork uow)
        {
            _uow = uow;
            _uyeRepo = _uow.GetRepository<Uye>();
            _uyeDTO = new UyeDTO();
        }
        public UyeDTO GetUserInformation(string userName, string password)
        {
            try
            {
                //SingleOrDefault = varsa getir yoksa null getir.
                var control = (from u in _uyeRepo.GetAll()
                               where u.KullaniciAdi == userName && u.Parola == password
                               select new UyeDTO
                               {
                                   KullaniciAdi = u.KullaniciAdi,
                                   Parola = u.Parola,
                                   Eposta = u.Eposta,
                                   UyeID = u.UyeID
                               }).SingleOrDefault();
                return control;
            }
            catch (Exception)
            {
                return new UyeDTO();
            }
        }
    }
}

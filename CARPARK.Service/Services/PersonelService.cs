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
    public class PersonelService : IPersonelService
    {

        private readonly IRepository<Personel> _personelRepo;
        private readonly IUnitofWork _uow;
        private PersonelDTO _personelDTO;

        public PersonelService(UnitofWork uow)
        {
            _uow = uow;
            _personelRepo = _uow.GetRepository<Personel>();
            _personelDTO = new PersonelDTO();
        }

        public List<PersonelDTO> GetAllPersonel()
        {
            try
            {
                var personelListe = (from u in _personelRepo.GetAll()
                                     select new PersonelDTO
                                     {
                                         PersonelID = u.PersonelID,
                                         Ad = u.Ad,
                                         Soyad = u.Soyad,
                                         TCNo = Convert.ToInt32(u.TCNo),
                                         Telefon = u.Telefon,
                                         Adres = u.Adres,
                                         Durum = Convert.ToBoolean(u.Durum),
                                         Fotograf = u.Fotograf,
                                         UyeID = Convert.ToInt32(u.UyeID),
                                         YetkiID = Convert.ToInt32(u.YetkiID)
                                     }).ToList();
                return personelListe;
            }
            catch (Exception)
            {
                return new List<PersonelDTO>();
            }
        }

        public PersonelDTO Personel(int id)
        {
            try
            {
                var entitis = (from u in _personelRepo.GetAll()
                               where u.PersonelID == id && u.Durum == true
                               select new PersonelDTO
                               {
                                   PersonelID = u.PersonelID,
                                   Ad = u.Ad,
                                   Soyad = u.Soyad,
                                   TCNo = Convert.ToInt32(u.TCNo),
                                   Telefon = u.Telefon,
                                   Adres = u.Adres,
                                   Durum = Convert.ToBoolean(u.Durum),
                                   Fotograf=u.Fotograf,
                                   UyeID= Convert.ToInt32(u.UyeID),
                                   YetkiID= Convert.ToInt32(u.YetkiID)
                               }).SingleOrDefault();
                return entitis;
            }
            catch (Exception)
            {
                return new PersonelDTO();
            }

        }

     
    }
}

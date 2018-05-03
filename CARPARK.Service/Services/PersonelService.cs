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
        private readonly IRepository<Uye> _uyeRepo;
        private readonly IUnitofWork _uow;
        private PersonelDTO _personelDTO;
        private UyeDTO _uyeDTO;
        public PersonelService(UnitofWork uow)
        {
            _uow = uow;
            _personelRepo = _uow.GetRepository<Personel>();
            _uyeRepo = _uow.GetRepository<Uye>();
            _personelDTO = new PersonelDTO();
            _uyeDTO = new UyeDTO();
        }

        public List<PersonelUyeDTO> GetAllPersonel()
        {
            try
            {
                var personelListe = (from u in _personelRepo.GetAll()
                                     join uye in _uyeRepo.GetAll() on u.UyeID equals uye.UyeID
                                     where u.Durum == true
                                     orderby u.PersonelID descending
                                     select new PersonelUyeDTO
                                     {
                                         PersonelID = u.PersonelID,
                                         Ad = u.Ad,
                                         Soyad = u.Soyad,
                                         TCNo = u.TCNo,
                                         Telefon = u.Telefon,
                                         Adres = u.Adres,
                                         Durum = u.Durum,
                                         Fotograf = u.Fotograf,
                                         UyeID = u.UyeID,
                                         YetkiID = u.YetkiID,
                                         Eposta = uye.Eposta,
                                         KullaniciAdi = uye.KullaniciAdi,
                                         Parola = uye.Parola
                                     }).ToList();
                return personelListe;
            }
            catch (Exception)
            {
                return new List<PersonelUyeDTO>();
            }
        }

        public PersonelDTO Personel(int id)
        {
            try
            {
                var entitis = (from u in _personelRepo.GetAll()
                               where u.PersonelID == id
                               select new PersonelDTO
                               {
                                   PersonelID = u.PersonelID,
                                   Ad = u.Ad,
                                   Soyad = u.Soyad,
                                   TCNo = u.TCNo,
                                   Telefon = u.Telefon,
                                   Adres = u.Adres,
                                   Durum = u.Durum,
                                   Fotograf = u.Fotograf,
                                   UyeID = u.UyeID,
                                   YetkiID = u.YetkiID
                               }).SingleOrDefault();
                return entitis;
            }
            catch (Exception)
            {
                return new PersonelDTO();
            }

        }

        public void Insert(PersonelUyeDTO personel)
        {
            // install - package automapper - version:4.1
            try
            {
                var uye = AutoMapper.Mapper.DynamicMap<Uye>(personel);
                _uyeRepo.Insert(uye);
                _uow.SaveChanges();
                var entity = AutoMapper.Mapper.DynamicMap<Personel>(personel);
                entity.Durum = true;
                entity.YetkiID = 2;
                entity.UyeID = uye.UyeID;
                _personelRepo.Insert(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(PersonelUyeDTO personel)
        {
            try
            {
                var perEntity = _personelRepo.Find(personel.PersonelID);
                AutoMapper.Mapper.DynamicMap(personel, perEntity);
                _personelRepo.Update(perEntity);
                var uyeEntity = _uyeRepo.Find(Convert.ToInt32(personel.UyeID));
                uyeEntity.KullaniciAdi = personel.KullaniciAdi;
                uyeEntity.Eposta = personel.Eposta;
                _uyeRepo.Update(uyeEntity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

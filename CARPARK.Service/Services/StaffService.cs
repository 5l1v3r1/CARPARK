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
    public class StaffService : IStaffService
    {

        private readonly IRepository<Personel> _personelRepo;
        private readonly IUnitofWork _uow;
        private PersonelDTO _personelDTO;
        public StaffService(UnitofWork uow)
        {
            _uow = uow;
            _personelRepo = _uow.GetRepository<Personel>();
            _personelDTO = new PersonelDTO();
        }

        public List<PersonelDTO> GetAllStaff()
        {
            try
            {
                var personelListe = (from u in _personelRepo.GetAll()
                                     where u.Durum == true
                                     orderby u.PersonelID descending
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
                                     }).ToList();
                return personelListe;
            }
            catch (Exception)
            {
                return new List<PersonelDTO>();
            }
        }

        public PersonelDTO Staff(int id)
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

        public void Insert(PersonelDTO personel, int uyeID)
        {
            // install - package automapper - version:4.1
            try
            {
                var entity = AutoMapper.Mapper.DynamicMap<Personel>(personel);
                entity.Durum = true;
                entity.YetkiID = 2;
                entity.UyeID = uyeID;
                _personelRepo.Insert(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(PersonelDTO personel)
        {
            try
            {
                var perEntity = _personelRepo.Find(personel.PersonelID);
                AutoMapper.Mapper.DynamicMap(personel, perEntity);
                _personelRepo.Update(perEntity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int perID)
        {
            try
            {
                var entity = _personelRepo.Find(perID);
                entity.Durum = false;
                _personelRepo.Update(entity);
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

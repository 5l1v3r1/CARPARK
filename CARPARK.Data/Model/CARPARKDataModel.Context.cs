﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CARPARK.Data.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CARPARKEntities : DbContext
    {
        public CARPARKEntities()
            : base("name=CARPARKEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Abone> Abone { get; set; }
        public virtual DbSet<AboneOdeme> AboneOdeme { get; set; }
        public virtual DbSet<Arac> Arac { get; set; }
        public virtual DbSet<AracMarka> AracMarka { get; set; }
        public virtual DbSet<AracModel> AracModel { get; set; }
        public virtual DbSet<Gelirler> Gelirler { get; set; }
        public virtual DbSet<KullaniciYetki> KullaniciYetki { get; set; }
        public virtual DbSet<Musteri> Musteri { get; set; }
        public virtual DbSet<MusteriPark> MusteriPark { get; set; }
        public virtual DbSet<MusteriYikama> MusteriYikama { get; set; }
        public virtual DbSet<Personel> Personel { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Uye> Uye { get; set; }
    }
}

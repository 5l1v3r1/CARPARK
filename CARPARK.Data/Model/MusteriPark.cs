//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class MusteriPark
    {
        public int ParkID { get; set; }
        public Nullable<System.DateTime> GirisTarihi { get; set; }
        public Nullable<System.DateTime> CikisTarihi { get; set; }
        public int MusteriID { get; set; }
    
        public virtual Musteri Musteri { get; set; }
    }
}

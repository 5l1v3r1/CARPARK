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
    
    public partial class AboneOdeme
    {
        public int OdemeID { get; set; }
        public Nullable<decimal> Tutar { get; set; }
        public Nullable<System.DateTime> OdemeTarihi { get; set; }
        public Nullable<int> AboneID { get; set; }
    
        public virtual Abone Abone { get; set; }
    }
}

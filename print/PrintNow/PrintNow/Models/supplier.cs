//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrintNow.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            this.Supply_Material = new HashSet<Supply_Material>();
        }
    
        public int supplierID { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string companyName { get; set; }
        public string phone { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public byte[] image { get; set; }
        public double rate { get; set; }
        public int block { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Supply_Material> Supply_Material { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PrintNow
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product_Has_Material
    {
        public int materialID { get; set; }
        public int productID { get; set; }
        public double price { get; set; }
        public int amount { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Product Product { get; set; }
    }
}

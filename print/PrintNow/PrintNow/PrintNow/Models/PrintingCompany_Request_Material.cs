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
    using System.ComponentModel.DataAnnotations;

    public partial class PrintingCompany_Request_Material
    {
        [Display(Name = "Printing Company Name")]
        public int printingID { get; set; }
        [Display(Name = "Material Name")]
        public int MaterialID { get; set; }
        [Display(Name = "Material Amount")]
        public Nullable<int> amount { get; set; }
        [Display(Name = "Order Date")]
        public Nullable<System.DateTime> orderDate { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Printing_Company Printing_Company { get; set; }
    }
}

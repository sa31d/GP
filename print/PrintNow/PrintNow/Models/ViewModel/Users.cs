using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrintNow.Models.ViewModel
{
    public class Users
    {
        public List<Customer> customers { get; set; }
        public List<Printing_Company> printing { get; set; }
        public List<Supplier> suppliers { get; set; }
        public List<Shipping_Company> shipping { get; set; }

        
    }
}
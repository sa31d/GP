using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrintNow.Models.ViewModel
{
    public class requestResponse
    {
        public Order order { get; set; }
        public PrintingCompany_Response response { get; set; }
    }
}
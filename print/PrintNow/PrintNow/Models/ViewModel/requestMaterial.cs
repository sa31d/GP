using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrintNow.Models.ViewModel
{
    public class requestMaterial
    {
        public IEnumerable<Material> materials { get; set; }
        public PrintingCompany_Request_Material request { get; set; }
    }
}
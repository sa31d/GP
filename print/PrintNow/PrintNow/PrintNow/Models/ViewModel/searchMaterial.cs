using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrintNow.Models.ViewModel
{
    public class searchMaterial
    {
        public int materialID { get; set; }
        public IEnumerable<Material> materials { get; set; }
    }
}
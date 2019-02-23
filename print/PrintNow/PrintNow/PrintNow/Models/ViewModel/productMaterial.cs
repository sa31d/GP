using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrintNow.Models.ViewModel
{
    public class productMaterial
    {
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<Material> materials { get; set; }
        public Product_Has_Material prodMaterial { get; set; }
    }
}
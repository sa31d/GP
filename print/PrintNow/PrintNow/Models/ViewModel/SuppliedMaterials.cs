using Gp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gp_project.ViewModels
{
    public class SuppliedMaterials
    {

        public Supply_Material supply_material { get; set; }
        public IEnumerable<Material> materials { get; set; }
    }
}

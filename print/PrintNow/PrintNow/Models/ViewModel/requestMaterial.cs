﻿using Gp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gp_project.ViewModels
{
    public class requestMaterial
    {
        public IEnumerable<Material> materials { get; set; }
        public PrintingCompany_Request_Material request { get; set; }
    }
}
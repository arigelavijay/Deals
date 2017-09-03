﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using users.Models;
using System.Runtime.Serialization;

namespace users.ViewModels.Location
{    
    public class ParentVm
    {
        public int id { get; set; }        
        public string name { get; set; }        
        public IEnumerable<LocationVm> locations { get; set; }        
    }
}
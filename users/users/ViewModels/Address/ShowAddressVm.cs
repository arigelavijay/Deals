using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Address
{
    public class ShowAddressVm
    {
        public int id { get; set; }
        public string name { get; set; }
        public int pincode { get; set; }
        public string address { get; set; }
        public string landmark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string phone { get; set; }
        public bool isDefault { get; set; }

    }    
}
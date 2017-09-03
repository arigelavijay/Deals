using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Address
{
    public class AddressVm
    {
        public string name { get; set; }
        public int pincode { get; set; }
        public string address { get; set; }
        public string landmark { get; set; }
        public string city { get; set; }
        public int state { get; set; }
        public string phone { get; set; }
    }
}
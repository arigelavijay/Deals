using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vendors.api.ViewModels.Login
{
    public class RegisterVm
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string vendorName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
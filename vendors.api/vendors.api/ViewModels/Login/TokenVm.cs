using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vendors.api.ViewModels.Login
{
    public class TokenVm
    {
        public string token { get; set; }
        public int id { get; set; }
        public bool status { get; set; }
    }
}
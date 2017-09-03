using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vendors.api.ViewModels.Deals
{
    public class ngTable
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public string sortColumn { get; set; }
        public string sortType { get; set; }
    }
}
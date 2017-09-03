using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.History
{
    public class HistoryVm
    {
        public int dealId { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public DateTime dateCreated { get; set; }
        public string image { get; set; }
        public int vendorId { get; set; }
    }
}
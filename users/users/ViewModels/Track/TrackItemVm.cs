using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Track
{
    public class TrackItemVm
    {
        public string image { get; set; }
        public string name { get; set; }
        public int dealId { get; set; }
        public int vendorId { get; set; }
        public int quantity { get; set; }
    }    
}
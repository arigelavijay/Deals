using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Cart
{    
    public class UpdateVm
    {
        public int bannerId { get;set;}
        public int dealId { get; set; }
        public int locationId { get; set; }
        public int quantity { get; set; }
        public int vendorId { get; set; }
    }

    
}
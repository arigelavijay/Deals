using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vendors.api.ViewModels.Deals
{
    public class ViewDealsVm
    {
        public int id { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public DateTime endsOn { get; set; }
        public string fileName { get; set; }
        public int locationId { get; set; }
        public string locationName { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public string name { get; set; }
        public DateTime startsOn { get; set; }
        public int bannerId { get; set; }

        public float unitPrice { get; set; }
        public float sellingPrice { get; set; }
        public float discount { get; set; }
        
    }
}
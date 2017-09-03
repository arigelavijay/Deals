using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Home
{
    public class DealsVm
    {
        public int dealId { get; set; }
        public int bannerId { get; set; }
        public int vendorId { get; set; }
        public string name { get; set; }
        public int? remaining { get; set; }
        public string image { get; set; }
        public DateTime startsOn { get; set; }
        public DateTime createdOn { get; set; }
        public float unitPrice { get; set; }
        public float discount { get; set; }
        public float sellingPrice { get; set; }
        public double hoursLeft { get; set; }
        public double? avgRating { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}
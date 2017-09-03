using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vendors.api.ViewModels.Deals
{
    public class DealsVm
    {
        /*
        public string name { get; set; }
        public string description { get; set; }
        public string startsOn { get; set; }
        public string endsOn { get; set; }
        public int count { get; set; }
        //public HttpPostedFileBase attatchment { get; set; }
        */

        public string attachment { get; set; }
        public int count { get; set; }
        public int? dataHeight { get; set; }
        public int? dataWidth { get; set; }
        public int? dataX { get; set; }
        public int? dataY { get; set; }
        public string description { get; set; }
        public DateTime endsOn { get; set; }
        public string fileName { get; set; }
        public int location { get; set; }
        public int category { get; set; }
        public string name { get; set; }
        public DateTime startsOn { get; set; }
        public int bannerId { get; set; }
        public float unitPrice { get; set; }
        public float discount { get; set; }
        public float sellingPrice { get; set; }
        public bool hasShipping { get; set; }
    }
}
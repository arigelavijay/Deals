using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace users.ViewModels.DealInfo
{
    public class dealInfoVm
    {
        [Required]
        public int dealId { get; set; }

        [Required]
        public int vendorId { get; set; }

        [Required]
        public int bannerId { get; set; }

        [Required]
        public int locationId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public DateTime startsOn { get; set; }
        public DateTime endsOn { get; set; }
        public int leftDays { get; set; }
        public int sold { get; set; }
        public float unitPrice { get; set; }
        public float discount { get; set; }
        public float sellingPrice { get; set; }
        public double hoursLeft { get; set; }
        public double rating { get; set; }
        public bool isRated { get; set; }
        public bool isReviewed { get; set; }
    }
}
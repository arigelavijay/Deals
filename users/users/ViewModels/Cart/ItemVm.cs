using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Cart
{
    public class ItemVm
    {
        public int dealId { get; set; }
        public string name { get; set; }
        public int bannerId { get; set; }
        public int locationId { get; set; }
        public int vendorId { get; set; }
        public string image { get; set; }
        public float unitPrice { get; set; }
        public float discount { get; set; }
        public float sellingPrice { get; set; }
        public int quantity { get; set; }
        public int count { get; set; }
        public int sold { get; set; }
        public DateTime endsOn { get; set; }
        public bool hasShipping { get; set; }
        public int categoryId { get; set; }
        public float promoDiscount { get; set; }

        public ItemVm()
        {
            this.promoDiscount = 0.0F;
        }
    }
}
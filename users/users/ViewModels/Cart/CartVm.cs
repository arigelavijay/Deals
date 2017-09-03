using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Cart
{
    public class CartVm
    {
        public List<ItemVm> ItemVm { get; set; }
        public float SubTotal { get; set; }
        public float GrandTotal { get; set; }
        public float Tax { get; set; }

        public CartVm()
        {
            this.ItemVm = new List<ItemVm>();
        }
    }
}
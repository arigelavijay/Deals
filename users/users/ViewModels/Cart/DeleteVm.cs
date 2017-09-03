using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Cart
{
    public class DeleteVm
    {
        public bool isGuest { get; set; }
        public int dealId { get; set; }
        public int userId { get; set; }
    }
}
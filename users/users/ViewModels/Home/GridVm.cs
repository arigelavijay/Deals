using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Home
{
    public class GridVm
    {
        public DealsVm main { get; set; }
        public List<DealsVm> other { get; set; }
        public List<Remaining> Remaining { get; set; }

        public GridVm()
        {
            this.other = new List<DealsVm>();
            this.Remaining = new List<Remaining>();
        }
    }

    public class Remaining
    {
        public List<DealsVm> RemainingList { get; set; }
        public int categoryId { get; set; }
        public string categoryName { get; set; }
        public Remaining()
        {
            this.RemainingList = new List<DealsVm>();
        }
    }
}
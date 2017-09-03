using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Track
{
    public class PageVm
    {
        public List<TrackVm> data { get; set; }
        public int total { get; set; }

        public PageVm()
        {
            this.data = new List<TrackVm>();
        }
    }
}
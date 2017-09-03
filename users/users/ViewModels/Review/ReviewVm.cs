using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace users.ViewModels.Review
{
    public class ReviewVm
    {
        public int userId { get; set; }
        public int dealId { get; set; }
        public string title { get; set; }
        public string review { get; set; }
        public int rating { get; set; }
        public string name { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RxShopyAdmin.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userreview
    {
        public int id { get; set; }
        public int userId { get; set; }
        public int dealId { get; set; }
        public string title { get; set; }
        public string review { get; set; }
        public int rating { get; set; }
        public string name { get; set; }
        public System.DateTime dateCreated { get; set; }
        public bool isActive { get; set; }
    
        public virtual deal deal { get; set; }
        public virtual user user { get; set; }
    }
}

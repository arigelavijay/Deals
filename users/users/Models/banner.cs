//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace users.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class banner
    {
        public banner()
        {
            this.deals = new HashSet<deal>();
            this.usercarts = new HashSet<usercart>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public System.DateTime createdDate { get; set; }
        public bool isActive { get; set; }
    
        public virtual ICollection<deal> deals { get; set; }
        public virtual ICollection<usercart> usercarts { get; set; }
    }
}

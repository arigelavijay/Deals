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
    
    public partial class location
    {
        public location()
        {
            this.deals = new HashSet<deal>();
            this.usercarts = new HashSet<usercart>();
        }
    
        public int locationId { get; set; }
        public string locationName { get; set; }
        public System.DateTime createddate { get; set; }
        public bool isActive { get; set; }
        public int parentId { get; set; }
    
        public virtual ICollection<deal> deals { get; set; }
        public virtual ICollection<usercart> usercarts { get; set; }
    }
}

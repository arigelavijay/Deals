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
    
    public partial class usercart
    {
        public int Id { get; set; }
        public int userId { get; set; }
        public int locationId { get; set; }
        public int bannerId { get; set; }
        public int vendorId { get; set; }
        public int quantity { get; set; }
        public System.DateTime dateCreated { get; set; }
        public System.DateTime dateModified { get; set; }
        public bool isActive { get; set; }
        public int dealId { get; set; }
        public bool isguest { get; set; }
    
        public virtual banner banner { get; set; }
        public virtual deal deal { get; set; }
        public virtual location location { get; set; }
        public virtual vendor vendor { get; set; }
    }
}

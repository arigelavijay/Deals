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
    
    public partial class state
    {
        public state()
        {
            this.addresses = new HashSet<address>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public System.DateTime createdOn { get; set; }
        public bool isactive { get; set; }
    
        public virtual ICollection<address> addresses { get; set; }
    }
}

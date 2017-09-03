﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class dbEntity : DbContext
    {
        public dbEntity()
            : base("name=dbEntity")
        {
            this.Configuration.ProxyCreationEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<address> addresses { get; set; }
        public DbSet<authentication_token> authentication_token { get; set; }
        public DbSet<banner> banners { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<deal> deals { get; set; }
        public DbSet<guestid> guestids { get; set; }
        public DbSet<location> locations { get; set; }
        public DbSet<orderitem> orderitems { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<promocode> promocodes { get; set; }
        public DbSet<shippingaddress> shippingaddresses { get; set; }
        public DbSet<state> states { get; set; }
        public DbSet<usercart> usercarts { get; set; }
        public DbSet<userhistory> userhistories { get; set; }
        public DbSet<userrating> userratings { get; set; }
        public DbSet<userreview> userreviews { get; set; }
        public DbSet<user> users { get; set; }
        public DbSet<vendor> vendors { get; set; }
        public DbSet<categoryconfig> categoryconfigs { get; set; }
        public DbSet<parentlocation> parentlocations { get; set; }
    
        public virtual ObjectResult<Nullable<int>> GetCurrentId()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("GetCurrentId");
        }
    }
}
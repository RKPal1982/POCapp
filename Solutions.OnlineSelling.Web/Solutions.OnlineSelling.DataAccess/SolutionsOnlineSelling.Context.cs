﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Solutions.OnlineSelling.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SolutionsOnlineSellingEntities : DbContext
    {
        public SolutionsOnlineSellingEntities()
            : base("name=SolutionsOnlineSellingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TblCategory> TblCategory { get; set; }
        public virtual DbSet<TblManufacturer> TblManufacturer { get; set; }
        public virtual DbSet<TblProduct> TblProduct { get; set; }
        public virtual DbSet<TblProductCategoryMapping> TblProductCategoryMapping { get; set; }
        public virtual DbSet<TblVendors> TblVendors { get; set; }
        public virtual DbSet<TblCalendar> TblCalendar { get; set; }
        public virtual DbSet<TBLCalendarGallery> TBLCalendarGallery { get; set; }
        public virtual DbSet<ChatMessageDetail> ChatMessageDetail { get; set; }
        public virtual DbSet<ChatPrivateMessageDetails> ChatPrivateMessageDetails { get; set; }
        public virtual DbSet<ChatPrivateMessageMaster> ChatPrivateMessageMaster { get; set; }
        public virtual DbSet<ChatUserDetail> ChatUserDetail { get; set; }
    }
}

//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class TblProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblProduct()
        {
            this.TblProductCategoryMapping = new HashSet<TblProductCategoryMapping>();
        }
    
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double ActualPrice { get; set; }
        public Nullable<double> WebPrice { get; set; }
        public int VendorId { get; set; }
        public int ManufacturerId { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public bool Active { get; set; }
    
        public virtual TblManufacturer TblManufacturer { get; set; }
        public virtual TblVendors TblVendors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TblProductCategoryMapping> TblProductCategoryMapping { get; set; }
    }
}

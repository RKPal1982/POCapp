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
    
    public partial class TblCalendar
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TblCalendar()
        {
            this.TBLCalendarGallery = new HashSet<TBLCalendarGallery>();
        }
    
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string EventDescription { get; set; }
        public System.DateTime EventStart { get; set; }
        public System.DateTime EventEnd { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBLCalendarGallery> TBLCalendarGallery { get; set; }
    }
}

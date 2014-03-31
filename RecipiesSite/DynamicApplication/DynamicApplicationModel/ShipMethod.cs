//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RecipiesModelNS
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipMethod
    {
        public ShipMethod()
        {
            this.PurchaseOrderHeaders = new HashSet<PurchaseOrderHeader>();
        }
    
        public int ShipMethodId { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> ShipBase { get; set; }
        public Nullable<decimal> ShipRate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
    
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeaders { get; set; }
    }
}
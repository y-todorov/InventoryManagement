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
    
    public partial class ProductHistory
    {
        public int ProductHistoryId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> UnitMeasureId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Nullable<decimal> UnitPrice { get; set; }
        public Nullable<double> UnitsInStock { get; set; }
        public Nullable<double> UnitsOnOrder { get; set; }
        public string Store { get; set; }
        public Nullable<double> ReorderLevel { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedByUser { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS_WebAPI_DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkFromHome
    {
        public long Id { get; set; }
        public int RefEmployeeId { get; set; }
        public System.DateTime Date { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int RefStatus { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int RefReason { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
        public virtual MasterDataValue MasterDataValue { get; set; }
        public virtual MasterDataValue MasterDataValue1 { get; set; }
    }
}

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
    
    public partial class Workflow
    {
        public long Id { get; set; }
        public long RefLeaveTransactionId { get; set; }
        public int RefApproverId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public int RefStatus { get; set; }
        public Nullable<int> RefCreatedBy { get; set; }
        public Nullable<int> RefModifiedBy { get; set; }
        public string ManagerComments { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
        public virtual EmployeeDetail EmployeeDetail1 { get; set; }
        public virtual EmployeeDetail EmployeeDetail2 { get; set; }
        public virtual EmployeeLeaveTransaction EmployeeLeaveTransaction { get; set; }
        public virtual MasterDataValue MasterDataValue { get; set; }
    }
}

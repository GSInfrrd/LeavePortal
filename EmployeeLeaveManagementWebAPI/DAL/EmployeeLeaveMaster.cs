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
    
    public partial class EmployeeLeaveMaster
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public Nullable<int> RewardedLeaveCount { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<int> SpentAdvanceLeave { get; set; }
        public Nullable<int> TakenLossOfPay { get; set; }
        public Nullable<int> EarnedCasualLeave { get; set; }
        public Nullable<int> TakenCompOff { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
        public virtual EmployeeDetail EmployeeDetail1 { get; set; }
    }
}

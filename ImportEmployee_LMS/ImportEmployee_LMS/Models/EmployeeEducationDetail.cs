//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImportEmployee_LMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EmployeeEducationDetail
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
    }
}
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
    
    public partial class UserAccount
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> Lastlogin { get; set; }
        public int RefEmployeeId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
    }
}

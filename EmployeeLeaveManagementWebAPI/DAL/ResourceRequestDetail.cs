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
    
    public partial class ResourceRequestDetail
    {
        public long Id { get; set; }
        public int RequestFromId { get; set; }
        public string RequestTo { get; set; }

        public int RequestToId { get; set; }
        public string ResourceRequestTitle { get; set; }
        public int NumberRequestedResources { get; set; }
        public string Skills { get; set; }
        public string Ticket { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
    
        public virtual EmployeeDetail EmployeeDetail { get; set; }
    }
}

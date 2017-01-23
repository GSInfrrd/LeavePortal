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
    
    public partial class MasterDataValue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MasterDataValue()
        {
            this.EmployeeContactDetails = new HashSet<EmployeeContactDetail>();
            this.EmployeeDetails = new HashSet<EmployeeDetail>();
            this.EmployeeDetails1 = new HashSet<EmployeeDetail>();
            this.EmployeeLeaveTransactions = new HashSet<EmployeeLeaveTransaction>();
            this.EmployeeLeaveTransactions1 = new HashSet<EmployeeLeaveTransaction>();
            this.EmployeeLeaveTransactionHistories = new HashSet<EmployeeLeaveTransactionHistory>();
            this.EmployeeLeaveTransactionHistories1 = new HashSet<EmployeeLeaveTransactionHistory>();
            this.LeaveMasters = new HashSet<LeaveMaster>();
            this.Notifications = new HashSet<Notification>();
            this.Workflows = new HashSet<Workflow>();
            this.WorkFromHomes = new HashSet<WorkFromHome>();
            this.WorkFromHomes1 = new HashSet<WorkFromHome>();
        }
    
        public int Id { get; set; }
        public int RefMasterType { get; set; }
        public string Value { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeContactDetail> EmployeeContactDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDetail> EmployeeDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeDetail> EmployeeDetails1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveTransaction> EmployeeLeaveTransactions { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveTransaction> EmployeeLeaveTransactions1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveTransactionHistory> EmployeeLeaveTransactionHistories { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeLeaveTransactionHistory> EmployeeLeaveTransactionHistories1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LeaveMaster> LeaveMasters { get; set; }
        public virtual MasterDataType MasterDataType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Workflow> Workflows { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkFromHome> WorkFromHomes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkFromHome> WorkFromHomes1 { get; set; }
    }
}

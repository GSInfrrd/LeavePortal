﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LeaveManagementSystemEntities : DbContext
    {
        public LeaveManagementSystemEntities()
            : base("name=LeaveManagementSystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Announcement> Announcements { get; set; }
        public virtual DbSet<EmailTemplateMapping> EmailTemplateMappings { get; set; }
        public virtual DbSet<EmailTemplateMaster> EmailTemplateMasters { get; set; }
        public virtual DbSet<EmployeeContactDetail> EmployeeContactDetails { get; set; }
        public virtual DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public virtual DbSet<EmployeeEducationDetail> EmployeeEducationDetails { get; set; }
        public virtual DbSet<EmployeeExperienceDetail> EmployeeExperienceDetails { get; set; }
        public virtual DbSet<EmployeeLeaveMaster> EmployeeLeaveMasters { get; set; }
        public virtual DbSet<EmployeeLeaveTransaction> EmployeeLeaveTransactions { get; set; }
        public virtual DbSet<EmployeeProjectDetail> EmployeeProjectDetails { get; set; }
        public virtual DbSet<EmployeeRewardedLeaveDetail> EmployeeRewardedLeaveDetails { get; set; }
        public virtual DbSet<EmployeeSkill> EmployeeSkills { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<LeaveMaster> LeaveMasters { get; set; }
        public virtual DbSet<MasterDataType> MasterDataTypes { get; set; }
        public virtual DbSet<MasterDataValue> MasterDataValues { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<ProjectMaster> ProjectMasters { get; set; }
        public virtual DbSet<ResourceRequestDetail> ResourceRequestDetails { get; set; }
        public virtual DbSet<UserAccount> UserAccounts { get; set; }
        public virtual DbSet<Workflow> Workflows { get; set; }
        public virtual DbSet<WorkflowHistory> WorkflowHistories { get; set; }
        public virtual DbSet<WorkFromHome> WorkFromHomes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Utils
{
    public enum LeaveType
    {
        [Description("Casual Leave")]
        CasualLeave = 8,
        [Description("Sick Leave")]
        SickLeave = 7,
        [Description("Compo Off")]
        CompOff = 25,
        [Description("Advance Leave")]
        AdvanceLeave = 26,
        [Description("LOP")]
        LOP = 27
    }
    public enum LeaveStatus
    {

        [Description("Planned")]
        Planned = 9,
        [Description("Submitted")]
        Submitted = 10,
        [Description("Rejected")]
        Rejected = 11,
        [Description("Approved")]
        Approved = 12,
        [Description("Reassigned")]
        Reassigned = 21
    }

    public enum EmployeeRole
    {

        [Description("Admin/HR")]
        HR = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("Employee")]
        Employee = 3
    }

    public enum ReportType
    {

        [Description("Applied Leaves")]
        AppliedLeaves = 1,
        [Description("Work From Home")]
        WorkfromHome = 2,
        [Description("Loss of Pay")]
        LossofPay = 3,
        [Description("Advanced Leaves")]
        AdvancedLeaves = 4,
        [Description("Comp Off")]
        CompOff = 5
    }
    public enum SkillsProjects
    {
        [Description("Skills")]
        Skills = 7
    }

    public enum AdvanceLeaveLimit
    {
        [Description("LeaveLimit")]
        limit = 8
    }

    public enum ResponseCodes
    {
        OK = 1000,
        DateAlreadyExists = 1001,
        NoLeaveBalance = 1002

    }

    public enum LOPLeaveLimit
    {
        [Description("LeaveLimit")]
        limit = 11
    }

    public enum ResourceRequestStatus
    {
        [Description("Requested")]
        Requested = 1,
        [Description("In Progress")]
        InProgress = 2,
        [Description("Partial Approval")]
        PartialApproval = 3,
        [Description("Approved")]
        Approved = 4
    }

    public enum MasterDataTypeEnum
    {
        [Description("Roles")]
        Role = 1,
        [Description("Projects")]
        ProjectName = 2,
        [Description("Leave Type")]
        LeaveType = 3,
        [Description("Leave Status")]
        LeaveStatus = 4,
        [Description("Hierarchy Level")]
        HierarchyLevel = 5,
        [Description("Work From Home Reasons")]
        WorkFromHomeReason = 6,
        [Description("Skills")]
        Skills = 7
    }

    public enum WorkFormHomeReasons
    {
        [Description("Personal Work")]
        Maintenance = 23,
        [Description("Commute Issue")]
        AvoidTravel = 24,
        [Description("Appointment with Doctors")]
        AppointmentWithDoctor = 28,
        [Description("Others")]
        Others = 29
    }  
    
    public enum TransactionType
    {
        [Description("Debit")]
        Debit = 2052,
        [Description("Credit")]
        Credit = 2053
    }

    public enum ActionsForMail
    {
        [Description("Leave Applied")]
        ApplyLeave = 151,
        [Description("Leave Approved")]
        ApproveLeave = 152,
        [Description("Leave Rejected")]
        RejectLeave = 153,
        [Description("Leave Reassigned")]
        ReassignLeave = 154,
        [Description("Apply WFH")]
        ApplyWFH = 155,
        [Description("Apply Compoff")]
        ApplyCompoff = 156,
        [Description("Approve Compoff")]
        ApproveCompoff = 157,
        [Description("Add Resource Request")]
        AddResourceRequest = 158,
        [Description("Resource Request Update")]
        ResourceRequestUpdate = 159,

    }
}

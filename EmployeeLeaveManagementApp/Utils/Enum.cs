using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Utils
{
    public enum LeaveType
    {

        [Description("Casual Leave")]
        CasualLeave = 8,
        [Description("Sick Leave")]
        SickLeave = 7,
        [Description("Comp Off")]
        CompoOff = 25,
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
        Reassigned = 21,
        [Description("Canceled")]
        Canceled = 20

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

    public enum HierarchyLevel
    {
        [Description("Level-0")]
        Level0 = 13,
        [Description("Level-1")]
        Level1 = 15,
        [Description("Level-2")]
        Level2 = 16,
        [Description("Level-3")]
        Level3 = 17,
        [Description("Level-4")]
        Level4 = 18,
        [Description("Level-5")]
        Level5 = 19
    }

    public enum WorkFormHomeReasons
    {
        [Description("Maintenance at home")]
        Maintenance = 23,
        [Description("To avoid Travel")]
        AvoidTravel = 24,
    }

    public enum NotificationTypes
    {
        [Description("LeaveNotification")]
        LeaveNotification = 27,

    }

    public enum MasterDataType
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

    public enum ResourceRequestStatus
    {
        [Description("In progress")]
        InProgress = 1,
        [Description("Approved")]
        Approved = 2
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
    public enum ResponseCodes
    {
        OK = 1000,
        DateAlreadyExists = 1001,
        NoLeaveBalance = 1002

    }
}

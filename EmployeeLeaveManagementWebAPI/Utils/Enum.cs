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
        AdvanceLeave = 26
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
        Approved = 12
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
}

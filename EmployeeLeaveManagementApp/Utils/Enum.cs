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
        [Description("Sick Leave")]
        SickLeave = 61,
        [Description("Casual Leave")]
        CasualLeave = 62,
        [Description("Comp Off")]
        CompoOff = 63,
        [Description("Advance Leave")]
        AdvanceLeave = 64,
        [Description("LOP")]
        LOP = 65
    }

    public enum LeaveStatus
    {

        [Description("Planned")]
        Planned = 71,
        [Description("Submitted")]
        Submitted = 72,
        [Description("Rejected")]
        Rejected = 73,
        [Description("Approved")]
        Approved = 74,
        [Description("Cancelled")]
        Canceled = 75,
        [Description("Reassigned")]
        Reassigned = 76
    }

    public enum EmployeeRole
    {

        [Description("Admin/HR")]
        HR = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("Senior Software Engineer")]
        SSE = 3,
        [Description("CEO")]
        CEO = 4,
        [Description("Team Lead")]
        TeamLead = 5,
        [Description("Software Engineer")]
        SoftwareEngineer = 6,
        [Description("Quality Analyst")]
        QA = 7,
        [Description("Dev Lead")]
        DevLead = 8,
        [Description("Test Engineer")]
        TestEngineer = 9,
        [Description("Senior Test Engineer")]
        SeniorTestEngineer = 10,
        [Description("Test Lead")]
        TestLead = 11,
        [Description("Tech Lead")]
        TechLead = 12,
        [Description("Technical Architect")]
        TechnicalArchitect = 13,
        [Description("Associate Tech Architect")]
        AssociateTechArchitect = 14,
        [Description("Project Manager")]
        ProjectManager = 15,
        [Description("Senior HR")]
        SeniorHR = 16,
        [Description("CTO")]
        CTO = 17,
        [Description("COO")]
        COO = 18,
        [Description("Finance")]
        Finance = 19,
        [Description("UI Designer")]
        UIDesigner = 20,
        [Description("Senior UI Designer")]
        SeniorUIDesigner = 21,
        [Description("Sales")]
        Sales = 22,
        [Description("Interns/Fresher")]
        InternOrFresher = 23
    }

    public enum HierarchyLevel
    {
        [Description("Level-0")]
        Level0 = 81,
        [Description("Level-1")]
        Level1 = 82,
        [Description("Level-2")]
        Level2 = 83,
        [Description("Level-3")]
        Level3 = 84,
        [Description("Level-4")]
        Level4 = 85,
        [Description("Level-5")]
        Level5 = 86
    }

    public enum WorkFormHomeReasons
    {
        [Description("Personal Work")]
        Maintenance = 91,
        [Description("Commute Issue")]
        AvoidTravel = 92,
        [Description("Appointment with Doctors")]
        AppointmentWithDoctor = 93,
        [Description("Others")]
        Others = 94
    }


    public enum NotificationTypes
    {
        [Description("LeaveNotification")]
        LeaveNotification = 161,

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
        [Description("In Progress")]
        InProgress = 183,
        [Description("Approved")]
        Approved = 184,
        [Description("Partial Approval")]
        PartialApproval = 185,
        [Description("Requested")]
        Requested = 186,
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
    public enum TransactionType
    {
        [Description("Debit")]
        Debit = 171,
        [Description("Credit")]
        Credit = 172
    }

    public enum EmployeeType
    {
        [Description("Fresher")]
        Fresher = 151,
        [Description("Experienced")]
        Experienced = 152
    }

    public enum ProfileType
    {
        [Description("CEO")]
        CEO = 200,
        [Description("Admin/HR")]
        HR = 201,
        [Description("Manager")]
        Manager = 202,
        [Description("Employee")]
        Employee = 203
    }
}

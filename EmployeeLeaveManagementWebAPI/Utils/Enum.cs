﻿using System.ComponentModel;

namespace LMS_WebAPI_Utils
{
    public enum LeaveType
    {
        [Description("Casual Leave")]
        CasualLeave = 62,
        [Description("Sick Leave")]
        SickLeave = 61,
        [Description("Comp Off")]
        CompOff = 63,
        [Description("Advance Leave")]
        AdvanceLeave = 64,
        [Description("LOP")]
        LOP = 65,
        [Description("Reward Leave")]
        RewardLeave = 66,
        [Description("Earned Leave")]
        EarnedLeave = 67,
        [Description("Cancelled Leave")]
        CancelledLeave = 68
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
        Cancelled = 75,
        [Description("Reassigned")]
        Reassigned = 76,
        
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
        [Description("Intern/Fresher")]
        InternOrFresher = 23,
        [Description("SuperAdmin")]
        SuperAdmin = 24,
        [Description("Custom Role")]
        CustomRole = 25
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

    public enum ResourceRequestStatus
    {
        [Description("In Progress")]
        InProgress = 183,
        [Description("Approved")]
        Approved = 184,
        [Description("Partial Approval")]
        PartialApproval = 185,
        [Description("Requested")]
        Requested = 186
    }

    public enum Environment
    {
        [Description("Prod")]
        Prod = 1183,
        [Description("QA")]
        QA = 1184,
        [Description("Dev")]
        Dev = 1185,
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
        Skills = 7,
        [Description("EmployeeType")]
        EmployeeType = 8,
        [Description("NotificationType")]
        NotificationType = 9,
        [Description("TransactionType")]
        TransactionType = 10,
        [Description("LOPLimit")]
        LOPLimit = 11,
        [Description("AdvanceLeaveLimit")]
        AdvanceLeaveLimit = 12,
        [Description("ResourceRequestStatus")]
        ResourceRequestStatus = 13,
        [Description("CreditLeaveLimitMonthly")]
        CreditLeaveLimitMonthly = 17,
        [Description("Relationship")]
        Relationship = 19,
        [Description("BloodGroup")]
        BloodGroup = 20
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
    
    public enum TransactionType
    {
        [Description("Debit")]
        Debit = 171,
        [Description("Credit")]
        Credit = 172
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
        [Description("WorkFromHome Applied")]
        WorkFromHome = 155,
        [Description("Apply Compoff")]
        ApplyCompoff = 156,
        [Description("Approve Compoff")]
        ApproveCompoff = 157,
        [Description("Add Resource Request")]
        AddResourceRequest = 158,
        [Description("Resource Request Update")]
        ResourceRequestUpdate = 159,
        [Description("Leave Reward")]
        RewardLeave = 160,
        [Description("Leave Cancelled")]
        CancelLeave = 161,
        [Description("Registered Successfully")]
        AddNewEmployee = 162,
        [Description("Password Changed")]
        ChangePassword = 163


    }

    public enum EmployeeGender
    {
        [Description("Male")]
        Male = 291,
        [Description("Female")]
        Female = 292
    }

    public enum NotificationTypes
    {
        [Description("Submit Leave Request")]
        SubmitLeaveRequest = 161,
        [Description("Cancel Leave")]
        CancelLeave = 162,
        [Description("Submit Resource Request")]
        SubmitResourceRequest = 163,
        [Description("Submit Resource Request Response")]
        SubmitResourceRequestResponse = 164,
        [Description("Reward Leave")]
        RewardLeave = 165,
        [Description("Work from Home")]
        WorkfromHome = 166,
        [Description("Sick Leave")]
        SickLeave = 167,
        [Description("Approve Leave")]
        ApproveLeave = 168,
        [Description("Reject Leave")]
        RejectLeave = 169,
        [Description("Reassign Leave")]
        ReassignLeave = 170,
    }

    public enum NotificationStatus
    {
        [Description("Active Notification")]
        Active = 1
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

    public enum ProfileType
    {
        [Description("CEO")]
        CEO = 200,
       [Description("Admin/HR")]
        HR = 201,
        [Description("Manager")]
        Manager = 202,
        [Description("Employee")]
        Employee = 203,
        [Description("SuperAdmin")]
        SuperAdmin = 204
    }

    public enum GraduationDegree
    {
        [Description("B.E/BTech")]
        BTech = 211,
        [Description("MTech")]
        MTech = 212,
        [Description("BCA")]
        BCA = 213,
        [Description("MCA")]
        MCA = 214,
        [Description("B.Sc")]
        BSc = 215,
        [Description("MBA")]
        MBA = 216,
        [Description("M.S")]
        MS = 217,
    }

    public enum Specialization
    {
        [Description("CSE")]
        CSE = 231,
        [Description("EEE")]
        EEE = 232,
        [Description("ECE")]
        ECE = 233,
        [Description("I.T")]
        IT = 234,

    }
}

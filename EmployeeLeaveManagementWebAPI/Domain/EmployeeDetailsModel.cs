using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_Domain
{
    public class EmployeeDetailsModel
    {
        public EmployeeDetailsModel()
        {
            this.Announcements = new List<Announcement>();
            this.leaveDetails = new LeaveReportModel();
            this.EmployeeEducationDetails = new List<EmployeeEducationDetails>();
            this.EmployeeExperienceDetails = new List<EmployeeExperienceDetails>();
            this.Projects = new List<ProjectsList>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProjectName { get; set; }
        public String RoleName { get; set; }
        public int? TotalLeaveCount { get; set; }
        public int? TotalCasualLeave { get; set; }
        public int? TotalWorkFromHome { get; set; }

        public int? TotalLOPLImit { get; set; }
        public int? TotalAdvanceLeaveTotake { get; set; }
        public double TotalApplied { get; set; }
        public double TotalSpent { get; set; }
        public string ManagerName { get; set; }
        public string MangerEmail { get; set; }
        public int? ManagerId { get; set; }
        public System.DateTime DateOfJoining { get; set; }

        public List<Announcement> Announcements { get; set; }

        public LeaveReportModel leaveDetails { get; set; }

        public List<EmployeeEducationDetails> EmployeeEducationDetails { get; set; }

        public List<EmployeeExperienceDetails> EmployeeExperienceDetails { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }

        public string DateOfBirthAsString { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string Bio { get; set; }
        public int RefRoleId { get; set; }
        public List<EmployeeSkillDetails> Skills { get; set; }
        public int EmployeeNumber { get; set; }
        public int RefHierarchyLevel { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string GooglePlusLink { get; set; }

        public List<ProjectsList> Projects { get; set; }

        public int EmployeeType { get; set; }

        public int LOPRemaining { get; set; }
        public int? CompOffTaken { get; set; }

        public int RefProfileType { get; set; }

    }

    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CarouselContent { get; set; }
        public string ImagePath { get; set; }
    }

    public class EmployeeEducationDetails
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string TimePeriod { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class EmployeeExperienceDetails
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public string TimePeriod { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string CompanyLogo { get; set; }

    }

    public class EmployeeSkillDetails
    {
        public int Id { get; set; }

        public int RefEmployeeId { get; set; }
        public string SkillName { get; set; }
        public bool IsSelected { get; set; }

    }

    public class MasterDataModel
    {
        public int Id { get; set; }

        public int RefMasterType { get; set; }
        public string Value { get; set; }

    }

    public class ProjectsList
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set;}
    }

    public class TeamMembers
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string FirstName { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
    }

}

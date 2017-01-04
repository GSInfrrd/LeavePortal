using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPP_Domain
{
   public class EmployeeDetailsModel
    {
        public EmployeeDetailsModel()
        {
            this.Announcements = new List<Announcement>();
            this.LeaveDetails = new LeaveReportModel();
            this.EmployeeEducationDetails = new List<EmployeeEducationDetails>();
            this.EmployeeExperienceDetails = new List<EmployeeExperienceDetails>();
            this.Skills = new List<EmployeeSkillDetails>();
            this.Colors = new List<string>();
        }
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ProjectName { get; set; }
        public String RoleName { get; set; }
        public int? TotalLeaveCount { get; set; }
        public double TotalApplied { get; set; }
        public double TotalSpent { get; set; }
        public int TotalLeft { get; set; }
        public string ManagerName { get; set; }
        public string MangerEmail { get; set; }
        public System.DateTime DateOfJoining { get; set; }
        public List<string> Colors { get; set; }
        public List<Announcement> Announcements { get; set; }

        public LeaveReportModel LeaveDetails { get; set; }

        public List<EmployeeEducationDetails> EmployeeEducationDetails { get; set; }

        public List<EmployeeExperienceDetails> EmployeeExperienceDetails { get; set; }

        public string City { get; set; }
        public string Country { get; set; }
        public string Telephone { get; set; }
        public string DateOfBirthAsString { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int? ManagerId { get; set; }
        public string ImagePath { get; set; }
        public string Bio { get; set; }
        public int RefRoleId { get; set; }

        public List<EmployeeSkillDetails> Skills { get; set; }
        public int EmployeeNumber { get; set; }
        public int RefHierarchyLevel { get; set; }
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

    }

    public class EmployeeExperienceDetails
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public string TimePeriod { get; set; }

    }

    public class EmployeeSkillDetails
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public string RefEmployeeId { get; set; }

    }
}

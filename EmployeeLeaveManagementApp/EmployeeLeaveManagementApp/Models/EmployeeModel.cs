using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeLeaveManagementApp.Models
{
    public class EmployeeModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Imagepath { get; set; }
        public int RoleId { get; set; }
        public List<string> Skills { get; set; }

        public int EmployeeNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int RefHierarchyLevel { get; set; }
        public string Telephone { get; set; }
        public string ManagerName { get; set; }
        public DateTime DateOfJoining { get; set; }

        public List<EmployeeEducationModel> EmployeeEducation { get; set; }
        public List<EmployeeExperienceModel> EmployeeExperience{ get; set; }
    }

    public class EmployeeEducationModel
    {
        public string Institution { get; set; }
        public string Graduation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class EmployeeExperienceModel
    {
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
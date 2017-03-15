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
        public List<EmployeeSkillsModel> Skills { get; set; }

        public int EmployeeNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int RefHierarchyLevel { get; set; }
        public string Telephone { get; set; }
        public string ManagerName { get; set; }

        public string Gender { get; set; }
        public string PassportNumber { get; set; }
        public string BloodGroup { get; set; }
        public string InfrrdEmailId { get; set; }
        public int EmployeeConractType { get; set; }
        public DateTime DateOfConfirmation { get; set; }
        public DateTime DateOfJoining { get; set; }

        public List<EmployeeEducationModel> EmployeeEducation { get; set; }

        public List<EmployeeWorkLocationDetail> EmployeeWorkLocationDetail { get; set; }

        public List<EmployeePermanentAddressDetail> EmployeePermanentAddressDetail { get; set; }

        public List<EmployeeCurrentAddressDetail> EmployeeCurrentAddressDetail { get; set; }

        public List<EmployeeEmergencyContactDetail> EmployeeEmergencyContactDetail { get; set; }
        public List<EmployeeExperienceModel> EmployeeExperience{ get; set; }

        public List<ProjectsList> Projects { get; set; }

        public int EmployeeType { get; set;}
        public int? ManagerId { get; set; }
        public int RefProfileType { get; set; }
    }

    public class EmployeeEducationModel
    {
        public string Institution { get; set; }
        public string Graduation { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Specialization { get; set; }
    }

    public class EmployeeExperienceModel
    {
        public string CompanyName { get; set; }
        public string Role { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }

    public class EmployeeSkillsModel
    {
        public string Id { get; set; }
        public string RefEmployeeId { get; set; }
        public string SkillName { get; set; }
    }

    public class ProjectsList
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
    }

    public class EmployeeWorkLocationDetail
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public string Facility { get; set; }
        public int IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> RefCreatedBy { get; set; }
        public Nullable<int> RefModifiedBy { get; set; }
    }

    public class EmployeePermanentAddressDetail
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> RefCreatedBy { get; set; }
        public Nullable<int> RefModifiedBy { get; set; }
    }

    public class EmployeeCurrentAddressDetail
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> RefCreatedBy { get; set; }
        public Nullable<int> RefModifiedBy { get; set; }
    }


    public class EmployeeEmergencyContactDetail
    {
        public int Id { get; set; }
        public int RefEmployeeId { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string Telephone { get; set; }

        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int IsActive { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> RefCreatedBy { get; set; }
        public Nullable<int> RefModifiedBy { get; set; }
    }

}
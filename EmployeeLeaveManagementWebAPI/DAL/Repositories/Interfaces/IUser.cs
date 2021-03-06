﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Utils;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
 public interface IUser
    {
        UserAccount  GetUser(string emailId, string password);

        EmployeeDetailsModel GetUserDetails(int UserEmpId);

        List<EmployeeDetail> GetTeamMembers(int EmpId);
        List<Announcement> GetAnnouncements();
        LeaveReportModel GetLeaveReportDetails(int year, int employeeId = 0,int leaveType=0);
        EmployeeDetail GetUserProfileDetails(int employeeId, out List<MasterDataModel> skills,out List<ProjectsList> projects);
        bool EditEmployeeDetails(EmployeeDetailsModel model);

        bool EditEmployeeEmergencyContactDetails(LMS_WebAPI_Domain.EmployeeEmergencyContactDetail model);

        bool EditEmployeeCurrentAddressDetails(LMS_WebAPI_Domain.EmployeeCurrentAddressDetail model);
        bool EditEmployeePermanentAddressDetails(LMS_WebAPI_Domain.EmployeePermanentAddressDetail model);
        bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails,int employeeId);

        bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId);

        bool EditEmployeeSkills(List<EmployeeSkillDetails> skills, int employeeId);
        string getUserProfileImage(int employeeId);
        bool CheckEmployeePassword(int employeeId, string currentPassword);
        bool UpdatePassword(int employeeId, string newPassword);
    }
}

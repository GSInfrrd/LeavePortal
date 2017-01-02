using System;
using System.Collections.Generic;
using System.Linq;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_ServiceHelpers
{
    public class UserManagement
    {
        private IUser user = new UserRepository();
        public UserAccountModel GetUser(string UserName, string password)
        {
            try
            {
                var VerifiedUser = new UserAccountModel();
                var userData = user.GetUser(UserName, password);
                if (null != userData)
                {
                    VerifiedUser.UserName = userData.EmployeeDetail.FirstName+" "+userData.EmployeeDetail.LastName;
                    VerifiedUser.Password = userData.Password;
                    VerifiedUser.Lastlogin = userData.Lastlogin;
                    VerifiedUser.RefEmployeeId = userData.RefEmployeeId;
                    VerifiedUser.CreatedDate = userData.CreatedDate;
                    VerifiedUser.RefRoleId = userData.EmployeeDetail.RefRoleId;
                    VerifiedUser.Imagepath = userData.EmployeeDetail.ImagePath;
                    VerifiedUser.DateOfJoining =Convert.ToDateTime(userData.EmployeeDetail.DateOfJoining);

                }
                return VerifiedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeDetailsModel GetEmployeeDatailsForDashboard(int EmpId,int year)
        {
            try
            {
                var userData = user.GetUserDetails(EmpId);
                var announcements = user.GetAnnouncements();
                var leaveReportDetails = user.GetLeaveReportDetails(year,EmpId);
                var VerifiedUser = new EmployeeDetailsModel();
                VerifiedUser.leaveDetails = leaveReportDetails;
                foreach (var item in announcements)
                {
                    LMS_WebAPI_Domain.Announcement announceItem = new LMS_WebAPI_Domain.Announcement();
                    announceItem.ImagePath = item.ImagePath;
                    announceItem.CarouselContent = item.CarouselContent;
                    announceItem.Title = item.Title;
                    announceItem.Id = item.Id;
                    VerifiedUser.Announcements.Add(announceItem);
                }
                if (null != userData)
                {
                    VerifiedUser.RoleName = userData.RoleName;
                    VerifiedUser.TotalLeaveCount = userData.TotalLeaveCount;
                    VerifiedUser.TotalApplied = userData.TotalApplied;
                    VerifiedUser.TotalSpent = userData.TotalSpent;
                    VerifiedUser.ManagerName = userData.ManagerName;
                    VerifiedUser.MangerEmail = userData.ManagerEmailId;
                    VerifiedUser.ManagerId = userData.ManagerId;
                    VerifiedUser.ProjectName = userData.ProjectName;
                    VerifiedUser.DateOfJoining = Convert.ToDateTime(userData.DateOfJoining);
                }
                return VerifiedUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeDetailsModel GetUserProfileDetails(int EmpId)
        {
            try
            {
                var userData = user.GetUserProfileDetails(EmpId);
                var profileDetails = new EmployeeDetailsModel();
                List<EmployeeEducationDetails> EEdetails = new List<EmployeeEducationDetails>();
                profileDetails.FirstName = userData.FirstName;
                profileDetails.LastName = userData.LastName;
                profileDetails.City = userData.City;
                profileDetails.Country = userData.Country;
                profileDetails.Telephone = userData.PhoneNumber;
                profileDetails.RefRoleId = userData.RefRoleId;
                profileDetails.RoleName = userData.MasterDataValue.Value;
                profileDetails.DateOfBirth = userData.DateOfBirth;
                profileDetails.DateOfBirthAsString = userData.DateOfBirth.ToString("MMM dd");
                profileDetails.Email = userData.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == EmpId).UserName;
                profileDetails.ImagePath = userData.ImagePath;
                profileDetails.Bio = userData.Bio;
                profileDetails.Id = userData.Id;
                foreach (var item in userData.EmployeeEducationDetails)
                {
                    var edet = new EmployeeEducationDetails();
                    edet.Degree = item.Degree;
                    edet.Institution = item.Institution;
                    edet.FromDate = item.FromDate;
                    edet.ToDate = item.ToDate;
                    edet.Id = item.Id;
                    edet.TimePeriod = item.FromDate.ToString("MMMM yyyy")+"~" +item.ToDate.ToString("MMMM yyyy");
                    EEdetails.Add(edet);
                }
                profileDetails.EmployeeEducationDetails = EEdetails;
                List<EmployeeExperienceDetails> EExpdetails = new List<EmployeeExperienceDetails>();
                foreach (var item in userData.EmployeeExperienceDetails)
                {
                    var exdet = new EmployeeExperienceDetails();
                    exdet.Id = item.Id;
                    exdet.Company = item.CompanyName;
                    exdet.Role = item.Role;
                    exdet.FromDate = item.FromDate;
                    exdet.ToDate = item.ToDate;
                    exdet.TimePeriod = item.FromDate.ToString("MMMM yyyy") + "~" + item.ToDate.ToString("MMMM yyyy");
                    EExpdetails.Add(exdet);
                }
                profileDetails.EmployeeExperienceDetails = EExpdetails;
                List<EmployeeSkillDetails> employeeSkills = new List<EmployeeSkillDetails>();
                foreach (var item in userData.EmployeeSkills)
                {;
                    var employeeSkill = new EmployeeSkillDetails();
                    employeeSkill.SkillName = item.Skill;
                    employeeSkill.RefEmployeeId = item.RefEmployeeId;
                    employeeSkill.Id = item.Id;
                    employeeSkills.Add(employeeSkill);

                }
                profileDetails.Skills = employeeSkills;


                return profileDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
}
    }
}

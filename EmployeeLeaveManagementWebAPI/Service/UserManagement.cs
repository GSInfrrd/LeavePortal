using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Utils;
using LMS_WebAPI_DAL;
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
                var leaveReportDetails = user.GetLeaveReportDetails(EmpId,year);
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
                    VerifiedUser.TotalCountTaken = userData.TotalCountTaken;
                    VerifiedUser.TotalLeaveCount = userData.TotalLeaveCount;
                    VerifiedUser.ManagerName = userData.ManagerName;
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
                profileDetails.RoleName = "S/W Engineer";
                profileDetails.DateOfBirth = userData.DateOfBirth.ToString("MMM dd,yyyy");
                profileDetails.Email = userData.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == EmpId).UserName;
                profileDetails.ImagePath = userData.ImagePath;
                profileDetails.Bio = userData.Bio;
                foreach (var item in userData.EmployeeEducationDetails)
                {
                    var edet = new EmployeeEducationDetails();
                    edet.Degree = item.Degree;
                    edet.Institution = item.Institution;
                    edet.TimePeriod = item.FromDate.ToString("MMMM yyyy")+"~" +item.ToDate.ToString("MMMM yyyy");
                    EEdetails.Add(edet);
                }
                profileDetails.EmployeeEducationDetails = EEdetails;
                List<EmployeeExperienceDetails> EExpdetails = new List<EmployeeExperienceDetails>();
                foreach (var item in userData.EmployeeExperienceDetails)
                {
                    var exdet = new EmployeeExperienceDetails();
                    exdet.Company = item.CompanyName;
                    exdet.Role = item.Role;
                    exdet.TimePeriod = item.FromDate.ToString("MMMM yyyy") + "~" + item.ToDate.ToString("MMMM yyyy");
                    EExpdetails.Add(exdet);
                }
                profileDetails.EmployeeExperienceDetails = EExpdetails;
                return profileDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
}
    }
}

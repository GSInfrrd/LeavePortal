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
                    VerifiedUser.UserName = userData.EmployeeDetail.Name;
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

        public EmployeeDetailsModel GetEmployeeDatailsForDashboard(int EmpId)
        {
            try
            {
                var userData = user.GetUserDetails(EmpId);
                var announcements = user.GetAnnouncements();
                var VerifiedUser = new EmployeeDetailsModel();
               foreach(var item in announcements)
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
    }
}

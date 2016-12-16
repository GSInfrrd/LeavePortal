using System;
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

        EmployeeCommon GetUserDetails(int UserEmpId);

        List<Announcement> GetAnnouncements();
        LeaveReportModel GetLeaveReportDetails(int employeeId,int year);

        EmployeeDetail GetUserProfileDetails(int employeeId);
    }
}

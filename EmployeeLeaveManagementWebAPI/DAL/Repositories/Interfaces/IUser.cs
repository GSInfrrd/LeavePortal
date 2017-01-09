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
        LeaveReportModel GetLeaveReportDetails(int year, int employeeId = 0);

        EmployeeDetail GetUserProfileDetails(int employeeId);
        bool EditEmployeeDetails(EmployeeDetailsModel model);
        bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails,int employeeId);

        bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId);
    }
}

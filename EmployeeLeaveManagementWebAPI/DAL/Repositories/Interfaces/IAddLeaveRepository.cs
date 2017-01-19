using Domain;
using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
  public  interface IAddLeaveRepository
    {
        List<string> GetLeaveType();
        bool InsertEmployeeLeaveDetails(int empId,int leaveType, string fromDate, string toDate, string comments, double workingDays);

        bool SubmitLeaveForApproval(int id);

        bool DeleteLeaveRequest(int id);

        EmployeeDetail CheckLeaveAvailability(int employeeId,out List<Holiday> holidayList, out int advanceLeaveLimit, out int lopLeaveLimit);

        RewardLeaveModel GetRewardLeaveModelDetails();

        bool Rewardleave(RewardLeaveModel model);
    }
}

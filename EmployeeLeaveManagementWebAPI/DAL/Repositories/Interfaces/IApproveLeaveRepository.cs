using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
    public interface IApproveLeaveRepository
    {
        List<ApproveLeaveModel> GetApproveLeave(int id);
        List<EmployeeDetailsModel> GetAllManagers(int id, int st);
        bool TakeActionOnEmployeeLeave(int Leaveid, string Leavecomments, string Leavestatus, int Approverid);
    }
}

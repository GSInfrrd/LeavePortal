using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using System.Collections.Generic;

namespace LMS_WebAPI_ServiceHelpers
{
    public  class ApproveLeaveManagement
    {
        private IApproveLeaveRepository EmployeeLeaves = new ApproveLeaveRepository();

        
        public List<EmployeeDetailsModel> GetAllManagers(int id , int st)
        {
            var ManagerDetails = EmployeeLeaves.GetAllManagers(id,st);
            // var leaveType = addLeaveRepository.GetLeaveType();
            // var retResult = ToModel(EmployeeLeaveTransaction);

            return ManagerDetails;
        }
        public List<ApproveLeaveModel> GetApproveLeave(int id)
        {
            var ApproveLeaves = EmployeeLeaves.GetApproveLeave(id);
            // var leaveType = addLeaveRepository.GetLeaveType();
            // var retResult = ToModel(EmployeeLeaveTransaction);

            return ApproveLeaves;
        }

        

        public bool ApproveEmployeeLeave(int id, string comments, int st , int apid)
        {
            var ApproveLeaves = EmployeeLeaves.ApproveEmployeeLeave(id,comments,st,apid);
            // var leaveType = addLeaveRepository.GetLeaveType();
            // var retResult = ToModel(EmployeeLeaveTransaction);

            return ApproveLeaves;
        }
    }
}

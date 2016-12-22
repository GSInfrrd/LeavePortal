using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_ServiceHelpers
{
   public  class ApproveLeaveManagement
    {
        private IApproveLeaveRepository EmployeeLeaves = new ApproveLeaveRepository();
      
        public List<ApproveLeaveModel> GetApproveLeave(int id)
        {
            var ApproveLeaves = EmployeeLeaves.GetApproveLeave(id);
            // var leaveType = addLeaveRepository.GetLeaveType();
            // var retResult = ToModel(EmployeeLeaveTransaction);

            return ApproveLeaves;
        }

        public bool ApproveEmployeeLeave(int id, string comments, int st)
        {
            var ApproveLeaves = EmployeeLeaves.ApproveEmployeeLeave(id,comments,st);
            // var leaveType = addLeaveRepository.GetLeaveType();
            // var retResult = ToModel(EmployeeLeaveTransaction);

            return ApproveLeaves;
        }
    }
}

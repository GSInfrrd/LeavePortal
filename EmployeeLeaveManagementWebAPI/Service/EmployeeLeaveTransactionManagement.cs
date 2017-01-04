using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_DAL;
using LMS_WebAPI_Utils;

namespace LMS_WebAPI_ServiceHelpers
{
    public class EmployeeLeaveTransactionManagement
    {
        private IEmployeeLeaveTransaction EmployeeLeaves  = new EmployeeLeaveTransactionRepository();
        private IAddLeaveRepository addLeaveRepository = new AddLeaveRepository();
        public List<EmployeeLeaveTransactionModel> GetEmployeeLeaveTransaction(int id,int leaveType = 0)
        {
            var EmployeeLeaveTransaction = EmployeeLeaves.GetEmployeeLeaveTransaction(id,leaveType);
            return EmployeeLeaveTransaction;
        }

       

        public bool InsertEmployeeLeaveDetails(int empId,int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            var insertEmployeeDetails = addLeaveRepository.InsertEmployeeLeaveDetails(empId,leaveType, fromDate, toDate, comments, workingDays);

            return insertEmployeeDetails;
        }

        public bool SubmitLeaveForApproval(int id)
        {
            var submitLeaveForApprovalDetails = addLeaveRepository.SubmitLeaveForApproval(id);

            return submitLeaveForApprovalDetails;
        }

        public bool DeleteLeaveRequest(int id)
        {
            var submitLeaveForApprovalDetails = addLeaveRepository.DeleteLeaveRequest(id);

            return submitLeaveForApprovalDetails;
        }
    }
}

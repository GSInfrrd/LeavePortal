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
        public List<EmployeeLeaveTransactionModel> GetEmployeeLeaveTransaction(int id)
        {
            var EmployeeLeaveTransaction = EmployeeLeaves.GetEmployeeLeaveTransaction(id);
            // var leaveType = addLeaveRepository.GetLeaveType();
           // var retResult = ToModel(EmployeeLeaveTransaction);
   
            return EmployeeLeaveTransaction;
        }

        //private List<EmployeeLeaveTransactionModel> ToModel(List<EmployeeLeaveTransaction> employeeLeaveTransaction)
        //{
        //    List<EmployeeLeaveTransactionModel> Empres = new List<EmployeeLeaveTransactionModel>();
        //    try
        //    {
                
        //        foreach (var m in employeeLeaveTransaction)
        //        {
        //            var newTrans = new EmployeeLeaveTransactionModel();
        //            newTrans.Id = m.Id;
        //            newTrans.RefEmployeeId = m.RefEmployeeId;
        //            newTrans.FromDate = m.FromDate;
        //            newTrans.ToDate = m.ToDate;
        //            newTrans.CreatedDate = m.CreatedDate;
        //            newTrans.RefStatus = m.RefStatus;
        //            newTrans.NumberOfWorkingDays = m.NumberOfWorkingDays;
        //            newTrans.RefLeaveType = m.RefLeaveType;
        //            newTrans.EmployeeComment = m.EmployeeComment;
        //            newTrans.LeaveTypeName = m.MasterDataValue.Value;
        //            newTrans.StatusName = m.MasterDataValue1.Value;
        //            //newTrans.ManagerComments = m.ManagerComments;
        //            newTrans.ModifiedDate = m.ModifiedDate;
        //            Empres.Add(newTrans);


        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return Empres;



        //}

        public bool InsertEmployeeLeaveDetails(int leaveType, string fromDate, string toDate, string comments, int workingDays)
        {
            var insertEmployeeDetails = addLeaveRepository.InsertEmployeeLeaveDetails(leaveType, fromDate, toDate, comments, workingDays);

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

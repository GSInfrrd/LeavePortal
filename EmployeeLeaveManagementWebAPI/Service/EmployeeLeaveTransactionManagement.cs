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
        public List<EmployeeLeaveTransactionModel> GetEmployeeLeaveTransaction(int id,int leaveType = 0,int month=0,int transactionType=0)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper GetEmployeeLeaveTransaction method ");
            try
            {
                var EmployeeLeaveTransaction = EmployeeLeaves.GetEmployeeLeaveTransaction(id,leaveType,month,transactionType);
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper GetEmployeeLeaveTransaction method ");
                return EmployeeLeaveTransaction;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper GetEmployeeLeaveTransaction method ");
                throw;
            }
        }

       

        public bool InsertEmployeeLeaveDetails(int empId,int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper InsertEmployeeLeaveDetails method ");
            try
            {
                var insertEmployeeDetails = addLeaveRepository.InsertEmployeeLeaveDetails(empId,leaveType, fromDate, toDate, comments, workingDays);
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper InsertEmployeeLeaveDetails method ");
                return insertEmployeeDetails;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper InsertEmployeeLeaveDetails method ");
                throw;
            }
        }

        public bool SubmitLeaveForApproval(int id)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper SubmitLeaveForApproval method ");
            try
            {
                var submitLeaveForApprovalDetails = addLeaveRepository.SubmitLeaveForApproval(id);
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper SubmitLeaveForApproval method ");
                return submitLeaveForApprovalDetails;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper SubmitLeaveForApproval method ");
                throw;
            }
        }

        public bool DeleteLeaveRequest(int id)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper DeleteLeaveRequest method ");
            try
            {
                var submitLeaveForApprovalDetails = addLeaveRepository.DeleteLeaveRequest(id);
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper DeleteLeaveRequest method ");
                return submitLeaveForApprovalDetails;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper DeleteLeaveRequest method ");
                throw;
            }
        }

        public RewardLeaveModel GetRewardLeaveDetails()
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper GetRewardLeaveDetails method ");
            try
            {
                var rewardLeaveDetailsDetails = addLeaveRepository.GetRewardLeaveModelDetails();
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper GetRewardLeaveDetails method ");
                return rewardLeaveDetailsDetails;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper GetRewardLeaveDetails method ");
                throw;
            }
        }

        public bool SubmitLeaveRewardManagement(RewardLeaveModel model)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper SubmitLeaveRewardManagement method ");
            try
            {
                bool leaveRewarded = false;
                leaveRewarded = addLeaveRepository.Rewardleave(model);
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper SubmitLeaveRewardManagement method ");
                return leaveRewarded;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper SubmitLeaveRewardManagement method ");
                throw;
            }
        }

        public EmployeeLeaveMasterDetails GetEmployeeLeaveMasterDetails(int employeeId)
        {
            Logger.Info("Entering into EmployeeLeaveTransactionManagement Service helper SubmitLeaveRewardManagement method ");
            try
            {
               var details = EmployeeLeaves.GetEmployeeLeaveMasterDetails(employeeId);
                Logger.Info("Exiting from into EmployeeLeaveTransactionManagement Service helper SubmitLeaveRewardManagement method ");
                return details;
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionManagement Service helper SubmitLeaveRewardManagement method ");
                throw;
            }
        }
    }
}

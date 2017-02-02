using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL;
using LMS_WebAPI_Utils;

namespace LMS_WebAPI_DAL.Repositories
{
    public class EmployeeLeaveTransactionRepository : IEmployeeLeaveTransaction
    {
        public List<EmployeeLeaveTransactionModel> GetEmployeeLeaveTransaction(int id, int leaveType = 0,int month=0,int transactionType=0)
        {
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransactionRepository API GetEmployeeLeaveTransaction method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    List<EmployeeLeaveTransactionModel> retResult = new List<EmployeeLeaveTransactionModel>();
                    var EmployeeLeaveTransactions = new List<EmployeeLeaveTransaction>();
                    var empDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == id).RefProfileType;
                    if (empDetails == (int)ProfileType.Manager)
                    {
                        var ids = ctx.EmployeeDetails.Where(x => x.ManagerId == id).Select(x=>x.Id).ToList();
                        EmployeeLeaveTransactions = ctx.EmployeeLeaveTransactions.Where(m => m.RefEmployeeId == id || ids.Contains(m.RefEmployeeId)).OrderByDescending(m => m.CreatedDate).ToList();

                    }

                    else
                    {
                        EmployeeLeaveTransactions = ctx.EmployeeLeaveTransactions.Where(m => m.RefEmployeeId == id).OrderByDescending(m => m.CreatedDate).ToList();
                    }

                    if (leaveType != 0)
                    {
                        EmployeeLeaveTransactions = EmployeeLeaveTransactions.Where(x => x.RefLeaveType == leaveType).ToList();
                     
                    }
                   if(month!=0)
                    {
                        EmployeeLeaveTransactions = EmployeeLeaveTransactions.Where(x =>x.FromDate!=null && x.FromDate.Value.Month == month || x.ToDate!=null && x.ToDate.Value.Month == month).ToList();
                    }
                   if(transactionType!=0)
                    {
                        EmployeeLeaveTransactions = EmployeeLeaveTransactions.Where(x => x.RefTransactionType == transactionType).ToList();
                    }
                    retResult = ToModel(EmployeeLeaveTransactions);
                    Logger.Info("Successfully exiting from EmployeeLeaveTransactionRepository API GetEmployeeLeaveTransaction method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionRepository API GetEmployeeLeaveTransaction method ");
                throw;
            }
        }

        private List<EmployeeLeaveTransactionModel> ToModel(List<EmployeeLeaveTransaction> employeeLeaveTransaction)
        {
            List<EmployeeLeaveTransactionModel> Empres = new List<EmployeeLeaveTransactionModel>();
            try
            {
                Logger.Info("Entering in EmployeeLeaveTransactionRepository API ToModel method");
                foreach (var m in employeeLeaveTransaction)
                {
                    var newTrans = new EmployeeLeaveTransactionModel();
                    newTrans.Id = m.Id;
                    newTrans.RefEmployeeId = m.RefEmployeeId;
                    newTrans.EmployeeName = m.EmployeeDetail.FirstName;
                    newTrans.FromDate = Convert.ToDateTime(m.FromDate);
                    newTrans.ToDate = m.ToDate;
                    newTrans.CreatedDate = m.CreatedDate;
                    newTrans.RefStatus = m.RefStatus;
                    newTrans.NumberOfWorkingDays = m.NumberOfWorkingDays;
                    newTrans.RefLeaveType = m.RefLeaveType;
                    newTrans.EmployeeComment = m.EmployeeComment;
                    newTrans.LeaveTypeName = m.MasterDataValue.Value;
                    newTrans.StatusName = m.MasterDataValue1.Value;
                    newTrans.RefTransactionType = m.RefTransactionType;
                    newTrans.TransactionName = CommonMethods.Description((TransactionType)m.RefTransactionType);
                    newTrans.ModifiedDate = m.ModifiedDate;
                    Empres.Add(newTrans);
                }
                Logger.Info("Successfully exiting from EmployeeLeaveTransactionRepository API ToModel method");
            }
            catch
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionRepository API ToModel method ");
                throw;
            }
            return Empres;
        }

        public EmployeeLeaveMasterDetails GetEmployeeLeaveMasterDetails(int employeeId)
        {
            try
            {
                Logger.Info("Exception occured at EmployeeLeaveTransactionRepository GetEmployeeLeaveMasterDetails method ");
                var details = new EmployeeLeaveMasterDetails();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var employeeMaster = ctx.EmployeeLeaveMasters.Where(x => x.RefEmployeeId == employeeId).FirstOrDefault();

                    details.SpentAdvanceLeave = employeeMaster.SpentAdvanceLeave;
                    details.EarnedCasualLeave = employeeMaster.EarnedCasualLeave;
                    details.RewardedLeaveCount = employeeMaster.RewardedLeaveCount;
                    details.TakenCompOff = employeeMaster.TakenCompOff;
                    details.TakenLossOfPay = employeeMaster.TakenLossOfPay;
                    details.RefEmployeeId = employeeMaster.RefEmployeeId;

                }
                Logger.Info("Exception occured at EmployeeLeaveTransactionRepository GetEmployeeLeaveMasterDetails method ");
                return details;
            }
            catch 
            {
                Logger.Error("Exception occured at EmployeeLeaveTransactionRepository GetEmployeeLeaveMasterDetails method ");
                throw;
            }
        }

    }
}

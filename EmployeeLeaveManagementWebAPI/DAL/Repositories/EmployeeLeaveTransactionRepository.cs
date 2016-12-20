using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;


using LMS_WebAPI_DAL;

namespace LMS_WebAPI_DAL.Repositories
{
    public class EmployeeLeaveTransactionRepository : IEmployeeLeaveTransaction
    {
        public List<EmployeeLeaveTransactionModel> GetEmployeeLeaveTransaction(int id)
        {
            using (var ctx = new LeaveManagementSystemEntities1())
            {


                var EmployeeLeaveTransactions = ctx.EmployeeLeaveTransactions.Where(m => m.RefEmployeeId == id).ToList();
                var retResult = ToModel(EmployeeLeaveTransactions);

                if (retResult != null)
                {
                    return retResult;
                }
                else
                    return null;
            }
        }

        private List<EmployeeLeaveTransactionModel> ToModel(List<EmployeeLeaveTransaction> employeeLeaveTransaction)
        {
            List<EmployeeLeaveTransactionModel> Empres = new List<EmployeeLeaveTransactionModel>();
            try
            {

                foreach (var m in employeeLeaveTransaction)
                {
                    var newTrans = new EmployeeLeaveTransactionModel();
                    newTrans.Id = m.Id;
                    newTrans.RefEmployeeId = m.RefEmployeeId;
                    newTrans.FromDate = m.FromDate;
                    newTrans.ToDate = m.ToDate;
                    newTrans.CreatedDate = m.CreatedDate;
                    newTrans.RefStatus = m.RefStatus;
                    newTrans.NumberOfWorkingDays = m.NumberOfWorkingDays;
                    newTrans.RefLeaveType = m.RefLeaveType;
                    newTrans.EmployeeComment = m.EmployeeComment;
                    newTrans.LeaveTypeName = m.MasterDataValue.Value;
                    newTrans.StatusName = m.MasterDataValue1.Value;
                    //newTrans.ManagerComments = m.ManagerComments;
                    newTrans.ModifiedDate = m.ModifiedDate;
                    Empres.Add(newTrans);


                }
            }
            catch (Exception)
            {

                throw;
            }
            return Empres;



        }
    }
}

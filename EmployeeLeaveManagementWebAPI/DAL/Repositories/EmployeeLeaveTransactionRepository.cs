using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_DAL.Repositories.Interfaces;


using LMS_WebAPI_DAL;

namespace LMS_WebAPI_DAL.Repositories
{
    public class EmployeeLeaveTransactionRepository : IEmployeeLeaveTransaction
    {
        public List<EmployeeLeaveTransaction> GetEmployeeLeaveTransaction()
        {
            using (var ctx = new LeaveManagementSystemEntities1())
            {

                var EmployeeLeaveTransactions = ctx.EmployeeLeaveTransactions.ToList();
                if (EmployeeLeaveTransactions != null)
                {
                    return EmployeeLeaveTransactions;
                }
                else
                    return null;
            }
        }
    }
}

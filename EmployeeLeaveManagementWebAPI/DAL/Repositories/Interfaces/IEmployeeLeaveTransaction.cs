using LMS_WebAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
    public interface IEmployeeLeaveTransaction
    {
       List<EmployeeLeaveTransactionModel> GetEmployeeLeaveTransaction(int id,int leaveType = 0);
    }
}

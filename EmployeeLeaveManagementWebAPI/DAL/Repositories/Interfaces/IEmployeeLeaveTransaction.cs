using LMS_WebAPI_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
    public interface IEmployeeLeaveTransaction
    {
       List<EmployeeLeaveTransaction> GetEmployeeLeaveTransaction();
    }
}

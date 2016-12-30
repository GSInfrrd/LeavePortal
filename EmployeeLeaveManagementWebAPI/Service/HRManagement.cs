using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_ServiceHelpers
{
    public class HRManagement
    {
       private IHRRepository hrRepo = new HRRepository();
        public bool SubmitEmployeeDetails(EmployeeDetailsModel model)
        {
            var result = hrRepo.SubmitEmployeeDetails(model);
            // var leaveType = addLeaveRepository.GetLeaveType();
            // var retResult = ToModel(EmployeeLeaveTransaction);

            return result;
        }

        public List<EmployeeDetailsModel> GetEmployeeList()
        {
            var result = hrRepo.GetEmployeeList();
            return result;
        }

        public List<EmployeeDetailsModel> GetManagerList(int refLevel)
        {
            var result = hrRepo.GetManagerList(refLevel);
            return result;
        }
    }
}

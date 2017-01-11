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
            IUser usr = new UserRepository();
            var leaveReport = usr.GetLeaveReportDetails(DateTime.Now.Year);       
            result[0].leaveDetails = leaveReport;
            return result;
        }

        public List<EmployeeDetailsModel> GetManagerList(int refLevel)
        {
            var result = hrRepo.GetManagerList(refLevel);
            return result;
        }

        public List<ConsolidatedEmployeeLeaveDetailsModel> GetReportData(string fromDate,string toDate,List<int> employeeId, out List<DetailedLeaveReport> detailsList)
        {
         
            var result = hrRepo.GetReportData(fromDate,toDate,employeeId,out detailsList);
            return result;
        }

        public ConsolidatedEmployeeLeaveDetailsModel GetChartDetails(int employeeId)
        {
            var result = hrRepo.GetChartDetails(employeeId);
            return result;
        }

        public bool AddNewMasterDataValues(int masterDataType,string masterDataValue)
        {
            var result = hrRepo.AddNewMasterDataValues(masterDataType,masterDataValue);
            return result;
        }

        public bool AddNewProjectInfo(string projectName, string description, string technology, DateTime startDate, int refManager)
        {
            var result = hrRepo.AddNewProjectInfo(projectName, description, technology, startDate, refManager);
            return result;
        }

        public List<ProjectsList> GetProjectsList()
        {
            var result = hrRepo.GetProjectsList();
            return result;
        }
    }
}

using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
  public interface IHRRepository 
    {
        bool SubmitEmployeeDetails(EmployeeDetailsModel model);
        List<EmployeeDetailsModel> GetEmployeeList();

        List<EmployeeDetailsModel> GetManagerList(int refLevel);

        List<ConsolidatedEmployeeLeaveDetailsModel> GetReportData(string fromDate,string toDate,List<int> employeeId, out List<DetailedLeaveReport> detailsList);
       ConsolidatedEmployeeLeaveDetailsModel GetChartDetails(int employeeId);

        bool AddNewMasterDataValues(int masterDataType, string masterDataValue);
        bool AddNewProjectInfo(string projectName, string description, string technology, DateTime startDate, int refManager);
        List<ProjectsList> GetProjectsList();
    }
}

﻿using LMS_WebAPI_Domain;
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

        List<ConsolidatedEmployeeLeaveDetailsModel> GetReportData(int employeeId=0, int leaveType=0);

    }
}

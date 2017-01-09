using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_ServiceHelpers
{
    public class AddLeaveManagement
    {
        private IAddLeaveRepository addLeaveRepo = new AddLeaveRepository();
        public bool CheckLeaveAvailability(int employeeId,DateTime fromDate,DateTime toDate)
        {
            var result = new EmployeeDetail();
            bool dateAlreadyExists = false;
            bool noLeaveBalance = false;
            var holidayList = new List<Holiday>();
            result = addLeaveRepo.CheckLeaveAvailability(employeeId, out holidayList);
            var noOfWorkingDays = 0;
            for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                var isHoliday = holidayList.FirstOrDefault(i => i.Date == date) != null ? true : false;
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !isHoliday)
                {
                    noOfWorkingDays++;
                }
            }
            var availableLeaves = result.EmployeeLeaveMasters1.FirstOrDefault(i => i.RefEmployeeId == employeeId).LeaveBalance;
            if(noOfWorkingDays>availableLeaves)
            {
                noLeaveBalance = true;
            }
            foreach (var item in result.EmployeeLeaveTransactions)
            {
                for (DateTime date = item.FromDate; date <= item.ToDate; date = date.AddDays(1))
                {
                    if (fromDate == date|| toDate==date )
                    {
                        dateAlreadyExists = true;
                        break;
                    }
                }

            }
            return true;
        }
    }
    }

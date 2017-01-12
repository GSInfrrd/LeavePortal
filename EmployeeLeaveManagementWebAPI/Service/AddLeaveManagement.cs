using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
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
        public LeaveTransactionResponse CheckLeaveAvailability(int employeeId, DateTime fromDate, DateTime toDate)
        {
            Logger.Info("Entering into AddLeaveManagement Service helper CheckLeaveAvailability method ");
            try
            {
                var result = new EmployeeDetail();
                var holidayList = new List<Holiday>();
                var response = new LeaveTransactionResponse();
                result = addLeaveRepo.CheckLeaveAvailability(employeeId, out holidayList);
                var noOfWorkingDays = 0;
                foreach (var item in result.EmployeeLeaveTransactions)
                {
                    for (DateTime date = item.FromDate; date <= item.ToDate; date = date.AddDays(1))
                    {
                        for (DateTime givenDate = fromDate; givenDate <= toDate; givenDate = givenDate.AddDays(1))
                        {
                            if (givenDate == date)
                            {
                                response.responseCode = (int)LMS_WebAPI_Utils.ResponseCodes.DateAlreadyExists;
                                break;
                            }
                        }
                    }

                }
                if (response.responseCode == 0)
                {
                    for (DateTime date = fromDate; date <= toDate; date = date.AddDays(1))
                    {
                        var isHoliday = holidayList.FirstOrDefault(i => i.Date == date) != null ? true : false;
                        if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !isHoliday)
                        {
                            noOfWorkingDays++;
                        }
                    }

                    response.noOfWorkingDays = noOfWorkingDays;
                    var availableLeaves = result.EmployeeLeaveMasters1.FirstOrDefault(i => i.RefEmployeeId == employeeId).EarnedCasualLeave;
                    if (availableLeaves == null || noOfWorkingDays > availableLeaves)
                    {
                        response.responseCode = (int)LMS_WebAPI_Utils.ResponseCodes.NoLeaveBalance;
                        response.availableLeaveBalance = (int)(availableLeaves != null ? availableLeaves : 0);
                        response.advanceLeaveBalance = (int)result.EmployeeLeaveMasters1.FirstOrDefault(i => i.RefEmployeeId == employeeId).SpentAdvanceLeave;

                    }
                }
                Logger.Info("Exiting from into AddLeaveManagement Service helper CheckLeaveAvailability method ");
                return response;
            }
            catch
            {
                Logger.Info("Exception occured at AddLeaveManagement Service helper CheckLeaveAvailability method ");
                throw;
            }
        }
       }
    }

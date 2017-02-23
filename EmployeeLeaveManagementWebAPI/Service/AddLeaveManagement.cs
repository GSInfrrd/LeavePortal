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
        public LeaveTransactionResponse CheckLeaveAvailability(int employeeId, DateTime fromDate, DateTime toDate, int leaveType)
        {
            Logger.Info("Entering into AddLeaveManagement Service helper CheckLeaveAvailability method ");
            try
            {
                var result = new EmployeeDetail();
                var holidayList = new List<Holiday>();
                var response = new LeaveTransactionResponse();
                var advanceLeaveLimit = 0;
                var lopLeaveLimit = 0;
                result = addLeaveRepo.CheckLeaveAvailability(employeeId, out holidayList, out advanceLeaveLimit, out lopLeaveLimit);

                var noOfWorkingDays = 0;

                foreach (var item in result.EmployeeLeaveTransactions)
                {
                    if (item.RefLeaveType != (Int32)LeaveType.RewardLeave && item.RefLeaveType != (Int32)LeaveType.EarnedLeave)
                    {
                        for (DateTime date = item.FromDate.Value; date <= item.ToDate; date = date.AddDays(1))
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
                }
                foreach (var item in result.WorkFromHomes)
                {
                    for (DateTime date = item.Date.Value; date <= item.Date; date = date.AddDays(1))
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
                    var leaveMaster = result.EmployeeLeaveMasters.FirstOrDefault(i => i.RefEmployeeId == employeeId);
                    var availableLeaves = leaveMaster.EarnedCasualLeave != null ? leaveMaster.EarnedCasualLeave : 0;
                    var rewardedLeaves = leaveMaster.RewardedLeaveCount != null ? leaveMaster.RewardedLeaveCount : 0;
                    availableLeaves = availableLeaves + rewardedLeaves;
                    //response.availableLeaveBalance = (int)(availableLeaves != null ? availableLeaves : 0);
                    //response.advanceLeaveBalance = (int)(leaveMaster.SpentAdvanceLeave != null ? advanceLeaveLimit - leaveMaster.SpentAdvanceLeave : advanceLeaveLimit - 0);
                    response.availableLeaveBalance = (availableLeaves != null ? availableLeaves : 0);
                    response.advanceLeaveBalance = (leaveMaster.AdvanceLeave);
                    response.lopLeaveBalance = (int)(leaveMaster.TakenLossOfPay != null ? lopLeaveLimit - leaveMaster.TakenLossOfPay : lopLeaveLimit - 0);

                    if (leaveType == (int)LeaveType.CasualLeave && (availableLeaves == null || noOfWorkingDays > availableLeaves))
                    {
                        response.responseCode = (int)LMS_WebAPI_Utils.ResponseCodes.NoLeaveBalance;
                    }
                    else if (leaveType == (int)LeaveType.AdvanceLeave && noOfWorkingDays > response.advanceLeaveBalance)
                    {
                        response.responseCode = (int)LMS_WebAPI_Utils.ResponseCodes.NoLeaveBalance;

                    }
                    else if (leaveType == (int)LeaveType.LOP && noOfWorkingDays > response.lopLeaveBalance)
                    {
                        response.responseCode = (int)LMS_WebAPI_Utils.ResponseCodes.NoLeaveBalance;

                    }
                    else
                    {
                        response.responseCode = (int)LMS_WebAPI_Utils.ResponseCodes.OK;
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

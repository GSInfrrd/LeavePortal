using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    [System.Web.Http.RoutePrefix("api/Holiday")]
    public class HolidayController : ApiController
    {
        HolidayManagement holidayManager = new HolidayManagement();
        EmployeeLeaveTransactionManagement eltm = new EmployeeLeaveTransactionManagement();

        [System.Web.Http.HttpPost]

        public IList<HolidayModel> AddNewHoliday(HolidayModel model)
        {
            try
            {
                Logger.Info("Entering in HolidayController API AddNewHoliday method");
                var result = holidayManager.AddNewHoliday(model);
                Logger.Info("Successfully exiting from HolidayController API AddNewHoliday method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HolidayController API AddNewHoliday method.", ex);
                return null;
            }
        }

        [System.Web.Http.HttpGet]

        public IList<HolidayModel> GetHolidayList()
        {
            try
            {
                Logger.Info("Entering in HolidayController API GetHolidayList method");
                var result = holidayManager.GetHolidayList();
                Logger.Info("Successfully exiting from HolidayController API GetHolidayList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HolidayController API GetHolidayList method.", ex);
                return null;
            }
        }

        [System.Web.Http.HttpDelete]

        public IList<HolidayModel> DeleteHoliday(long Id)
        {
            try
            {
                Logger.Info("Entering in HolidayController API DeleteHoliday method");
                Logger.Info("Successfully exiting from HolidayController API DeleteHoliday method");
                return holidayManager.DeleteHoliday(Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HolidayController API DeleteHoliday method.", ex);
                return null;
            }
        }


        [System.Web.Http.HttpPut]

        public IList<HolidayModel> UpdateHoliday(HolidayModel model)
        {
            try
            {
                Logger.Info("Entering in HolidayController API UpdateHoliday method");
                Logger.Info("Successfully exiting from HolidayController API UpdateHoliday method");
                return holidayManager.UpdateHoliday(model);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HolidayController API UpdateHoliday method.", ex);
                return null;
            }
        }

        [System.Web.Http.Route("GetCalendarEvents")]
        public List<CalendarEvents> GetCalendarEvents(int employeeId)
        {
            try
            {
                Logger.Info("Entering in HolidayController API GetCalendarEvents method");
                var calendarEvents = new List<CalendarEvents>();
                var holidayList = holidayManager.GetHolidayList();
                var leaveList = eltm.GetEmployeeLeaveTransaction(employeeId, 0);
                var holidayEvents = holidayList.Select(m => new CalendarEvents() { Title = m.Description, StartDate = m.Date.Value.ToShortDateString() }).ToList();

                var leaveEvents = leaveList.Where(m => m.RefLeaveType != (Int32)LeaveType.RewardLeave && m.RefLeaveType != (Int32)LeaveType.EarnedLeave && m.RefStatus==(Int32)LeaveStatus.Approved).
                        Select(m => new CalendarEvents()
                        {
                            Title = m.RefEmployeeId == employeeId ? m.LeaveTypeName : m.EmployeeName + " : " + m.LeaveTypeName,
                            StartDate = m.FromDate.ToShortDateString(),
                            EndDate = m.ToDate.Value.ToShortDateString()
                        }).ToList();
                calendarEvents.AddRange(holidayEvents);
                calendarEvents.AddRange(leaveEvents);

                Logger.Info("Successfully exiting from HolidayController API GetCalendarEvents method");
                return calendarEvents;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at HolidayController API GetCalendarEvents method.", ex);
                return null;
            }
        }

    }
}
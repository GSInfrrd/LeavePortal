using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class HolidayRepository : IHoliday
    {
        public IList<Holiday> GetHolidayList()
        {
            Logger.Info("Entering in HolidayRepository API GetHolidayList method");
            var list = new List<Holiday>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    list = ctx.Holidays.ToList();
                }
                Logger.Info("Successfully exiting from HolidayRepository API GetHolidayList method");
                return list;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayRepository API GetHolidayList method ");
                throw;
            }
        }

        public bool AddHoliday(Holiday newHoliday)
        {
            Logger.Info("Entering in HolidayRepository API AddHoliday method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    ctx.Holidays.Add(newHoliday);
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from HolidayRepository API AddHoliday method");
                    return true;
                }
            }
            catch 
            {
                Logger.Info("Exception occured at HolidayRepository API AddHoliday method ");
                throw;
            }
        }

        public bool DeleteHolidayRequest(long id)
        {
            Logger.Info("Entering in HolidayRepository API DeleteHolidayRequest method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveDetails = ctx.Holidays.FirstOrDefault(x => x.Id == id);
                    if (null != leaveDetails)
                    {
                        ctx.Holidays.Remove(leaveDetails);
                        ctx.SaveChanges();
                        return true;
                    }
                    Logger.Info("Successfully exiting from HolidayRepository API DeleteHolidayRequest method");
                    return false;
                }
            }
            catch
            {
                Logger.Info("Exception occured at HolidayRepository API DeleteHolidayRequest method ");
                throw;
            }
        }

        public bool UpdateHoliday(Holiday holiday)
        {
            Logger.Info("Entering in HolidayRepository API UpdateHoliday method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var holidaySelected = ctx.Holidays.FirstOrDefault(x => x.Id == holiday.Id);
                    if (null != holidaySelected)//Insert
                    {
                        holidaySelected.Date = holiday.Date;
                        holidaySelected.Year = holiday.Year;
                        holidaySelected.Description = holiday.Description;
                        holidaySelected.IsActive = holiday.IsActive;
                        holidaySelected.ModifiedDate = DateTime.Now;
                        holidaySelected.ModifiedBy = holiday.ModifiedBy;
                        ctx.Holidays.Attach(holidaySelected);
                        ctx.Entry(holidaySelected).State = EntityState.Modified;
                        ctx.SaveChanges();
                    }
                    Logger.Info("Successfully exiting from HolidayRepository API UpdateHoliday method");
                    return true;
                }
            }
            catch
            {
                Logger.Info("Exception occured at HolidayRepository API UpdateHoliday method ");
                throw;
            }
        }
    }
}

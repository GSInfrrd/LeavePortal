using LMS_WebAPI_DAL.Repositories.Interfaces;
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
            var list = new List<Holiday>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    list = ctx.Holidays.ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddHoliday(Holiday newHoliday)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    ctx.Holidays.Add(newHoliday);
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public bool DeleteHolidayRequest(long id)
        {
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
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateHoliday(Holiday holiday)
        {
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
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

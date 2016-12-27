using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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

      public bool DeleteLeaveRequest(int id)
        {
            return true;
        }
    }
}

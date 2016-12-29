using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
  public  interface IHoliday
    {
        IList<Holiday> GetHolidayList();

        bool AddHoliday(Holiday newHoliday);

        bool DeleteHolidayRequest(long id);

        bool UpdateHoliday(Holiday holiday);
    }
}

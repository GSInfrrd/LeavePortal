using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class HolidayController : ApiController
    {
       HolidayManagement holidayManager = new HolidayManagement();

        [System.Web.Http.HttpPost]
        [System.Web.Http.AllowAnonymous]
        public IList<HolidayModel> AddNewHoliday(HolidayModel model)
        {
            try
            {
                var result = holidayManager.AddNewHoliday(model);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.AllowAnonymous]
        public IList<HolidayModel> GetHolidayList()
        {
            try
            {
                var result = holidayManager.GetHolidayList();
                return result;
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
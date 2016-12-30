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

        [System.Web.Http.HttpDelete]

        public IList<HolidayModel> DeleteHoliday(long Id)
        {
            try
            {
                return holidayManager.DeleteHoliday(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [System.Web.Http.HttpPut]
       
        public IList<HolidayModel> UpdateHoliday(HolidayModel model)
        {
            try
            {
                return holidayManager.UpdateHoliday(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
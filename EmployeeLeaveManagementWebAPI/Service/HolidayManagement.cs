using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_ServiceHelpers
{
    public class HolidayManagement
    {
        private IHoliday holiday = new HolidayRepository();
        public IList<HolidayModel> AddNewHoliday(HolidayModel model)
        {
            try
            {
                LMS_WebAPI_DAL.Holiday newholiday = new LMS_WebAPI_DAL.Holiday()
                {
                    Date = model.Date,
                    Year = model.Year,
                    Description = model.Description,
                    IsActive = true,
                    Id = model.Id

                };
                if (holiday.AddHoliday(newholiday) == true) { return GetHolidayList(); }
                else
                {
                    throw new Exception("Something went wrong");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<HolidayModel> GetHolidayList()
        {
            try
            {
                var resultList = holiday.GetHolidayList();
                IList<HolidayModel> holidayList = new List<HolidayModel>();
                holidayList = (from holi in resultList
                               select new HolidayModel()
                               {
                                   Id = holi.Id,
                                   Year = holi.Year,
                                   Date = holi.Date,
                                   Description = holi.Description,
                                   IsActive = holi.IsActive
                               }).ToList();
                return holidayList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<HolidayModel> UpdateHoliday(HolidayModel model)
        {
            try
            {
                LMS_WebAPI_DAL.Holiday newholiday = new LMS_WebAPI_DAL.Holiday()
                {
                    Date = model.Date,
                    Year = model.Year,
                    Description = model.Description,
                    IsActive = true,
                    Id = model.Id,
                    ModifiedBy = model.ModifiedBy
                };
                if (holiday.UpdateHoliday(newholiday))
                {
                    return GetHolidayList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<HolidayModel> DeleteHoliday(long id)
        {
            try
            {
                if (holiday.DeleteHolidayRequest(id))
                {
                    return GetHolidayList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}


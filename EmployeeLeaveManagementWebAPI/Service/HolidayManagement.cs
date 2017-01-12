using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;

namespace LMS_WebAPI_ServiceHelpers
{
    public class HolidayManagement
    {
        private IHoliday holiday = new HolidayRepository();
        public IList<HolidayModel> AddNewHoliday(HolidayModel model)
        {
            Logger.Info("Entering into HolidayManagement Service helper AddNewHoliday method ");
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
                Logger.Info("Exiting from into HolidayManagement Service helper AddNewHoliday method ");
                if (holiday.AddHoliday(newholiday) == true) { return GetHolidayList(); }
                else
                {
                    throw new Exception("Something went wrong");
                }
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement Service helper AddNewHoliday method ");
                throw;
            }
        }

        public IList<HolidayModel> GetHolidayList()
        {
            Logger.Info("Entering into HolidayManagement Service helper GetHolidayList method ");
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
                Logger.Info("Exiting from into HolidayManagement Service helper GetHolidayList method ");
                return holidayList;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement Service helper GetHolidayList method ");
                throw;
            }
        }

        public IList<HolidayModel> UpdateHoliday(HolidayModel model)
        {
            Logger.Info("Entering into HolidayManagement Service helper UpdateHoliday method ");
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
                Logger.Info("Exiting from into HolidayManagement Service helper UpdateHoliday method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement Service helper UpdateHoliday method ");
                throw;
            }
        }

        public IList<HolidayModel> DeleteHoliday(long id)
        {
            Logger.Info("Entering into HolidayManagement Service helper DeleteHoliday method ");
            try
            {
                if (holiday.DeleteHolidayRequest(id))
                {
                    return GetHolidayList();
                }
                Logger.Info("Exiting from into HolidayManagement Service helper DeleteHoliday method ");
                return null;
            }
            catch
            {
                Logger.Info("Exception occured at HolidayManagement Service helper DeleteHoliday method ");
                throw;
            }
        }
    }
}


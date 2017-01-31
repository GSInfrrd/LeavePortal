using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Utils;

namespace LMS_WebAPI_ServiceHelpers
{
    public class WorkFromHomeManagement
    {
        private IWorkFromHome WorkFromHome = new WorkFromHomeRepository();
        public long AddNewWorkFromHome(WorkFromHomeModel model)
        {
            Logger.Info("Entering into WorkFromHomeManagement Service helper AddNewWorkFromHome method ");
            try
            {
                LMS_WebAPI_DAL.WorkFromHome newWorkFromHome = new LMS_WebAPI_DAL.WorkFromHome()
                {
                    RefEmployeeId = model.RefEmployeeId,
                    Date = model.Date,
                    RefStatus = model.RefStatus,
                    CreatedBy = model.CreatedBy,
                    RefReason =model.RefReason,
                    OtherReason=model.OtherReason,
                    CreatedDate=DateTime.Now
                };
                Logger.Info("Exiting from into WorkFromHomeManagement Service helper AddNewWorkFromHome method ");
                return WorkFromHome.AddWorkFromHome(newWorkFromHome);
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeManagement Service helper AddNewWorkFromHome method ");
                throw;
            }
        }

        public IList<WorkFromHomeModel> GetWorkFromHomeList(int EmpId)
        {
            Logger.Info("Entering into WorkFromHomeManagement Service helper GetWorkFromHomeList method ");
            try
            {
                var resultList = WorkFromHome.GetWorkFromHomeList(EmpId);
                IList<WorkFromHomeModel> WorkFromHomeList = new List<WorkFromHomeModel>();
                WorkFromHomeList = (from holi in resultList
                                    select new WorkFromHomeModel()
                                    {
                                        Id = holi.Id,
                                        CreatedDate = holi.CreatedDate,
                                        Date = holi.Date,
                                        StatusName = holi.StatusName,
                                        Reason = holi.Reason,
                                        RefStatus = holi.RefStatus,
                                        RefReason = holi.RefReason,
                                        RefEmployeeId = holi.RefEmployeeId,
                                    }).ToList();
                Logger.Info("Exiting from into WorkFromHomeManagement Service helper GetWorkFromHomeList method ");
                return WorkFromHomeList;
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeManagement Service helper GetWorkFromHomeList method ");
                throw;
            }
        }

        public bool UpdateWorkFromHome(WorkFromHomeModel model)
        {
            Logger.Info("Entering into WorkFromHomeManagement Service helper UpdateWorkFromHome method ");
            try
            {
                LMS_WebAPI_DAL.WorkFromHome newWorkFromHome = new LMS_WebAPI_DAL.WorkFromHome()
                {
                    RefEmployeeId = model.RefEmployeeId,
                    Date = model.Date,
                    Id= model.Id,
                    RefReason =model.RefReason,
                    RefStatus = model.RefStatus,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedDate =DateTime.Now
                };
                Logger.Info("Exiting from into WorkFromHomeManagement Service helper UpdateWorkFromHome method ");
                return WorkFromHome.UpdateWorkFromHome(newWorkFromHome);
                
            }
            catch 
            {
                Logger.Info("Exception occured at WorkFromHomeManagement Service helper UpdateWorkFromHome method ");
                throw;
            }
        }

        public long DeleteWorkFromHome(long id)
        {
            Logger.Info("Entering into WorkFromHomeManagement Service helper DeleteWorkFromHome method ");
            try
            {
                Logger.Info("Exiting from into WorkFromHomeManagement Service helper DeleteWorkFromHome method ");
                return WorkFromHome.DeleteWorkFromHomeRequest(id);
            }
            catch 
            {
                Logger.Info("Exception occured at WorkFromHomeManagement Service helper DeleteWorkFromHome method ");
                throw;
            }
        }

        public List<WorkFromHomeReasonModel> GetWorkFromHomeReasonsList()
        {
            Logger.Info("Entering into WorkFromHomeManagement Service helper GetWorkFromHomeReasonList method ");
            try
            {
                Logger.Info("Exiting from into WorkFromHomeManagement Service helper GetWorkFromHomeReasonList method ");
                return WorkFromHome.GetWorkFromHomeReasonsList();
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeManagement Service helper GetWorkFromHomeReasonList method ");
                throw;
            }
        }
    }
}

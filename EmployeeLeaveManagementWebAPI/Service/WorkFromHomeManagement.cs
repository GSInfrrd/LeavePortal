using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_ServiceHelpers
{
    public class WorkFromHomeManagement
    {
        private IWorkFromHome WorkFromHome = new WorkFromHomeRepository();
        public long AddNewWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                LMS_WebAPI_DAL.WorkFromHome newWorkFromHome = new LMS_WebAPI_DAL.WorkFromHome()
                {
                    RefEmployeeId = model.RefEmployeeId,
                    Date = model.Date,
                    RefStatus = model.RefStatus,
                    CreatedBy = model.CreatedBy,
                    RefReason =model.RefReason,
                };
                return WorkFromHome.AddWorkFromHome(newWorkFromHome);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<WorkFromHomeModel> GetWorkFromHomeList(int EmpId)
        {
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
                return WorkFromHomeList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateWorkFromHome(WorkFromHomeModel model)
        {
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
                return WorkFromHome.UpdateWorkFromHome(newWorkFromHome);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long DeleteWorkFromHome(long id)
        {
            try
            {
                return WorkFromHome.DeleteWorkFromHomeRequest(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

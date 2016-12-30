using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Utils;
using System.Data.Entity;

namespace LMS_WebAPI_DAL.Repositories
{
    public class WorkFromHomeRepository : IWorkFromHome
    {
        public IList<WorkFromHomeCommonModel> GetWorkFromHomeList(int EmpId)
        {
            var list = new List<WorkFromHomeCommonModel>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var listModel = (from wfh in ctx.WorkFromHomes where wfh.RefEmployeeId == EmpId select wfh).ToList();
                    if (null != listModel)
                    {
                        list = (from n in listModel
                                select new WorkFromHomeCommonModel()
                                {
                                    Id = n.Id,
                                    Date = n.Date,
                                    CreatedDate = n.CreatedDate,
                                    CreatedBy = n.CreatedBy,
                                    RefEmployeeId = n.RefEmployeeId,
                                    RefStatus = n.RefStatus,
                                    RefReason = n.RefReason,
                                    Reason = n.MasterDataValue1.Value,
                                    StatusName = n.MasterDataValue.Value

                                }).ToList();
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public long AddWorkFromHome(WorkFromHome newWorkFromHome)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var newRecord = ctx.WorkFromHomes.Add(newWorkFromHome);
                    ctx.SaveChanges();
                    return newRecord.Id;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public long DeleteWorkFromHomeRequest(long id)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var workFromHomeDetails = ctx.WorkFromHomes.FirstOrDefault(x => x.Id == id);
                    if (null != workFromHomeDetails)
                    {
                        ctx.WorkFromHomes.Remove(workFromHomeDetails);
                        ctx.SaveChanges();
                        return workFromHomeDetails.Id;
                    }
                    return 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateWorkFromHome(WorkFromHome WorkFromHome)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var WorkFromHomeSelected = ctx.WorkFromHomes.FirstOrDefault(x => x.Id == WorkFromHome.Id);
                    if (null != WorkFromHomeSelected)//Insert
                    {
                        WorkFromHomeSelected.Date = WorkFromHome.Date;
                        WorkFromHomeSelected.RefStatus = WorkFromHome.RefStatus;
                        WorkFromHomeSelected.RefEmployeeId = WorkFromHome.RefEmployeeId;
                        WorkFromHomeSelected.RefReason = WorkFromHome.RefReason;
                        WorkFromHomeSelected.ModifiedDate = DateTime.Now;
                        WorkFromHomeSelected.ModifiedBy = WorkFromHome.ModifiedBy;
                        ctx.WorkFromHomes.Attach(WorkFromHomeSelected);
                        ctx.Entry(WorkFromHomeSelected).State = EntityState.Modified;
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

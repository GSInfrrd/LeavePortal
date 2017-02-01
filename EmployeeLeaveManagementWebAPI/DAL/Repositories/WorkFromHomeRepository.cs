using LMS_WebAPI_DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Utils;
using System.Data.Entity;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_DAL.Repositories
{
    public class WorkFromHomeRepository : IWorkFromHome
    {
        public IList<WorkFromHomeCommonModel> GetWorkFromHomeList(int EmpId)
        {
            Logger.Info("Entering in WorkFromHomeRepository API GetWorkFromHomeList method");
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
                                    Date = n.Date.Value,
                                    CreatedDate = n.CreatedDate,
                                    CreatedBy = n.CreatedBy,
                                    RefEmployeeId = n.RefEmployeeId,
                                    RefStatus = n.RefStatus,
                                    RefReason = n.RefReason,
                                    Reason =n.RefReason==(int)WorkFormHomeReasons.Others?n.OtherReason: n.MasterDataValue.Value,
                                    StatusName = n.MasterDataValue1.Value                                 
                                }).ToList();
                    }
                }
                Logger.Info("Successfully exiting from WorkFromHomeRepository API GetWorkFromHomeList method");
                return list;
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeRepository GetWorkFromHomeList method ");
                throw;
            }
        }

        public long AddWorkFromHome(WorkFromHome newWorkFromHome)
        {
            Logger.Info("Entering in WorkFromHomeRepository API AddWorkFromHome method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var newRecord = ctx.WorkFromHomes.Add(newWorkFromHome);
                    ctx.SaveChanges();

                    //Send notification to manager
                    var employeeDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == newRecord.RefEmployeeId);
                    int RefApproverId = Convert.ToInt32(employeeDetails.ManagerId);
                    string Firstname = employeeDetails.FirstName;
                    string Lastname = employeeDetails.LastName;

                    string employeeName = Firstname;
                    if (Lastname != null)
                    {
                        employeeName += " ";
                        employeeName += Lastname;
                    }

                    employeeName += " has applied for work for home";
                    int Status = (Int16)NotificationStatus.Active;
                    int notificationType = (Int16)NotificationTypes.WorkfromHome;
                    ApproveLeaveRepository alr = new ApproveLeaveRepository();
                    alr.InsertNotification(RefApproverId, employeeName, Status, notificationType);
                    Logger.Info("Successfully exiting from WorkFromHomeRepository API AddWorkFromHome method");
                    return newRecord.Id;
                }
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeRepository AddWorkFromHome method ");
                throw;
            }

        }

        public long DeleteWorkFromHomeRequest(long id)
        {
            Logger.Info("Entering in WorkFromHomeRepository API DeleteWorkFromHomeRequest method");
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
                    Logger.Info("Successfully exiting from WorkFromHomeRepository API DeleteWorkFromHomeRequest method");
                    return 0;
                }
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeRepository DeleteWorkFromHomeRequest method ");
                throw;
            }
        }

        public bool UpdateWorkFromHome(WorkFromHome WorkFromHome)
        {
            Logger.Info("Entering in WorkFromHomeRepository API UpdateWorkFromHome method");
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
                    Logger.Info("Successfully exiting from WorkFromHomeRepository API UpdateWorkFromHome method");
                    return true;
                }
            }
            catch 
            {
                Logger.Info("Exception occured at WorkFromHomeRepository UpdateWorkFromHome method ");
                throw;
            }
        }

        public List<WorkFromHomeReasonModel> GetWorkFromHomeReasonsList()
        {
            Logger.Info("Entering in WorkFromHomeRepository API GetWorkFromHomeReasonsList method");
            try
            {
                var wfhReasonsList = new List<WorkFromHomeReasonModel>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var workFromHomeReasons = ctx.MasterDataValues.Where(i => i.RefMasterType == (int)MasterDataTypeEnum.WorkFromHomeReason).ToList();
                 foreach(var item in workFromHomeReasons)
                    {
                        var wfh = new WorkFromHomeReasonModel();
                        wfh.Reason = item.Value;
                        wfh.RefReason = item.Id;
                        wfhReasonsList.Add(wfh);
                    }
                    Logger.Info("Successfully exiting from WorkFromHomeRepository API GetWorkFromHomeReasonsList method");
                    return wfhReasonsList;
                }
            }
            catch
            {
                Logger.Info("Exception occured at WorkFromHomeRepository GetWorkFromHomeReasonsList method ");
                throw;
            }
        }
    }
}

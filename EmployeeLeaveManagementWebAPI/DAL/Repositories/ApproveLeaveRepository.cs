using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class ApproveLeaveRepository : IApproveLeaveRepository
    {

        public List<EmployeeDetailsModel> GetAllManagers(int id, int st)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API GetAllManagers method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == id).FirstOrDefault();
                    var level = EmployeeDetails.RefHierarchyLevel;
                    var ManagersDetails = ctx.EmployeeDetails.Where(m => (m.RefHierarchyLevel >= level) && (m.Id != id)).ToList();
                    var retResult = ToModelMasterDetails(ManagersDetails);
                    Logger.Info("Successfully exiting from ApproveLeaveRepository API GetAllManagers method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Error("Exception occured at ApproveLeaveRepository API GetAllManagers method ");
                throw;
            }
        }
        public List<ApproveLeaveModel> GetApproveLeave(int id)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API GetApproveLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var ApproveLeaves = ctx.Workflows.Where(m => (m.RefApproverId == id) && ((m.RefStatus ==(Int16) LeaveStatus.Submitted) || (m.RefStatus == (Int16)LeaveStatus.Reassigned))).OrderByDescending(m => m.CreatedDate).ToList();
                    var retResult = ToModel(ApproveLeaves);
                    Logger.Info("Successfully exiting from ApproveLeaveRepository API GetApproveLeave method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API GetApproveLeave method ");
                throw;
            }
        }


        public List<ApproveLeaveModel> GetViewApprovedLeave(int id)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API GetViewApprovedLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MyEmployees = ctx.EmployeeDetails.Where(m => m.ManagerId == id).ToList();
                    List<Int32> EmployeedIds = new List<int>();
                    foreach (var emp in MyEmployees)
                    {
                        EmployeedIds.Add(emp.Id);
                    }
                    DateTime bd = DateTime.Now.AddMonths(-2);
                    var ApprovedLeaves = ctx.EmployeeLeaveTransactions.Where(m => EmployeedIds.Contains(m.RefEmployeeId) && (m.RefStatus == (Int16)LeaveStatus.Approved)&& (DateTime.Compare(m.FromDate.Value, bd)>0)).ToList();
                    var retResult = ToModelForApprovedLeaves(ApprovedLeaves);
                    Logger.Info("Successfully exiting from ApproveLeaveRepository API GetViewApprovedLeave method");
                    return retResult;
                }
            }
            catch(Exception ex)
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API GetViewApprovedLeave method ");
                throw;
            }
        }

        public bool TakeActionOnEmployeeLeave(int Leaveid, string Leavecomments, string Leavestatus, int Approverid)
        {
            var result = false;
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API ApproveEmployeeLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    
                    var leaveDetails = ctx.Workflows.Where(x => x.EmployeeLeaveTransaction.Id == Leaveid).OrderByDescending(x=>x.ModifiedDate).FirstOrDefault();
                    var EmployeeId = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.Id;
                    var ApproverId = leaveDetails.RefApproverId;
                    int Status = (Int16)NotificationStatus.Active;
                    if (Leavestatus == CommonMethods.Description(LeaveStatus.Approved))
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = Convert.ToInt32(LeaveStatus.Approved);
                        leaveDetails.EmployeeLeaveTransaction.ModifiedDate = DateTime.Now;
                        Workflow wf = new Workflow();
                        wf.RefLeaveTransactionId = leaveDetails.RefLeaveTransactionId;
                        wf.RefApproverId = leaveDetails.RefApproverId;
                        wf.CreatedDate = leaveDetails.CreatedDate;
                        wf.ModifiedDate = DateTime.Now;
                        wf.RefStatus = Convert.ToInt32(LeaveStatus.Approved);
                        wf.RefCreatedBy = leaveDetails.RefCreatedBy;
                        wf.ManagerComments = Leavecomments;
                        int ModifiedById = leaveDetails.RefApproverId;
                        wf.RefModifiedBy = ModifiedById;
                        leaveDetails.EmployeeLeaveTransaction.RefModifiedBy = ModifiedById;
                        ctx.Workflows.Add(wf);
                        ctx.SaveChanges();

                        var Allworkflowleavedetails = ctx.Workflows.Where(x => x.EmployeeLeaveTransaction.Id == Leaveid).ToList();

                        InsertintoLeaveHistory(Allworkflowleavedetails);
                        Deletefromworkflow(Allworkflowleavedetails);
                        var leaveMaster = ctx.EmployeeLeaveMasters.FirstOrDefault(i => i.RefEmployeeId == EmployeeId);
                        if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.CasualLeave || leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.SickLeave)
                        {

                            leaveMaster.EarnedCasualLeave=(Double)leaveMaster.EarnedCasualLeave - leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;

                        }
                        else if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.AdvanceLeave)
                        {
                            leaveMaster.EarnedCasualLeave = Convert.ToInt32((Double)leaveMaster.EarnedCasualLeave - leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays);
                            var spentleave = leaveMaster.SpentAdvanceLeave != null ? leaveMaster.SpentAdvanceLeave : 0;
                            leaveMaster.SpentAdvanceLeave = spentleave+(int)leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        }
                        else if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.LOP)
                        {
                            var lopLeave = leaveMaster.TakenLossOfPay != null ? leaveMaster.TakenLossOfPay : 0;
                            leaveMaster.TakenLossOfPay = lopLeave+(int)leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        }
                        else if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.CompOff)
                        {
                            var compOff = leaveMaster.TakenCompOff != null ? leaveMaster.TakenCompOff : 0;

                            leaveMaster.TakenCompOff = compOff+(int)leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        }
                        ctx.SaveChanges();
                        var ManagerDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == ApproverId);
                        string ManagerName = ManagerDetails.FirstName;
                        if (ManagerDetails.LastName != null)
                        {
                            ManagerName += " ";
                            ManagerName += ManagerDetails.LastName;
                        }

                        string Text = "Your Manager " + ManagerName + " has approved your leave";
                        int notificationType = (Int16)NotificationTypes.ApproveLeave;
                        InsertNotification(EmployeeId, Text, Status, notificationType);



                    }
                    if (Leavestatus == CommonMethods.Description(LeaveStatus.Rejected))
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = Convert.ToInt32(LeaveStatus.Rejected);
                        leaveDetails.EmployeeLeaveTransaction.ModifiedDate = DateTime.Now;
                        Workflow wf = new Workflow();
                        wf.RefLeaveTransactionId = leaveDetails.RefLeaveTransactionId;
                        wf.RefApproverId = leaveDetails.RefApproverId;
                        wf.CreatedDate = leaveDetails.CreatedDate;
                        wf.ModifiedDate = DateTime.Now;
                        wf.RefStatus = Convert.ToInt32(LeaveStatus.Rejected);
                        wf.RefCreatedBy = leaveDetails.RefCreatedBy;
                        wf.ManagerComments = Leavecomments;
                        int ModifiedById = leaveDetails.RefApproverId;
                        wf.RefModifiedBy = ModifiedById;
                        leaveDetails.EmployeeLeaveTransaction.RefModifiedBy = ModifiedById;
                        ctx.Workflows.Add(wf);
                        ctx.SaveChanges();

                        var Allworkflowleavedetails = ctx.Workflows.Where(x => x.EmployeeLeaveTransaction.Id == Leaveid).ToList();
                        InsertintoLeaveHistory(Allworkflowleavedetails);
                        Deletefromworkflow(Allworkflowleavedetails);

                        var ManagerDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == ApproverId);
                        string ManagerName = ManagerDetails.FirstName;
                        if (ManagerDetails.LastName != null)
                        {
                            ManagerName += " ";
                            ManagerName += ManagerDetails.LastName;
                        }

                        string Text = "Your Manager " + ManagerName + " has rejected your leave";
                        int notificationType = (Int16)NotificationTypes.RejectLeave;
                        InsertNotification(EmployeeId, Text, Status, notificationType);
                    }
                    if (Leavestatus == CommonMethods.Description(LeaveStatus.Reassigned))
                    {
                        Workflow wf = new Workflow();
                        wf.RefLeaveTransactionId = leaveDetails.RefLeaveTransactionId;
                        wf.RefApproverId = Approverid;
                        wf.CreatedDate = leaveDetails.CreatedDate;
                        wf.ModifiedDate = DateTime.Now;
                        wf.RefStatus = Convert.ToInt32(LeaveStatus.Reassigned); ;
                        wf.RefCreatedBy = leaveDetails.RefCreatedBy;
                        wf.ManagerComments = Leavecomments;
                        int ModifiedById = leaveDetails.RefApproverId;
                        wf.RefModifiedBy = ModifiedById;
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = Convert.ToInt32(LeaveStatus.Reassigned);
                        leaveDetails.EmployeeLeaveTransaction.ModifiedDate = DateTime.Now;
                        leaveDetails.EmployeeLeaveTransaction.RefModifiedBy = ModifiedById;
                        ctx.Workflows.Add(wf);
                        ctx.SaveChanges();

                        //Send notification to the Employee that manager has reassigned the leaves to other manager
                        var ManagerDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == ApproverId);
                        string ManagerName = ManagerDetails.FirstName;
                        if (ManagerDetails.LastName != null)
                        {
                            ManagerName += " ";
                            ManagerName += ManagerDetails.LastName;
                        }

                        var assignedManager = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == Approverid);

                        string assignedManagerName = assignedManager.FirstName;

                        if (assignedManager.LastName != null)
                        {
                            assignedManagerName += " ";
                            assignedManagerName += assignedManager.LastName;
                        }

                        string Text = "Your Manager " + ManagerName + " has reassigned your leave to " + assignedManagerName;
                        int notificationType = (Int16)NotificationTypes.ReassignLeave;
                        InsertNotification(EmployeeId, Text, Status, notificationType);

                        //Send notification to the new manager that employee has applied for the leaves

                        int RefApproverId = assignedManager.Id;
                        string EmployeeFirstname = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.FirstName;
                        string EmployeeLastname = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.LastName;
                        notificationType = (Int16)NotificationTypes.SubmitLeaveRequest;

                        string Text1 = EmployeeFirstname;
                        if (EmployeeLastname != null)
                        {
                            Text1 += " ";
                            Text1 += EmployeeLastname;
                        }

                        Text1 += " has applied for leave";
                        InsertNotification(RefApproverId, Text1, Status, notificationType);
                    }
                    Logger.Info("Successfully exiting from ApproveLeaveRepository API ApproveEmployeeLeave method");
                }
                result = true;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API ApproveEmployeeLeave method ");
                throw;
            }
            return result;

        }


        public bool CancelEmployeeLeave(int Leaveid)
        {
            var result = false;
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API CancelEmployeeLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {


                    var leaveDetails = ctx.EmployeeLeaveTransactions.Where(x => x.Id == Leaveid).FirstOrDefault();
                    leaveDetails.RefStatus = (int)LeaveStatus.Cancelled;
                    ctx.SaveChanges();
                    var leaveTransactionEntity = new EmployeeLeaveTransaction()
                    {
                        RefEmployeeId = leaveDetails.RefEmployeeId,
                        CreatedDate = leaveDetails.CreatedDate,
                        NumberOfWorkingDays = leaveDetails.NumberOfWorkingDays,
                        RefLeaveType = (int)LeaveType.CancelledLeave,
                        RefStatus = (int)LeaveStatus.Cancelled,
                        FromDate = Convert.ToDateTime(leaveDetails.FromDate),
                        ToDate = Convert.ToDateTime(leaveDetails.ToDate),
                        ModifiedDate = DateTime.Now,
                        RefCreatedBy = leaveDetails.RefCreatedBy,
                        RefModifiedBy = leaveDetails.RefModifiedBy,
                        RefTransactionType = (int)TransactionType.Credit
                    };

                    var leaveTransactionRecord = ctx.EmployeeLeaveTransactions.Add(leaveTransactionEntity);
                    var leaveTransactionResult = ctx.SaveChanges();

                    var WorkflowHistoryEntity = new WorkflowHistory()
                    {
                        Id = leaveDetails.Id,
                        RefEmployeeId = leaveDetails.RefEmployeeId,
                        CreatedDate = leaveDetails.CreatedDate,
                        FromDate = Convert.ToDateTime(leaveDetails.FromDate),
                        ToDate = Convert.ToDateTime(leaveDetails.ToDate),
                        NumberOfWorkingDays = leaveDetails.NumberOfWorkingDays,
                        RefLeaveType = (int)LeaveType.CancelledLeave,
                        RefStatus = (int)LeaveStatus.Cancelled,
                        ModifiedDate = DateTime.Now,
                        RefCreatedBy = leaveDetails.RefCreatedBy,
                        RefModifiedBy = leaveDetails.RefModifiedBy,
                    };

                    var WorkflowHistoryRecord = ctx.WorkflowHistories.Add(WorkflowHistoryEntity);
                    var WorkflowHistoryResult = ctx.SaveChanges();




                    var leaveMasterRecord = ctx.EmployeeLeaveMasters.Where(x => x.RefEmployeeId == leaveDetails.RefEmployeeId).FirstOrDefault();
                    if (null != leaveMasterRecord)
                    {
                        if (null != leaveMasterRecord.EarnedCasualLeave)
                        { leaveMasterRecord.EarnedCasualLeave += leaveDetails.NumberOfWorkingDays; }
                        else
                        { leaveMasterRecord.EarnedCasualLeave = leaveDetails.NumberOfWorkingDays; }

                        var resultLeaveMaster = ctx.SaveChanges();
                    }
                    else
                    {
                        var employeeLeavemaster = new EmployeeLeaveMaster()
                        {
                            RefEmployeeId = leaveDetails.RefEmployeeId,
                            EarnedCasualLeave = leaveDetails.NumberOfWorkingDays,
                            ModifiedDate = DateTime.Now,
                            ModifiedBy = leaveDetails.RefModifiedBy
                        };
                        var newLeavemasterRecord = ctx.EmployeeLeaveMasters.Add(employeeLeavemaster);
                        var resultLeavemasterRecord = ctx.SaveChanges();
                    }
                    
                    Logger.Info("Successfully exiting from ApproveLeaveRepository API CancelEmployeeLeave method");
                }
                result = true;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API CancelEmployeeLeave method ");
                throw;
            }
            return result;

        }

        public void InsertNotification(int id, string Text, int status, int notificationType)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API insertNotification method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var m = new Notification();
                    m.RefEmployeeId = id;
                    m.Text = Text;
                    m.CreatedDate = Convert.ToDateTime(DateTime.Now);
                    m.Status = status;
                    m.RefNotificationType = notificationType;
                    ctx.Notifications.Add(m);
                    ctx.SaveChanges();
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API insertNotification method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API insertNotification method ");
                throw;
            }

        }
        public void InsertintoLeaveHistory(List<Workflow> leaveDetails)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API insertintoLeaveHistory method");
                foreach (Workflow wf in leaveDetails)
                {
                    using (var ctx = new LeaveManagementSystemEntities1())
                    {
                        var m = new WorkflowHistory();
                        m.Id = wf.EmployeeLeaveTransaction.Id;
                        m.RefEmployeeId = wf.EmployeeLeaveTransaction.EmployeeDetail.Id;
                        m.FromDate = Convert.ToDateTime(wf.EmployeeLeaveTransaction.FromDate);
                        m.ToDate = Convert.ToDateTime(wf.EmployeeLeaveTransaction.ToDate);
                        m.CreatedDate = Convert.ToDateTime(wf.EmployeeLeaveTransaction.CreatedDate);
                        m.RefStatus = wf.RefStatus;
                        m.NumberOfWorkingDays = wf.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        m.RefLeaveType = wf.EmployeeLeaveTransaction.RefLeaveType;
                        m.EmployeeComment = wf.EmployeeLeaveTransaction.EmployeeComment;
                        m.ManagerComment = wf.ManagerComments;
                        m.RefCreatedBy = wf.RefCreatedBy;
                        m.RefModifiedBy = wf.RefModifiedBy;
                        m.ModifiedDate = Convert.ToDateTime(wf.ModifiedDate);

                        ctx.WorkflowHistories.Add(m);
                        ctx.SaveChanges();
                    }
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API insertintoLeaveHistory method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API insertintoLeaveHistory method ");
                throw;
            }
        }

        public void Deletefromworkflow(List<Workflow> LeaveDetails)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API deletefromworkflow method");
                foreach (Workflow wf in LeaveDetails)
                {
                    using (var ctx = new LeaveManagementSystemEntities1())
                    {
                        var leaveDetails = ctx.Workflows.FirstOrDefault(x => x.EmployeeLeaveTransaction.Id == wf.RefLeaveTransactionId);
                        ctx.Workflows.Remove(leaveDetails);
                        ctx.SaveChanges();
                    }
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API deletefromworkflow method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API deletefromworkflow method ");
                throw;
            }
        }

        private ApproveLeaveModel ToModelSingle(EmployeeLeaveTransaction employeeLeaveTransaction)
        {
            ApproveLeaveModel Empres = new ApproveLeaveModel();
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API ToModelSingle method");
                var m = employeeLeaveTransaction;
                Empres.Id = m.Id;
                Empres.EmployeeName = m.EmployeeDetail.FirstName + " " + m.EmployeeDetail.LastName;
                Empres.RefEmployeeId = m.RefEmployeeId;
                Empres.FromDate =m.FromDate.Value;
                Empres.ToDate = m.ToDate;
                Empres.CreatedDate = m.CreatedDate;
                Empres.RefStatus = m.RefStatus;
                Empres.NumberOfWorkingDays = m.NumberOfWorkingDays;
                Empres.RefLeaveType = m.RefLeaveType;
                Empres.EmployeeComment = m.EmployeeComment;
                Empres.LeaveTypeName = m.MasterDataValue.Value;
                Empres.StatusName = m.MasterDataValue1.Value;
                //newTrans.ManagerComments = m.ManagerComments;
                Empres.ModifiedDate = m.ModifiedDate;
                Logger.Info("Successfully exiting from ApproveLeaveRepository API ToModelSingle method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API ToModelSingle method ");
                throw;
            }
            return Empres;
        }

        private List<EmployeeDetailsModel> ToModelMasterDetails(List<EmployeeDetail> EmployeeDetails)
        {
            Logger.Info("Entering in ApproveLeaveRepository API ToModelMasterDetails method");
            List<EmployeeDetailsModel> Empres = new List<EmployeeDetailsModel>();
            try
            {
                foreach (var m in EmployeeDetails)
                {
                    var newTrans = new EmployeeDetailsModel();
                    newTrans.Id = m.Id;
                    newTrans.FirstName = m.FirstName;
                    newTrans.LastName = m.LastName;
                    Empres.Add(newTrans);
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API ToModelMasterDetails method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API ToModelMasterDetails method ");
                throw;
            }
            return Empres;
        }
        private List<ApproveLeaveModel> ToModel(List<Workflow> LeaveWorkflow)
        {
            Logger.Info("Entering in ApproveLeaveRepository API ToModel method");
            List<ApproveLeaveModel> Empres = new List<ApproveLeaveModel>();
            try
            {
                foreach (var m in LeaveWorkflow)
                {
                    var newTrans = new ApproveLeaveModel();
                    newTrans.Id = m.EmployeeLeaveTransaction.Id;
                    newTrans.EmployeeName = m.EmployeeLeaveTransaction.EmployeeDetail.FirstName + " " + m.EmployeeLeaveTransaction.EmployeeDetail.LastName;
                    newTrans.RefEmployeeId = m.EmployeeLeaveTransaction.EmployeeDetail.Id;
                    newTrans.FromDate =m.EmployeeLeaveTransaction.FromDate.Value;
                    newTrans.ToDate = m.EmployeeLeaveTransaction.ToDate;
                    newTrans.CreatedDate = m.EmployeeLeaveTransaction.CreatedDate;
                    newTrans.RefStatus = m.EmployeeLeaveTransaction.RefStatus;
                    newTrans.NumberOfWorkingDays = m.EmployeeLeaveTransaction.NumberOfWorkingDays;
                    newTrans.RefLeaveType = m.EmployeeLeaveTransaction.RefLeaveType;
                    newTrans.EmployeeComment = m.EmployeeLeaveTransaction.EmployeeComment;
                    newTrans.LeaveTypeName = m.EmployeeLeaveTransaction.MasterDataValue.Value;
                    newTrans.StatusName = m.EmployeeLeaveTransaction.MasterDataValue1.Value;
                    //newTrans.ManagerComments = m.ManagerComments;
                    newTrans.ModifiedDate = m.ModifiedDate;
                    Empres.Add(newTrans);
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API ToModel method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API ToModel method ");
                throw;
            }
            return Empres;
        }

        private List<ApproveLeaveModel> ToModelForApprovedLeaves(List<EmployeeLeaveTransaction> LeaveTransaction)
        {
            Logger.Info("Entering in ApproveLeaveRepository API ToModelForApprovedLeaves method");
            List<ApproveLeaveModel> Empres = new List<ApproveLeaveModel>();
            try
            {
                foreach (var m in LeaveTransaction)
                {
                    var newTrans = new ApproveLeaveModel();
                    newTrans.Id = m.Id;
                    newTrans.EmployeeName = m.EmployeeDetail.FirstName + " " + m.EmployeeDetail.LastName;
                    newTrans.RefEmployeeId = m.EmployeeDetail.Id;
                    newTrans.FromDate = m.FromDate.Value;
                    newTrans.ToDate = m.ToDate;
                    newTrans.CreatedDate = m.CreatedDate;
                    newTrans.RefStatus = m.RefStatus;
                    newTrans.NumberOfWorkingDays = m.NumberOfWorkingDays;
                    newTrans.RefLeaveType = m.RefLeaveType;
                    newTrans.EmployeeComment = m.EmployeeComment;
                    newTrans.LeaveTypeName = m.MasterDataValue.Value;
                    newTrans.StatusName = m.MasterDataValue1.Value;
                    //newTrans.ManagerComments = m.ManagerComments;
                    newTrans.ModifiedDate = m.ModifiedDate;
                    Empres.Add(newTrans);
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API ToModelForApprovedLeaves method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API ToModelForApprovedLeaves method ");
                throw;
            }
            return Empres;
        }
    }
}

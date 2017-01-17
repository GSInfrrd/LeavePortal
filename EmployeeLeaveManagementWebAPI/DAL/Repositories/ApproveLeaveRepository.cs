using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
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
                Logger.Info("Exception occured at ApproveLeaveRepository API GetAllManagers method ");
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
                    var ApproveLeaves = ctx.Workflows.Where(m => (m.RefApproverId == id) && ((m.RefStatus == 10) || (m.RefStatus == 21))).ToList();
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

        public bool TakeActionOnEmployeeLeave(int Leaveid, string Leavecomments, string Leavestatus, int Approverid)
        {
            var result = false;
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API ApproveEmployeeLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveDetails = ctx.Workflows.FirstOrDefault(x => x.EmployeeLeaveTransaction.Id == Leaveid);
                    var EmployeeId = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.Id;
                    var ApproverId = leaveDetails.RefApproverId;
                    int Status = 1;
                    int NotificationType = 27;
                    if (Leavestatus ==CommonMethods.Description(LeaveStatus.Approved))
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 12;
                        leaveDetails.ManagerComments = Leavecomments;
                        ctx.SaveChanges();
                        insertintoLeaveHistory(leaveDetails);
                        deletefromworkflow(Leaveid);
                        var leaveMaster = ctx.EmployeeLeaveMasters.FirstOrDefault(i => i.RefEmployeeId == EmployeeId);
                        if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.CasualLeave || leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.SickLeave)
                        {

                            leaveMaster.EarnedCasualLeave = Convert.ToInt32((Double)leaveMaster.EarnedCasualLeave - leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays);

                        }
                        else if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.AdvanceLeave)
                        {
                            leaveMaster.EarnedCasualLeave = Convert.ToInt32((Double)leaveMaster.EarnedCasualLeave - leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays);

                            leaveMaster.SpentAdvanceLeave = (int)leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        }
                        else if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.LOP)
                        {
                            leaveMaster.TakenLossOfPay = (int)leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        }
                        else if (leaveDetails.EmployeeLeaveTransaction.RefLeaveType == (int)LMS_WebAPI_Utils.LeaveType.CompOff)
                        {
                            leaveMaster.TakenCompOff = (int)leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                        }
                        ctx.SaveChanges();
                        var ManagerDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == ApproverId);
                        string ManagerName = ManagerDetails.FirstName;
                        if (ManagerDetails.LastName != null)
                        {
                            ManagerName += " ";
                            ManagerName += ManagerDetails.LastName;
                        }

                        string Text = "Your Manager " + ManagerName + " has approved your leaves.";
                        insertNotification(EmployeeId, Text , Status , NotificationType);



                    }
                    if (Leavestatus == "Rejected")
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 11;
                        leaveDetails.ManagerComments = Leavecomments;
                        ctx.SaveChanges();
                        insertintoLeaveHistory(leaveDetails);
                        deletefromworkflow(Leaveid);

                        var ManagerDetails = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == ApproverId);
                        string ManagerName = ManagerDetails.FirstName;
                        if (ManagerDetails.LastName != null)
                        {
                            ManagerName += " ";
                            ManagerName += ManagerDetails.LastName;
                        }

                        string Text = "Your Manager " + ManagerName + " has rejected your leaves.";
                        insertNotification(EmployeeId, Text , Status , NotificationType);
                    }
                    if (Leavestatus == "Reassigned")
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 21;
                        leaveDetails.RefApproverId = Approverid;
                        leaveDetails.ManagerComments = Leavecomments;
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

                        string Text = "Your Manager " + ManagerName + " has reassigned your leaves to " + assignedManagerName + ".";
                        insertNotification(EmployeeId, Text, Status, NotificationType);

                        //Send notification to the new manager that employee has applied for the leaves

                        int RefApproverId = assignedManager.Id;
                        string EmployeeFirstname = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.FirstName;
                        string EmployeeLastname = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.LastName;

                        string Text1 = EmployeeFirstname;
                        if (EmployeeLastname != null)
                        {
                            Text1 += " ";
                            Text1 += EmployeeLastname;
                        }

                        Text1 += " has applied for leave.";
                        ApproveLeaveRepository alr = new ApproveLeaveRepository();
                        alr.insertNotification(RefApproverId, Text1, Status, NotificationType);
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

        public void insertNotification(int id, string Text , int status, int notificationType)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API insertNotification method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    Notification N = new Notification();
                    var m = N;
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
        public void insertintoLeaveHistory(Workflow leaveDetails)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API insertintoLeaveHistory method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    EmployeeLeaveTransactionHistory elth = new EmployeeLeaveTransactionHistory();
                    var m = elth;
                    m.Id = leaveDetails.EmployeeLeaveTransaction.Id;
                    m.RefEmployeeId = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.Id;
                    m.FromDate = Convert.ToDateTime(leaveDetails.EmployeeLeaveTransaction.FromDate);
                    m.ToDate = Convert.ToDateTime(leaveDetails.EmployeeLeaveTransaction.ToDate);
                    m.CreatedDate = Convert.ToDateTime(leaveDetails.EmployeeLeaveTransaction.CreatedDate);
                    m.RefStatus = leaveDetails.EmployeeLeaveTransaction.RefStatus;
                    m.NumberOfWorkingDays = leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                    m.RefLeaveType = leaveDetails.EmployeeLeaveTransaction.RefLeaveType;
                    m.EmployeeComment = leaveDetails.EmployeeLeaveTransaction.EmployeeComment;
                    m.ManagerComment = leaveDetails.ManagerComments;
                    m.ModifiedBy = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.ManagerId.ToString();
                    ctx.EmployeeLeaveTransactionHistories.Add(m);
                    ctx.SaveChanges();
                }
                Logger.Info("Successfully exiting from ApproveLeaveRepository API insertintoLeaveHistory method");
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveRepository API insertintoLeaveHistory method ");
                throw;
            }
        }

        public void deletefromworkflow(int id)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveRepository API deletefromworkflow method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveDetails = ctx.Workflows.FirstOrDefault(x => x.EmployeeLeaveTransaction.Id == id);
                    ctx.Workflows.Remove(leaveDetails);
                    ctx.SaveChanges();
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
                Empres.FromDate = m.FromDate;
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
                    newTrans.FromDate = m.EmployeeLeaveTransaction.FromDate;
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
    }
}

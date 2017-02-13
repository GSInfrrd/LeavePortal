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
    public class MailRepository : IMailRepository
    {
        public MailDetailsModel GetMailTemplateForLeaveApplied(ActionsForMail actionName, int EmployeeId)
        {
            try
            {
                Logger.Info("Entering in MailRepository API GetMailTemplateForLeaveApplied method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MailDetails = ctx.EmailTemplateMappings.Where(m => m.ActionName == actionName.ToString()).FirstOrDefault();
                    var TemplatePath = MailDetails.EmailTemplateMaster.Template;

                    var hrManagerId = ctx.EmployeeDetails.Where(x => x.RefRoleId == (int)EmployeeRole.HR).OrderByDescending(x => x.RefHierarchyLevel).FirstOrDefault().Id;
                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == EmployeeId).FirstOrDefault();
                    int ManagerId = EmployeeDetails.ManagerId.HasValue==true ? EmployeeDetails.ManagerId.Value : hrManagerId;
                    string EmployeeEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == EmployeeId).FirstOrDefault().UserName;
                    string ManagerEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == ManagerId).FirstOrDefault().UserName;
                    var ManagerDetails = ctx.EmployeeDetails.Where(m => m.Id == ManagerId).FirstOrDefault();
                    string ManagerName = ManagerDetails.FirstName;
                    string EmployeeName = EmployeeDetails.FirstName;
                    string EmployeeLastName = EmployeeDetails.LastName;
                    if (EmployeeLastName != null)
                    {
                        EmployeeName = string.Format(EmployeeName + " " + EmployeeLastName);
                    }

                    MailDetailsModel retResult = new MailDetailsModel();
                    retResult.ToMailId = ManagerEmailId;
                    retResult.CcMailId = EmployeeEmailId;
                    retResult.TemplatePath = TemplatePath;
                    retResult.ManagerName = ManagerName;
                    retResult.EmployeeName = EmployeeName;
                    Logger.Info("Successfully exiting from MailRepository API GetMailTemplateForLeaveApplied method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at MailRepository API GetMailTemplateForLeaveApplied method ");
                throw;
            }

        }

        public MailDetailsModel GetMailTemplateForWorkFromHome(ActionsForMail actionName, int EmployeeId)
        {
            try
            {
                Logger.Info("Entering in MailRepository API GetMailTemplateForWorkFromHome method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MailDetails = ctx.EmailTemplateMappings.Where(m => m.ActionName == actionName.ToString()).FirstOrDefault();
                    var TemplatePath = MailDetails.EmailTemplateMaster.Template;

                    var hrManagerId = ctx.EmployeeDetails.Where(x => x.RefRoleId == (int)EmployeeRole.HR).OrderByDescending(x => x.RefHierarchyLevel).FirstOrDefault().Id;
                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == EmployeeId).FirstOrDefault();
                    int ManagerId = EmployeeDetails.ManagerId.HasValue == true ? EmployeeDetails.ManagerId.Value : hrManagerId;
                    string EmployeeEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == EmployeeId).FirstOrDefault().UserName;
                    string ManagerEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == ManagerId).FirstOrDefault().UserName;
                    var ManagerDetails = ctx.EmployeeDetails.Where(m => m.Id == ManagerId).FirstOrDefault();
                    string ManagerName = ManagerDetails.FirstName;
                    string EmployeeName = EmployeeDetails.FirstName;
                    string EmployeeLastName = EmployeeDetails.LastName;
                    if (EmployeeLastName != null)
                    {
                        EmployeeName = string.Format(EmployeeName + " " + EmployeeLastName);
                    }

                    MailDetailsModel retResult = new MailDetailsModel();
                    retResult.ToMailId = ManagerEmailId;
                    retResult.CcMailId = EmployeeEmailId;
                    retResult.TemplatePath = TemplatePath;
                    retResult.ManagerName = ManagerName;
                    retResult.EmployeeName = EmployeeName;
                    Logger.Info("Successfully exiting from MailRepository API GetMailTemplateForWorkFromHome method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at MailRepository API GetMailTemplateForWorkFromHome method ");
                throw;
            }

        }

        public MailDetailsModel GetMailTemplateForAddResourceRequest(ActionsForMail actionName, int EmployeeId , int HrId)
        {
            try
            {
                Logger.Info("Entering in MailRepository API GetMailTemplateForAddResourceRequest method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MailDetails = ctx.EmailTemplateMappings.Where(m => m.ActionName == actionName.ToString()).FirstOrDefault();
                    var TemplatePath = MailDetails.EmailTemplateMaster.Template;

                    var HrDetails = ctx.EmployeeDetails.Where(m => m.Id == HrId).FirstOrDefault();
                    string HrEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == HrId).FirstOrDefault().UserName;
                    string HrName = HrDetails.FirstName;

                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == EmployeeId).FirstOrDefault();
                    string EmployeeEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == EmployeeId).FirstOrDefault().UserName;
                    string EmployeeName = EmployeeDetails.FirstName;
                    string EmployeeLastName = EmployeeDetails.LastName;
                    if (EmployeeLastName != null)
                    {
                        EmployeeName = string.Format(EmployeeName + " " + EmployeeLastName);
                    }

                    MailDetailsModel retResult = new MailDetailsModel();
                    retResult.ToMailId = HrEmailId;
                    retResult.CcMailId = EmployeeEmailId;
                    retResult.TemplatePath = TemplatePath;
                    retResult.ManagerName = HrName;
                    retResult.EmployeeName = EmployeeName;
                    Logger.Info("Successfully exiting from MailRepository API GetMailTemplateForAddResourceRequest method");
                    return retResult;
                }
            }
            catch(Exception ex)
            {
                Logger.Info("Exception occured at MailRepository API GetMailTemplateForAddResourceRequest method ");
                throw;
            }

        }


        public MailDetailsModel GetMailTemplateForResourceRequestUpdate(ActionsForMail actionName, int EmployeeId, int HrId)
        {
            try
            {
                Logger.Info("Entering in MailRepository API GetMailTemplateForResourceRequestUpdate method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MailDetails = ctx.EmailTemplateMappings.Where(m => m.ActionName == actionName.ToString()).FirstOrDefault();
                    var TemplatePath = MailDetails.EmailTemplateMaster.Template;

                    var HrDetails = ctx.EmployeeDetails.Where(m => m.Id == HrId).FirstOrDefault();
                    string HrEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == HrId).FirstOrDefault().UserName;
                    string HrName = HrDetails.FirstName;
                    string HRLastName = HrDetails.LastName;
                    if (HRLastName != null)
                    {
                        HrName = string.Format(HrName + " " + HRLastName);
                    }

                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == EmployeeId).FirstOrDefault();
                    string EmployeeEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == EmployeeId).FirstOrDefault().UserName;
                    string EmployeeName = EmployeeDetails.FirstName;

                    MailDetailsModel retResult = new MailDetailsModel();
                    retResult.ToMailId = EmployeeEmailId;
                    retResult.CcMailId = HrEmailId;
                    retResult.TemplatePath = TemplatePath;
                    retResult.ManagerName = HrName;
                    retResult.EmployeeName = EmployeeName;
                    Logger.Info("Successfully exiting from MailRepository API GetMailTemplateForResourceRequestUpdate method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at MailRepository API GetMailTemplateForResourceRequestUpdate method ");
                throw;
            }

        }

        public MailDetailsModel GetMailTemplateForRewardLeave(ActionsForMail actionName, int EmployeeId, int ManagerId)
        {
            try
            {
                Logger.Info("Entering in MailRepository API GetMailTemplateForRewardLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MailDetails = ctx.EmailTemplateMappings.Where(m => m.ActionName == actionName.ToString()).FirstOrDefault();
                    var TemplatePath = MailDetails.EmailTemplateMaster.Template;

                    var ManagerDetails = ctx.EmployeeDetails.Where(m => m.Id == ManagerId).FirstOrDefault();
                    string ManagerEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == ManagerId).FirstOrDefault().UserName;
                    string ManagerName = ManagerDetails.FirstName;
                    string ManagerLastName = ManagerDetails.LastName;
                    if (ManagerLastName != null)
                    {
                        ManagerName = string.Format(ManagerName + " " + ManagerLastName);
                    }

                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == EmployeeId).FirstOrDefault();
                    string EmployeeEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == EmployeeId).FirstOrDefault().UserName;
                    string EmployeeName = EmployeeDetails.FirstName;

                    MailDetailsModel retResult = new MailDetailsModel();
                    retResult.ToMailId = EmployeeEmailId;
                    retResult.CcMailId = ManagerEmailId;
                    retResult.TemplatePath = TemplatePath;
                    retResult.ManagerName = ManagerName;
                    retResult.EmployeeName = EmployeeName;
                    Logger.Info("Successfully exiting from MailRepository API GetMailTemplateForRewardLeave method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at MailRepository API GetMailTemplateForRewardLeave method ");
                throw;
            }

        }

        public MailDetailsModel GetMailTemplateForTakeActionOnLeave(ActionsForMail actionName, int LeaveId)
        {
            try
            {
                Logger.Info("Entering in MailRepository API GetMailTemplateForTakeActionOnLeave method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var MailDetails = ctx.EmailTemplateMappings.Where(m => m.ActionName == actionName.ToString()).FirstOrDefault();
                    var TemplatePath = MailDetails.EmailTemplateMaster.Template;

                    var LeaveDetails = ctx.EmployeeLeaveTransactions.FirstOrDefault(x => x.Id == LeaveId);
                    string EmployeeComments = LeaveDetails.EmployeeComment;
                    int EmployeeId = LeaveDetails.RefEmployeeId;
                    int ManagerId = Convert.ToInt32(LeaveDetails.RefModifiedBy);
                    string ManagerEmailId = ctx.UserAccounts.Where(m => m.RefEmployeeId == ManagerId).FirstOrDefault().UserName;
                    var ManagerDetails = ctx.EmployeeDetails.Where(m => m.Id == ManagerId).FirstOrDefault();
                    string ManagerName = ManagerDetails.FirstName;
                    string ManagerLastName = ManagerDetails.LastName;
                    if (ManagerLastName != null)
                    {
                        ManagerName = string.Format(ManagerName+" "+ManagerLastName);
                    }
                    var EmployeeDetails = ctx.EmployeeDetails.Where(m => m.Id == EmployeeId).FirstOrDefault();
                    string EmployeeName = EmployeeDetails.FirstName;
                    string EmployeeLastName = EmployeeDetails.LastName;
                    if (EmployeeLastName != null)
                    {
                        EmployeeName = string.Format(EmployeeName + " " + EmployeeLastName);
                    }
                    string EmployeeEmailId = EmployeeDetails.UserAccounts.Where(x => x.RefEmployeeId == EmployeeId).FirstOrDefault().UserName;
                    
                    MailDetailsModel retResult = new MailDetailsModel();
                    retResult.ToMailId = EmployeeEmailId;
                    retResult.CcMailId = ManagerEmailId;
                    retResult.TemplatePath = TemplatePath;
                    retResult.EmployeeName = EmployeeName;
                    retResult.ManagerName = ManagerName;
                    retResult.EmployeeComments = EmployeeComments;
                    retResult.EmployeeId = EmployeeId;
                    retResult.LeaveFromDate = Convert.ToString((LeaveDetails.FromDate.Value).ToShortDateString());
                    retResult.LeaveToDate = Convert.ToString((LeaveDetails.ToDate.Value).ToShortDateString());
                    retResult.NumberOfWorkingDays = Convert.ToInt32(LeaveDetails.NumberOfWorkingDays);
                    retResult.ManagerComments = null;
                    retResult.NewManagerName = null;
                    if (actionName== ActionsForMail.ApproveLeave || actionName== ActionsForMail.RejectLeave)
                    {
                        var LeaveHistoryDetails = ctx.WorkflowHistories.Where(x => x.Id == LeaveId).OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
                        retResult.ManagerComments = LeaveHistoryDetails.ManagerComment;
                        //retResult.ManagerName = LeaveHistoryDetails.RefModifiedBy;
                    }
                    else
                    {
                        var LeaveWorkFlowDetails = ctx.Workflows.Where(x => x.RefLeaveTransactionId == LeaveId).OrderByDescending(x => x.ModifiedDate).FirstOrDefault();
                        int NewManagerId = Convert.ToInt32(LeaveWorkFlowDetails.RefApproverId);
                        var NewManagerDetails = ctx.EmployeeDetails.Where(m => m.Id == NewManagerId).FirstOrDefault();
                        string NewManagerName = NewManagerDetails.FirstName;
                        string NewManagerLastName = NewManagerDetails.LastName;
                        string NewManagerMailId = NewManagerDetails.UserAccounts.Where(x => x.RefEmployeeId == NewManagerId).FirstOrDefault().UserName;
                        retResult.NewManagerMailId = NewManagerMailId;
                        if (NewManagerLastName!=null)
                        {
                            NewManagerName = string.Format(NewManagerName+" "+NewManagerLastName);
                        }
                        retResult.NewManagerName = NewManagerName;
                        retResult.ManagerComments = LeaveWorkFlowDetails.ManagerComments;
                    }

                    Logger.Info("Successfully exiting from MailRepository API GetMailTemplateForTakeActionOnLeave method");
                    return retResult;
                }
            }
            catch
            {
                Logger.Info("Exception occured at MailRepository API GetMailTemplateForTakeActionOnLeave method ");
                throw;
            }

        }
    }
}

using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_ServiceHelpers
{
    public class MailManagement
    {
        private IMailRepository MailDetails = new MailRepository();


        public MailDetailsModel GetMailTemplateForLeaveApplied(ActionsForMail actionName , int EmployeeId)
        {
            Logger.Info("Entering into MailManagemet Service helper GetMailTemplateForLeaveApplied method ");
            try
            {
                var MailDetail = MailDetails.GetMailTemplateForLeaveApplied(actionName, EmployeeId);
                Logger.Info("Exiting from into MailManagemet Service helper GetMailTemplateForLeaveApplied method ");
                return MailDetail;
            }
            catch
            {
                Logger.Info("Exception occured at MailManagemet Service helper GetMailTemplateForLeaveApplied method ");
                throw;
            }
        }

        public MailDetailsModel GetMailTemplateForWorkFromHome(ActionsForMail actionName, int EmployeeId)
        {
            Logger.Info("Entering into MailManagemet Service helper GetMailTemplateForWorkFromHome method ");
            try
            {
                var MailDetail = MailDetails.GetMailTemplateForWorkFromHome(actionName, EmployeeId);
                Logger.Info("Exiting from into MailManagemet Service helper GetMailTemplateForWorkFromHome method ");
                return MailDetail;
            }
            catch
            {
                Logger.Info("Exception occured at MailManagemet Service helper GetMailTemplateForWorkFromHome method ");
                throw;
            }
        }

        public MailDetailsModel GetMailTemplateForTakeActionOnLeave(ActionsForMail actionName, int LeaveId)
        {
            Logger.Info("Entering into MailManagemet Service helper GetMailTemplateForTakeActionOnLeave method ");
            try
            {
                var MailDetail = MailDetails.GetMailTemplateForTakeActionOnLeave(actionName, LeaveId);
                Logger.Info("Exiting from into MailManagemet Service helper GetMailTemplateForTakeActionOnLeave method ");
                return MailDetail;
            }
            catch
            {
                Logger.Info("Exception occured at MailManagemet Service helper GetMailTemplateForTakeActionOnLeave method ");
                throw;
            }
        }

        public MailDetailsModel GetMailTemplateForAddResourceRequest(ActionsForMail actionName, int EmployeeId , int HrId)
        {
            Logger.Info("Entering into MailManagemet Service helper GetMailTemplateForAddResourceRequest method ");
            try
            {
                var MailDetail = MailDetails.GetMailTemplateForAddResourceRequest(actionName, EmployeeId, HrId);
                Logger.Info("Exiting from into MailManagemet Service helper GetMailTemplateForAddResourceRequest method ");
                return MailDetail;
            }
            catch
            {
                Logger.Info("Exception occured at MailManagemet Service helper GetMailTemplateForAddResourceRequest method ");
                throw;
            }
        }

        public MailDetailsModel GetMailTemplateForResourceRequestUpdate(ActionsForMail actionName, int EmployeeId, int HrId)
        {
            Logger.Info("Entering into MailManagemet Service helper GetMailTemplateForResourceRequestUpdate method ");
            try
            {
                var MailDetail = MailDetails.GetMailTemplateForResourceRequestUpdate(actionName, EmployeeId, HrId);
                Logger.Info("Exiting from into MailManagemet Service helper GetMailTemplateForResourceRequestUpdate method ");
                return MailDetail;
            }
            catch
            {
                Logger.Info("Exception occured at MailManagemet Service helper GetMailTemplateForResourceRequestUpdate method ");
                throw;
            }
        }

        public MailDetailsModel GetMailTemplateForRewardLeave(ActionsForMail actionName, int EmployeeId, int ManagerId)
        {
            Logger.Info("Entering into MailManagemet Service helper GetMailTemplateForRewardLeave method ");
            try
            {
                var MailDetail = MailDetails.GetMailTemplateForRewardLeave(actionName, EmployeeId, ManagerId);
                Logger.Info("Exiting from into MailManagemet Service helper GetMailTemplateForRewardLeave method ");
                return MailDetail;
            }
            catch
            {
                Logger.Info("Exception occured at MailManagemet Service helper GetMailTemplateForRewardLeave method ");
                throw;
            }
        }
    }
}

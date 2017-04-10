using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
    public interface IMailRepository
    {
        MailDetailsModel GetMailTemplateForLeaveApplied(ActionsForMail actionName, int EmployeeId);

        MailDetailsModel GetMailTemplateForWorkFromHome(ActionsForMail actionName, int EmployeeId);

        MailDetailsModel GetMailTemplateForTakeActionOnLeave(ActionsForMail actionName, int LeaveId);

        MailDetailsModel GetMailTemplateForCancelEmployeeLeave(ActionsForMail actionName, int LeaveId);

        MailDetailsModel GetMailTemplateForAddNewEmployee(ActionsForMail actionName, int EmployeeId, int HrId);

        MailDetailsModel GetMailTemplateForChangePassword(ActionsForMail actionName, int EmployeeId);

        MailDetailsModel GetMailTemplateForAddResourceRequest(ActionsForMail actionName, int EmployeeId);

        MailDetailsModel GetMailTemplateForResourceRequestUpdate(ActionsForMail actionName, int EmployeeId, int HelpDeskMemberId);
        MailDetailsModel GetMailTemplateForRewardLeave(ActionsForMail actionName, int EmployeeId, int ManagerId);
    }
}

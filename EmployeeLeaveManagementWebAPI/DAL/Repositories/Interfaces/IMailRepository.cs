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

        MailDetailsModel GetMailTemplateForAddResourceRequest(ActionsForMail actionName, int EmployeeId , int Hrid);

        MailDetailsModel GetMailTemplateForResourceRequestUpdate(ActionsForMail actionName, int EmployeeId, int Hrid);
        MailDetailsModel GetMailTemplateForRewardLeave(ActionsForMail actionName, int EmployeeId, int ManagerId);
    }
}

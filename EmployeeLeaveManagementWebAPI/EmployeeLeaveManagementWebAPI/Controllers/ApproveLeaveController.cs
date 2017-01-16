using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Web.Http;
//using LMS_WebAPI_DAL;

namespace EmployeeLeaveManagementWebAPI.Controllers
{

    public class ApproveLeaveController : ApiController
    {
        // GET: ApproveLeave

        public List<EmployeeDetailsModel> Get(int id, int st)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var res = ALM.GetAllManagers(id, st);
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }
        public List<ApproveLeaveModel> Get(int id)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var res = ALM.GetApproveLeave(id);
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }
        
        public List<ApproveLeaveModel> GetTakeActionOnEmployeeLeave(int Leaveid, string Leavecomments, string Leavestatus, int Approverid)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var EmployeeLeaveApproved = ALM.TakeActionOnEmployeeLeave(Leaveid, Leavecomments, Leavestatus, Approverid);

                var res = new List<ApproveLeaveModel>();
                if (EmployeeLeaveApproved)
                {
                    res = ALM.GetApproveLeave(Leaveid);
                }
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }
    }
}
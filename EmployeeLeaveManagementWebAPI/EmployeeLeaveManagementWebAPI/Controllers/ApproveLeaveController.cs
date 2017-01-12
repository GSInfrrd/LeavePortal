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
                ApproveLeaveManagement ELTM = new ApproveLeaveManagement();
                var res = ELTM.GetAllManagers(id, st);
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
                ApproveLeaveManagement ELTM = new ApproveLeaveManagement();
                var res = ELTM.GetApproveLeave(id);
                Logger.Info("Successfully exiting from ApproveLeaveController API Get method");
                return res;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at ApproveLeaveController API Get method.", ex);
                return null;
            }

        }
        
        public List<ApproveLeaveModel> Get(int id , string comments , int st, int apid)
        {
            try
            {
                Logger.Info("Entering in ApproveLeaveController API Get method");
                ApproveLeaveManagement ALM = new ApproveLeaveManagement();
                var EmployeeLeaveApproved = ALM.ApproveEmployeeLeave(id, comments, st, apid);

                var res = new List<ApproveLeaveModel>();
                if (EmployeeLeaveApproved)
                {
                    res = ALM.GetApproveLeave(id);
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
using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
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
            ApproveLeaveManagement ELTM = new ApproveLeaveManagement();
            var res = ELTM.GetAllManagers(id,st);

            return res;

        }
        public List<ApproveLeaveModel> Get(int id)
        {
            ApproveLeaveManagement ELTM = new ApproveLeaveManagement();
            var res = ELTM.GetApproveLeave(id);

            return res;

        }
        
        public List<ApproveLeaveModel> Get(int id , string comments , int st, int apid)
        {
            ApproveLeaveManagement ALM = new ApproveLeaveManagement();
            var EmployeeLeaveApproved = ALM.ApproveEmployeeLeave(id,comments,st, apid);

            var res = new List<ApproveLeaveModel>();
            if (EmployeeLeaveApproved)
            {
                res = ALM.GetApproveLeave(id);
            }
            return res;
            

        }
    }
}
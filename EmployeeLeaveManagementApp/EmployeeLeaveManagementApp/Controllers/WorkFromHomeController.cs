using LMS_WebAPP_Domain;
using LMS_WebAPP_Utils;
using LMS_WebAPP_ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EmployeeLeaveManagementApp.Controllers
{
    public class WorkFromHomeController : Controller
    {
        // GET: WorkFromHome
        WorkFromHomeManagement wfhOperations = new WorkFromHomeManagement();
        public async Task<ActionResult> Index()
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var WorkFromHomeList = await wfhOperations.GetWorkFromHomeListAsync(data.RefEmployeeId);
                    return View(WorkFromHomeList);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<JsonResult> AddWorkFromHomes(DateTime date, int Reason)
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var model = new WorkFromHomeModel()
                    {
                        Date = date,
                        RefStatus = Convert.ToInt16(LeaveStatus.Approved),
                        RefReason = Reason,
                        CreatedBy = data.Id,
                        RefEmployeeId = data.RefEmployeeId,
                    };

                    var result = await wfhOperations.AddNewWorkFromHomeDetailsAsync(model);
                    //Send mail to first level manager
                    if (null != data.ManagerEmail & data.ManagerEmail != "")
                    {
                        CommonMethods.SendMailWithMultipleAttachments(data.ManagerEmail, false, ReadResouce.GetEmailConstant(Constants.SUBJECT), ReadResouce.GetEmailConstant(Constants.Content));
                    }
                    //else send mail to hr?
                        var resultJson = new { result = result };
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public async Task<JsonResult> GetWorkFromHomeList()
        {
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var WorkFromHomeList = await wfhOperations.GetWorkFromHomeListAsync(data.RefEmployeeId);

                    var resultJson = new { result = WorkFromHomeList };
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
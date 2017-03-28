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
        public async Task<ActionResult> ApplyWorkFromHome()
        {
            Logger.Info("Entering in WorkFromHomeController APP ApplyWorkFromHome method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var WorkFromHomeList = await wfhOperations.GetWorkFromHomeListAsync(data.RefEmployeeId);
                    Logger.Info("Successfully exiting from WorkFromHomeController APP ApplyWorkFromHome method");
                    return View(WorkFromHomeList);
                }
                else
                {
                    Logger.Info("Successfully exiting from WorkFromHomeController APP ApplyWorkFromHome method");
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController APP Index method.", ex);
                return View("Error");
            }
        }
        //[HttpPost]
        public async Task<JsonResult> AddWorkFromHomes(DateTime date, int Reason,string textReason="")
        {
            Logger.Info("Entering in WorkFromHomeController APP AddWorkFromHomes method");
            try
            {
                var wfhList = new List<WorkFromHomeModel>();
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
                        OtherReason = Reason == (int)WorkFormHomeReasons.Others ? textReason : string.Empty
                    };

                    var result = await wfhOperations.AddNewWorkFromHomeDetailsAsync(model);
                    ////Send mail to first level manager
                    //if (null != data.ManagerEmail & data.ManagerEmail != "")
                    //{
                    //    CommonMethods.SendMailWithMultipleAttachments(data.ManagerEmail, false, ReadResource.GetEmailConstant(Constants.SUBJECT), ReadResource.GetEmailConstant(Constants.Content),null,string.Empty);
                    //}
                    if(result!=0)
                    {
                       wfhList=await GetWorkFromHomeList();
                    }
                    //else send mail to hr?
                        var resultJson = new { result = result,wfhList=wfhList };
                    Logger.Info("Successfully exiting from WorkFromHomeController APP AddWorkFromHomes method");
                    return Json(resultJson, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Logger.Info("Successfully exiting from WorkFromHomeController APP AddWorkFromHomes method");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController APP AddWorkFromHomes method.", ex);
                return null;
            }
        }


        public async Task<List<WorkFromHomeModel>> GetWorkFromHomeList()
        {
            Logger.Info("Entering in WorkFromHomeController APP GetWorkFromHomeList method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var WorkFromHomeList = await wfhOperations.GetWorkFromHomeListAsync(data.RefEmployeeId);

                    Logger.Info("Successfully exiting from WorkFromHomeController APP GetWorkFromHomeList method");
                    return WorkFromHomeList;
                }
                Logger.Info("Successfully exiting from WorkFromHomeController APP GetWorkFromHomeList method");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController APP GetWorkFromHomeList method.", ex);
                return null;
            }
        }

        public async Task<JsonResult> GetWorkFromHomeReasonsList()
        {

            Logger.Info("Entering in WorkFromHomeController APP GetWorkFromHomeReasonsList method");
            try
            {
                if (null != Session[Constants.SESSION_OBJ_USER])
                {
                    var data = (UserAccount)Session[Constants.SESSION_OBJ_USER];
                    var workFromHomeReasonsList = await wfhOperations.GetWorkFromHomeReasonsListAsync();

                    Logger.Info("Successfully exiting from WorkFromHomeController APP GetWorkFromHomeReasonsList method");
                    return Json(new { workFromHomeReasonsList=workFromHomeReasonsList });
                }
                Logger.Info("Successfully exiting from WorkFromHomeController APP GetWorkFromHomeReasonsList method");
                return null;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController APP GetWorkFromHomeReasonsList method.", ex);
                return null;
            }
        }
    }
}
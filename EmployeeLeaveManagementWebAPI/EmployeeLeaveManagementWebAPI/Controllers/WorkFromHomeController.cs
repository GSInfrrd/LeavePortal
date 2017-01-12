using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace EmployeeLeaveManagementWebAPI.Controllers
{
    public class WorkFromHomeController : ApiController
    {
        WorkFromHomeManagement WorkFromHomeManager = new WorkFromHomeManagement();

        [System.Web.Http.HttpPost]
        public long AddNewWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API AddNewWorkFromHome method");
                var result = WorkFromHomeManager.AddNewWorkFromHome(model);
                Logger.Info("Successfully exiting from WorkFromHomeController API AddNewWorkFromHome method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API AddNewWorkFromHome method.", ex);
                throw ex;
            }
        }

        [System.Web.Http.HttpGet]
        public IList<WorkFromHomeModel> GetWorkFromHomeList(int EmpId)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API GetWorkFromHomeList method");
                var result = WorkFromHomeManager.GetWorkFromHomeList(EmpId);
                Logger.Info("Successfully exiting from WorkFromHomeController API GetWorkFromHomeList method");
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API GetWorkFromHomeList method.", ex);
                throw ex;
            }
        }

        [System.Web.Http.HttpDelete]
        public long DeleteWorkFromHome(long Id)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API DeleteWorkFromHome method");
                Logger.Info("Successfully exiting from WorkFromHomeController API DeleteWorkFromHome method");
                return WorkFromHomeManager.DeleteWorkFromHome(Id);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API DeleteWorkFromHome method.", ex);
                throw ex;
            }
        }

        [System.Web.Http.HttpPut]
        public bool UpdateWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                Logger.Info("Entering in WorkFromHomeController API UpdateWorkFromHome method");
                Logger.Info("Successfully exiting from WorkFromHomeController API UpdateWorkFromHome method");
                return WorkFromHomeManager.UpdateWorkFromHome(model);
            }
            catch (Exception ex)
            {
                Logger.Error("Error at WorkFromHomeController API UpdateWorkFromHome method.", ex);
                throw ex;
            }
        }

    }
}
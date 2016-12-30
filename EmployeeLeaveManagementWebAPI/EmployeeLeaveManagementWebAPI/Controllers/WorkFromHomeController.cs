using LMS_WebAPI_Domain;
using LMS_WebAPI_ServiceHelpers;
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
                var result = WorkFromHomeManager.AddNewWorkFromHome(model);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [System.Web.Http.HttpGet]
        public IList<WorkFromHomeModel> GetWorkFromHomeList(int EmpId)
        {
            try
            {
                var result = WorkFromHomeManager.GetWorkFromHomeList(EmpId);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.HttpDelete]
        public long DeleteWorkFromHome(long Id)
        {
            try
            {
                return WorkFromHomeManager.DeleteWorkFromHome(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [System.Web.Http.HttpPut]
        public bool UpdateWorkFromHome(WorkFromHomeModel model)
        {
            try
            {
                return WorkFromHomeManager.UpdateWorkFromHome(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
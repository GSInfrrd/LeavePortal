using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System.Collections.Generic;

namespace LMS_WebAPI_ServiceHelpers
{
    public  class ApproveLeaveManagement
    {
        private IApproveLeaveRepository EmployeeLeaves = new ApproveLeaveRepository();

        
        public List<EmployeeDetailsModel> GetAllManagers(int id , int st)
        {
            Logger.Info("Entering into ApproveLeaveManagement Service helper GetAllManagers method ");
            try
            {
                var ManagerDetails = EmployeeLeaves.GetAllManagers(id, st);
                Logger.Info("Exiting from into ApproveLeaveManagement Service helper GetAllManagers method ");
                return ManagerDetails;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveManagement Service helper GetAllManagers method ");
                throw;
            }
        }
        public List<ApproveLeaveModel> GetApproveLeave(int id)
        {
            Logger.Info("Entering into ApproveLeaveManagement Service helper GetApproveLeave method ");
            try
            {
                var ApproveLeaves = EmployeeLeaves.GetApproveLeave(id);
                Logger.Info("Exiting from into ApproveLeaveManagement Service helper GetApproveLeave method ");
                return ApproveLeaves;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveManagement Service helper GetApproveLeave method ");
                throw;

            }
        }

        

        public bool ApproveEmployeeLeave(int id, string comments, int st , int apid)
        {
            Logger.Info("Entering into ApproveLeaveManagement Service helper ApproveEmployeeLeave method ");
            try
            {
                var ApproveLeaves = EmployeeLeaves.ApproveEmployeeLeave(id,comments,st,apid);
                Logger.Info("Exiting from into ApproveLeaveManagement Service helper ApproveEmployeeLeave method ");
                return ApproveLeaves;
            }
            catch
            {
                Logger.Info("Exception occured at ApproveLeaveManagement Service helper ApproveEmployeeLeave method ");
                throw;

            }
        }
    }
}

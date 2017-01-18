﻿using Domain;
using LMS_WebAPI_DAL;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ResourceRequestManagemenet
    {
        private IResourceRequestRepository _resourceRequest = new ResourceRequestRepository();
        public ResourceDetails GetResourceRequestFormDetails(int managerId)
        {
            try
            {
                var resourceDetails = new ResourceDetails();
                resourceDetails = _resourceRequest.GetResourceRequestFormDetails(managerId);
                return resourceDetails;
            }
            catch
            {

                throw;
            }
        }

        public ResourceRequestDetailModel SubmitResourceRequest(ResourceRequestDetail model)
        {
            try
            {
                var requestModel = _resourceRequest.SubmitResourceRequest(model);

                return requestModel;
            }
            catch
            {

                throw;
            }
        }

        public List<ResourceRequestDetailModel> GetResourceRequests(int userId, bool viewAll)
        {
            try
            {
                var requestModel = _resourceRequest.GetResourceRequestDetails(userId, viewAll);

                return requestModel;
            }
            catch 
            {

                throw;
            }
        }

        public ResourceRequestDetailModel SubmitResourceRequestResponse(ResourceRequestDetail model)
        {
            try
            {
                var responseModel = _resourceRequest.SubmitResourceRequestResponse(model);

                return responseModel;
            }
            catch 
            {

                throw;
            }
        }

        public bool DeleteResourceRequestManagement(string ticket)
        {
            Logger.Info("Entering into ResourceRequestManagement Service helper DeleteResourceRequestManagement method ");
            try
            {
                bool deleted = false;
                deleted = _resourceRequest.DeleteRequest(ticket);

                return deleted;
            }
            catch
            {
                Logger.Error("Exception occurred at ResourceRequestManagement Service helper DeleteResourceRequestManagement method ");
                throw;
            }
        }

        public List<TeamMembers> GetProjectMembersList(int projectId)
        {
            Logger.Info("Entering into ResourceRequestManagement Service helper GetProjectMembersList method ");

            try
            {

                var responseModel = _resourceRequest.GetProjectMembersList(projectId);
                Logger.Info("Exiting ResourceRequestManagement Service helper GetProjectMembersList method ");
                return responseModel;
            }
            catch
            {
                Logger.Error("Exception occurred at ResourceRequestManagement Service helper GetProjectMembersList method ");

                throw;
            }
        }

        public bool RemoveProjectResource(int projectId)
        {
            Logger.Info("Entering into ResourceRequestManagement Service helper RemoveProjectResource method ");

            try
            {

                var result = _resourceRequest.RemoveProjectResource(projectId);
                Logger.Info("Exiting ResourceRequestManagement Service helper RemoveProjectResource method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occurred at ResourceRequestManagement Service helper RemoveProjectResource method ");

                throw;
            }
        }

        public List<TeamMembers> GetResourceList()
        {
            Logger.Info("Entering into ResourceRequestManagement Service helper GetResourceList method ");

            try
            {

                var responseModel = _resourceRequest.GetResourceList();
                Logger.Info("Exiting ResourceRequestManagement Service helper GetResourceList method ");
                return responseModel;
            }
            catch
            {
                Logger.Error("Exception occurred at ResourceRequestManagement Service helper GetResourceList method ");

                throw;
            }
        }

        public bool AddNewProjectResource(int employeeId, int projectId)
        {
            Logger.Info("Entering into ResourceRequestManagement Service helper RemoveProjectResource method ");

            try
            {

                var result = _resourceRequest.AddNewProjectResource(employeeId, projectId);
                Logger.Info("Exiting ResourceRequestManagement Service helper RemoveProjectResource method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occurred at ResourceRequestManagement Service helper RemoveProjectResource method ");

                throw;
            }
        }
    }
}

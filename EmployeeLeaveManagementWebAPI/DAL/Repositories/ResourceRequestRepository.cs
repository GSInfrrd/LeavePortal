using Domain;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class ResourceRequestRepository : IResourceRequestRepository
    {
        public ResourceDetails GetResourceRequestFormDetails(int managerId)
        {
            var resourceDetails = new ResourceDetails();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var allHR = ctx.EmployeeDetails.Include("MasterDataValue").Where(x => x.MasterDataValue.Id == (Int16)EmployeeRole.HR).ToList();
                    var skills = ctx.MasterDataValues.Where(y => y.RefMasterType == (Int16)SkillsProjects.Skills).Select(y => y.Value).ToList();

                    var lstHR = new List<EmployeeDetailsModel>();

                    foreach (var hr in allHR)
                    {
                        var resourceFormDetails = new EmployeeDetailsModel()
                        {
                            Id = hr.Id,
                            FirstName = hr.FirstName,
                            LastName = hr.LastName
                        };
                        lstHR.Add(resourceFormDetails);
                    }
                    resourceDetails.ListOfHR = lstHR;
                    resourceDetails.Skills = skills;

                    bool isManager = true;
                    resourceDetails.ResourceRequestHistory = GetResourceRequestDetails(managerId, isManager);
                }
                return resourceDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ResourceRequestDetailModel> GetResourceRequestDetails(int Id,bool isManager)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var lstHR = ctx.EmployeeDetails.Include("MasterDataValue").Where(x => x.MasterDataValue.Id == (Int16)EmployeeRole.HR).ToList();
                    var lstManagers = ctx.EmployeeDetails.Include("MasterDataValue").Where(x => x.MasterDataValue.Id == (Int16)EmployeeRole.Manager).ToList();
                    var lstResourceDetails = new List<ResourceRequestDetailModel>();
                    var resourceRequests = ctx.ResourceRequestDetails.ToList();
                    if (isManager)
                    {
                        resourceRequests = resourceRequests.Where(x => x.RequestFromId == Id).ToList();
                        if (null != resourceRequests)
                        {
                            foreach (var resource in resourceRequests)
                            {
                                var resourceRequest = new ResourceRequestDetailModel()
                                {
                                    Id = resource.Id,
                                    ResourceRequestTitle = resource.ResourceRequestTitle,
                                    CreatedDate = resource.CreatedDate,
                                    UpdatedDate = resource.UpdatedDate,
                                    Ticket = resource.Ticket,
                                    Status = resource.Status,
                                    NumberRequestedResources = resource.NumberRequestedResources,
                                    Skills = resource.Skills,
                                    RequestToName = lstHR.Where(x => x.Id == resource.RequestToId).Select(x => x.FirstName + x.LastName).FirstOrDefault()
                                };
                                lstResourceDetails.Add(resourceRequest);
                            }
                        }
                    }
                    else
                    {
                        resourceRequests = resourceRequests.Where(x => x.RequestToId == Id).ToList();
                        if (null != resourceRequests)
                        {
                            foreach (var resource in resourceRequests)
                            {
                                var resourceRequest = new ResourceRequestDetailModel()
                                {
                                    Id = resource.Id,
                                    ResourceRequestTitle = resource.ResourceRequestTitle,
                                    CreatedDate = resource.CreatedDate,
                                    UpdatedDate = resource.UpdatedDate,
                                    Ticket = resource.Ticket,
                                    Status = resource.Status,
                                    NumberRequestedResources = resource.NumberRequestedResources,
                                    Skills = resource.Skills,
                                    RequestFromName = lstManagers.Where(x => x.Id == resource.RequestFromId).Select(x => x.FirstName + x.LastName).FirstOrDefault()
                                };
                                lstResourceDetails.Add(resourceRequest);
                            }
                        }
                    }
                    return lstResourceDetails;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ResourceRequestDetailModel SubmitResourceRequest(ResourceRequestDetail model)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var newRecord = ctx.ResourceRequestDetails.Add(model);
                    ctx.SaveChanges();
                    var lstHR = ctx.EmployeeDetails.Include("MasterDataValue").Where(x => x.MasterDataValue.Id == (Int16)EmployeeRole.HR).ToList();
                    var resourceDetail = new ResourceRequestDetailModel()
                    {
                        Ticket = model.Ticket,
                        ResourceRequestTitle = model.ResourceRequestTitle,
                        NumberRequestedResources = model.NumberRequestedResources,
                        Status = model.Status,
                        RequestToName = lstHR.Where(x => x.Id == model.RequestToId).Select(x => x.FirstName + x.LastName).FirstOrDefault(),
                        Skills = model.Skills,
                        CreatedDate = model.CreatedDate,
                        UpdatedDate = model.UpdatedDate
                    };
                    return resourceDetail;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResourceRequestDetailModel SubmitResourceRequestResponse(ResourceRequestDetail model)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var request = ctx.ResourceRequestDetails.FirstOrDefault(x => x.Ticket == model.Ticket);
                    if (null != request)
                    {
                        request.Status = model.Status;
                        ctx.SaveChanges();
                    }
                    else
                    {
                        return null;
                    }
                    var resourceRequestReponse = new ResourceRequestDetailModel()
                    {
                        Ticket = model.Ticket,
                        Status = model.Status
                    };
                    return resourceRequestReponse;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}



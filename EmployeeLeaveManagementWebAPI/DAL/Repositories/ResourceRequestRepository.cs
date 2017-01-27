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
                    var allHR = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int16)EmployeeRole.HR).ToList();
                    var skills = ctx.MasterDataValues.Where(y => y.RefMasterType == (Int16)MasterDataTypeEnum.Skills).Select(y => y.Value).ToList();

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

                    bool viewAll = false;
                    int count;
                    resourceDetails.ResourceRequestHistory = GetResourceRequestDetails(managerId, viewAll, out count);
                    resourceDetails.Count = count;
                }
                return resourceDetails;
            }
            catch
            {
                throw;
            }
        }

        public List<ResourceRequestDetailModel> GetResourceRequestDetails(int userId, bool viewAll, out int count)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var lstHR = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int16)EmployeeRole.HR).ToList();
                    var lstManagers = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int16)EmployeeRole.Manager).ToList();
                    var role = ctx.EmployeeDetails.Where(x => x.Id == userId).Select(y => y.RefRoleId).FirstOrDefault();
                    var lstResourceDetails = new List<ResourceRequestDetailModel>();
                    var resourceRequests = ctx.ResourceRequestDetails.ToList();
                    count = resourceRequests.Count;
                    if (role == (Int16)EmployeeRole.Manager)
                    {
                        if (!viewAll)
                        {
                            resourceRequests = resourceRequests.Where(x => x.RequestFromId == userId).OrderByDescending(y => y.UpdatedDate).Take(10).ToList();
                        }
                        else
                        {
                            resourceRequests = resourceRequests.Where(x => x.RequestFromId == userId).OrderByDescending(y => y.UpdatedDate).Skip(10).ToList();
                        }

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
                                    RequestFromId = resource.RequestFromId,
                                    RequestToId = resource.RequestToId,
                                    RequestToName = lstHR.Where(x => x.Id == resource.RequestToId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault()
                                };
                                lstResourceDetails.Add(resourceRequest);
                            }
                        }
                    }
                    else if (role == (Int16)EmployeeRole.HR)
                    {
                        if (!viewAll)
                        {
                            resourceRequests = resourceRequests.Where(x => x.RequestToId == userId).OrderByDescending(y => y.UpdatedDate).Take(10).ToList();
                        }
                        else
                        {
                            resourceRequests = resourceRequests.Where(x => x.RequestToId == userId).OrderByDescending(y => y.UpdatedDate).Skip(10).ToList();
                        }

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
                                    RequestFromId = resource.RequestFromId,
                                    RequestToId = resource.RequestToId,
                                    RequestFromName = lstManagers.Where(x => x.Id == resource.RequestFromId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault()
                                };
                                lstResourceDetails.Add(resourceRequest);
                            }
                        }
                    }
                    return lstResourceDetails;
                }
            }
            catch
            {

                throw;
            }
        }

        public ResourceDetails SubmitResourceRequest(ResourceRequestDetail model)
        {
            try
            {
                var resourceRequests = new ResourceDetails();
                resourceRequests.Result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var newRecord = ctx.ResourceRequestDetails.Add(model);
                    ctx.SaveChanges();
                    if (null != model)
                    {
                        int count;
                        bool viewAll = false;
                        resourceRequests.Result = true;
                        resourceRequests.ResourceRequestHistory = GetResourceRequestDetails(model.RequestFromId, viewAll, out count);
                        resourceRequests.Count = count;

                        return resourceRequests;
                    }
                    //var lstHR = ctx.EmployeeDetails.Include("MasterDataValue").Where(x => x.MasterDataValue.Id == (Int16)EmployeeRole.HR).ToList();
                    else
                    {
                        return resourceRequests;
                    }
                }
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
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var lstHR = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int16)EmployeeRole.HR).ToList();
                    var lstManagers = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int16)EmployeeRole.Manager).ToList();
                    var requestSubmit = ctx.ResourceRequestDetails.FirstOrDefault(x => x.Ticket == model.Ticket);

                    if (null != requestSubmit)
                    {
                        requestSubmit.Status = model.Status;
                        requestSubmit.UpdatedDate = DateTime.Now;
                        var result = ctx.SaveChanges();
                        if (result == 1)
                        {
                            var resourceResponseDetails = new ResourceRequestDetailModel()
                            {
                                ResourceRequestTitle = model.ResourceRequestTitle,
                                Ticket = model.Ticket,
                                RequestFromId = model.RequestFromId,
                                RequestToId = model.RequestToId,
                                Skills = model.Skills,
                                Result = true
                            };
                            return resourceResponseDetails;
                        }
                    }
                    return null;
                    
                }
            }
            catch
            {

                throw;
            }
        }

        public ResourceDetails DeleteRequest(string ticket, int userId)
        {
            Logger.Info("Entering in ResourceRequestRepository API GetProjectMembersList method");
            try
            {
                var resourceRequests = new ResourceDetails();
                resourceRequests.Result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var request = ctx.ResourceRequestDetails.FirstOrDefault(x => x.Ticket == ticket);
                    if (null != request)
                    {
                        ctx.ResourceRequestDetails.Remove(request);
                        ctx.SaveChanges();

                        int count;
                        bool viewAll = false;
                        resourceRequests.Result = true;
                        resourceRequests.ResourceRequestHistory = GetResourceRequestDetails(userId, viewAll, out count);
                        resourceRequests.Count = count;

                        Logger.Info("Exiting in ResourceRequestRepository API GetProjectMembersList method");
                        return resourceRequests;
                    }
                    else
                    {
                        Logger.Info("Exiting in ResourceRequestRepository API GetProjectMembersList method");
                        return resourceRequests;
                    }
                }
            }
            catch
            {
                Logger.Error("Exception occured at ResourceRequestRepository GetProjectMembersList method ");
                throw;
            }
        }

        public List<TeamMembers> GetProjectMembersList(int projectId)
        {
            Logger.Info("Entering in ResourceRequestRepository API GetProjectMembersList method");
            try
            {
                var employeeList = new List<TeamMembers>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var projectsList = ctx.EmployeeProjectDetails.Include("EmployeeDetail").Where(i => i.RefProjectId == projectId && i.IsActive == true).ToList();

                    foreach (var item in projectsList)
                    {
                        var employee = new TeamMembers();
                        employee.Id = item.Id;
                        employee.ImagePath = string.Format("data:image/png;base64,{0}", item.EmployeeDetail.ImagePath); ;
                        employee.FirstName = item.EmployeeDetail.FirstName;
                        employee.Role = item.EmployeeDetail.MasterDataValue.Value;
                        employeeList.Add(employee);
                    }
                }
                Logger.Info("Exiting in ResourceRequestRepository API GetProjectMembersList method");

                return employeeList;
            }
            catch
            {
                Logger.Error("Exception occured at ResourceRequestRepository GetProjectMembersList method ");
                throw;
            }
        }

        public bool RemoveProjectResource(int projectId)
        {
            Logger.Info("Entering in ResourceRequestRepository API RemoveProjectResource method");
            try
            {
                var result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var project = ctx.EmployeeProjectDetails.FirstOrDefault(i => i.Id == projectId);
                    if (project != null)
                    {
                        project.IsActive = false;
                        project.EndDate = DateTime.Now;
                        project.ModifiedDate = DateTime.Now;
                        ctx.SaveChanges();
                        result = true;
                    }
                }
                Logger.Info("Exiting in ResourceRequestRepository API RemoveProjectResource method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at ResourceRequestRepository RemoveProjectResource method ");
                throw;
            }
        }

        public List<TeamMembers> GetResourceList()
        {
            Logger.Info("Entering in ResourceRequestRepository API GetResourceList method");
            try
            {
                var employeeList = new List<TeamMembers>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    // var workingEmployees= ctx.EmployeeProjectDetails.Where(x => x.IsActive == true).Select(x => x.RefEmployeeId).ToList();
                    var employeeOnBench = ctx.EmployeeDetails.Include("EmployeeProjectDetails").Where(x => x.RefRoleId != (int)EmployeeRole.HR).ToList();
                    foreach (var item in employeeOnBench)
                    {
                        var employee = new TeamMembers();
                        var isActive = false;
                        employee.Id = item.Id;
                        employee.FirstName = item.FirstName;
                        if (item.EmployeeProjectDetails.Count != 0)
                        {
                            isActive = item.EmployeeProjectDetails.Any(x => x.RefEmployeeId == item.Id && x.IsActive == true);
                        }
                        employee.IsActive = isActive;
                        employeeList.Add(employee);
                    }
                }

                Logger.Info("Exiting in ResourceRequestRepository API GetResourceList method");

                return employeeList;
            }
            catch
            {
                Logger.Error("Exception occured at ResourceRequestRepository GetResourceList method ");
                throw;
            }
        }

        public bool AddNewProjectResource(int employeeId, int projectId)
        {
            Logger.Info("Entering in ResourceRequestRepository API AddNewProjectResource method");
            try
            {
                var result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var project = ctx.EmployeeProjectDetails.FirstOrDefault(i => i.RefEmployeeId == employeeId);

                    if (project != null)
                    {
                        project.IsActive = false;
                        project.EndDate = DateTime.Now;
                        project.ModifiedDate = DateTime.Now;
                        ctx.SaveChanges();
                    }
                    var newProject = new EmployeeProjectDetail();
                    newProject.RefEmployeeId = employeeId;
                    newProject.RefProjectId = projectId;
                    newProject.IsActive = true;
                    newProject.StartDate = DateTime.Now;
                    newProject.CreatedDate = DateTime.Now;
                    ctx.EmployeeProjectDetails.Add(newProject);
                    ctx.SaveChanges();
                    result = true;
                }
                Logger.Info("Exiting in ResourceRequestRepository API AddNewProjectResource method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at ResourceRequestRepository AddNewProjectResource method ");
                throw;
            }
        }
    }
}



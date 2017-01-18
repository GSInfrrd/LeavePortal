using Domain;
using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
    public interface IResourceRequestRepository
    {
        ResourceDetails GetResourceRequestFormDetails(int managerId);

        ResourceRequestDetailModel SubmitResourceRequest(ResourceRequestDetail model);

        List<ResourceRequestDetailModel> GetResourceRequestDetails(int userId, bool viewAll);

        ResourceRequestDetailModel SubmitResourceRequestResponse(ResourceRequestDetail model);

        bool DeleteRequest(string ticket);

        List<TeamMembers> GetProjectMembersList(int projectId);

        bool RemoveProjectResource(int projectId);

        List<TeamMembers> GetResourceList();

        bool AddNewProjectResource(int employeeId, int projectId);
    }
}
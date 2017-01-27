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

        ResourceDetails SubmitResourceRequest(ResourceRequestDetail model);

        List<ResourceRequestDetailModel> GetResourceRequestDetails(int userId, bool viewAll, out int count);

        ResourceRequestDetailModel SubmitResourceRequestResponse(ResourceRequestDetail model);

        ResourceDetails DeleteRequest(string ticket, int userId);

        List<TeamMembers> GetProjectMembersList(int projectId);

        bool RemoveProjectResource(int projectId);

        List<TeamMembers> GetResourceList();

        bool AddNewProjectResource(int employeeId, int projectId);
    }
}
﻿using Domain;
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

        List<ResourceRequestDetailModel> GetResourceRequestDetails(int hrId, bool isManager);

        ResourceRequestDetailModel SubmitResourceRequestResponse(ResourceRequestDetail model);
    }
}
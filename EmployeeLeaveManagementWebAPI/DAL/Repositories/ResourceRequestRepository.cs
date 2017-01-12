using Domain;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class ResourceRequestRepository : IResourceRequestRepository
    {
        public ResourceDetails GetResourceRequestFormDetails()
        {
            Logger.Info("Entering in ResourceRequestRepository API GetResourceRequestFormDetails method");
            var resourceDetails = new ResourceDetails();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var allHR = ctx.EmployeeDetails.Include("MasterDataValue").Where(x => x.MasterDataValue.Id == (Int16)EmployeeRole.HR).ToList();
                    var skills = ctx.MasterDataValues.Where(x => x.RefMasterType == (Int16)SkillsProjects.Skills).Select(x => x.Value).ToList();
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
                }
                Logger.Info("Successfully exiting from ResourceRequestRepository API GetResourceRequestFormDetails method");
                return resourceDetails;
            }
            catch
            {
                Logger.Info("Exception occured at ResourceRequestRepository GetResourceRequestFormDetails method ");
                throw;
            }
        }
    }
}



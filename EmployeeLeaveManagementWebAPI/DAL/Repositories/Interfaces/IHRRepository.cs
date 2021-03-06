﻿using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories.Interfaces
{
  public interface IHRRepository 
    {
        bool SubmitEmployeeDetails(EmployeeDetailsModel model,string OTP, out int EmployeeId);
        List<EmployeeDetailsModel> GetEmployeeList();

        List<EmployeeDetailsModel> GetManagerList(int refLevel);

        List<ConsolidatedEmployeeLeaveDetailsModel> GetReportData(string fromDate,string toDate,List<int> employeeId, out List<DetailedLeaveReport> detailsList);
       ConsolidatedEmployeeLeaveDetailsModel GetChartDetails(int employeeId);

        bool AddNewMasterDataValues(int masterDataType, string masterDataValue);
        bool AddNewProjectInfo(string projectName, string description, string technology, string technologyDetails, DateTime startDate, int refManager);

        bool AddCompanyAnnouncements(string title, string carouselContent, string imagePath);
        List<ProjectsList> GetProjectsList(int managerId=0);
        List<EmployeeSkillDetails> GetSkillsList();

        List<CountryDetails> GetCountries();

        List<RelationshipDetails> GetRelationships();

        List<BloodGroupDetails> GetBloodGroups();

        List<FacilityDetails> GetFacilities();

        List<StateDetails> GetStates(int CountryId);

        FacilityDetails GetWorkFacilityDetails(int FacilityId);

        List<CityDetails> GetCities(int StateId);

        List<FacilityDetails> GetFacilities(int CityId);

        List<TechnologyDetails> GetTechnologiesList();

        List<TechnologyDescriptions> GetTechnologyDetailsList(List<TechnologyDetails> technologies);
        bool CheckForExistingMasterDataValues(int masterDataType, string masterDataValue);
        bool CheckForExistingProjectMasterDataValues(string projectName, string technology, int refManager);

        List<MasterDataModel> GetRolesList();
        LeaveReportModel GetProjectwiseReport(int projectId, int fromMonth, int toMonth, int year);
        List<ProjectsList> GetProjectwiseEmployeeDetails(int projectId, int fromMonth, int toMonth, int year);

        bool CheckEmployeeNumber(string employeeNumber);

        bool CheckEmployeeMail(string employeeMailid);
    }
}

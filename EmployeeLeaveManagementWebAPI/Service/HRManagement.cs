﻿using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS_WebAPI_Utils;

namespace LMS_WebAPI_ServiceHelpers
{
    public class HRManagement
    {
       private IHRRepository hrRepo = new HRRepository();
        public bool SubmitEmployeeDetails(EmployeeDetailsModel model)
        {
            Logger.Info("Entering into HRManagement Service helper SubmitEmployeeDetails method ");
            try
            {
                var result = hrRepo.SubmitEmployeeDetails(model);
                Logger.Info("Exiting from into HRManagement Service helper SubmitEmployeeDetails method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper SubmitEmployeeDetails method ");
                throw;
            }
        }

        public List<EmployeeDetailsModel> GetEmployeeList()
        {
            Logger.Info("Entering into HRManagement Service helper GetEmployeeList method ");
            try
            {
                var result = hrRepo.GetEmployeeList();
                IUser usr = new UserRepository();
                var leaveReport = usr.GetLeaveReportDetails(DateTime.Now.Year);       
                result[0].leaveDetails = leaveReport;
                Logger.Info("Exiting from into HRManagement Service helper GetEmployeeList method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetEmployeeList method ");
                throw;
            }
        }

        public List<EmployeeDetailsModel> GetManagerList(int refLevel)
        {
            Logger.Info("Entering into HRManagement Service helper GetManagerList method ");
            try
            {
                var result = hrRepo.GetManagerList(refLevel);
                Logger.Info("Exiting from into HRManagement Service helper GetManagerList method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetManagerList method ");
                throw;
            }
        }

        public List<ConsolidatedEmployeeLeaveDetailsModel> GetReportData(string fromDate,string toDate,List<int> employeeId, out List<DetailedLeaveReport> detailsList)
        {
            Logger.Info("Entering into HRManagement Service helper GetReportData method ");
            try
            {
                var result = hrRepo.GetReportData(fromDate,toDate,employeeId,out detailsList);
                Logger.Info("Exiting from into HRManagement Service helper GetReportData method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetReportData method ");
                throw;
            }
        }

        public ConsolidatedEmployeeLeaveDetailsModel GetChartDetails(int employeeId)
        {
            Logger.Info("Entering into HRManagement Service helper GetChartDetails method ");
            try
            {
                var result = hrRepo.GetChartDetails(employeeId);
                Logger.Info("Exiting from into HRManagement Service helper GetChartDetails method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetChartDetails method ");
                throw;
            }
        }

        public bool AddNewMasterDataValues(int masterDataType,string masterDataValue)
        {
            Logger.Info("Entering into HRManagement Service helper AddNewMasterDataValues method ");
            try
            {
                var result = hrRepo.AddNewMasterDataValues(masterDataType,masterDataValue);
                Logger.Info("Exiting from into HRManagement Service helper AddNewMasterDataValues method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper AddNewMasterDataValues method ");
                throw;
            }
        }

        public bool AddNewProjectInfo(string projectName, string description, string technology, DateTime startDate, int refManager)
        {
            Logger.Info("Entering into HRManagement Service helper AddNewProjectInfo method ");
            try
            {
                var result = hrRepo.AddNewProjectInfo(projectName, description, technology, startDate, refManager);
                Logger.Info("Exiting from into HRManagement Service helper AddNewProjectInfo method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper AddNewProjectInfo method ");
                throw;
            }
        }

        public bool AddCompanyAnnouncements(string title, string carouselContent, string imagePath)
        {
            Logger.Info("Entering into HRManagement Service helper AddCompanyAnnouncements method ");
            try
            {
                var result = hrRepo.AddCompanyAnnouncements(title, carouselContent, imagePath);
                Logger.Info("Exiting from into HRManagement Service helper AddCompanyAnnouncements method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at HRManagement Service helper AddCompanyAnnouncements method ");
                throw;
            }
        }


        public List<ProjectsList> GetProjectsList(int managerId=0)
        {
            Logger.Info("Entering into HRManagement Service helper GetProjectsList method ");
            try
            {
                var result = hrRepo.GetProjectsList(managerId);
                Logger.Info("Exiting from into HRManagement Service helper GetProjectsList method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetProjectsList method ");
                throw;
            }
        }
        public List<EmployeeSkillDetails> GetSkillsList()
        {
            Logger.Info("Entering into HRManagement Service helper GetSkillsList method ");
            try
            {
                var result = hrRepo.GetSkillsList();
                Logger.Info("Exiting from into HRManagement Service helper GetSkillsList method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetSkillsList method ");
                throw;
            }
        }

        public bool CheckForExistingMasterDataValues(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering into HRManagement Service helper CheckForExistingMasterDataValues method ");
            try
            {
                var result = hrRepo.CheckForExistingMasterDataValues(masterDataType, masterDataValue);
                Logger.Info("Exiting from into HRManagement Service helper CheckForExistingMasterDataValues method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper CheckForExistingMasterDataValues method ");
                throw;
            }
        }
        public bool CheckForExistingProjectMasterDataValues(string projectName, string technology, int refManager)
        {
            Logger.Info("Entering into HRManagement Service helper CheckForExistingMasterDataValues method ");
            try
            {
                var result = hrRepo.CheckForExistingProjectMasterDataValues(projectName,technology,refManager);
                Logger.Info("Exiting from into HRManagement Service helper CheckForExistingMasterDataValues method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper CheckForExistingMasterDataValues method ");
                throw;
            }
        }

        public List<MasterDataModel> GetRolesList()
        {
            Logger.Info("Entering into HRManagement Service helper GetRolesList method ");
            try
            {
                var result = hrRepo.GetRolesList();
                Logger.Info("Exiting from into HRManagement Service helper GetRolesList method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetRolesList method ");
                throw;
            }
        }

        public LeaveReportModel GetProjectwiseReport(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering into HRManagement Service helper GetProjectwiseReport method ");
            try
            {
                var result = hrRepo.GetProjectwiseReport(projectId,fromMonth,toMonth,year);
                Logger.Info("Exiting from into HRManagement Service helper GetProjectwiseReport method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetProjectwiseReport method ");
                throw;
            }
        }
        public List<ProjectsList> GetProjectwiseEmployeeDetails(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering into HRManagement Service helper GetProjectwiseEmployeeDetails method ");
            try
            {
                var result = hrRepo.GetProjectwiseEmployeeDetails(projectId, fromMonth, toMonth, year);
                Logger.Info("Exiting from into HRManagement Service helper GetProjectwiseEmployeeDetails method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper GetProjectwiseEmployeeDetails method ");
                throw;
            }
        }

        public bool CheckEmployeeNumber(string employeeNumber)
        {
            Logger.Info("Entering into HRManagement Service helper CheckEmployeeNumber method ");
            try
            {
                var result = hrRepo.CheckEmployeeNumber(employeeNumber);
                Logger.Info("Exiting from into HRManagement Service helper CheckEmployeeNumber method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper CheckEmployeeNumber method ");
                throw;
            }
        }

        public bool CheckEmployeeMail(string employeeMailid)
        {
            Logger.Info("Entering into HRManagement Service helper CheckEmployeeMail method ");
            try
            {
                var result = hrRepo.CheckEmployeeMail(employeeMailid);
                Logger.Info("Exiting from into HRManagement Service helper CheckEmployeeMail method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRManagement Service helper CheckEmployeeMail method ");
                throw;
            }
        }

    }
}

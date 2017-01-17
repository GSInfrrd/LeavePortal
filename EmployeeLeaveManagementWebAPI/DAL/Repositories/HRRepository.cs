﻿using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class HRRepository : IHRRepository
    {
        public bool SubmitEmployeeDetails(EmployeeDetailsModel model)
        {
            Logger.Info("Entering in HRRepository API SubmitEmployeeDetails method");
            var result = false;
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    byte[] imageByteData = System.IO.File.ReadAllBytes(model.ImagePath);
                    string imageBase64Data = Convert.ToBase64String(imageByteData);
                    var employeeDetails = new EmployeeDetail
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        RefRoleId = model.RefRoleId,
                        City = model.City,
                        Country = model.Country,
                        CreatedDate = DateTime.Now,
                        EmpNumber = model.EmployeeNumber.ToString(),
                        PhoneNumber = model.Telephone,
                        RefHierarchyLevel = model.RefHierarchyLevel,
                        ManagerId = Convert.ToInt32(model.ManagerName),
                        DateOfJoining = model.DateOfJoining,
                        ImagePath = imageBase64Data,
                        RefEmployeeType = model.EmployeeType


                    };
                    ctx.EmployeeDetails.Add(employeeDetails);
                    ctx.SaveChanges();
                    var id = employeeDetails.Id;

                    var employeeEducationDetails = new EmployeeEducationDetail
                    {

                        Degree = model.EmployeeEducationDetails[0].Degree,
                        Institution = model.EmployeeEducationDetails[0].Institution,
                        FromDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[0]),
                        ToDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[1]),
                        RefEmployeeId = id
                    };
                    ctx.EmployeeEducationDetails.Add(employeeEducationDetails);
                    ctx.SaveChanges();
                    var employeeExperienceDetails = new EmployeeExperienceDetail
                    {
                        CompanyName = model.EmployeeExperienceDetails[0].Company,
                        Role = model.EmployeeExperienceDetails[0].Role,
                        FromDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[0]),
                        ToDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[1]),
                        RefEmployeeId = id
                    };
                    ctx.EmployeeExperienceDetails.Add(employeeExperienceDetails);
                    ctx.SaveChanges();
                    foreach (var skill in model.Skills)
                    {
                        var empSkill = new EmployeeSkill();
                        empSkill.RefEmployeeId = id;
                        empSkill.Skill = skill.SkillName;
                        ctx.EmployeeSkills.Add(empSkill);
                        ctx.SaveChanges();
                    }
                    if (model.Projects != null)
                    {
                        var projectDetails = new EmployeeProjectDetail();
                        projectDetails.RefEmployeeId = id;
                        projectDetails.RefProjectId = model.Projects[0].Id;
                        projectDetails.CreatedDate = DateTime.Now;
                        projectDetails.IsActive = true;
                        projectDetails.StartDate = DateTime.Now;
                        ctx.EmployeeProjectDetails.Add(projectDetails);
                        ctx.SaveChanges();
                    }
                    var userDetails = new UserAccount
                    {
                        UserName = model.Email,
                        Password = "Temp@123",
                        RefEmployeeId = id,
                        CreatedDate = DateTime.Now
                    };
                    ctx.UserAccounts.Add(userDetails);
                    ctx.SaveChanges();
                    var employeeMaster = new EmployeeLeaveMaster

                    {
                        RefEmployeeId = id,
                        EarnedCasualLeave = 2
                    };
                    ctx.EmployeeLeaveMasters.Add(employeeMaster);
                    ctx.SaveChanges();

                    Logger.Info("Successfully exiting from HRRepository API SubmitEmployeeDetails method");
                }
                result = true;

            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API SubmitEmployeeDetails method ");
                throw;
            }
            return result;

        }

        public List<EmployeeDetailsModel> GetEmployeeList()
        {
            Logger.Info("Entering in HRRepository API GetEmployeeList method");
            var list = new List<EmployeeDetailsModel>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var empList = ctx.EmployeeDetails.ToList();
                    foreach (var item in empList)
                    {
                        var listItem = new EmployeeDetailsModel();
                        listItem.Id = item.Id;
                        listItem.FirstName = item.FirstName;
                        listItem.LastName = item.LastName;
                        listItem.ManagerName = item.ManagerId != null ? ctx.EmployeeDetails.FirstOrDefault(i => i.Id == item.ManagerId).FirstName : string.Empty;
                        listItem.DateOfJoining = Convert.ToDateTime(item.DateOfJoining);
                        listItem.EmployeeNumber = Convert.ToInt32(item.EmpNumber);
                        listItem.RoleName = item.RefRoleId != 0 ? ctx.MasterDataValues.FirstOrDefault(i => i.Id == item.RefRoleId).Value : string.Empty;
                        list.Add(listItem);
                    }
                    var leaveDetails = ctx.EmployeeLeaveTransactions.GroupBy(x => x.CreatedDate.Month).ToList();

                }
                Logger.Info("Successfully exiting from HRRepository API GetEmployeeList method");
                return list;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API GetEmployeeList method ");
                throw;
            }
        }

        public List<EmployeeDetailsModel> GetManagerList(int refLevel)
        {
            Logger.Info("Entering in HRRepository API GetManagerList method");
            var list = new List<EmployeeDetailsModel>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var empList = ctx.EmployeeDetails.Where(i => i.RefHierarchyLevel < refLevel).ToList();
                    foreach (var item in empList)
                    {
                        var listItem = new EmployeeDetailsModel();
                        listItem.Id = item.Id;
                        listItem.FirstName = item.FirstName;
                        listItem.LastName = item.LastName;
                        list.Add(listItem);
                    }

                }
                Logger.Info("Successfully exiting from HRRepository API GetManagerList method");
                return list;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API GetManagerList method ");
                throw;
            }
        }

        public List<ConsolidatedEmployeeLeaveDetailsModel> GetReportData(string fromDate, string toDate, List<int> employeeId, out List<DetailedLeaveReport> detailsList)
        {
            Logger.Info("Entering in HRRepository API GetReportData method");
            var list = new List<ConsolidatedEmployeeLeaveDetailsModel>();
            var ddList = new List<DetailedLeaveReport>();
            try
            {
                var reportFromDate = Convert.ToDateTime(fromDate);
                var reportToDate = Convert.ToDateTime(toDate);

                var dataList = new List<EmployeeDetail>();
                var empDatalist = new EmployeeDetail();
                var detailedDataList = new List<EmployeeLeaveTransactionHistory>();
                var detailedWfhList = new List<WorkFromHome>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var totalLeaves = ctx.LeaveMasters.Select(x => x.Count).Sum();
                    if (employeeId[0] == 0)
                    {
                        dataList = ctx.EmployeeDetails.Include("EmployeeLeaveTransactionHistories").Include("WorkFromHomes").ToList();
                        detailedDataList = ctx.EmployeeLeaveTransactionHistories.Where(i => i.FromDate >= reportFromDate && i.ToDate <= reportToDate).ToList();
                        detailedWfhList = ctx.WorkFromHomes.Where(i => i.Date >= reportFromDate && i.Date <= reportToDate).ToList();
                    }
                    else if (employeeId[0] != 0)
                    {
                        foreach (var item in employeeId)
                        {

                            empDatalist = ctx.EmployeeDetails.Include("EmployeeLeaveTransactionHistories").Include("WorkFromHomes").FirstOrDefault(x => x.Id == item);
                            detailedDataList = ctx.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == item && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).ToList();
                            detailedWfhList = ctx.WorkFromHomes.Where(i => i.RefEmployeeId == item && i.Date >= reportFromDate && i.Date <= reportToDate).ToList();

                            dataList.Add(empDatalist);
                        }
                    }
                    foreach (var item in detailedDataList)
                    {
                        var detailedList = new DetailedLeaveReport();
                        detailedList.EmpoyeeName = item.EmployeeDetail.FirstName;
                        detailedList.LeaveType = item.MasterDataValue.Value;
                        detailedList.FromDate = item.FromDate;
                        detailedList.ToDate = item.ToDate;
                        ddList.Add(detailedList);
                    }
                    foreach (var item in detailedWfhList)
                    {
                        var detailedList = new DetailedLeaveReport();
                        detailedList.EmpoyeeName = item.EmployeeDetail.FirstName;
                        detailedList.LeaveType = item.MasterDataValue.Value;
                        detailedList.FromDate = item.Date;
                        detailedList.ToDate = item.Date;
                        ddList.Add(detailedList);
                    }
                    foreach (var item in dataList)
                    {
                        var listItem = new ConsolidatedEmployeeLeaveDetailsModel();
                        listItem.RefEmployeeId = item.Id;
                        listItem.EmployeeName = item.FirstName;
                        listItem.AppliedLeavesCount = (int)item.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == item.Id && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).Select(i => i.NumberOfWorkingDays).Sum();
                        listItem.LossofPayCount = listItem.AppliedLeavesCount > totalLeaves ? listItem.AppliedLeavesCount - totalLeaves : 0;
                        listItem.WorkFromHomeCount = item.WorkFromHomes.Where(i => i.RefEmployeeId == item.Id && i.Date >= reportFromDate && i.Date <= reportToDate).ToList().Count;
                        listItem.CompOffCount = (int)item.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == item.Id && i.RefLeaveType == (int)LeaveType.CompOff && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).Select(i => i.NumberOfWorkingDays).Sum();
                        listItem.AdvancedLeavesCount = (int)item.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == item.Id && i.RefLeaveType == (int)LeaveType.AdvanceLeave && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).Select(i => i.NumberOfWorkingDays).Sum();
                        list.Add(listItem);
                    }
                }
                detailsList = ddList;
                Logger.Info("Successfully exiting from HRRepository API GetReportData method");
                return list;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API GetReportData method ");
                throw;
            }
        }

        public ConsolidatedEmployeeLeaveDetailsModel GetChartDetails(int employeeId)
        {
            Logger.Info("Entering in HRRepository API GetChartDetails method");
            var listItem = new ConsolidatedEmployeeLeaveDetailsModel();
            try
            {
                var empDatalist = new EmployeeDetail();

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var totalLeaves = ctx.LeaveMasters.Select(x => x.Count).Sum();
                    empDatalist = ctx.EmployeeDetails.Include("EmployeeLeaveTransactionHistories").Include("WorkFromHomes").FirstOrDefault(x => x.Id == employeeId);



                    listItem.RefEmployeeId = empDatalist.Id;
                    listItem.EmployeeName = empDatalist.FirstName;
                    listItem.AppliedLeavesCount = (int)empDatalist.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == empDatalist.Id && (i.RefLeaveType != (int)LeaveType.AdvanceLeave || i.RefLeaveType != (int)LeaveType.CompOff)).Select(i => i.NumberOfWorkingDays).Sum();
                    listItem.LossofPayCount = listItem.AppliedLeavesCount > totalLeaves ? listItem.AppliedLeavesCount - totalLeaves : 0;
                    listItem.WorkFromHomeCount = empDatalist.WorkFromHomes.Where(i => i.RefEmployeeId == empDatalist.Id).ToList().Count;
                    listItem.CompOffCount = (int)empDatalist.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == empDatalist.Id && i.RefLeaveType == (int)LeaveType.CompOff).Select(i => i.NumberOfWorkingDays).Sum();
                    listItem.AdvancedLeavesCount = (int)empDatalist.EmployeeLeaveTransactionHistories.Where(i => i.RefEmployeeId == empDatalist.Id && i.RefLeaveType == (int)LeaveType.AdvanceLeave).Select(i => i.NumberOfWorkingDays).Sum();


                }
                Logger.Info("Successfully exiting from HRRepository API GetChartDetails method");
                return listItem;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API GetChartDetails method ");
                throw;
            }
        }

        public bool AddNewMasterDataValues(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering in HRRepository API AddNewMasterDataValues method");
            try
            {

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var data = new MasterDataValue();
                    data.Value = masterDataValue;
                    data.RefMasterType = masterDataType;
                    ctx.MasterDataValues.Add(data);
                    ctx.SaveChanges();
                }
                Logger.Info("Successfully exiting from HRRepository API AddNewMasterDataValues method");
                return true;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API AddNewMasterDataValues method ");
                throw;
            }
        }

        public bool AddNewProjectInfo(string projectName, string description, string technology, DateTime startDate, int refManager)
        {
            Logger.Info("Entering in HRRepository API AddNewProjectInfo method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var projectInfo = new ProjectMaster();
                    projectInfo.ProjectName = projectName;
                    projectInfo.Description = description;
                    projectInfo.IsActive = true;
                    projectInfo.StartDate = startDate;
                    projectInfo.Technology = technology;
                    projectInfo.RefManagerId = refManager;
                    ctx.ProjectMasters.Add(projectInfo);
                    ctx.SaveChanges();

                }
                Logger.Info("Successfully exiting from HRRepository API AddNewProjectInfo method");
                return true;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API AddNewProjectInfo method ");
                throw;
            }
        }

        public List<ProjectsList> GetProjectsList(int managerId)
        {
            Logger.Info("Entering in HRRepository API GetProjectsList method");
            try
            {
                var projectsList = new List<ProjectsList>();
                var list = new List<ProjectMaster>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    if (managerId != 0)
                    {
                        list = ctx.ProjectMasters.Where(x=>x.RefManagerId==managerId).ToList();
                    }
                    else
                    {
                        list = ctx.ProjectMasters.ToList();
                   }

                    foreach (var item in list)
                    {
                        var project = new ProjectsList();
                        if (item.IsActive)
                        {
                            project.Id = item.Id;
                            project.ProjectName = item.ProjectName;
                            projectsList.Add(project);
                        }
                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetProjectsList method");
                return projectsList;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API GetProjectsList method ");
                throw;
            }

        }

        public List<EmployeeSkillDetails> GetSkillsList()
        {
            Logger.Info("Entering in HRRepository API GetSkillsList method");
            try
            {
                var skillsList = new List<EmployeeSkillDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.MasterDataValues.Where(x => x.RefMasterType == (int)MasterDataTypeEnum.Skills).ToList();
                    foreach (var item in list)
                    {
                        var skill = new EmployeeSkillDetails();
                        skill.Id = item.Id;
                        skill.SkillName = item.Value;
                        skillsList.Add(skill);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetSkillsList method");
                return skillsList;
            }
            catch
            {
                Logger.Info("Exception occured at HRRepository API GetSkillsList method ");
                throw;
            }

        }
    }
}

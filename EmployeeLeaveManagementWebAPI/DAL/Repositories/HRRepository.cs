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
                    var hrManagerId = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int32)EmployeeRole.HR).OrderBy(x => x.RefHierarchyLevel).FirstOrDefault().Id;
                    string imageBase64Data = string.Empty;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {
                        byte[] imageByteData = System.IO.File.ReadAllBytes(model.ImagePath);
                        imageBase64Data = Convert.ToBase64String(imageByteData);

                    }
                    var hierarchyLevel = (Int32)HierarchyLevel.Level5;
                    if (model.RefRoleId == (Int32)EmployeeRole.CEO)
                    {
                        hierarchyLevel = (Int32)HierarchyLevel.Level0;
                    }
                    else if (model.RefRoleId == (Int32)EmployeeRole.COO || model.RefRoleId == (Int32)EmployeeRole.CTO || model.RefRoleId == (Int32)EmployeeRole.SeniorHR || model.RefRoleId == (Int32)EmployeeRole.HR)
                    {
                        hierarchyLevel = (Int32)HierarchyLevel.Level1;
                    }
                    else if (model.RefRoleId == (Int32)EmployeeRole.TeamLead || model.RefRoleId == (Int32)EmployeeRole.TechLead || model.RefRoleId == (Int32)EmployeeRole.TestLead || model.RefRoleId == (Int32)EmployeeRole.TechnicalArchitect || model.RefRoleId == (Int32)EmployeeRole.Manager || model.RefRoleId == (Int32)EmployeeRole.ProjectManager || model.RefRoleId == (Int32)EmployeeRole.DevLead)
                    {
                        hierarchyLevel = (Int32)HierarchyLevel.Level2;
                    }
                    else if (model.RefRoleId == (Int32)EmployeeRole.SeniorTestEngineer || model.RefRoleId == (Int32)EmployeeRole.SeniorUIDesigner || model.RefRoleId == (Int32)EmployeeRole.SSE)
                    {
                        hierarchyLevel = (Int32)HierarchyLevel.Level3;
                    }
                    else if (model.RefRoleId == (Int32)EmployeeRole.TestEngineer || model.RefRoleId == (Int32)EmployeeRole.UIDesigner || model.RefRoleId == (Int32)EmployeeRole.SoftwareEngineer || model.RefRoleId == (Int32)EmployeeRole.QA || model.RefRoleId == (Int32)EmployeeRole.Finance || model.RefRoleId == (Int32)EmployeeRole.AssociateTechArchitect || model.RefRoleId == (Int32)EmployeeRole.Sales)
                    {
                        hierarchyLevel = (Int32)HierarchyLevel.Level4;
                    }
                    EmployeeGender EGender = (EmployeeGender)Convert.ToInt16(model.Gender);
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
                        Gender = EGender.Description(),
                        PassportNumber = model.PassportNumber,
                        BloodGroup = model.BloodGroup,
                        EmailId = model.Email,
                        RefEmployeeContractType = model.EmployeeConractType,
                        DateOfConfirmation = model.DateOfConfirmation,
                        RefHierarchyLevel = hierarchyLevel,
                        ManagerId = model.ManagerId != null ? model.ManagerId : hrManagerId,
                        DateOfJoining = model.DateOfJoining,
                        ImagePath = imageBase64Data,
                        RefEmployeeType = model.EmployeeType,
                        RefProfileType = model.RefProfileType


                    };
                    ctx.EmployeeDetails.Add(employeeDetails);
                    ctx.SaveChanges();
                    var id = employeeDetails.Id;
                    
                    if (model.EmployeeEducationDetails.Count > 0)
                    {

                        GraduationDegree GDegree = (GraduationDegree)Convert.ToInt16(model.EmployeeEducationDetails[0].Degree);
                        Specialization Spec = (Specialization)Convert.ToInt16(model.EmployeeEducationDetails[0].Specialization);
                        var employeeEducationDetails = new EmployeeEducationDetail
                        {
                            Degree = GDegree != 0 ? GDegree.Description() : "",
                            Institution = model.EmployeeEducationDetails[0].Institution,
                            FromDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[0]),
                            ToDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[1]),
                            Specialization = Spec != 0 ? Spec.Description() : "",
                            RefEmployeeId = id
                        };
                        ctx.EmployeeEducationDetails.Add(employeeEducationDetails);
                        ctx.SaveChanges();
                    }

                    if (model.EmployeeEmergencyContactDetail.Count > 0)
                    {
                        var employeeEmergencyContactDetails = new EmployeeEmergencyContactDetail
                        {
                            Name = model.EmployeeEmergencyContactDetail[0].Name,
                            Relationship = model.EmployeeEmergencyContactDetail[0].Relationship,
                            Telephone = model.EmployeeEmergencyContactDetail[0].Telephone,
                            Country = model.EmployeeEmergencyContactDetail[0].Country,
                            State = model.EmployeeEmergencyContactDetail[0].State,
                            City = model.EmployeeEmergencyContactDetail[0].City,
                            AddressLine1 = model.EmployeeEmergencyContactDetail[0].AddressLine1,
                            AddressLine2 = model.EmployeeEmergencyContactDetail[0].AddressLine2,
                            RefEmployeeId = id,
                            IsActive = 1,
                            CreatedDate = (DateTime.Now).Date,
                            RefCreatedBy = model.EmployeeEmergencyContactDetail[0].RefCreatedBy

                        };
                        ctx.EmployeeEmergencyContactDetails.Add(employeeEmergencyContactDetails);
                        ctx.SaveChanges();
                    }

                    if (model.EmployeeCurrentAddressDetail.Count > 0)
                    {
                        var employeeCurrentAddressDetails = new EmployeeCurrentAddressDetail
                        {
                            Country = model.EmployeeCurrentAddressDetail[0].Country,
                            State = model.EmployeeCurrentAddressDetail[0].State,
                            City = model.EmployeeCurrentAddressDetail[0].City,
                            Pincode = model.EmployeeCurrentAddressDetail[0].Pincode,
                            AddressLine1 = model.EmployeeCurrentAddressDetail[0].AddressLine1,
                            AddressLine2 = model.EmployeeCurrentAddressDetail[0].AddressLine2,
                            RefEmployeeId = id,
                            IsActive = 1,
                            CreatedDate = (DateTime.Now).Date,
                            RefCreatedBy = model.EmployeeCurrentAddressDetail[0].RefCreatedBy

                        };
                        ctx.EmployeeCurrentAddressDetails.Add(employeeCurrentAddressDetails);
                        ctx.SaveChanges();
                    }

                    if (model.EmployeePermanentAddressDetail.Count > 0)
                    {
                        var employeePermanentAddressDetails = new EmployeePermanentAddressDetail
                        {
                            Country = model.EmployeePermanentAddressDetail[0].Country,
                            State = model.EmployeePermanentAddressDetail[0].State,
                            City = model.EmployeePermanentAddressDetail[0].City,
                            Pincode = model.EmployeePermanentAddressDetail[0].Pincode,
                            AddressLine1 = model.EmployeePermanentAddressDetail[0].AddressLine1,
                            AddressLine2 = model.EmployeePermanentAddressDetail[0].AddressLine2,
                            RefEmployeeId = id,
                            IsActive = 1,
                            CreatedDate = (DateTime.Now).Date,
                            RefCreatedBy = model.EmployeePermanentAddressDetail[0].RefCreatedBy

                        };
                        ctx.EmployeePermanentAddressDetails.Add(employeePermanentAddressDetails);
                        ctx.SaveChanges();
                    }

                    if (model.EmployeeWorkLocationDetail.Count > 0)
                    {
                        var employeeWorkLocationDetails = new EmployeeWorkLocationDetail
                        {
                            Country = model.EmployeeWorkLocationDetail[0].Country,
                            State = model.EmployeeWorkLocationDetail[0].State,
                            City = model.EmployeeWorkLocationDetail[0].City,
                            Facility = model.EmployeeWorkLocationDetail[0].Facility,
                            RefEmployeeId = id,
                            IsActive = 1,
                            CreatedDate = (DateTime.Now).Date,
                            RefCreatedBy = model.EmployeeWorkLocationDetail[0].RefCreatedBy

                        };
                        ctx.EmployeeWorkLocationDetails.Add(employeeWorkLocationDetails);
                        ctx.SaveChanges();
                    }

                    

                    if (model.EmployeeExperienceDetails.Count > 0)
                    {
                        var employeeExperienceDetails = new EmployeeExperienceDetail
                        {
                            CompanyName = model.EmployeeExperienceDetails[0].Company,
                            Role = model.EmployeeExperienceDetails[0].Role,
                            FromDate = Convert.ToDateTime(model.EmployeeExperienceDetails[0].TimePeriod.Split('~')[0]),
                            ToDate = Convert.ToDateTime(model.EmployeeExperienceDetails[0].TimePeriod.Split('~')[1]),
                            RefEmployeeId = id
                        };
                        ctx.EmployeeExperienceDetails.Add(employeeExperienceDetails);
                        ctx.SaveChanges();
                    }
                    
                    foreach (var skill in model.Skills)
                    {
                        if(skill.SkillName != null)
                        { 
                        var empSkill = new EmployeeSkill();
                        empSkill.RefEmployeeId = id;
                        empSkill.Skill = skill.SkillName;
                        ctx.EmployeeSkills.Add(empSkill);
                        ctx.SaveChanges();
                        }
                    }
                    if (model.Projects.Count > 0)
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
                        UserName = model.InfrrdEmailId,
                        Password = "Temp@123",
                        RefEmployeeId = id,
                        CreatedDate = DateTime.Now
                    };
                    ctx.UserAccounts.Add(userDetails);
                    ctx.SaveChanges();


                    int DateOfJoiningMonth = model.DateOfJoining.Month;
                    int DateOfJoiningDay = model.DateOfJoining.Day;
                    var masterDataValues = ctx.MasterDataValues.ToList();
                    int advanceLeaveLimit = Convert.ToInt32(masterDataValues.FirstOrDefault(x => x.RefMasterType == (Int32)MasterDataTypeEnum.AdvanceLeaveLimit).Value);
                    int creditLeaveLimitMonthly = Convert.ToInt32(masterDataValues.FirstOrDefault(x => x.RefMasterType == (Int32)MasterDataTypeEnum.CreditLeaveLimitMonthly).Value);
                    var employeeMaster = new EmployeeLeaveMaster();
                    if (DateOfJoiningDay <= 15)
                    {

                        employeeMaster.RefEmployeeId = id;
                        employeeMaster.EarnedCasualLeave = creditLeaveLimitMonthly;
                        employeeMaster.AdvanceLeave = (advanceLeaveLimit - creditLeaveLimitMonthly) - (creditLeaveLimitMonthly * (DateOfJoiningMonth - 1));
                    }
                    else
                    {

                        employeeMaster.RefEmployeeId = id;
                        employeeMaster.EarnedCasualLeave = creditLeaveLimitMonthly/ creditLeaveLimitMonthly;
                        employeeMaster.AdvanceLeave = (advanceLeaveLimit - creditLeaveLimitMonthly) - (creditLeaveLimitMonthly * (DateOfJoiningMonth - 1));
                    }
                    ctx.EmployeeLeaveMasters.Add(employeeMaster);
                    ctx.SaveChanges();
                    var leaveTransaction = new EmployeeLeaveTransaction
                    {
                        RefEmployeeId = id,
                        RefLeaveType = (int)LeaveType.EarnedLeave,
                        RefStatus = (int)LeaveStatus.Approved,
                        CreatedDate = DateTime.Now,
                        NumberOfWorkingDays = 2,
                        RefTransactionType = (int)TransactionType.Credit
                    };
                    ctx.EmployeeLeaveTransactions.Add(leaveTransaction);
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from HRRepository API SubmitEmployeeDetails method");
                }
                result = true;

            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API SubmitEmployeeDetails method ");
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
                Logger.Error("Exception occured at HRRepository API GetEmployeeList method ");
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
                Logger.Error("Exception occured at HRRepository API GetManagerList method ");
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
                var detailedDataList = new List<WorkflowHistory>();
                var detailedWfhList = new List<WorkFromHome>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var totalLeaves = ctx.LeaveMasters.Select(x => x.Count).Sum();
                    if (employeeId[0] == 0)
                    {
                        dataList = ctx.EmployeeDetails.Include("WorkflowHistories").Include("WorkFromHomes").ToList();
                        detailedDataList = ctx.WorkflowHistories.Where(i => i.FromDate >= reportFromDate && i.ToDate <= reportToDate).ToList();
                        detailedWfhList = ctx.WorkFromHomes.Where(i => i.Date >= reportFromDate && i.Date <= reportToDate).ToList();
                    }
                    else if (employeeId[0] != 0)
                    {
                        foreach (var item in employeeId)
                        {

                            empDatalist = ctx.EmployeeDetails.Include("WorkflowHistories").Include("WorkFromHomes").FirstOrDefault(x => x.Id == item);
                            detailedDataList = ctx.WorkflowHistories.Where(i => i.RefEmployeeId == item && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).ToList();
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
                        detailedList.FromDate = item.Date.Value;
                        detailedList.ToDate = item.Date.Value;
                        ddList.Add(detailedList);
                    }
                    foreach (var item in dataList)
                    {
                        var listItem = new ConsolidatedEmployeeLeaveDetailsModel();
                        listItem.RefEmployeeId = item.Id;
                        listItem.EmployeeName = item.FirstName;
                        listItem.AppliedLeavesCount = (int)item.WorkflowHistories.Where(i => i.RefEmployeeId == item.Id && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).Select(i => i.NumberOfWorkingDays).Sum();
                        listItem.LossofPayCount = listItem.AppliedLeavesCount > totalLeaves ? listItem.AppliedLeavesCount - totalLeaves : 0;
                        listItem.WorkFromHomeCount = item.WorkFromHomes.Where(i => i.RefEmployeeId == item.Id && i.Date >= reportFromDate && i.Date <= reportToDate).ToList().Count;
                        listItem.CompOffCount = (int)item.WorkflowHistories.Where(i => i.RefEmployeeId == item.Id && i.RefLeaveType == (int)LeaveType.CompOff && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).Select(i => i.NumberOfWorkingDays).Sum();
                        listItem.AdvancedLeavesCount = (int)item.WorkflowHistories.Where(i => i.RefEmployeeId == item.Id && i.RefLeaveType == (int)LeaveType.AdvanceLeave && i.FromDate >= reportFromDate && i.ToDate <= reportToDate).Select(i => i.NumberOfWorkingDays).Sum();
                        list.Add(listItem);
                    }
                }
                detailsList = ddList.OrderBy(x => x.EmpoyeeName).ToList();
                Logger.Info("Successfully exiting from HRRepository API GetReportData method");
                return list;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetReportData method ");
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
                    empDatalist = ctx.EmployeeDetails.Include("WorkflowHistories").Include("WorkFromHomes").FirstOrDefault(x => x.Id == employeeId);



                    listItem.RefEmployeeId = empDatalist.Id;
                    listItem.EmployeeName = empDatalist.FirstName;
                    listItem.AppliedLeavesCount = (int)empDatalist.WorkflowHistories.Where(i => i.RefEmployeeId == empDatalist.Id && (i.RefLeaveType != (int)LeaveType.AdvanceLeave || i.RefLeaveType != (int)LeaveType.CompOff)).Select(i => i.NumberOfWorkingDays).Sum();
                    listItem.LossofPayCount = listItem.AppliedLeavesCount > totalLeaves ? listItem.AppliedLeavesCount - totalLeaves : 0;
                    listItem.WorkFromHomeCount = empDatalist.WorkFromHomes.Where(i => i.RefEmployeeId == empDatalist.Id).ToList().Count;
                    listItem.CompOffCount = (int)empDatalist.WorkflowHistories.Where(i => i.RefEmployeeId == empDatalist.Id && i.RefLeaveType == (int)LeaveType.CompOff).Select(i => i.NumberOfWorkingDays).Sum();
                    listItem.AdvancedLeavesCount = (int)empDatalist.WorkflowHistories.Where(i => i.RefEmployeeId == empDatalist.Id && i.RefLeaveType == (int)LeaveType.AdvanceLeave).Select(i => i.NumberOfWorkingDays).Sum();


                }
                Logger.Info("Successfully exiting from HRRepository API GetChartDetails method");
                return listItem;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetChartDetails method ");
                throw;
            }
        }

        public bool AddNewMasterDataValues(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering in HRRepository API AddNewMasterDataValues method");
            try
            {
                var result = false;
               
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var ids= ctx.MasterDataValues.Select(x => x.Id).ToList();
                    var id =ctx.MasterDataValues.Where(x => x.RefMasterType == masterDataType).Select(x => x.Id).ToList();
                    var data = new MasterDataValue();
                    data.Id =!ids.Contains(id.Last()+1)? id.Last() + 1:ids.Last()+1;
                    data.Value = masterDataValue.Trim();
                    data.RefMasterType = masterDataType;
                    ctx.MasterDataValues.Add(data);
                    ctx.SaveChanges();
                    result = true;
                }
                Logger.Info("Successfully exiting from HRRepository API AddNewMasterDataValues method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API AddNewMasterDataValues method ");
                throw;
            }
        }

        public bool AddNewProjectInfo(string projectName, string description, string technology, string technologyDetails, DateTime startDate, int refManager)
        {
            Logger.Info("Entering in HRRepository API AddNewProjectInfo method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    technologyDetails = technologyDetails.Replace("Sharp", "#");
                    var projectInfo = new ProjectMaster();
                    projectInfo.ProjectName = projectName;
                    projectInfo.Description = description;
                    projectInfo.IsActive = true;
                    projectInfo.StartDate = startDate;
                    projectInfo.Technology = technology;
                    projectInfo.TechnologyDetails = technologyDetails;
                    projectInfo.RefManagerId = refManager;
                    ctx.ProjectMasters.Add(projectInfo);
                    ctx.SaveChanges();

                    var existingProjectDetails = ctx.EmployeeProjectDetails.Where(x => x.RefEmployeeId == refManager && x.ProjectMaster.IsBench == true).ToList();
                    if(existingProjectDetails!=null)
                    {
                        existingProjectDetails.ForEach(x => x.EndDate = DateTime.Now);
                        ctx.SaveChanges();
                    }
                    var projectDetails = new EmployeeProjectDetail();
                    projectDetails.RefProjectId = projectInfo.Id;
                    projectDetails.IsActive = true;
                    projectDetails.RefEmployeeId = refManager;
                    projectDetails.CreatedDate = DateTime.Now;
                    projectDetails.StartDate = DateTime.Now;
                    ctx.EmployeeProjectDetails.Add(projectDetails);
                    ctx.SaveChanges();

                }
                Logger.Info("Successfully exiting from HRRepository API AddNewProjectInfo method");
                return true;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API AddNewProjectInfo method ");
                throw;
            }
        }

        public bool AddCompanyAnnouncements(string title, string carouselContent, string imagePath)
        {
            Logger.Info("Entering in HRRepository API AddCompanyAnnouncements method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var companyAnnouncementInfo = new Announcement();
                    companyAnnouncementInfo.Title = title;
                    companyAnnouncementInfo.CarouselContent = carouselContent;
                    companyAnnouncementInfo.ImagePath = imagePath;
                    companyAnnouncementInfo.IsActive = true;
                    ctx.Announcements.Add(companyAnnouncementInfo);
                    ctx.SaveChanges();
                }
                Logger.Info("Successfully exiting from HRRepository API AddCompanyAnnouncements method");
                return true;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API AddCompanyAnnouncements method ");
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
                        list = ctx.ProjectMasters.Where(x => x.RefManagerId == managerId).ToList();
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
                Logger.Error("Exception occured at HRRepository API GetProjectsList method ");
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
                Logger.Error("Exception occured at HRRepository API GetSkillsList method ");
                throw;
            }

        }

        public List<CountryDetails> GetCountries()
        {
            Logger.Info("Entering in HRRepository API GetCountries method");
            try
            {
                var CountriesList = new List<CountryDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.Geo_Location_Country_Master.ToList();
                    foreach (var item in list)
                    {
                        var country = new CountryDetails();
                        country.Id = item.Id;
                        country.CountryName = item.Name;
                        CountriesList.Add(country);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetCountries method");
                return CountriesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetCountries method ");
                throw;
            }

        }

        public List<RelationshipDetails> GetRelationships()
        {
            Logger.Info("Entering in HRRepository API GetRelationships method");
            try
            {
                var RelationshipsList = new List<RelationshipDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.MasterDataValues.Where(x => x.RefMasterType == (int)MasterDataTypeEnum.Relationship).ToList();
                    foreach (var item in list)
                    {
                        var relationship = new RelationshipDetails();
                        relationship.Id = item.Id;
                        relationship.Relationship = item.Value;
                        RelationshipsList.Add(relationship);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetRelationships method");
                return RelationshipsList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetRelationships method ");
                throw;
            }

        }

        public List<BloodGroupDetails> GetBloodGroups()
        {
            Logger.Info("Entering in HRRepository API GetBloodGroups method");
            try
            {
                var BloodGroupsList = new List<BloodGroupDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.MasterDataValues.Where(x => x.RefMasterType == (int)MasterDataTypeEnum.BloodGroup).ToList();
                    foreach (var item in list)
                    {
                        var bloodGroup = new BloodGroupDetails();
                        bloodGroup.Id = item.Id;
                        bloodGroup.BloodGroup = item.Value;
                        BloodGroupsList.Add(bloodGroup);
                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetBloodGroups method");
                return BloodGroupsList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetBloodGroups method ");
                throw;
            }

        }

        public List<FacilityDetails> GetFacilities()
        {
            Logger.Info("Entering in HRRepository API GetFacilities method");
            try
            {
                var FacilitiesList = new List<FacilityDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.Geo_Location_Facility_Master.ToList();
                    foreach (var item in list)
                    {
                        var facility = new FacilityDetails();
                        facility.Id = item.Id;
                        facility.FacilityName = item.Name;
                        FacilitiesList.Add(facility);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetFacilities method");
                return FacilitiesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetFacilities method ");
                throw;
            }

        }

        public List<StateDetails> GetStates(int CountryId)
        {
            Logger.Info("Entering in HRRepository API GetStates method");
            try
            {
                var StatesList = new List<StateDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    
                        var list = ctx.Geo_Location_State_Master.Where(x=>x.RefCountryId == CountryId).ToList();
                        foreach (var item in list)
                        {
                            var state = new StateDetails();
                            state.Id = item.Id;
                            state.StateName = item.Name;
                            StatesList.Add(state);
                        }
                   
                }
                Logger.Info("Successfully exiting from HRRepository API GetStates method");
                return StatesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetStates method ");
                throw;
            }

        }

        public FacilityDetails GetWorkFacilityDetails(int FacilityId)
        {
            Logger.Info("Entering in HRRepository API GetWorkFacilityDetails method");
            try
            {
                var FacilityDetail = new FacilityDetails();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var facilityDetails = ctx.Geo_Location_Facility_Master.Where(x => x.Id == FacilityId).FirstOrDefault();
                    var cityDetails = ctx.Geo_Location_City_Master.Where(x => x.Id == facilityDetails.RefCityId).FirstOrDefault();
                    var stateDetails = ctx.Geo_Location_State_Master.Where(x => x.Id == cityDetails.RefStateId).FirstOrDefault();
                    var countryDetails = ctx.Geo_Location_Country_Master.Where(x => x.Id == stateDetails.RefCountryId).FirstOrDefault();

                    FacilityDetail.CountryId = countryDetails.Id;
                    FacilityDetail.CountryName = countryDetails.Name;
                    FacilityDetail.StateId = stateDetails.Id;
                    FacilityDetail.StateName = stateDetails.Name;
                    FacilityDetail.CityId = cityDetails.Id;
                    FacilityDetail.CityName = cityDetails.Name;
                }
                Logger.Info("Successfully exiting from HRRepository API GetWorkFacilityDetails method");
                return FacilityDetail;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetWorkFacilityDetails method ");
                throw;
            }

        }

        public List<CityDetails> GetCities(int StateId)
        {
            Logger.Info("Entering in HRRepository API GetCities method");
            try
            {
                var CitiesList = new List<CityDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    
                        var list = ctx.Geo_Location_City_Master.Where(x=>x.RefStateId == StateId).ToList();
                        foreach (var item in list)
                        {
                            var city = new CityDetails();
                            city.Id = item.Id;
                            city.CityName = item.Name;
                            CitiesList.Add(city);
                        }
                }
                Logger.Info("Successfully exiting from HRRepository API GetCities method");
                return CitiesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetCities method ");
                throw;
            }

        }


        public List<FacilityDetails> GetFacilities(int CityId)
        {
            Logger.Info("Entering in HRRepository API GetFacilities method");
            try
            {
                var FacilitiesList = new List<FacilityDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.Geo_Location_Facility_Master.Where(x => x.RefCityId == CityId).ToList();
                    foreach (var item in list)
                    {
                        var facility = new FacilityDetails();
                        facility.Id = item.Id;
                        facility.FacilityName = item.Name;
                        FacilitiesList.Add(facility);
                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetFacilities method");
                return FacilitiesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetFacilities method ");
                throw;
            }

        }
        public List<TechnologyDetails> GetTechnologiesList()
        {
            Logger.Info("Entering in HRRepository API GetTechnologiesList method");
            try
            {
                var TechnologiesList = new List<TechnologyDetails>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.TechnologyMasters.ToList();
                    foreach (var item in list)
                    {
                        var Technology = new TechnologyDetails();
                        Technology.Id = item.Id;
                        Technology.Technology = item.Technology;
                        TechnologiesList.Add(Technology);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetTechnologiesList method");
                return TechnologiesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetTechnologiesList method ");
                throw;
            }

        }

        public List<TechnologyDescriptions> GetTechnologyDetailsList(List<TechnologyDetails> technologies)
        {
            Logger.Info("Entering in HRRepository API GetTechnologyDetailsList method");
            try
            {
                var TechnologyDetailsList = new List<TechnologyDescriptions>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    
                    List<string> TechnologyNames = new List<string>();
                    foreach (var TN in technologies)
                    {
                        TechnologyNames.Add(TN.Technology);
                    }
                    var TList = ctx.TechnologyMasters.Where(m => TechnologyNames.Contains(m.Technology)).ToList();
                    List<int> TechnologyIds = new List<int>();
                    foreach (var TID in TList)
                    {
                        TechnologyIds.Add(TID.Id);
                    }
                    var list = ctx.TechnologyDetails.Where(m => TechnologyIds.Contains(m.RefTechnologyId)).ToList();
                    foreach (var item in list)
                    {
                        var TechnologyDetail = new TechnologyDescriptions();
                        TechnologyDetail.Id = item.Id;
                        TechnologyDetail.RefTechnology = item.RefTechnologyId;
                        TechnologyDetail.TechnologyDetails = item.TechnologyDetails;
                        TechnologyDetailsList.Add(TechnologyDetail);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetTechnologyDetailsList method");
                return TechnologyDetailsList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetTechnologyDetailsList method ");
                throw;
            }

        }

        public bool CheckForExistingMasterDataValues(int masterDataType, string masterDataValue)
        {
            Logger.Info("Entering in HRRepository API CheckForExistingMasterDataValues method");
            try
            {
                var result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var dataExists = ctx.MasterDataValues.Any(i => i.RefMasterType == masterDataType && i.Value.Trim().ToLower() == masterDataValue.Trim().ToLower());
                    if (dataExists)
                    {
                        result = true;
                    }
                }
                Logger.Info("Successfully exiting from HRRepository API CheckForExistingMasterDataValues method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API CheckForExistingMasterDataValues method ");
                throw;
            }
        }
        public bool CheckForExistingProjectMasterDataValues(string projectName, string technology, int refManager)
        {
            Logger.Info("Entering in HRRepository API CheckForExistingMasterDataValues method");
            try
            {
                var result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var dataExists = ctx.ProjectMasters.Any(i => i.ProjectName.ToLower().Trim() == projectName.ToLower().Trim() && i.Technology.Trim().ToLower() == technology.Trim().ToLower() && i.RefManagerId == refManager);
                    if (dataExists)
                    {
                        result = true;
                    }
                }
                Logger.Info("Successfully exiting from HRRepository API CheckForExistingMasterDataValues method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API CheckForExistingMasterDataValues method ");
                throw;
            }
        }

        public List<MasterDataModel> GetRolesList()
        {
            Logger.Info("Entering in HRRepository API GetRolesList method");
            try
            {
                var rolesList = new List<MasterDataModel>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var list = ctx.MasterDataValues.Where(x => x.RefMasterType == (int)MasterDataTypeEnum.Role).ToList();
                    foreach (var item in list)
                    {
                        var roles = new MasterDataModel();
                        roles.Id = item.Id;
                        roles.Value = item.Value;
                        rolesList.Add(roles);


                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetRolesList method");
                return rolesList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetRolesList method ");
                throw;
            }

        }

        public LeaveReportModel GetProjectwiseReport(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering in HRRepository API GetProjectwiseReport method");
            try
            {
                var leaveReport = new LeaveReportModel();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var startDate = new DateTime(year, 1, 1);
                    
                    var endDate = new DateTime(year, 1, 1).AddYears(1).AddDays(-1);
                    var list=ctx.EmployeeProjectDetails.Where(i => i.RefProjectId == projectId &&
                        ((i.StartDate.Value >= startDate && (i.EndDate.Value < endDate || i.EndDate == null)) ||
                        (i.EndDate >= startDate && i.EndDate < endDate) ||
                        (i.StartDate >= startDate && i.StartDate < endDate) ||
                        (i.StartDate <= startDate && i.EndDate == null))).ToList();
                      foreach (var item in list)
                    {
                        for (int date = item.StartDate.Value.Month; date <= endDate.Month; date++)
                        {
                            if (date >= startDate.Month && year==item.StartDate.Value.Year)
                            {
                                if (item.EndDate == null || (item.EndDate.HasValue && date <= item.EndDate.Value.Month))
                                {
                                    leaveReport.Jan = date == 1 ? leaveReport.Jan + 1 : leaveReport.Jan;
                                    leaveReport.Feb = date == 2 ? leaveReport.Feb + 1 : leaveReport.Feb;
                                    leaveReport.Mar = date == 3 ? leaveReport.Mar + 1 : leaveReport.Mar;
                                    leaveReport.Apr = date == 4 ? leaveReport.Apr + 1 : leaveReport.Apr;
                                    leaveReport.May = date == 5 ? leaveReport.May + 1 : leaveReport.May;
                                    leaveReport.Jun = date == 6 ? leaveReport.Jun + 1 : leaveReport.Jun;
                                    leaveReport.Jul = date == 7 ? leaveReport.Jul + 1 : leaveReport.Jul;
                                    leaveReport.Aug = date == 8 ? leaveReport.Aug + 1 : leaveReport.Aug;
                                    leaveReport.Sep = date == 9 ? leaveReport.Sep + 1 : leaveReport.Sep;
                                    leaveReport.Oct = date == 10 ? leaveReport.Oct + 1 : leaveReport.Oct;
                                    leaveReport.Nov = date == 11 ? leaveReport.Nov + 1 : leaveReport.Nov;
                                    leaveReport.Dec = date == 12 ? leaveReport.Dec + 1 : leaveReport.Dec;
                                }
                            }
                        }
                    }
                }
                Logger.Info("Successfully exiting from HRRepository API GetProjectwiseReport method");
                return leaveReport;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetProjectwiseReport method ");
                throw;
            }
        }

        public List<ProjectsList> GetProjectwiseEmployeeDetails(int projectId, int fromMonth, int toMonth, int year)
        {
            Logger.Info("Entering in HRRepository API GetProjectwiseEmployeeDetails method");
            try
            {
                var employeeList = new List<ProjectsList>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var startDate = new DateTime(year, 1, 1);

                    var endDate = new DateTime(year, 1, 1).AddYears(1).AddDays(-1);
                    employeeList = ctx.EmployeeProjectDetails.
                        Where(i => i.RefProjectId == projectId && year>=i.StartDate.Value.Year &&
                        ((i.StartDate.Value >= startDate && (i.EndDate.Value < endDate || i.EndDate == null)) ||
                        (i.EndDate >= startDate && i.EndDate < endDate) ||
                        (i.StartDate >= startDate && i.StartDate < endDate) ||
                        (i.StartDate <= startDate && i.EndDate == null))).
                        Select(item => new ProjectsList()
                        {                            
                            EmployeeName = item.EmployeeDetail.FirstName + "" + item.EmployeeDetail.LastName,
                            ProjectName = item.ProjectMaster.ProjectName,
                            StartDate = item.StartDate.Value,
                            EndDate = item.EndDate.HasValue == true ? item.EndDate.Value : DateTime.Now
                        }).ToList();

                }
                Logger.Info("Successfully exiting from HRRepository API GetProjectwiseEmployeeDetails method");
                return employeeList;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API GetProjectwiseEmployeeDetails method ");
                throw;
            }
        }

        public bool CheckEmployeeNumber(string employeeNumber)
        {
            Logger.Info("Entering in HRRepository API CheckEmployeeNumber method");
            try
            {
                bool result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var EmpNumber = ctx.EmployeeDetails.Where(x => x.EmpNumber == employeeNumber).FirstOrDefault();
                    if (EmpNumber != null)
                        result = true;
                    
                }
                Logger.Info("Successfully exiting from HRRepository API CheckEmployeeNumber method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API CheckEmployeeNumber method ");
                throw;
            }
        }

        public bool CheckEmployeeMail(string employeeMailid)
        {
            Logger.Info("Entering in HRRepository API CheckEmployeeMail method");
            try
            {
                bool result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var EmpNumber = ctx.UserAccounts.Where(x => x.UserName == employeeMailid).FirstOrDefault();
                    if (EmpNumber != null)
                        result = true;

                }
                Logger.Info("Successfully exiting from HRRepository API CheckEmployeeMail method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at HRRepository API CheckEmployeeMail method ");
                throw;
            }
        }
    }
}

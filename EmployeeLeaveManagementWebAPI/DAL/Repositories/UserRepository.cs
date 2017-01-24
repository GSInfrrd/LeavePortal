using LMS_WebAPI_DAL.Repositories.Interfaces;
using System.Linq;
using LMS_WebAPI_Utils;
using LMS_WebAPI_DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LMS_WebAPI_Domain;

namespace LMS_WebAPI_DAL.Repositories
{
    public class UserRepository : IUser
    {
        public UserAccount GetUser(string emailId, string password)
        {
            Logger.Info("Entering in UserRepository API GetUser method");
            try
            {
                var userData = new UserAccount();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        userData = ctx.UserAccounts.Include("EmployeeDetail").FirstOrDefault(x => x.UserName == emailId && x.Password == password);

                    }
                    else
                    {
                        userData = ctx.UserAccounts.Include("EmployeeDetail").FirstOrDefault(x => x.UserName == emailId);

                    }
                    Logger.Info("Successfully exiting from UserRepository API GetUser method");
                    return userData;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository GetUser method ");
                throw;
            }
        }

        public EmployeeCommonDetails GetUserDetails(int userEmpId)
        {
            Logger.Info("Entering in UserRepository API GetUserDetails method");

            try
            {

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    var empDetail = (from emp in ctx.EmployeeDetails
                                     where emp.Id == userEmpId
                                     select emp).FirstOrDefault();


                    var masterDetails = (from master in ctx.MasterDataValues select master);

                    EmployeeCommonDetails empData = new EmployeeCommonDetails();
                    if (empDetail != null)
                    {
                        empData.Id = empDetail.Id;
                        empData.Name = empDetail.FirstName;
                        empData.ManagerId = empDetail.ManagerId;
                        empData.Experience = empDetail.Experience;
                        empData.RoleName = empDetail.MasterDataValue.Value;
                        empData.DateOfJoining = empDetail.DateOfJoining;
                        if (empDetail.EmployeeDetail1 != null)
                        {
                            empData.ManagerName = empDetail.EmployeeDetail1.FirstName;
                            empData.ManagerEmailId = empDetail.EmployeeDetail1.UserAccounts.FirstOrDefault().UserName;
                        }

                        var employeeLeaves = empDetail.EmployeeLeaveMasters.FirstOrDefault();

                        if (employeeLeaves != null)
                        {

                            var advanceLimit = masterDetails.ToList().Where(x => x.RefMasterType == Convert.ToInt32(AdvanceLeaveLimit.limit)).FirstOrDefault();
                            var lopLimit = masterDetails.ToList().Where(x => x.RefMasterType == Convert.ToInt32(LOPLeaveLimit.limit)).FirstOrDefault();
                            empData.TotalAdvanceLeaveToTake = ((employeeLeaves.SpentAdvanceLeave == 0) || (employeeLeaves.SpentAdvanceLeave == null)) ? Convert.ToInt16(advanceLimit.Value) : (Convert.ToInt16(advanceLimit.Value) - employeeLeaves.SpentAdvanceLeave);
                            empData.TotalCasualLeave = employeeLeaves.EarnedCasualLeave + (employeeLeaves.RewardedLeaveCount != null ? employeeLeaves.RewardedLeaveCount : 0);
                            empData.TotalLeaveCount = employeeLeaves.EarnedCasualLeave + (employeeLeaves.RewardedLeaveCount != null ? employeeLeaves.RewardedLeaveCount : 0);
                            empData.LOPLeaveLimit = Convert.ToInt32(lopLimit.Value);
                            empData.CompOffTaken = employeeLeaves.TakenCompOff;
                        }

                        empData.TotalSpent = empDetail.EmployeeLeaveTransactions.Where(x => x.RefStatus == (int)(LeaveStatus.Approved)).Select(x => x.NumberOfWorkingDays).ToList().Sum();
                        empData.TotalApplied = empDetail.EmployeeLeaveTransactions.Where(x => x.RefStatus == (int)(LeaveStatus.Submitted)).Select(x => x.NumberOfWorkingDays).ToList().Sum();
                        empData.TotalWorkFromHome = empDetail.WorkFromHomes.ToList().Count();


                        ctx.Configuration.LazyLoadingEnabled = false;
                    }
                    
                    Logger.Info("Successfully exiting from UserRepository API GetUserDetails method");
                    return empData;

                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository GetUserDetails method ");
                throw;
            }
        }

        public List<Announcement> GetAnnouncements()
        {
            Logger.Info("Entering in UserRepository API GetAnnouncements method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var announcements = ctx.Announcements.Where(x => x.IsActive == true).ToList();
                    Logger.Info("Successfully exiting from UserRepository API GetAnnouncements method");
                    return announcements;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository GetAnnouncements method ");
                throw;
            }
        }

        public LeaveReportModel GetLeaveReportDetails(int year, int employeeId = 0, int leaveType = 0)
        {
            Logger.Info("Entering in UserRepository API GetLeaveReportDetails method");
            try
            {
                var leaveReport = new LeaveReportModel();
                var years = new List<EmployeeLeaveTransaction>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    years = ctx.EmployeeLeaveTransactions.Where(i => i.RefStatus == (int)LeaveStatus.Approved && i.FromDate.Year == year && i.ToDate.Year == year).ToList();

                    if (employeeId != 0)
                    {
                        years = years.Where(i => i.RefEmployeeId == employeeId).ToList();
                    }
                    if (leaveType != 0)
                    {
                        years = years.Where(i => i.RefLeaveType == leaveType).ToList();
                    }

                    foreach (var item in years)
                    {

                        for (DateTime date = item.FromDate; date <= item.ToDate; date = date.AddDays(1))
                        {
                            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                            {
                                switch (date.Month)
                                {
                                    case 1:

                                        leaveReport.Jan++;
                                        break;
                                    case 2:
                                        leaveReport.Feb++;
                                        break;
                                    case 3:
                                        leaveReport.Mar++;
                                        break;
                                    case 4:
                                        leaveReport.Apr++;
                                        break;
                                    case 5:
                                        leaveReport.May++;
                                        break;
                                    case 6:
                                        leaveReport.Jun++;
                                        break;
                                    case 7:
                                        leaveReport.Jul++;
                                        break;
                                    case 8:
                                        leaveReport.Aug++;
                                        break;
                                    case 9:
                                        leaveReport.Sep++;
                                        break;
                                    case 10:
                                        leaveReport.Oct++;
                                        break;
                                    case 11:
                                        leaveReport.Nov++;
                                        break;
                                    case 12:
                                        leaveReport.Dec++;
                                        break;
                                }
                            }
                        }
                    }
                    Logger.Info("Successfully exiting from UserRepository API GetLeaveReportDetails method");
                    return leaveReport;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository GetLeaveReportDetails method ");
                throw;
            }
        }

        public EmployeeDetail GetUserProfileDetails(int employeeId, out List<MasterDataModel> skills, out List<ProjectsList> projects)
        {
            Logger.Info("Entering in UserRepository API GetUserProfileDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var Skills = new List<MasterDataModel>();
                    var profileDetails = ctx.EmployeeDetails.Include("EmployeeEducationDetails").Include("EmployeeExperienceDetails").Include("UserAccounts").Include("EmployeeSkills").Include("MasterDataValue2").FirstOrDefault(i => i.Id == employeeId);
                    var allSkills = ctx.MasterDataValues.Where(i => i.RefMasterType == 7).ToList();
                    foreach (var item in allSkills)
                    {
                        var skill = new MasterDataModel();
                        skill.Id = item.Id;
                        skill.Value = item.Value;
                        skill.RefMasterType = item.RefMasterType;
                        Skills.Add(skill);
                    }
                    skills = Skills;
                    var ProjectList = new List<ProjectsList>();
                    var projectDetails = ctx.EmployeeProjectDetails.FirstOrDefault(i => i.RefEmployeeId == employeeId);
                    if (projectDetails != null)
                    {
                        var project = new ProjectsList();
                        project.Id = projectDetails.Id;
                        project.ProjectName = ctx.ProjectMasters.FirstOrDefault(i => i.Id == projectDetails.RefProjectId).ProjectName;
                        project.StartDate = projectDetails.StartDate.Value;
                        project.EndDate = projectDetails.EndDate != null ? projectDetails.EndDate.Value : DateTime.Now;
                        ProjectList.Add(project);
                    }
                    projects = ProjectList;
                    Logger.Info("Successfully exiting from UserRepository API GetUserProfileDetails method");
                    return profileDetails;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository GetUserProfileDetails method ");
                throw;
            }
        }

        public bool EditEmployeeDetails(EmployeeDetailsModel model)
        {
            Logger.Info("Entering in UserRepository API EditEmployeeDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var empData = new EmployeeDetail();
                    empData = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == model.Id);
                    empData.FirstName = model.FirstName;
                    empData.LastName = model.LastName;
                    empData.City = model.City;
                    empData.Country = model.Country;
                    empData.DateOfBirth = model.DateOfBirth;
                    empData.PhoneNumber = model.Telephone;
                    empData.ModifiedDate = DateTime.Now;
                    empData.ModifiedBy = model.FirstName;
                    empData.FacebookLink = model.FacebookLink;
                    empData.TwitterLink = model.TwitterLink;
                    empData.GooglePlusLink = model.GooglePlusLink;
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository EditEmployeeDetails method ");
                throw;
            }
        }

        public bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails, int employeeId)
        {
            Logger.Info("Entering in UserRepository API EditEmployeeEducationDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    foreach (var item in educationDetails)
                    {
                        var employeeEdDetails = ctx.EmployeeEducationDetails.FirstOrDefault(i => i.Id == item.Id);
                        if (employeeEdDetails != null)
                        {

                            employeeEdDetails.Degree = item.Degree;
                            employeeEdDetails.Institution = item.Institution;
                            employeeEdDetails.FromDate = item.FromDate;
                            employeeEdDetails.ToDate = item.ToDate;
                            ctx.SaveChanges();
                        }
                        else
                        {
                            var edDetails = new EmployeeEducationDetail();
                            edDetails.Degree = item.Degree;
                            edDetails.Institution = item.Institution;
                            edDetails.FromDate = item.FromDate;
                            edDetails.ToDate = item.ToDate;
                            edDetails.RefEmployeeId = employeeId;
                            ctx.EmployeeEducationDetails.Add(edDetails);
                            ctx.SaveChanges();
                        }

                    }
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeEducationDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository EditEmployeeEducationDetails method ");
                throw;
            }
        }

        public bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        {
            Logger.Info("Entering in UserRepository API EditEmployeeExperienceDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    foreach (var item in experienceDetails)
                    {
                        var expDetails = ctx.EmployeeExperienceDetails.FirstOrDefault(i => i.Id == item.Id);

                        if (expDetails != null)
                        {
                            expDetails.CompanyName = item.Company;
                            expDetails.Role = item.Role;
                            expDetails.FromDate = item.FromDate;
                            expDetails.ToDate = item.ToDate;
                            ctx.SaveChanges();

                        }
                        else
                        {
                            var employeeExpdetails = new EmployeeExperienceDetail();
                            employeeExpdetails.CompanyName = item.Company;
                            employeeExpdetails.Role = item.Role;
                            employeeExpdetails.FromDate = item.FromDate;
                            employeeExpdetails.ToDate = item.ToDate;
                            employeeExpdetails.RefEmployeeId = employeeId;
                            ctx.EmployeeExperienceDetails.Add(employeeExpdetails);
                            ctx.SaveChanges();
                        }

                    }
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeExperienceDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository EditEmployeeExperienceDetails method ");
                throw;
            }
        }
        public bool EditEmployeeSkills(List<EmployeeSkillDetails> skills, int employeeId)
        {
            Logger.Info("Entering in UserRepository API EditEmployeeSkills method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var empSkills = ctx.EmployeeSkills.Where(i => i.RefEmployeeId == employeeId).ToList();
                    foreach (var item in empSkills)
                    {
                        ctx.EmployeeSkills.Remove(item);
                        ctx.SaveChanges();
                    }
                    foreach (var item in skills)
                    {
                        var skill = new EmployeeSkill();
                        skill.RefEmployeeId = employeeId;
                        skill.Skill = item.SkillName;
                        ctx.EmployeeSkills.Add(skill);
                        ctx.SaveChanges();
                    }
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeSkills method");
                    return true;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository EditEmployeeSkills method ");
                throw;
            }
        }

        public string getUserProfileImage(int employeeId)
        {
            Logger.Info("Entering in UserRepository API getUserProfileImage method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var profileDetails = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == employeeId);
                    //return Convert.FromBase64String(profileDetails.ImagePath);
                    Logger.Info("Successfully exiting from UserRepository API getUserProfileImage method");
                    return profileDetails.ImagePath;
                }
            }
            catch
            {
                Logger.Info("Exception occured at UserRepository getUserProfileImage method ");
                throw;
            }
        }

        public List<EmployeeDetail> GetTeamMembers(int UserEmpId)
        {
            try
            {
                List<EmployeeDetail> resTeamMembers = new List<EmployeeDetail>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var lstProjectDetails = ctx.EmployeeProjectDetails.Where(m => m.RefEmployeeId == UserEmpId && m.IsActive == true).Select(m => m.RefProjectId).ToList();
                    if (lstProjectDetails != null && lstProjectDetails.Count > 0)
                    {
                        resTeamMembers = ctx.EmployeeProjectDetails.Include("EmployeeDetail").Include("MasterDataValue").Where(m => lstProjectDetails.Contains(m.RefProjectId)).Select(m => m.EmployeeDetail).ToList();

                    }
                }
                return resTeamMembers;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

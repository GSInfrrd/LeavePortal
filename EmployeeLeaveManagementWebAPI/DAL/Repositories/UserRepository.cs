using LMS_WebAPI_DAL.Repositories.Interfaces;
using System.Linq;
using LMS_WebAPI_Utils;
using LMS_WebAPI_DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LMS_WebAPI_Domain;
using System.Security.Cryptography;
using System.Text;

namespace LMS_WebAPI_DAL.Repositories
{
    public class UserRepository : IUser
    {
        public UserAccount GetUser(string emailId, string password)
        {
            Logger.Info("Entering in UserRepository API GetUser method");
            try
            {
                string encryptedPassword = CommonMethods.encryption(password);
                var userData = new UserAccount();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    if (!string.IsNullOrEmpty(encryptedPassword))
                    {
                        userData = ctx.UserAccounts.Include("EmployeeDetail").FirstOrDefault(x => x.UserName.ToLower().Trim().Equals(emailId.ToLower().Trim()) && x.Password.ToLower().Trim().Equals(encryptedPassword.ToLower().Trim()));
                    }
                    else
                    {
                        userData = ctx.UserAccounts.Include("EmployeeDetail").FirstOrDefault(x => x.UserName.ToLower().Trim().Equals(emailId.ToLower().Trim()));
                    }
                    Logger.Info("Successfully exiting from UserRepository API GetUser method");
                    return userData;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository GetUser method ");
                throw;
            }
        }
        
        public EmployeeDetailsModel GetUserDetails(int userEmpId)
        {
            Logger.Info("Entering in UserRepository API GetUserDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    ctx.Configuration.LazyLoadingEnabled = true;
                    var employeeDetails = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == userEmpId);
                    var masterDataValues = ctx.MasterDataValues.ToList();
                    var advanceLeaveLimit = Convert.ToInt32(masterDataValues.FirstOrDefault(x => x.RefMasterType == (Int32)MasterDataTypeEnum.AdvanceLeaveLimit).Value);
                    var lopLeaveLimit = Convert.ToInt32(masterDataValues.FirstOrDefault(x => x.RefMasterType == (Int32)MasterDataTypeEnum.LOPLimit).Value);
                    var empModel = new EmployeeDetailsModel();
                    var employeeLeaves = employeeDetails.EmployeeLeaveMasters.FirstOrDefault();
                    var leaveTransactions = employeeDetails.EmployeeLeaveTransactions;
                    var RewardLeaves = ctx.EmployeeRewardedLeaveDetails.Where(x => x.RefEmployeeId == userEmpId).ToList();
                    int CompOffReceived = 0;
                    foreach (var RL in RewardLeaves)
                    {
                        CompOffReceived += RL.LeaveCount;
                    }
                    if (employeeDetails != null)
                    {
                        empModel.RoleName = employeeDetails.MasterDataValue.Value;
                        empModel.TotalLeaveCount = employeeLeaves.EarnedCasualLeave + (employeeLeaves.RewardedLeaveCount != null ? employeeLeaves.RewardedLeaveCount : 0);
                        empModel.EarnedLeave = employeeLeaves.EarnedCasualLeave;
                        empModel.TotalApplied = leaveTransactions.Count != 0 ? leaveTransactions.Where(x => x.RefStatus == (Int32)LeaveStatus.Submitted).Select(i => i.NumberOfWorkingDays).Sum() : 0;
                        empModel.TotalSpent = leaveTransactions.Count != 0 ? leaveTransactions.Where(x => x.RefStatus == (Int32)LeaveStatus.Approved && x.RefLeaveType != (Int32)LeaveType.RewardLeave && x.RefLeaveType != (Int32)LeaveType.EarnedLeave).Select(i => i.NumberOfWorkingDays).Sum() : 0;
                        empModel.AdvanceLeave = employeeLeaves.AdvanceLeave;
                        empModel.LeaveBalence = employeeLeaves.EarnedCasualLeave + employeeLeaves.AdvanceLeave;
                        //empModel.LeaveBalence = advanceLeaveLimit + (employeeLeaves.CarryForwardedLeave != null ? employeeLeaves.CarryForwardedLeave : 0) - (empModel.TotalSpent);
                        empModel.TotalWorkFromHome = employeeDetails.WorkFromHomes.Count();
                        empModel.ManagerName = employeeDetails.EmployeeDetail1 != null ? employeeDetails.EmployeeDetail1.FirstName : string.Empty;
                        empModel.TotalLOPLImit = lopLeaveLimit;
                        empModel.TotalCasualLeave = empModel.TotalLeaveCount;
                       // empModel.TotalAdvanceLeaveTotake = (employeeLeaves.SpentAdvanceLeave == 0 || employeeLeaves.SpentAdvanceLeave == null) ? advanceLeaveLimit : advanceLeaveLimit - employeeLeaves.SpentAdvanceLeave;
                        empModel.TotalAdvanceLeaveTotake = employeeLeaves.AdvanceLeave;
                        empModel.MangerEmail = employeeDetails.EmployeeDetail1 != null ? employeeDetails.EmployeeDetail1.UserAccounts.FirstOrDefault().UserName : string.Empty;
                        empModel.ManagerId = employeeDetails.ManagerId;
                        empModel.CompOffTaken = employeeLeaves.TakenCompOff;
                        empModel.DateOfJoining = Convert.ToDateTime(employeeDetails.DateOfJoining);
                        empModel.CompOffReceived = CompOffReceived;
                    }
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Logger.Info("Successfully exiting from UserRepository API GetUserDetails method");
                    return empModel;

                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository GetUserDetails method ");
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
                Logger.Error("Exception occured at UserRepository GetAnnouncements method ");
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
                    years = ctx.EmployeeLeaveTransactions.Where(i => i.RefStatus == (int)LeaveStatus.Approved && i.FromDate != null && i.ToDate != null && i.FromDate.Value.Year == year && i.ToDate.Value.Year == year).ToList();

                    if (employeeId != 0)
                    {
                        years = years.Where(i => i.RefEmployeeId == employeeId).ToList();
                    }
                    if (leaveType != 0)
                    {
                        years = years.Where(i => i.RefLeaveType == leaveType).ToList();
                    }
                    var holidays = ctx.Holidays.Where(x => x.Year == year).Select(x => x.Date).ToList();
                    foreach (var item in years)
                    {
                        for (DateTime date = item.FromDate.Value; date <= item.ToDate; date = date.AddDays(1))
                        {
                            if (!holidays.Contains(date))
                            {
                                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                                {
                                    leaveReport.LeaveCount++;
                                    leaveReport.Jan = date.Month == 1 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Jan + 1 : leaveReport.Jan + 0.5 : leaveReport.Jan;
                                    leaveReport.Feb = date.Month == 2 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Feb + 1 : leaveReport.Feb + 0.5 : leaveReport.Feb;
                                    leaveReport.Mar = date.Month == 3 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Mar + 1 : leaveReport.Mar + 0.5 : leaveReport.Mar;
                                    leaveReport.Apr = date.Month == 4 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Apr + 1 : leaveReport.Apr + 0.5 : leaveReport.Apr;
                                    leaveReport.May = date.Month == 5 ? item.NumberOfWorkingDays >= 1 ? leaveReport.May + 1 : leaveReport.May + 0.5 : leaveReport.May;
                                    leaveReport.Jun = date.Month == 6 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Jun + 1 : leaveReport.Jun + 0.5 : leaveReport.Jun;
                                    leaveReport.Jul = date.Month == 7 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Jul + 1 : leaveReport.Jul + 0.5 : leaveReport.Jul;
                                    leaveReport.Aug = date.Month == 8 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Aug + 1 : leaveReport.Aug + 0.5 : leaveReport.Aug;
                                    leaveReport.Sep = date.Month == 9 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Sep + 1 : leaveReport.Sep + 0.5 : leaveReport.Sep;
                                    leaveReport.Oct = date.Month == 10 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Oct + 1 : leaveReport.Oct + 0.5 : leaveReport.Oct;
                                    leaveReport.Nov = date.Month == 11 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Nov + 1 : leaveReport.Nov + 0.5 : leaveReport.Nov;
                                    leaveReport.Dec = date.Month == 12 ? item.NumberOfWorkingDays >= 1 ? leaveReport.Dec + 1 : leaveReport.Dec + 0.5 : leaveReport.Dec;
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
                Logger.Error("Exception occured at UserRepository GetLeaveReportDetails method ");
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
                    var profileDetails = ctx.EmployeeDetails.Include("EmployeeEducationDetails").Include("EmployeeExperienceDetails").Include("UserAccounts").Include("EmployeeSkills").Include("MasterDataValue2").Include("EmployeeCurrentAddressDetails").Include("EmployeePermanentAddressDetails").Include("EmployeeWorkLocationDetails").Include("EmployeeEmergencyContactDetails").FirstOrDefault(i => i.Id == employeeId);
                    var allSkills = ctx.MasterDataValues.Where(i => i.RefMasterType == (int)MasterDataTypeEnum.Skills).ToList();
                    int cityid = Convert.ToInt32(profileDetails.EmployeeCurrentAddressDetails.FirstOrDefault().City);
                    int countryid = Convert.ToInt32(profileDetails.EmployeeCurrentAddressDetails.FirstOrDefault().Country);
                    int roleid = Convert.ToInt32(profileDetails.RefRoleId);
                    string city = cityid != 0 ? ctx.Geo_Location_City_Master.Where(i => i.Id == cityid).FirstOrDefault().Name : "";
                    string country = ctx.Geo_Location_Country_Master.Where(i => i.Id == countryid).FirstOrDefault().Name;
                    string RoleName = ctx.MasterDataValues.Where(i => i.Id == roleid).FirstOrDefault().Value;
                    int ProjectId = profileDetails.EmployeeProjectDetails.FirstOrDefault().RefProjectId;
                    string ProjectName = ctx.ProjectMasters.Where(i => i.Id == ProjectId).FirstOrDefault().ProjectName;
                    int ManagerId = Convert.ToInt32(profileDetails.ManagerId);
                    string ManagerName = ManagerId !=0 ? ctx.EmployeeDetails.Where(i => i.Id == ManagerId).FirstOrDefault().FirstName : "";
                    string ManagerLastName = ManagerId != 0 ?  ctx.EmployeeDetails.Where(i => i.Id == ManagerId).FirstOrDefault().LastName : "";
                    int EmployeeTypeId = Convert.ToInt32(profileDetails.RefEmployeeType);
                    string EmployeeType = EmployeeTypeId !=0 ?  ctx.MasterDataValues.Where(i => i.Id == EmployeeTypeId).FirstOrDefault().Value : "";
                    int EmployeeContractTypeId =  Convert.ToInt32(profileDetails.RefEmployeeContractType);
                    string EmployeeContractType = EmployeeContractTypeId != 0 ? ctx.MasterDataValues.Where(i => i.Id == EmployeeContractTypeId).FirstOrDefault().Value : "";
                    int profileTypeId = profileDetails.RefProfileType;
                    string profileType = profileTypeId != 0 ? ctx.MasterDataValues.Where(i => i.Id == profileTypeId).FirstOrDefault().Value : "";
                    int facilityId = profileDetails.EmployeeWorkLocationDetails.Count > 0 ? Convert.ToInt32(profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().Facility) : 0;
                    string facilityName = facilityId !=0 ? ctx.Geo_Location_Facility_Master.Where(i => i.Id == facilityId).FirstOrDefault().Name : "";
                    int countryId = profileDetails.EmployeeWorkLocationDetails.Count > 0 ? Convert.ToInt32(profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().Country) : 0;
                    string countryName = countryId !=0 ? ctx.Geo_Location_Country_Master.Where(i => i.Id == countryId).FirstOrDefault().Name : "";
                    int stateId = profileDetails.EmployeeWorkLocationDetails.Count > 0 ? Convert.ToInt32(profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().State) : 0;
                    string stateName =  stateId != 0 ? ctx.Geo_Location_State_Master.Where(i => i.Id == stateId).FirstOrDefault().Name : "";
                    int cityId = profileDetails.EmployeeWorkLocationDetails.Count > 0 ? Convert.ToInt32(profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().City) : 0;
                    string cityName = cityId !=0 ? ctx.Geo_Location_City_Master.Where(i => i.Id == cityId).FirstOrDefault().Name : "";
                    if (ManagerLastName!=null)
                    {
                        ManagerName += " ";
                        ManagerName += ManagerLastName;
                    }
                    profileDetails.City = city;
                    profileDetails.Country = country;
                    profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().Facility = facilityName;
                    profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().Country = countryName;
                    profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().State = stateName;
                    profileDetails.EmployeeWorkLocationDetails.FirstOrDefault().City = cityName;
                    profileDetails.RoleName = RoleName;
                    profileDetails.ProjectName = ProjectName;
                    profileDetails.ManagerName = ManagerName;
                    profileDetails.EmployeeType = EmployeeType;
                    profileDetails.EmployeeContractType = EmployeeContractType;
                    profileDetails.ProfileType = profileType;
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
                    var projectIdList = ctx.EmployeeProjectDetails.Where(i => i.RefEmployeeId == employeeId && i.ProjectMaster.IsBench == false).Select(m => m.RefProjectId).Distinct().ToList();
                    var projectDetails = ctx.EmployeeProjectDetails.
                        Where(m => m.RefEmployeeId == employeeId && projectIdList.Contains(m.RefProjectId))
                        .Select(n => new ProjectsList()
                        {
                            Id = n.Id,
                            EndDate = n.EndDate.HasValue == true ? n.EndDate.Value : DateTime.Now,
                            ProjectName = n.ProjectMaster.ProjectName,
                            StartDate = n.StartDate.HasValue == true ? n.StartDate.Value : DateTime.Now
                        }).OrderBy(m => m.StartDate)
                        .GroupBy(m => m.ProjectName).Select(a => a.FirstOrDefault())
                        .ToList();
                    projects = projectDetails;
                    Logger.Info("Successfully exiting from UserRepository API GetUserProfileDetails method");
                    return profileDetails;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository GetUserProfileDetails method ");
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
                    string imageBase64Data = string.Empty;
                    if (!string.IsNullOrEmpty(model.ImagePath))
                    {
                        byte[] imageByteData = System.IO.File.ReadAllBytes(model.ImagePath);
                        imageBase64Data = Convert.ToBase64String(imageByteData);

                    }
                    var empData = new EmployeeDetail();
                    empData = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == model.Id);
                    empData.FirstName = model.FirstName;
                    empData.LastName = model.LastName;
                    if(!string.IsNullOrEmpty(imageBase64Data))
                    { empData.ImagePath = imageBase64Data; }
                    empData.DateOfBirth = model.DateOfBirth;
                    empData.PhoneNumber = model.Telephone;
                    empData.ModifiedDate = DateTime.Now;
                    empData.ModifiedBy = model.FirstName;
                    empData.FacebookLink = model.FacebookLink;
                    empData.TwitterLink = model.TwitterLink;
                    empData.GooglePlusLink = model.GooglePlusLink;
                    ctx.SaveChanges();

                    var empcurrentaddress = new EmployeeCurrentAddressDetail();
                    empcurrentaddress.City = model.City;
                    empcurrentaddress.Country = model.Country;
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository EditEmployeeDetails method ");
                throw;
            }
        }


        public bool EditEmployeeEmergencyContactDetails(LMS_WebAPI_Domain.EmployeeEmergencyContactDetail model)
        {
            Logger.Info("Entering in UserRepository API EditEmployeeEmergencyContactDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    
                    var empData = new EmployeeEmergencyContactDetail();
                    empData = ctx.EmployeeEmergencyContactDetails.FirstOrDefault(i => i.RefEmployeeId == model.RefEmployeeId);
                    empData.Name = model.Name;
                    empData.Relationship= model.Relationship;
                    empData.Telephone = model.Telephone;
                    empData.Country = model.Country;
                    empData.State = model.State;
                    empData.City = model.City;
                    empData.AddressLine1 = model.AddressLine1;
                    empData.AddressLine2 = model.AddressLine2;
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository EditEmployeeDetails method ");
                throw;
            }
        }

        public bool EditEmployeeCurrentAddressDetails(LMS_WebAPI_Domain.EmployeeCurrentAddressDetail model)
        {
            Logger.Info("Entering in UserRepository API EditEmployeeCurrentAddressDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var empData = new EmployeeCurrentAddressDetail();
                    empData = ctx.EmployeeCurrentAddressDetails.FirstOrDefault(i => i.RefEmployeeId == model.RefEmployeeId);
                    empData.Country = model.Country;
                    empData.State = model.State;
                    empData.City = model.City;
                    empData.Pincode = model.Pincode;
                    empData.AddressLine1 = model.AddressLine1;
                    empData.AddressLine2 = model.AddressLine2;
                    empData.RefModifiedBy = model.RefEmployeeId;
                    empData.ModifiedDate = DateTime.Now.Date;
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeCurrentAddressDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository EditEmployeeCurrentAddressDetails method ");
                throw;
            }
        }

        public bool EditEmployeePermanentAddressDetails(LMS_WebAPI_Domain.EmployeePermanentAddressDetail model)
        {
            Logger.Info("Entering in UserRepository API EditEmployeePermanentAddressDetails method");
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var empData = new EmployeePermanentAddressDetail();
                    empData = ctx.EmployeePermanentAddressDetails.FirstOrDefault(i => i.RefEmployeeId == model.RefEmployeeId);
                    empData.Country = model.Country;
                    empData.State = model.State;
                    empData.City = model.City;
                    empData.Pincode = model.Pincode;
                    empData.AddressLine1 = model.AddressLine1;
                    empData.AddressLine2 = model.AddressLine2;
                    empData.RefModifiedBy = model.RefEmployeeId;
                    empData.ModifiedDate = DateTime.Now.Date;
                    ctx.SaveChanges();
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeePermanentAddressDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository EditEmployeePermanentAddressDetails method ");
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
                            if (item.Degree != null && item.Institution != null)
                            {
                                if (item.Degree.Trim() != "" && item.Institution.Trim() != "")
                                {
                                    employeeEdDetails.Degree = item.Degree;
                                    employeeEdDetails.Institution = item.Institution;
                                    employeeEdDetails.FromDate = item.FromDate;
                                    employeeEdDetails.ToDate = item.ToDate;
                                    ctx.SaveChanges();
                                }
                            }
                            
                        }
                        else
                        {
                            if (item.Degree != null && item.Institution != null)
                            {
                                if (item.Degree.Trim() != "" && item.Institution.Trim() != "")
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
                        }

                    }
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeEducationDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository EditEmployeeEducationDetails method ");
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
                            if (item.Company != null && item.Role != null)
                            {
                                if (item.Company.Trim() != "" && item.Role.Trim() != "")
                                {
                                    expDetails.CompanyName = item.Company;
                                    expDetails.Role = item.Role;
                                    expDetails.FromDate = item.FromDate;
                                    expDetails.ToDate = item.ToDate;
                                    expDetails.CompanyLogo = item.CompanyLogo;
                                    ctx.SaveChanges();
                                }
                            }

                        }
                        else
                        {
                            if (item.Company != null && item.Role != null)
                            {
                                if (item.Company.Trim() != "" && item.Role.Trim() != "")
                                {
                                    var employeeExpdetails = new EmployeeExperienceDetail();
                                    employeeExpdetails.CompanyName = item.Company;
                                    employeeExpdetails.Role = item.Role;
                                    employeeExpdetails.FromDate = item.FromDate;
                                    employeeExpdetails.ToDate = item.ToDate;
                                    employeeExpdetails.RefEmployeeId = employeeId;
                                    employeeExpdetails.CompanyLogo = item.CompanyLogo;
                                    ctx.EmployeeExperienceDetails.Add(employeeExpdetails);
                                    ctx.SaveChanges();
                                }
                            }
                        }

                    }
                    Logger.Info("Successfully exiting from UserRepository API EditEmployeeExperienceDetails method");
                    return true;
                }
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository EditEmployeeExperienceDetails method ");
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
                Logger.Error("Exception occured at UserRepository EditEmployeeSkills method ");
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
                Logger.Error("Exception occured at UserRepository getUserProfileImage method ");
                throw;
            }
        }

        public List<EmployeeDetail> GetTeamMembers(int UserEmpId)
        {
            try
            {
                Logger.Info("Exception occured at UserRepository getUserProfileImage method ");
                List<EmployeeDetail> resTeamMembers = new List<EmployeeDetail>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var lstProjectDetails = ctx.EmployeeProjectDetails.Where(m => m.RefEmployeeId == UserEmpId && m.IsActive == true).Select(m => m.RefProjectId).ToList();
                    if (lstProjectDetails != null && lstProjectDetails.Count > 0)
                    {
                        resTeamMembers = ctx.EmployeeProjectDetails.Include("EmployeeDetail").Include("MasterDataValue").Where(m => lstProjectDetails.Contains(m.RefProjectId)).Select(m => m.EmployeeDetail).Distinct().ToList();
                    }
                }
                Logger.Info("Exception occured at UserRepository getUserProfileImage method ");
                return resTeamMembers;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception occured at UserRepository GetTeamMembers method ");
                throw ex;
            }
        }

        public bool CheckEmployeePassword(int employeeId, string currentPassword)
        {
            Logger.Info("Entering in UserRepository API CheckEmployeePassword method");
            try
            {
                string encryptedPassword = CommonMethods.encryption(currentPassword);
                bool result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    string userName = ctx.UserAccounts.Where(x => x.RefEmployeeId == employeeId).FirstOrDefault().UserName;
                    var Employee = ctx.UserAccounts.Where(x => x.UserName == userName && x.Password == encryptedPassword).FirstOrDefault();
                    if (Employee != null)
                        result = true;

                }
                Logger.Info("Successfully exiting from UserRepository API CheckEmployeePassword method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository API CheckEmployeePassword method ");
                throw;
            }
        }

        public bool UpdatePassword(int employeeId, string newPassword)
        {
            Logger.Info("Entering in UserRepository API UpdatePassword method");
            try
            {
                string encryptedPassword = CommonMethods.encryption(newPassword);
                bool result = false;
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    string userName = ctx.UserAccounts.Where(x => x.RefEmployeeId == employeeId).FirstOrDefault().UserName;
                    var Employee = ctx.UserAccounts.Where(x => x.UserName == userName).FirstOrDefault();
                    if (Employee != null)
                    {
                        Employee.Password = encryptedPassword;
                        ctx.SaveChanges();
                        result = true;
                    }
                }
                Logger.Info("Successfully exiting from UserRepository API UpdatePassword method");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at UserRepository API UpdatePassword method ");
                throw;
            }
        }

    }
}

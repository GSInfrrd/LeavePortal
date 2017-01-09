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
                    //var userData = (from c in ctx.UserAccounts
                    //                where c.UserName == emailId && c.Password == password
                    //                select c).FirstOrDefault();
                    if (userData != null)
                    {
                        return userData;
                    }
                    return null;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public EmployeeCommon GetUserDetails(int UserEmpId)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var empDetails = (from n in ctx.EmployeeDetails
                                      where n.Id == UserEmpId
                                      select new EmployeeCommon()
                                      {
                                          Id = n.Id,
                                          Name = n.FirstName,
                                          ManagerId = n.ManagerId,
                                          Experience = n.Experience,
                                          RoleName = n.MasterDataValue.Value,
                                          DateOfJoining = n.DateOfJoining,
                                      }).FirstOrDefault();
                    if (null != empDetails)
                    {
                        var leaveType = (from n in ctx.EmployeeLeaveMasters where n.RefEmployeeId == UserEmpId select n).SingleOrDefault();
                        if (null != leaveType)
                        {
                            int ad = Convert.ToInt16(AdvanceLeaveLimit.limit);
                            var advanceLimit = (from n in ctx.MasterDataValues where n.RefMasterType == ad select n).FirstOrDefault();
                            var sickLeaveint = Convert.ToInt16(LeaveType.SickLeave);
                            var CasualLeaveint = Convert.ToInt16(LeaveType.CasualLeave);
                            empDetails.TotalAdvanceLeaveToTake = (Convert.ToInt16(advanceLimit.Value) < leaveType.AdvancedLeaveCount) ? Convert.ToInt16(advanceLimit.Value) : leaveType.AdvancedLeaveCount;
                            empDetails.TotalCasualLeave = leaveType.LeaveBalance + leaveType.RewardedLeaveCount;
                            empDetails.TotalLeaveCount = leaveType.AdvancedLeaveCount + leaveType.LeaveBalance + leaveType.RewardedLeaveCount;
                        }

                        empDetails.TotalSpent = (from c in ctx.EmployeeLeaveTransactions
                                                 where c.RefEmployeeId == UserEmpId & c.RefStatus == (int)(LeaveStatus.Approved)
                                                 select c.NumberOfWorkingDays).ToList().Sum();

                        empDetails.TotalApplied = (from c in ctx.EmployeeLeaveTransactions
                                                   where c.RefEmployeeId == UserEmpId & c.RefStatus == (int)(LeaveStatus.Submitted)
                                                   select c.NumberOfWorkingDays).ToList().Sum();

                        var empdata = (from n in ctx.EmployeeProjectDetails
                                       where n.RefEmployeeId == UserEmpId
                                       select n).SingleOrDefault();
                        empDetails.ProjectName = empdata != null ? empdata.MasterDataValue.Value : string.Empty;
                        var managerDetails = (from n in ctx.EmployeeDetails where n.Id == empDetails.ManagerId select n).SingleOrDefault();
                        if (null != managerDetails)
                        {
                            empDetails.ManagerName = managerDetails.FirstName;
                            empDetails.ManagerEmailId = managerDetails.UserAccounts.FirstOrDefault().UserName;
                        }

                        return empDetails;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Announcement> GetAnnouncements()
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var announcements = ctx.Announcements.Where(x => x.IsActive == true).ToList();

                    return announcements;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public LeaveReportModel GetLeaveReportDetails(int year, int employeeId = 0)
        {
            try
            {
                var leaveReport = new LeaveReportModel();
                var years = new List<EmployeeLeaveTransaction>();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    if (employeeId != 0)
                    {
                        years = ctx.EmployeeLeaveTransactions.Where(i => i.RefEmployeeId == employeeId && i.RefStatus == (int)LeaveStatus.Approved && i.FromDate.Year == year && i.ToDate.Year == year).ToList();
                    }
                    else
                    {

                        years = ctx.EmployeeLeaveTransactions.Where(i => i.RefStatus == (int)LeaveStatus.Approved && i.FromDate.Year == year && i.ToDate.Year == year).ToList();
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
                    return leaveReport;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EmployeeDetail GetUserProfileDetails(int employeeId)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var profileDetails = ctx.EmployeeDetails.Include("EmployeeEducationDetails").Include("EmployeeExperienceDetails").Include("UserAccounts").Include("EmployeeSkills").Include("MasterDataValue").FirstOrDefault(i => i.Id == employeeId);

                    return profileDetails;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool EditEmployeeDetails(EmployeeDetailsModel model)
        {
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
                    ctx.SaveChanges();
                    return true;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails, int employeeId)
        {
            try
            {

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var eduDetails = ctx.EmployeeEducationDetails.Where(i => i.RefEmployeeId == employeeId).ToList();
                    foreach (var item in educationDetails)
                    {
                        foreach (var eduItem in eduDetails)
                        {
                            if (item.Id == eduItem.Id)
                            {
                                eduItem.Degree = item.Degree;
                                eduItem.Institution = item.Institution;
                                eduItem.FromDate = item.FromDate;
                                eduItem.ToDate = item.ToDate;
                                ctx.SaveChanges();

                            }
                            else
                            {
                                eduItem.Degree = item.Degree;
                                eduItem.Institution = item.Institution;
                                eduItem.FromDate = item.FromDate;
                                eduItem.ToDate = item.ToDate;
                                eduItem.RefEmployeeId = employeeId;
                                ctx.EmployeeEducationDetails.Add(eduItem);
                                ctx.SaveChanges();
                            }
                        }

                    }

                    return true;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        {
            try
            {

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var expDetails = ctx.EmployeeExperienceDetails.Where(i => i.RefEmployeeId == employeeId).ToList();
                    foreach (var item in experienceDetails)
                    {
                        foreach (var expItem in expDetails)
                        {
                            if (item.Id == expItem.Id)
                            {
                                expItem.CompanyName = item.Company;
                                expItem.Role = item.Role;
                                expItem.FromDate = item.FromDate;
                                expItem.ToDate = item.ToDate;
                                ctx.SaveChanges();

                            }
                            else
                            {
                                expItem.CompanyName = item.Company;
                                expItem.Role = item.Role;
                                expItem.FromDate = item.FromDate;
                                expItem.ToDate = item.ToDate;
                                expItem.RefEmployeeId = employeeId;
                                ctx.EmployeeExperienceDetails.Add(expItem);
                                ctx.SaveChanges();
                            }
                        }

                    }

                    return true;
                }
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public string getUserProfileImage(int employeeId)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var profileDetails = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == employeeId);
                    //return Convert.FromBase64String(profileDetails.ImagePath);

                    return profileDetails.ImagePath;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

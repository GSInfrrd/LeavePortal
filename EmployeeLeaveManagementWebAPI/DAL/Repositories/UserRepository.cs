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
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var userData = ctx.UserAccounts.Include("EmployeeDetail").FirstOrDefault(x => x.UserName == emailId && x.Password == password);
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
                        var LeaveType = ctx.LeaveMasters.ToList();
                        empDetails.TotalLeaveCount = LeaveType.Sum(q => q.Count);

                        empDetails.TotalCountTaken = (from c in ctx.EmployeeLeaveTransactions
                                                      where c.RefEmployeeId == UserEmpId
                                                      select c.NumberOfWorkingDays).ToList().Sum();
                        
                        var empdata = (from n in ctx.EmployeeProjectDetails
                                       where n.RefEmployeeId == UserEmpId
                                       select n).SingleOrDefault();
                        empDetails.ProjectName = empdata.MasterDataValue.Value;
                        empDetails.ManagerName = (from n in ctx.EmployeeDetails where n.Id == empDetails.ManagerId select n.FirstName).SingleOrDefault();

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

        public LeaveReportModel GetLeaveReportDetails(int employeeId,int year)
        {
            try
            {
                var leaveReport = new LeaveReportModel();
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var years = ctx.EmployeeLeaveTransactions.Where(i=>i.RefEmployeeId==employeeId && i.FromDate.Year==year && i.ToDate.Year==year).ToList();
                    foreach(var item in years)
                    {
                       
                        for (DateTime date=item.FromDate;date<=item.ToDate;date=date.AddDays(1))
                        {
                            if(date.DayOfWeek!=DayOfWeek.Saturday && date.DayOfWeek!=DayOfWeek.Sunday)
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

                    //var Year = new SqlParameter("@Year", 2016);
                    //var data= ctx.Database.SqlQuery<LeaveReportModel>("dbo.GetLeaveReportProcedure @Year", Year).ToList();
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
                    var profileDetails = ctx.EmployeeDetails.Include("EmployeeEducationDetails").Include("EmployeeExperienceDetails").Include("UserAccounts").FirstOrDefault(i => i.Id == employeeId);

                    return profileDetails;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}

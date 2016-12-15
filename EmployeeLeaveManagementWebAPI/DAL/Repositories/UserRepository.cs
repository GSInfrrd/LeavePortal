using LMS_WebAPI_DAL.Repositories.Interfaces;
using System.Linq;
using LMS_WebAPI_Utils;
using LMS_WebAPI_DAL;
using System;
using System.Collections.Generic;

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
                                          Name = n.Name,
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
                        empDetails.ManagerName = (from n in ctx.EmployeeDetails where n.Id == empDetails.ManagerId select n.Name).SingleOrDefault();

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

    }
}

using System.Collections.Generic;
using System.Linq;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL;
using System;
using LMS_WebAPI_Utils;
using System.Data.Entity;

namespace LMS_WebAPI_DAL.Repositories
{
    public class AddLeaveRepository :IAddLeaveRepository
    {
       
        public List<string> GetLeaveType()
        {
            using (var ctx = new LeaveManagementSystemEntities1())
            {

                //var leavetypeid  = (from s in ctx.MasterDataTypes
                //                    where s.Type=="LeaveType"
                //                    select s).SingleOrDefault();

                var leaveType = ctx.MasterDataValues.Where(x => x.RefMasterType == 3).Select(x => x.Value).ToList();

                return leaveType;

            }
        }

        public bool InsertEmployeeLeaveDetails(int leaveType, string fromDate, string toDate, string comments, int workingDays)
        {
           
            var result = false;

            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var employeeLeaveDetails = new EmployeeLeaveTransaction
                    {
                        EmployeeComment = comments,
                        FromDate = Convert.ToDateTime(fromDate),
                        ToDate = Convert.ToDateTime(toDate),
                        CreatedDate = DateTime.Now,
                        NumberOfWorkingDays = workingDays,
                        RefLeaveType = leaveType,
                        RefStatus =(int)LeaveStatus.Planned,
                        RefEmployeeId = 1,
                        CreatedBy = "Alekya"
                    };
                    ctx.EmployeeLeaveTransactions.Add(employeeLeaveDetails);                       
                        ctx.SaveChanges();
                 

                    }
                    result = true;
         
            }
            catch (Exception ex)
            {
                  throw;
            }
               return result;
        }


        public bool SubmitLeaveForApproval(int id)
        {

            var result = false;

            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var leaveDetails = ctx.EmployeeLeaveTransactions.FirstOrDefault(x => x.Id == id);
                    leaveDetails.RefStatus =(int) LeaveStatus.Submitted;
                    ctx.SaveChanges();         
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
        public bool DeleteLeaveRequest(int id)
        {

            var result = false;

            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var leaveDetails = ctx.EmployeeLeaveTransactions.FirstOrDefault(x => x.Id == id);
                    ctx.EmployeeLeaveTransactions.Remove(leaveDetails);
                    ctx.SaveChanges();
                }
                result = true;

            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }

}
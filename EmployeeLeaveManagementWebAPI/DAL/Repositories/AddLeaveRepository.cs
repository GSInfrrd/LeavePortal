using System.Collections.Generic;
using System.Linq;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_DAL;
using System;
using LMS_WebAPI_Utils;
using System.Data.Entity;
using System.Net.Mail;

namespace LMS_WebAPI_DAL.Repositories
{
    public class AddLeaveRepository : IAddLeaveRepository
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

        public bool InsertEmployeeLeaveDetails(int empId, int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            var result = false;
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    switch ((LeaveType)leaveType)
                    {
                        case LeaveType.AdvanceLeave:
                        case LeaveType.CasualLeave:
                        case LeaveType.CompOff:
                            //go for approval
                            var employeeLeaveDetails = new EmployeeLeaveTransaction
                            {
                                EmployeeComment = comments,
                                FromDate = Convert.ToDateTime(fromDate),
                                ToDate = Convert.ToDateTime(toDate),
                                CreatedDate = DateTime.Now,
                                NumberOfWorkingDays = workingDays,
                                RefLeaveType = leaveType,
                                RefStatus = (int)LeaveStatus.Submitted,
                                RefEmployeeId = empId,
                                CreatedBy = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == empId).FirstName
                            };
                            var newID = ctx.EmployeeLeaveTransactions.Add(employeeLeaveDetails);
                            ctx.SaveChanges();
                            var newId = Convert.ToInt16(newID.Id);
                            //on apply leave the status go to submitted
                            //Adding workflow table insertions
                            SubmitLeaveForApproval(newId);
                            break;
                        case LeaveType.SickLeave:
                            //auto approval
                            var employeeLeaveDetailsSick = new EmployeeLeaveTransaction
                            {
                                EmployeeComment = comments,
                                FromDate = Convert.ToDateTime(fromDate),
                                ToDate = Convert.ToDateTime(toDate),
                                CreatedDate = DateTime.Now,
                                NumberOfWorkingDays = workingDays,
                                RefLeaveType = leaveType,
                                RefStatus = (int)LeaveStatus.Approved,
                                RefEmployeeId = empId,
                                CreatedBy = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == empId).FirstName
                            };
                            ctx.EmployeeLeaveTransactions.Add(employeeLeaveDetailsSick);
                            ctx.SaveChanges();
                            break;
                    }
                }
                result = true;
            }
            catch (Exception)
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
                    leaveDetails.RefStatus = (int)LeaveStatus.Submitted;
                    ctx.SaveChanges();
                    var mailFrom = ctx.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == leaveDetails.RefEmployeeId);
                    var fromEmail = ctx.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == mailFrom.Id).UserName;
                    var toEmail = ctx.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == mailFrom.EmployeeDetail.ManagerId).UserName;
                    var workFlow = new Workflow
                    {
                        RefLeaveTransactionId = leaveDetails.Id,
                        RefApproverId = (int)ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).ManagerId,
                        ModifiedDate = DateTime.Now,
                        RefStatus = (int)LeaveStatus.Submitted,
                        CreatedDate = DateTime.Now,
                        CreatedBy = leaveDetails.EmployeeDetail.FirstName
                    };
                    ctx.Workflows.Add(workFlow);
                    ctx.SaveChanges();

                    var op = SendMail(leaveDetails.EmployeeDetail.FirstName, fromEmail, toEmail);

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
                    var workFlowDet = ctx.Workflows.FirstOrDefault(x => x.RefLeaveTransactionId == leaveDetails.Id);
                    ctx.Workflows.Remove(workFlowDet);
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

        public bool SendMail(string firstName, string fromEmail, string toEmail)
        {

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail);

            message.To.Add(new MailAddress("alekhya.kk9@gmail.com"));
            message.CC.Add(new MailAddress("alekya@infrrd.ai"));
            message.Subject = "Leave Notification";
            message.IsBodyHtml = true;
            message.Body = @"Hi,<br/><b>" + firstName + "</b> has applied for leave.Login to your account to approve/reject.<br/><br/>Best Regards,<br/><b>Infrrd Leave Management<b>";



            using (var client = new SmtpClient())
            {
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("alekya@infrrd.ai", "alkvyS9.");

                client.Send(message);
            }

            return true;
        }

        public EmployeeDetail CheckLeaveAvailability(int employeeId, out List<Holiday> holidayList)
        {

            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var data = ctx.EmployeeDetails.Include("EmployeeLeaveMasters1").Include("EmployeeLeaveTransactions").FirstOrDefault(i => i.Id == employeeId);
                    holidayList = ctx.Holidays.ToList();

                    return data;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}


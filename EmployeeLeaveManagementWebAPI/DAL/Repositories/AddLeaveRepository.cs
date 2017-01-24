﻿using System.Collections.Generic;
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
            try
            {
                Logger.Info("Entering in AddLeaveRepository API GetLeaveType method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveType = ctx.MasterDataValues.Where(x => x.RefMasterType == 3).Select(x => x.Value).ToList();
                    Logger.Info("Successfully exiting from AddLeaveRepository API AddLeaveRepository method");
                    return leaveType;
                }
            }
            catch 
            {
                Logger.Info("Exception occured at AddLeaveRepository API AddLeaveRepository method ");
                throw;
            }
        }

        public bool InsertEmployeeLeaveDetails(int empId, int leaveType, string fromDate, string toDate, string comments, double workingDays)
        {
            try
            {
                Logger.Info("Entering in AddLeaveRepository API InsertEmployeeLeaveDetails method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    switch ((LeaveType)leaveType)
                    {

                        case LeaveType.AdvanceLeave:
                        case LeaveType.CasualLeave:
                        case LeaveType.CompOff:
                        case LeaveType.LOP:
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
                                RefTransactionType=(int)TransactionType.Debit,
                                RefCreatedBy = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == empId).Id
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
                                RefTransactionType = (int)TransactionType.Debit,
                                RefCreatedBy = ctx.EmployeeDetails.FirstOrDefault(i => i.Id == empId).Id
                            };
                            ctx.EmployeeLeaveTransactions.Add(employeeLeaveDetailsSick);
                            ctx.SaveChanges();
                            var leaveMaster = (from n in ctx.EmployeeLeaveMasters
                                               where n.RefEmployeeId == empId
                                               select n).SingleOrDefault();
                            leaveMaster.EarnedCasualLeave = leaveMaster.EarnedCasualLeave - Convert.ToInt16(workingDays);
                            ctx.EmployeeLeaveMasters.Attach(leaveMaster);
                            ctx.Entry(leaveMaster).State = EntityState.Modified;
                            ctx.SaveChanges();
                            break;
                    }
                    Logger.Info("Successfully exiting from AddLeaveRepository API InsertEmployeeLeaveDetails method");
                    return true;
                }
            }
            catch 
            {
                Logger.Info("Exception occured at AddLeaveRepository API InsertEmployeeLeaveDetails method ");
                throw;
            }
        }

        public bool SubmitLeaveForApproval(int id)
        {
            var result = false;
            try
            {
                Logger.Info("Entering in AddLeaveRepository API SubmitLeaveForApproval method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveDetails = ctx.EmployeeLeaveTransactions.FirstOrDefault(x => x.Id == id);
                    leaveDetails.RefStatus = (int)LeaveStatus.Submitted;
                    ctx.SaveChanges();
                    //var mailFrom = ctx.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == leaveDetails.RefEmployeeId);
                    //var fromEmail = ctx.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == mailFrom.Id).UserName;
                    //var toEmail = ctx.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == mailFrom.EmployeeDetail.ManagerId).UserName;
                    var workFlow = new Workflow
                    {
                        RefLeaveTransactionId = leaveDetails.Id,
                        RefApproverId = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).ManagerId!=null?(int)ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).ManagerId:2,
                        ModifiedDate = DateTime.Now,
                        RefStatus = (int)LeaveStatus.Submitted,
                        CreatedDate = DateTime.Now,
                        RefCreatedBy = leaveDetails.EmployeeDetail.Id
                    };
                    ctx.Workflows.Add(workFlow);
                    ctx.SaveChanges();
                    //TO:DO Send mail asynchronosly
                   // var op = SendMail(leaveDetails.EmployeeDetail.FirstName, fromEmail, toEmail);

                    //Send notification to manager
                    int RefApproverId = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).ManagerId!=null?(int)ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).ManagerId:2;
                    string Firstname = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).FirstName;
                    string Lastname = ctx.EmployeeDetails.FirstOrDefault(x => x.Id == leaveDetails.RefEmployeeId).LastName;

                    string Text = Firstname;
                    if (Lastname != null)
                    {
                        Text += " ";
                        Text += Lastname;
                    }

                    Text += " has applied for leave.";
                    int Status = 1;
                    int NotificationType =(Int16)LMS_WebAPI_Utils.NotificationType.NotificationType;
                    ApproveLeaveRepository alr = new ApproveLeaveRepository();
                    alr.insertNotification(RefApproverId, Text, Status, NotificationType);
                }
                Logger.Info("Successfully exiting from AddLeaveRepository API SubmitLeaveForApproval method");
                result = true;

            }
            catch 
            {
                Logger.Info("Exception occured at AddLeaveRepository API SubmitLeaveForApproval method ");
                throw;
            }
            return result;
        }

        public bool DeleteLeaveRequest(int id)
        {

            var result = false;
            try
            {
                Logger.Info("Entering in AddLeaveRepository API DeleteLeaveRequest method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveDetails = ctx.EmployeeLeaveTransactions.FirstOrDefault(x => x.Id == id);
                    var workFlowDet = ctx.Workflows.FirstOrDefault(x => x.RefLeaveTransactionId == leaveDetails.Id);
                    if (workFlowDet != null)
                    {
                        ctx.Workflows.Remove(workFlowDet);
                    }
                    ctx.EmployeeLeaveTransactions.Remove(leaveDetails);             
                    ctx.SaveChanges();
                }
                Logger.Info("Successfully exiting from AddLeaveRepository API DeleteLeaveRequest method");
                result = true;

            }
            catch
            {
                Logger.Info("Exception occured at AddLeaveRepository API DeleteLeaveRequest method ");
                throw;
            }
            return result;
        }

        public bool SendMail(string firstName, string fromEmail, string toEmail)
        {
            try
            {
                Logger.Info("Entering in AddLeaveRepository API SendMail method");
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
                Logger.Info("Successfully exiting from AddLeaveRepository API SendMail method");
            }
            catch
            {
                Logger.Info("Exception occured at AddLeaveRepository API SendMail method ");
                throw;
            }
            return true;
        }

        public EmployeeDetail CheckLeaveAvailability(int employeeId, out List<Holiday> holidayList,out int advanceLeaveLimit,out int lopLeaveLimit)
        {

            try
            {
                Logger.Info("Entering in AddLeaveRepository API CheckLeaveAvailability method");
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var data = ctx.EmployeeDetails.Include("EmployeeLeaveMasters").Include("EmployeeLeaveTransactions").Include("WorkFromHomes").FirstOrDefault(i => i.Id == employeeId);
                    holidayList = ctx.Holidays.ToList();
                    advanceLeaveLimit =Convert.ToInt32(ctx.MasterDataValues.FirstOrDefault(i => i.RefMasterType == (int)AdvanceLeaveLimit.limit).Value);
                    lopLeaveLimit = Convert.ToInt32(ctx.MasterDataValues.FirstOrDefault(i => i.RefMasterType == (int)LOPLeaveLimit.limit).Value);
                    Logger.Info("Successfully exiting from AddLeaveRepository API CheckLeaveAvailability method");
                    return data;
                }
            }
            catch
            {
                Logger.Info("Exception occured at AddLeaveRepository API CheckLeaveAvailability method ");
                throw;
            }
        }

        public RewardLeaveModel GetRewardLeaveModelDetails()
        {
            try
            {
                Logger.Info("Entering in AddLeaveRepository API GetRewardLeaveModelDetails method");
                var rewardLeaveModel = new RewardLeaveModel();

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var lstProjects = new List<Details>();
                    var lstEmployees = new List<Details>();
                    var projects = ctx.ProjectMasters.Select(x => new { x.Id, x.ProjectName }).ToList();

                    foreach (var project in projects)
                    {
                        Details projectDetails = new Details();
                        projectDetails.Id = project.Id;
                        projectDetails.Name = project.ProjectName;
                        lstProjects.Add(projectDetails);
                    }
                    rewardLeaveModel.Projects = lstProjects;

                    var employees = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int16)EmployeeRole.Employee).Select(x => new { x.Id, x.FirstName, x.LastName }).ToList();

                    foreach (var employee in employees)
                    {
                        Details employeeDetails = new Details();
                        employeeDetails.Id = employee.Id;
                        employeeDetails.Name = employee.FirstName + " " + employee.LastName;
                        lstEmployees.Add(employeeDetails);
                    }
                    rewardLeaveModel.Employees = lstEmployees;

                    Logger.Info("Successfully exiting from AddLeaveRepository API GetRewardLeaveModelDetails method");
                    return rewardLeaveModel;
                }
            }
            catch
            {
                Logger.Info("Exception occured at AddLeaveRepository API GetRewardLeaveModelDetails method ");
                throw;
            }
        }

        public bool Rewardleave(RewardLeaveModel model)
        {
            try
            {
                Logger.Info("Entering in AddLeaveRepository API Rewardleave method");
                bool leaveRewarded = false;

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var rewardedLeaveEntity = new EmployeeRewardedLeaveDetail()
                    {
                        RewardedBy = model.ManagerId,
                        RefEmployeeId = model.EmplooyeeId,
                        RefProjectId = model.ProjectId,
                        LeaveCount = model.NumberofDays,
                        RewardedDate = DateTime.Now
                    };

                    var newRecord = ctx.EmployeeRewardedLeaveDetails.Add(rewardedLeaveEntity);
                    var result = ctx.SaveChanges();
                    if (result == 1)
                    {
                        var leaveMasterRecord = ctx.EmployeeLeaveMasters.Where(x => x.RefEmployeeId == model.EmplooyeeId).FirstOrDefault();
                        if (null != leaveMasterRecord)
                        {
                            leaveMasterRecord.RewardedLeaveCount += model.NumberofDays;
                            var resultLeaveMaster = ctx.SaveChanges();
                            if (resultLeaveMaster == 1)
                                leaveRewarded = true;
                        }
                        else
                        {
                            var employeeLeavemaster = new EmployeeLeaveMaster()
                            {
                                RefEmployeeId = model.EmplooyeeId,
                                RewardedLeaveCount = model.NumberofDays,
                                ModifiedDate = DateTime.Now,
                                ModifiedBy = model.ManagerId
                            };
                            var newLeavemasterRecord = ctx.EmployeeLeaveMasters.Add(employeeLeavemaster);
                            var resultLeavemasterRecord = ctx.SaveChanges();
                            if (resultLeavemasterRecord == 1)
                                leaveRewarded = true;
                            else
                                leaveRewarded = false;
                        }

                        //if (leaveRewarded)
                        //{
                        //    var leaveTransactionEntity = new EmployeeLeaveTransaction()
                        //    {
                        //        RefEmployeeId = model.EmplooyeeId,
                        //        CreatedDate = DateTime.Now,
                        //        NumberOfWorkingDays = model.NumberofDays,
                        //        RefLeaveType = (int)TransactionType.Credit,
                        //        CreatedBy = model.ManagerName
                        //    };

                        //    var leaveTransactionRecord = ctx.EmployeeLeaveTransactions.Add(leaveTransactionEntity);
                        //    var leaveTransactionResult = ctx.SaveChanges();
                        //    if (leaveTransactionResult == 1)
                        //        leaveRewarded = true;
                            
                        //}
                    }

                Logger.Info("Successfully exiting from AddLeaveRepository API Rewardleave method");
                    return leaveRewarded;
                }
            }
            catch(Exception ex)
            {
                Logger.Info("Exception occured at AddLeaveRepository API Rewardleave method ");
                throw;
            }
        }
    }
}


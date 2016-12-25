using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class ApproveLeaveRepository : IApproveLeaveRepository
    {

        public List<EmployeeDetailsModel> GetAllManagers(int id , int st)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())

                {


                    var ManagersDetails = ctx.EmployeeDetails.Where(m => ((m.RefRoleId == 1) || (m.RefRoleId == 2))&&(m.Id != id) ).ToList();
                    var retResult = ToModelMasterDetails(ManagersDetails);

                    if (retResult != null)
                    {
                        return retResult;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<ApproveLeaveModel> GetApproveLeave(int id)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())

                {


                    var ApproveLeaves = ctx.Workflows.Where(m => (m.RefApproverId == id)).ToList();
                    var retResult = ToModel(ApproveLeaves);

                    if (retResult != null)
                    {
                        return retResult;
                    }
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool ApproveEmployeeLeave(int id, string comments, int st , int apid)
        {

            var result = false;

            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var leaveDetails = ctx.Workflows.FirstOrDefault(x => x.EmployeeLeaveTransaction.Id == id);
                    if (st == 1)
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 12;
                        leaveDetails.ManagerComments = comments;
                        ctx.SaveChanges();
                        insertintoLeaveHistory(leaveDetails);
                        deletefromworkflow(id);
                    }
                    if (st == 0)
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 11;
                        leaveDetails.ManagerComments = comments;
                        ctx.SaveChanges();
                        insertintoLeaveHistory(leaveDetails);
                        deletefromworkflow(id);
                    }
                    if (st == 2)
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 20;
                        leaveDetails.ManagerComments = comments;
                        ctx.SaveChanges();
                        insertintoLeaveHistory(leaveDetails);
                        deletefromworkflow(id);
                    }
                    if (st == 3)
                    {
                        leaveDetails.EmployeeLeaveTransaction.RefStatus = 21;
                        leaveDetails.RefApproverId = apid;
                        leaveDetails.ManagerComments = comments;
                        ctx.SaveChanges();
                       // insertintoLeaveHistory(leaveDetails);
                       // deletefromworkflow(id);
                    }
                    

                }
                result = true;

            }
            catch (Exception ex)
            {
                throw;
            }
            return result;

        }

        public void insertintoLeaveHistory(Workflow leaveDetails)
        {
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    EmployeeLeaveTransactionHistory elth = new EmployeeLeaveTransactionHistory();
                    var m = elth;
                    m.Id = leaveDetails.EmployeeLeaveTransaction.Id;
                    m.RefEmployeeId = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.Id;
                    m.FromDate = Convert.ToDateTime(leaveDetails.EmployeeLeaveTransaction.FromDate);
                    m.ToDate = Convert.ToDateTime(leaveDetails.EmployeeLeaveTransaction.ToDate);
                    m.CreatedDate = Convert.ToDateTime(leaveDetails.EmployeeLeaveTransaction.CreatedDate);
                    m.RefStatus = leaveDetails.EmployeeLeaveTransaction.RefStatus;
                    m.NumberOfWorkingDays = leaveDetails.EmployeeLeaveTransaction.NumberOfWorkingDays;
                    m.RefLeaveType = leaveDetails.EmployeeLeaveTransaction.RefLeaveType;
                    m.EmployeeComment = leaveDetails.EmployeeLeaveTransaction.EmployeeComment;
                    m.ManagerComment = leaveDetails.ManagerComments;
                    m.ModifiedBy = leaveDetails.EmployeeLeaveTransaction.EmployeeDetail.ManagerId.ToString();
                    ctx.EmployeeLeaveTransactionHistories.Add(m);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void deletefromworkflow(int id)
        {
            try
            {

                using (var ctx = new LeaveManagementSystemEntities1())
                {
                    var leaveDetails = ctx.Workflows.FirstOrDefault(x => x.EmployeeLeaveTransaction.Id == id);
                    ctx.Workflows.Remove(leaveDetails);
                    ctx.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private ApproveLeaveModel ToModelSingle(EmployeeLeaveTransaction employeeLeaveTransaction)
        {
            ApproveLeaveModel Empres = new ApproveLeaveModel();
            try
            {

                var m = employeeLeaveTransaction;


                Empres.Id = m.Id;
                Empres.EmployeeName = m.EmployeeDetail.FirstName + " " + m.EmployeeDetail.LastName;
                Empres.RefEmployeeId = m.RefEmployeeId;
                Empres.FromDate = m.FromDate;
                Empres.ToDate = m.ToDate;
                Empres.CreatedDate = m.CreatedDate;
                Empres.RefStatus = m.RefStatus;
                Empres.NumberOfWorkingDays = m.NumberOfWorkingDays;
                Empres.RefLeaveType = m.RefLeaveType;
                Empres.EmployeeComment = m.EmployeeComment;
                Empres.LeaveTypeName = m.MasterDataValue.Value;
                Empres.StatusName = m.MasterDataValue1.Value;
                //newTrans.ManagerComments = m.ManagerComments;
                Empres.ModifiedDate = m.ModifiedDate;




            }
            catch (Exception)
            {

                throw;
            }
            return Empres;



        }

        private List<EmployeeDetailsModel> ToModelMasterDetails(List<EmployeeDetail> EmployeeDetails)
        {
            List<EmployeeDetailsModel> Empres = new List<EmployeeDetailsModel>();
            try
            {


                foreach (var m in EmployeeDetails)
                {

                    var newTrans = new EmployeeDetailsModel();
                    newTrans.Id = m.Id;
                    newTrans.FirstName = m.FirstName;
                    newTrans.LastName = m.LastName;
                    Empres.Add(newTrans);
                }



            }
            catch (Exception)
            {

                throw;
            }
            return Empres;



        }
        private List<ApproveLeaveModel> ToModel(List<Workflow> LeaveWorkflow)
        {
            List<ApproveLeaveModel> Empres = new List<ApproveLeaveModel>();
            try
            {

                foreach (var m in LeaveWorkflow)
                {
                    var newTrans = new ApproveLeaveModel();
                    newTrans.Id = m.EmployeeLeaveTransaction.Id;
                    newTrans.EmployeeName = m.EmployeeLeaveTransaction.EmployeeDetail.FirstName + " " + m.EmployeeLeaveTransaction.EmployeeDetail.LastName;
                    newTrans.RefEmployeeId = m.EmployeeLeaveTransaction.EmployeeDetail.Id;
                    newTrans.FromDate = m.EmployeeLeaveTransaction.FromDate;
                    newTrans.ToDate = m.EmployeeLeaveTransaction.ToDate;
                    newTrans.CreatedDate = m.EmployeeLeaveTransaction.CreatedDate;
                    newTrans.RefStatus = m.EmployeeLeaveTransaction.RefStatus;
                    newTrans.NumberOfWorkingDays = m.EmployeeLeaveTransaction.NumberOfWorkingDays;
                    newTrans.RefLeaveType = m.EmployeeLeaveTransaction.RefLeaveType;
                    newTrans.EmployeeComment = m.EmployeeLeaveTransaction.EmployeeComment;
                    newTrans.LeaveTypeName = m.EmployeeLeaveTransaction.MasterDataValue.Value;
                    newTrans.StatusName = m.EmployeeLeaveTransaction.MasterDataValue1.Value;
                    //newTrans.ManagerComments = m.ManagerComments;
                    newTrans.ModifiedDate = m.ModifiedDate;
                    Empres.Add(newTrans);


                }
            }
            catch (Exception)
            {

                throw;
            }
            return Empres;



        }
    }
}

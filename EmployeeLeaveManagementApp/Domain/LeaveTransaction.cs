using System;
using System.Collections.Generic;

namespace LMS_WebAPP_Domain
{
    public class LeaveTransaction
    {
        public long Id { get; set; }
        public int RefEmployeeId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int RefStatus { get; set; }
        public double NumberOfWorkingDays { get; set; }
        public int RefLeaveType { get; set; }
        public string EmployeeComment { get; set; }
        public string ManagerComments { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public List<string> LeaveType { get; set; }
        public string LeaveTypeName { get; set; }
        public string StatusName { get; set; }
        public int RefTransactionType { get; set; }
        public string FormattedDate
        {
            get
            {
                if (FromDate != null)
                    return Convert.ToDateTime(FromDate).ToString("dd MMM yyyy");
                else
                {
                    return "";
                }
            }
        }
        public string FormattedCreated
        {
            get
            {
                if (FromDate != null)
                    return Convert.ToDateTime(CreatedDate).ToString("dd MMM yyyy");
                else
                {
                    return "";
                }
            }
        }

        public string FormattedToDate
        {
            get
            {
                if (FromDate != null)
                    return Convert.ToDateTime(ToDate).ToString("dd MMM yyyy");
                else
                {
                    return "";
                }
            }
        }


        //public string LeaveTypeName
        //{
        //    get
        //    {
        //        if (RefStatus != 0)
        //            return (CommonMethods.Description((LeaveType)RefLeaveType));
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}


        //public string StatusName
        //{
        //    get
        //    {
        //        if (RefStatus != 0)
        //            return (CommonMethods.Description((LeaveStatus)RefStatus));
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}
    }
}

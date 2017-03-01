using ImportEmployee_LMS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace ImportEmployee_LMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public bool UploadEmployeeData(EmployeeUploadModel model)
        {
            try
            {
                bool result = false;

                var employeeFile = model.EmployeeFile;

                if (employeeFile != null)
                {
                    string serverMapFolderPath = Server.MapPath("~\\Documents");
                    string serverMappedFilePath = serverMapFolderPath + employeeFile.FileName;
                    employeeFile.SaveAs(serverMappedFilePath);
                    int hrManagerId = 0;
                    using (var ctx = new LeaveManagementSystemEntities())
                    {
                        hrManagerId = ctx.EmployeeDetails.Where(x => x.RefRoleId == (Int32)EmployeeRole.HR).OrderBy(x => x.RefHierarchyLevel).FirstOrDefault().Id;
                    }
                    if (hrManagerId > 0)
                    {
                        var employeeList = GetEmployeeList(Path.GetExtension(employeeFile.FileName), serverMappedFilePath, hrManagerId);
                        using (var ctx = new LeaveManagementSystemEntities())
                        {
                            ctx.EmployeeDetails.AddRange(employeeList);
                            ctx.SaveChanges();
                        }
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetDataListFromExcel(string fileExtension, string fileLocation)
        {
            string conStr = "";
            switch (fileExtension)
            {
                case ".xls": //Excel 97-03
                    conStr = WebConfigurationManager.AppSettings.Get("Excel97ConnectionString");
                    break;
                case ".xlsx": //Excel 07
                    conStr = WebConfigurationManager.AppSettings.Get("Excel07ConnectionString");
                    break;
            }
            conStr = string.Format(conStr, fileLocation);
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dataList = new DataTable();
            cmdExcel.Connection = connExcel;
            try
            {
                //Get the name of First Sheet
                connExcel.Open();
                DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetNameWithDollarSuffix = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString().Trim().TrimStart('\'').TrimEnd('\'');
                connExcel.Close();
                connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + sheetNameWithDollarSuffix + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dataList);
                connExcel.Close();
                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connExcel.State != ConnectionState.Closed || null == connExcel)
                {
                    connExcel.Dispose();
                    connExcel.Close();
                }
            }
        }

        private List<Models.EmployeeDetail> GetEmployeeList(string fileExtension, string fileLocation, int hrManagerId)
        {
            var employeeList = GetDataListFromExcel(fileExtension, fileLocation);

            List<Models.EmployeeDetail> employeeDataList = new List<Models.EmployeeDetail>();

            foreach (DataRow employee in employeeList.Rows)
            {
                var userDetails = new UserAccount
                {
                    UserName = !string.IsNullOrWhiteSpace(employee.ItemArray[2].ToString()) ? employee.ItemArray[2].ToString().Trim() : string.Empty,
                    Password = "Temp@123",
                    CreatedDate = DateTime.Now
                };
                var userAccounts = new List<UserAccount>();
                userAccounts.Add(userDetails);

                var employeeMaster = new EmployeeLeaveMaster
                {
                    RewardedLeaveCount = !string.IsNullOrWhiteSpace(employee.ItemArray[12].ToString()) ? Convert.ToInt32(employee.ItemArray[12].ToString().Trim()) : 0,
                    SpentAdvanceLeave = !string.IsNullOrWhiteSpace(employee.ItemArray[13].ToString()) ? Convert.ToInt32(employee.ItemArray[13].ToString().Trim()) : 0,
                    TakenLossOfPay = !string.IsNullOrWhiteSpace(employee.ItemArray[14].ToString()) ? Convert.ToInt32(employee.ItemArray[14].ToString().Trim()) : 0,
                    EarnedCasualLeave = !string.IsNullOrWhiteSpace(employee.ItemArray[15].ToString()) ? Convert.ToInt32(employee.ItemArray[15].ToString().Trim()) : 0,
                    TakenCompOff = !string.IsNullOrWhiteSpace(employee.ItemArray[16].ToString()) ? Convert.ToInt32(employee.ItemArray[16].ToString().Trim()) : 0
                };
                var employeeLeaveMasters = new List<EmployeeLeaveMaster>();
                employeeLeaveMasters.Add(employeeMaster);

                var roleTypeList = EnumDropDownList(typeof(EmployeeRole));

                var roleId = !string.IsNullOrEmpty(employee.ItemArray[4].ToString()) ? roleTypeList.FirstOrDefault(x => x.Text.ToLower().Trim().Equals(employee.ItemArray[4].ToString().ToLower().Trim())) != null ? roleTypeList.FirstOrDefault(x => x.Text.ToLower().Trim().Equals(employee.ItemArray[4].ToString().ToLower().Trim())).Value : (Int32)EmployeeRole.InternOrFresher : (Int32)EmployeeRole.InternOrFresher;

                var hierarchyLevel = (Int32)HierarchyLevel.Level5;
                if (roleId == (Int32)EmployeeRole.CEO)
                {
                    hierarchyLevel = (Int32)HierarchyLevel.Level0;
                }
                else if (roleId == (Int32)EmployeeRole.COO || roleId == (Int32)EmployeeRole.CTO || roleId == (Int32)EmployeeRole.SeniorHR || roleId == (Int32)EmployeeRole.HR)
                {
                    hierarchyLevel = (Int32)HierarchyLevel.Level1;
                }
                else if (roleId == (Int32)EmployeeRole.TeamLead || roleId == (Int32)EmployeeRole.TechLead || roleId == (Int32)EmployeeRole.TestLead || roleId == (Int32)EmployeeRole.TechnicalArchitect || roleId == (Int32)EmployeeRole.Manager || roleId == (Int32)EmployeeRole.ProjectManager || roleId == (Int32)EmployeeRole.DevLead)
                {
                    hierarchyLevel = (Int32)HierarchyLevel.Level2;
                }
                else if (roleId == (Int32)EmployeeRole.SeniorTestEngineer || roleId == (Int32)EmployeeRole.SeniorUIDesigner || roleId == (Int32)EmployeeRole.SSE)
                {
                    hierarchyLevel = (Int32)HierarchyLevel.Level3;
                }
                else if (roleId == (Int32)EmployeeRole.TestEngineer || roleId == (Int32)EmployeeRole.UIDesigner || roleId == (Int32)EmployeeRole.SoftwareEngineer || roleId == (Int32)EmployeeRole.QA || roleId == (Int32)EmployeeRole.Finance || roleId == (Int32)EmployeeRole.AssociateTechArchitect || roleId == (Int32)EmployeeRole.Sales)
                {
                    hierarchyLevel = (Int32)HierarchyLevel.Level4;
                }

                var employeeDetails = new EmployeeDetail
                {
                    FirstName = !string.IsNullOrWhiteSpace(employee.ItemArray[0].ToString()) ? employee.ItemArray[0].ToString().Trim() : string.Empty,
                    LastName = !string.IsNullOrWhiteSpace(employee.ItemArray[1].ToString()) ? employee.ItemArray[1].ToString().Trim() : string.Empty,
                    DateOfBirth = Convert.ToDateTime(employee.ItemArray[11].ToString().Trim()).Date,
                    RefRoleId = roleId,
                    City = !string.IsNullOrWhiteSpace(employee.ItemArray[8].ToString()) ? employee.ItemArray[8].ToString().Trim() : string.Empty,
                    Country = !string.IsNullOrWhiteSpace(employee.ItemArray[9].ToString()) ? employee.ItemArray[9].ToString().Trim() : string.Empty,
                    CreatedDate = DateTime.Now,
                    EmpNumber = !string.IsNullOrWhiteSpace(employee.ItemArray[3].ToString()) ? employee.ItemArray[3].ToString().Trim() : string.Empty,
                    PhoneNumber = !string.IsNullOrWhiteSpace(employee.ItemArray[10].ToString()) ? employee.ItemArray[10].ToString().Trim() : string.Empty,
                    RefHierarchyLevel = hierarchyLevel,
                    ManagerId = hrManagerId,
                    DateOfJoining = !string.IsNullOrWhiteSpace(employee.ItemArray[5].ToString()) ? Convert.ToDateTime(employee.ItemArray[5].ToString().Trim()).Date : DateTime.Now.Date,
                    ImagePath = string.Empty,
                    RefEmployeeType = !string.IsNullOrWhiteSpace(employee.ItemArray[6].ToString()) ? (Int32)((EmployeeType)Enum.Parse(typeof(EmployeeType), employee.ItemArray[6].ToString().Trim(), true)) : (Int32)EmployeeType.Fresher,
                    RefProfileType = !string.IsNullOrWhiteSpace(employee.ItemArray[7].ToString()) ? (Int32)((ProfileType)Enum.Parse(typeof(ProfileType), employee.ItemArray[7].ToString().Trim(), true)) : (Int32)ProfileType.Employee,
                    UserAccounts = userAccounts,
                    EmployeeLeaveMasters = employeeLeaveMasters
                };

                employeeDataList.Add(employeeDetails);
            }
            return employeeDataList;
        }

        public List<ItemList> EnumDropDownList(Type enumType)
        {

            var list = new List<ItemList>();
            var values = Enum.GetValues(enumType);
            foreach (var item in values)
            {
                var listItem = new ItemList();
                var field = enumType.GetField(item.ToString());
                var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                listItem.Text = attributes.Length == 0
                ? item.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
                listItem.Value = (int)Enum.Parse(enumType, item.ToString());
                list.Add(listItem);
            }
            return list;
        }
    }

    public class ItemList
    {
        public string Text { get; set; }
        public int Value { get; set; }
    }

    public enum EmployeeType
    {
        [Description("Fresher")]
        Fresher = 151,
        [Description("Experienced")]
        Experienced = 152
    }

    public enum HierarchyLevel
    {
        [Description("Level-0")]
        Level0 = 81,
        [Description("Level-1")]
        Level1 = 82,
        [Description("Level-2")]
        Level2 = 83,
        [Description("Level-3")]
        Level3 = 84,
        [Description("Level-4")]
        Level4 = 85,
        [Description("Level-5")]
        Level5 = 86
    }

    public enum ProfileType
    {
        [Description("CEO")]
        CEO = 200,
        [Description("Admin/HR")]
        HR = 201,
        [Description("Manager")]
        Manager = 202,
        [Description("Employee")]
        Employee = 203
    }

    public enum EmployeeRole
    {

        [Description("Admin/HR")]
        HR = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("Senior Software Engineer")]
        SSE = 3,
        [Description("CEO")]
        CEO = 4,
        [Description("Team Lead")]
        TeamLead = 5,
        [Description("Software Engineer")]
        SoftwareEngineer = 6,
        [Description("Quality Analyst")]
        QA = 7,
        [Description("Dev Lead")]
        DevLead = 8,
        [Description("Test Engineer")]
        TestEngineer = 9,
        [Description("Senior Test Engineer")]
        SeniorTestEngineer = 10,
        [Description("Test Lead")]
        TestLead = 11,
        [Description("Tech Lead")]
        TechLead = 12,
        [Description("Technical Architect")]
        TechnicalArchitect = 13,
        [Description("Associate Tech Architect")]
        AssociateTechArchitect = 14,
        [Description("Project Manager")]
        ProjectManager = 15,
        [Description("Senior HR")]
        SeniorHR = 16,
        [Description("CTO")]
        CTO = 17,
        [Description("COO")]
        COO = 18,
        [Description("Finance")]
        Finance = 19,
        [Description("UI Designer")]
        UIDesigner = 20,
        [Description("Senior UI Designer")]
        SeniorUIDesigner = 21,
        [Description("Sales")]
        Sales = 22,
        [Description("Intern/Fresher")]
        InternOrFresher = 23
    }
}
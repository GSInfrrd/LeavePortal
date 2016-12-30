using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_WebAPI_DAL.Repositories
{
    public class HRRepository : IHRRepository
    {
        public bool SubmitEmployeeDetails(EmployeeDetailsModel model)
        {
            var result = false;

            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {

                    var employeeDetails = new EmployeeDetail
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        DateOfBirth = model.DateOfBirth,
                        RefRoleId = model.RefRoleId,
                        City = model.City,
                        Country = model.Country,
                        CreatedDate = DateTime.Now,
                        EmpNumber = model.EmployeeNumber.ToString(),
                        PhoneNumber=model.Telephone,
                        RefHierarchyLevel=model.RefHierarchyLevel,
                        ManagerId=ctx.EmployeeDetails.FirstOrDefault(x=>x.FirstName== model.ManagerName).Id,
                        DateOfJoining=model.DateOfJoining,
                        ImagePath=model.ImagePath
                        

                    };
                    ctx.EmployeeDetails.Add(employeeDetails);
                    ctx.SaveChanges();
                    var id = employeeDetails.Id;

                    var employeeEducationDetails = new EmployeeEducationDetail
                    {

                        Degree = model.EmployeeEducationDetails[0].Degree,
                        Institution = model.EmployeeEducationDetails[0].Institution,
                        FromDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[0]),
                        ToDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[1]),
                        RefEmployeeId = id
                    };
                    ctx.EmployeeEducationDetails.Add(employeeEducationDetails);
                    ctx.SaveChanges();
                    var employeeExperienceDetails = new EmployeeExperienceDetail
                    {
                        CompanyName = model.EmployeeExperienceDetails[0].Company,
                        Role = model.EmployeeExperienceDetails[0].Role,
                        FromDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[0]),
                        ToDate = Convert.ToDateTime(model.EmployeeEducationDetails[0].TimePeriod.Split('~')[1]),
                        RefEmployeeId = id
                    };
                    ctx.EmployeeExperienceDetails.Add(employeeExperienceDetails);
                    ctx.SaveChanges();
                    //List<EmployeeSkill> empSkillList = new List<EmployeeSkill>();
                    foreach (var skill in model.Skills)
                    {
                        var empSkill = new EmployeeSkill();
                        empSkill.RefEmployeeId = id;
                        empSkill.Skill = skill;
                        ctx.EmployeeSkills.Add(empSkill);
                        ctx.SaveChanges();
                    }
                    var userDetails = new UserAccount
                    {
                        UserName = model.Email,
                        Password = "Temp@123",
                        RefEmployeeId = id,
                        CreatedDate = DateTime.Now
                    };

                    ctx.UserAccounts.Add(userDetails);
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

        public List<EmployeeDetailsModel> GetEmployeeList()
        {
            var list = new List<EmployeeDetailsModel>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                  var empList   = ctx.EmployeeDetails.ToList();
                    foreach(var item in empList)
                    {
                        var listItem = new EmployeeDetailsModel();
                        listItem.Id = item.Id;
                        listItem.FirstName = item.FirstName;
                        listItem.LastName = item.LastName;
                        listItem.ManagerName =item.ManagerId!=null?ctx.EmployeeDetails.FirstOrDefault(i => i.Id == item.ManagerId).FirstName:string.Empty;
                        listItem.DateOfJoining =Convert.ToDateTime(item.DateOfJoining);
                        listItem.EmployeeNumber =Convert.ToInt32(item.EmpNumber);
                        listItem.RoleName= item.RefRoleId != 0 ? ctx.MasterDataValues.FirstOrDefault(i => i.Id == item.RefRoleId).Value : string.Empty;

                        list.Add(listItem);
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EmployeeDetailsModel> GetManagerList(int refLevel)
        {
            var list = new List<EmployeeDetailsModel>();
            try
            {
                using (var ctx = new LeaveManagementSystemEntities1())
                {
                   var empList = ctx.EmployeeDetails.Where(i=>i.RefHierarchyLevel<refLevel).ToList();
                    foreach (var item in empList)
                    {
                        var listItem = new EmployeeDetailsModel();
                        listItem.Id = item.Id;
                        listItem.FirstName = item.FirstName;
                        listItem.LastName = item.LastName;                    
                        list.Add(listItem);
                    }

                }
                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}

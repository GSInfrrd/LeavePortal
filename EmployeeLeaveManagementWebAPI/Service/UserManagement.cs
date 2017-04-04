using System;
using System.Collections.Generic;
using System.Linq;
using LMS_WebAPI_DAL.Repositories;
using LMS_WebAPI_DAL.Repositories.Interfaces;
using LMS_WebAPI_Domain;
using LMS_WebAPI_Utils;
using LMS_WebAPI_DAL;

namespace LMS_WebAPI_ServiceHelpers
{
    public class UserManagement
    {
        private IUser user = new UserRepository();
        public UserAccountModel GetUser(string UserName, string password)
        {
            Logger.Info("Entering into UserManagement Service helper GetUser method ");
            try
            {
                var VerifiedUser = new UserAccountModel();
                var userData = user.GetUser(UserName, password);
                if (null != userData)
                {
                    VerifiedUser.Id = userData.Id;
                    VerifiedUser.UserName = userData.EmployeeDetail.FirstName+" "+userData.EmployeeDetail.LastName;
                    VerifiedUser.Password = userData.Password;
                    VerifiedUser.Lastlogin = userData.Lastlogin;
                    VerifiedUser.RefEmployeeId = userData.RefEmployeeId;
                    VerifiedUser.CreatedDate = userData.CreatedDate;
                    VerifiedUser.RefRoleId = userData.EmployeeDetail.RefRoleId;
                    VerifiedUser.RefProfileType = userData.EmployeeDetail.RefProfileType;
                    VerifiedUser.Imagepath =!string.IsNullOrEmpty(userData.EmployeeDetail.ImagePath)? string.Format("data:image/png;base64,{0}", userData.EmployeeDetail.ImagePath):string.Empty;
                    VerifiedUser.DateOfJoining =Convert.ToDateTime(userData.EmployeeDetail.DateOfJoining);

                }
                Logger.Info("Exiting from into UserManagement Service helper GetUser method ");
                return VerifiedUser;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper GetUser method ");
                throw;
            }
        }

        public EmployeeDetailsModel GetEmployeeDatailsForDashboard(int EmpId,int year,int leaveType=0)
        {
            Logger.Info("Entering into UserManagement Service helper GetEmployeeDatailsForDashboard method ");
            try
            {
                var userData = user.GetUserDetails(EmpId);
                var announcements = user.GetAnnouncements();
                var leaveReportDetails = user.GetLeaveReportDetails(year,EmpId,leaveType);
                var VerifiedUser = new EmployeeDetailsModel();
                userData.leaveDetails = leaveReportDetails;
                foreach (var item in announcements)
                {
                    LMS_WebAPI_Domain.Announcement announceItem = new LMS_WebAPI_Domain.Announcement();
                    announceItem.ImagePath = item.ImagePath;
                    announceItem.CarouselContent = item.CarouselContent;
                    announceItem.Title = item.Title;
                    announceItem.Id = item.Id;
                    userData.Announcements.Add(announceItem);
                }
                Logger.Info("Exiting from into UserManagement Service helper GetEmployeeDatailsForDashboard method ");
                return userData;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper GetEmployeeDatailsForDashboard method ");
                throw;
            }
        }

        public List<EmployeeDetailsModel> GetTeamMembersForDashboard(int EmpId)
        {
            try
            {
                List<EmployeeDetailsModel> resList = new List<EmployeeDetailsModel>();
                var returnMembers = user.GetTeamMembers(EmpId);
                resList = ToEmployeeModel(returnMembers);
                return resList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<EmployeeDetailsModel> ToEmployeeModel(List<EmployeeDetail> returnMembers)
        {
            try
            {
                List<EmployeeDetailsModel> resList = new List<EmployeeDetailsModel>();
                if (returnMembers != null && returnMembers.Count > 0)
                {
                    foreach (var m in returnMembers)
                    {
                        var res = new EmployeeDetailsModel();
                        res.Bio = m.Bio;
                        res.City = m.City;
                        res.Country = m.Country;
                        res.DateOfBirth = m.DateOfBirth;
                        res.DateOfJoining = m.DateOfJoining.Value;
                        res.FirstName = m.FirstName;
                        res.Id = m.Id;
                        res.RefRoleId = m.RefRoleId;
                        res.LastName = m.LastName;
                        res.RoleName = CommonMethods.Description((EmployeeRole)m.RefRoleId);
                        resList.Add(res);
                    }
                    resList = resList.OrderBy(n => n.RefRoleId).ToList();
                }
                return resList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public EmployeeDetailsModel GetUserProfileDetails(int EmpId)
        {
            Logger.Info("Entering into UserManagement Service helper GetUserProfileDetails method ");
            try
            {
                var skills = new List<MasterDataModel>();
                var projects = new List<ProjectsList>();
                var userData = user.GetUserProfileDetails(EmpId, out skills,out projects);
                var profileDetails = new EmployeeDetailsModel();
                List<EmployeeEducationDetails> EEdetails = new List<EmployeeEducationDetails>();
                profileDetails.FirstName = userData.FirstName;
                profileDetails.LastName = userData.LastName;
                profileDetails.City = userData.City;
                profileDetails.Country = userData.Country;
                profileDetails.Telephone = userData.PhoneNumber;
                profileDetails.RefRoleId = userData.RefRoleId;
                profileDetails.RoleName = userData.MasterDataValue2.Value;
                profileDetails.DateOfBirth = userData.DateOfBirth;
                profileDetails.DateOfBirthAsString = userData.DateOfBirth.ToString("MMM dd");
                profileDetails.Email = userData.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == EmpId).UserName;
                profileDetails.ImagePath = !string.IsNullOrEmpty(userData.ImagePath)?  string.Format("data:image/png;base64,{0}", userData.ImagePath):string.Empty;
                profileDetails.Bio = userData.Bio;
                profileDetails.Id = userData.Id;
                profileDetails.FacebookLink = userData.FacebookLink;
                profileDetails.TwitterLink = userData.TwitterLink;
                profileDetails.GooglePlusLink = userData.GooglePlusLink;
                profileDetails.EmployeeNumber = userData.EmpNumber;
                profileDetails.InfrrdEmailId = userData.UserAccounts.FirstOrDefault(i => i.RefEmployeeId == EmpId).UserName;
                profileDetails.DateOfJoining = Convert.ToDateTime(userData.DateOfJoining).Date;
                profileDetails.RoleName = userData.RoleName;
                profileDetails.EmployeeTypeName = userData.EmployeeType;
                profileDetails.EmployeeConractTypeName = userData.EmployeeContractType;
                profileDetails.DateOfConfirmation = Convert.ToDateTime(userData.DateOfConfirmation).Date;
                profileDetails.ManagerName = userData.ManagerName;
                profileDetails.ProjectName = userData.ProjectName;
                profileDetails.ProfileTypeName = userData.ProfileType;
                foreach (var item in userData.EmployeeEducationDetails)
                {
                    var edet = new EmployeeEducationDetails();
                    edet.Degree = item.Degree;
                    edet.Institution = item.Institution;
                    edet.FromDate = item.FromDate;
                    edet.ToDate = item.ToDate;
                    edet.Id = item.Id;
                    edet.TimePeriod = item.FromDate.ToString("MMMM yyyy") + "~" + item.ToDate.ToString("MMMM yyyy");
                    EEdetails.Add(edet);
                }
                profileDetails.EmployeeEducationDetails = EEdetails;
                List<EmployeeExperienceDetails> EExpdetails = new List<EmployeeExperienceDetails>();
                foreach (var item in userData.EmployeeExperienceDetails)
                {
                    var exdet = new EmployeeExperienceDetails();
                    exdet.Id = item.Id;
                    exdet.Company = item.CompanyName;
                    exdet.Role = item.Role;
                    exdet.FromDate = item.FromDate;
                    exdet.ToDate = item.ToDate;
                    exdet.TimePeriod = item.FromDate.ToString("MMMM yyyy") + "~" + item.ToDate.ToString("MMMM yyyy");
                    exdet.CompanyLogo = item.CompanyLogo;
                    EExpdetails.Add(exdet);
                }
                profileDetails.EmployeeExperienceDetails = EExpdetails;
                List<EmployeeSkillDetails> employeeSkills = new List<EmployeeSkillDetails>();

                foreach (var skill in skills)
                {
                    var employeeSkill = new EmployeeSkillDetails();
                    employeeSkill.IsSelected = userData.EmployeeSkills.FirstOrDefault(i => i.Skill.Trim() == skill.Value.Trim()) != null ? true : false;
                    employeeSkill.SkillName = skill.Value;
                    employeeSkill.RefEmployeeId = userData.EmployeeSkills.FirstOrDefault(i => i.Skill.Trim() == skill.Value.Trim()) != null ? userData.Id : 0;
                    employeeSkill.Id = userData.EmployeeSkills.FirstOrDefault(i => i.Skill.Trim() == skill.Value.Trim()) != null ? userData.EmployeeSkills.FirstOrDefault(i => i.Skill.Trim() == skill.Value.Trim()).Id : 0;
                    employeeSkills.Add(employeeSkill);

                }
                List<LMS_WebAPI_Domain.EmployeeCurrentAddressDetail> ECADetails = new List<LMS_WebAPI_Domain.EmployeeCurrentAddressDetail>();
                var employeeCurrentAddressDetials = new LMS_WebAPI_Domain.EmployeeCurrentAddressDetail();
                employeeCurrentAddressDetials.Country = userData.EmployeeCurrentAddressDetails.FirstOrDefault().Country;
                employeeCurrentAddressDetials.State = userData.EmployeeCurrentAddressDetails.FirstOrDefault().State;
                employeeCurrentAddressDetials.City = userData.EmployeeCurrentAddressDetails.FirstOrDefault().City;
                employeeCurrentAddressDetials.Pincode = userData.EmployeeCurrentAddressDetails.FirstOrDefault().Pincode;
                employeeCurrentAddressDetials.AddressLine1 = userData.EmployeeCurrentAddressDetails.FirstOrDefault().AddressLine1;
                employeeCurrentAddressDetials.AddressLine2 = userData.EmployeeCurrentAddressDetails.FirstOrDefault().AddressLine2;
                ECADetails.Add(employeeCurrentAddressDetials);
                profileDetails.EmployeeCurrentAddressDetail = ECADetails;

                List<LMS_WebAPI_Domain.EmployeePermanentAddressDetail> EPADetails = new List<LMS_WebAPI_Domain.EmployeePermanentAddressDetail>();
                var employeePermanentAddressDetials = new LMS_WebAPI_Domain.EmployeePermanentAddressDetail();
                employeePermanentAddressDetials.Country = userData.EmployeePermanentAddressDetails.FirstOrDefault().Country;
                employeePermanentAddressDetials.State = userData.EmployeePermanentAddressDetails.FirstOrDefault().State;
                employeePermanentAddressDetials.City = userData.EmployeePermanentAddressDetails.FirstOrDefault().City;
                employeePermanentAddressDetials.Pincode = userData.EmployeePermanentAddressDetails.FirstOrDefault().Pincode;
                employeePermanentAddressDetials.AddressLine1 = userData.EmployeePermanentAddressDetails.FirstOrDefault().AddressLine1;
                employeePermanentAddressDetials.AddressLine2 = userData.EmployeePermanentAddressDetails.FirstOrDefault().AddressLine2;
                EPADetails.Add(employeePermanentAddressDetials);
                profileDetails.EmployeePermanentAddressDetail = EPADetails;

                List<LMS_WebAPI_Domain.EmployeeWorkLocationDetail> EWLDetails = new List<LMS_WebAPI_Domain.EmployeeWorkLocationDetail>();
                var employeeWorkLocationDetials = new LMS_WebAPI_Domain.EmployeeWorkLocationDetail();
                employeeWorkLocationDetials.Facility = userData.EmployeeWorkLocationDetails.FirstOrDefault().Facility;
                employeeWorkLocationDetials.Country = userData.EmployeeWorkLocationDetails.FirstOrDefault().Country;
                employeeWorkLocationDetials.State = userData.EmployeeWorkLocationDetails.FirstOrDefault().State;
                employeeWorkLocationDetials.City = userData.EmployeeWorkLocationDetails.FirstOrDefault().City;
                EWLDetails.Add(employeeWorkLocationDetials);
                profileDetails.EmployeeWorkLocationDetail = EWLDetails;

                List<LMS_WebAPI_Domain.EmployeeEmergencyContactDetail> EECDDetails = new List<LMS_WebAPI_Domain.EmployeeEmergencyContactDetail>();
                var employeeEmergencyContactDetials = new LMS_WebAPI_Domain.EmployeeEmergencyContactDetail();
                employeeEmergencyContactDetials.Name = userData.EmployeeEmergencyContactDetails.FirstOrDefault().Name;
                employeeEmergencyContactDetials.Relationship = userData.EmployeeEmergencyContactDetails.FirstOrDefault().Relationship;
                employeeEmergencyContactDetials.Telephone = userData.EmployeeEmergencyContactDetails.FirstOrDefault().Telephone;
                employeeEmergencyContactDetials.Country = userData.EmployeeEmergencyContactDetails.FirstOrDefault().Country;
                employeeEmergencyContactDetials.State = userData.EmployeeEmergencyContactDetails.FirstOrDefault().State;
                employeeEmergencyContactDetials.City = userData.EmployeeEmergencyContactDetails.FirstOrDefault().City;
                employeeEmergencyContactDetials.AddressLine1 = userData.EmployeeEmergencyContactDetails.FirstOrDefault().AddressLine1;
                employeeEmergencyContactDetials.AddressLine2 = userData.EmployeeEmergencyContactDetails.FirstOrDefault().AddressLine2;
                EECDDetails.Add(employeeEmergencyContactDetials);
                profileDetails.EmployeeEmergencyContactDetail = EECDDetails;



                profileDetails.Skills = employeeSkills;
                  profileDetails.Projects = projects;

                Logger.Info("Exiting from into UserManagement Service helper GetUserProfileDetails method ");
                return profileDetails;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper GetUserProfileDetails method ");
                throw;
            }
        }


        public bool EditEmployeeDetails(EmployeeDetailsModel model)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeeDetails method ");
            try
            {
                var result = user.EditEmployeeDetails(model);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeeDetails method ");
                return result;
            }
            catch 
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeeDetails method ");
                throw;
            }
        }

        public bool EditEmployeeEmergencyContactDetails(LMS_WebAPI_Domain.EmployeeEmergencyContactDetail model)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeeEmergencyContactDetails method ");
            try
            {
                var result = user.EditEmployeeEmergencyContactDetails(model);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeeEmergencyContactDetails method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeeEmergencyContactDetails method ");
                throw;
            }
        }

        public bool EditEmployeeCurrentAddressDetails(LMS_WebAPI_Domain.EmployeeCurrentAddressDetail model)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeeCurrentAddressDetails method ");
            try
            {
                var result = user.EditEmployeeCurrentAddressDetails(model);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeeCurrentAddressDetails method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeeCurrentAddressDetails method ");
                throw;
            }
        }

        public bool EditEmployeePermanentAddressDetails(LMS_WebAPI_Domain.EmployeePermanentAddressDetail model)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeePermanentAddressDetails method ");
            try
            {
                var result = user.EditEmployeePermanentAddressDetails(model);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeePermanentAddressDetails method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeePermanentAddressDetails method ");
                throw;
            }
        }

        public bool EditEmployeeEducationDetails(List<EmployeeEducationDetails> educationDetails, int employeeId)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeeEducationDetails method ");
            try
            {
                var result = user.EditEmployeeEducationDetails(educationDetails, employeeId);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeeEducationDetails method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeeEducationDetails method ");
                throw;
            }
        }

        public bool EditEmployeeExperienceDetails(List<EmployeeExperienceDetails> experienceDetails, int employeeId)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeeExperienceDetails method ");
            try
            {
                var result = user.EditEmployeeExperienceDetails(experienceDetails, employeeId);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeeExperienceDetails method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeeExperienceDetails method ");
                throw;
            }
        }

        public bool EditEmployeeSkills(List<EmployeeSkillDetails> skills, int employeeId)
        {
            Logger.Info("Entering into UserManagement Service helper EditEmployeeSkills method ");
            try
            {
                var result = user.EditEmployeeSkills(skills, employeeId);
                Logger.Info("Exiting from into UserManagement Service helper EditEmployeeSkills method ");
                return result;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper EditEmployeeSkills method ");
                throw;
            }
        }

        public string getUserProfileImage(int empId)
        {
            Logger.Info("Entering into UserManagement Service helper getUserProfileImage method ");
            try
            {
                var profileImageByteArray = user.getUserProfileImage(empId);
                Logger.Info("Exiting from into UserManagement Service helper getUserProfileImage method ");
                return profileImageByteArray;
            }
            catch
            {
                Logger.Info("Exception occured at UserManagement Service helper getUserProfileImage method ");
                throw;
            }
        }

        public bool CheckEmployeePassword(int employeeId, string currentPassword)
        {
            Logger.Info("Entering into UserManagement Service helper CheckEmployeePassword method ");
            try
            {
                var result = user.CheckEmployeePassword(employeeId, currentPassword);
                Logger.Info("Exiting from into UserManagement Service helper CheckEmployeePassword method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at UserManagement Service helper CheckEmployeePassword method ");
                throw;
            }
        }

        public bool UpdatePassword(int employeeId, string newPassword)
        {
            Logger.Info("Entering into UserManagement Service helper UpdatePassword method ");
            try
            {
                var result = user.UpdatePassword(employeeId, newPassword);
                Logger.Info("Exiting from into UserManagement Service helper UpdatePassword method ");
                return result;
            }
            catch
            {
                Logger.Error("Exception occured at UserManagement Service helper UpdatePassword method ");
                throw;
            }
        }


    }
}

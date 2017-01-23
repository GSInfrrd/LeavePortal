package models;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

/**
 * Created by Anuj Dutt on 12/26/2016.
 */

public class EmployeeDetailsModel {

    @SerializedName("TotalApplied")
    public int totalApplied;
    @SerializedName("TotalSpent")
    public int totalSpent;
    @SerializedName("MangerEmail")
    public String mangerEmail;
    @SerializedName("ManagerId")
    public int managerId;
    @SerializedName("RefImageId")
    public int refImageId;
    @SerializedName("leaveDetails")
    public LeaveReportModel leaveDetails;
    @SerializedName("Id")
    private int id;
    @SerializedName("FirstName")
    private String firstName;
    @SerializedName("LastName")
    private String lastName;
    @SerializedName("CreatedDate")
    private Date createdDate;
    @SerializedName("ModifiedDate")
    private Date modifiedDate;
    @SerializedName("ProjectName")
    private String projectName;
    @SerializedName("RoleName")
    private String roleName;
    @SerializedName("TotalLeaveCount")
    private int totalLeaveCount;
    @SerializedName("ManagerName")
    private String managerName;
    @SerializedName("DateOfJoining")
    private Date dateOfJoining;
    @SerializedName("Announcements")
    private List<Announcement> announcements;
    @SerializedName("EmployeeEducationDetails")
    private List<EmployeeEducationDetails> employeeEducationDetails;
    @SerializedName("EmployeeExperienceDetails")
    private List<EmployeeExperienceDetails> employeeExperienceDetails;
    @SerializedName("City")
    private String city;
    @SerializedName("Country")
    private String country;
    @SerializedName("Telephone")
    private String telephone;
    @SerializedName("DateOfBirthAsString")
    private String dateOfBirthAsString;
    @SerializedName("DateOfBirth")
    private Date dateOfBirth;
    @SerializedName("Email")
    private String email;
    @SerializedName("ImagePath")
    private String imagePath;
    @SerializedName("Bio")
    private String bio;
    @SerializedName("RefRoleId")
    private int refRoleId;
    @SerializedName("Skills")
    private List<EmployeeSkillDetails> skills;
    @SerializedName("EmployeeNumber")
    private int employeeNumber;
    @SerializedName("RefHierarchyLevel")
    private int refHierarchyLevel;
    public EmployeeDetailsModel() {
        this.announcements = new ArrayList<Announcement>();
        this.leaveDetails = new LeaveReportModel();
        this.employeeEducationDetails = new ArrayList<EmployeeEducationDetails>();
        this.employeeExperienceDetails = new ArrayList<EmployeeExperienceDetails>();
    }

    public int getTotalSpent() {
        return totalSpent;
    }

    public void setTotalSpent(int totalSpent) {
        this.totalSpent = totalSpent;
    }

    public int getManagerId() {
        return managerId;
    }

    public void setManagerId(int managerId) {
        this.managerId = managerId;
    }

    public String getMangerEmail() {
        return mangerEmail;
    }

    public void setMangerEmail(String mangerEmail) {
        this.mangerEmail = mangerEmail;
    }

    public int getRefImageId() {
        return refImageId;
    }

    public void setRefImageId(int refImageId) {
        this.refImageId = refImageId;
    }

    public int getTotalApplied() {
        return totalApplied;
    }

    public void setTotalApplied(int totalApplied) {
        this.totalApplied = totalApplied;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getTotalLeaveCount() {
        return totalLeaveCount;
    }

    public void setTotalLeaveCount(int totalLeaveCount) {
        this.totalLeaveCount = totalLeaveCount;
    }

    public List<Announcement> getAnnouncements() {
        return announcements;
    }

    public void setAnnouncements(List<Announcement> announcements) {
        this.announcements = announcements;
    }

    public String getBio() {
        return bio;
    }

    public void setBio(String bio) {
        this.bio = bio;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public Date getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(Date createdDate) {
        this.createdDate = createdDate;
    }

    public Date getDateOfBirth() {
        return dateOfBirth;
    }

    public void setDateOfBirth(Date dateOfBirth) {
        this.dateOfBirth = dateOfBirth;
    }

    public String getDateOfBirthAsString() {
        return dateOfBirthAsString;
    }

    public void setDateOfBirthAsString(String dateOfBirthAsString) {
        this.dateOfBirthAsString = dateOfBirthAsString;
    }

    public Date getDateOfJoining() {
        return dateOfJoining;
    }

    public void setDateOfJoining(Date dateOfJoining) {
        this.dateOfJoining = dateOfJoining;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public List<EmployeeEducationDetails> getEmployeeEducationDetails() {
        return employeeEducationDetails;
    }

    public void setEmployeeEducationDetails(List<EmployeeEducationDetails> employeeEducationDetails) {
        this.employeeEducationDetails = employeeEducationDetails;
    }

    public List<EmployeeExperienceDetails> getEmployeeExperienceDetails() {
        return employeeExperienceDetails;
    }

    public void setEmployeeExperienceDetails(List<EmployeeExperienceDetails> employeeExperienceDetails) {
        this.employeeExperienceDetails = employeeExperienceDetails;
    }

    public int getEmployeeNumber() {
        return employeeNumber;
    }

    public void setEmployeeNumber(int employeeNumber) {
        this.employeeNumber = employeeNumber;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getImagePath() {
        return imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public String getLastName() {
        return lastName;
    }

    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    public LeaveReportModel getLeaveDetails() {
        return leaveDetails;
    }

    public void setLeaveDetails(LeaveReportModel leaveDetails) {
        this.leaveDetails = leaveDetails;
    }

    public String getManagerName() {
        return managerName;
    }

    public void setManagerName(String managerName) {
        this.managerName = managerName;
    }

    public Date getModifiedDate() {
        return modifiedDate;
    }

    public void setModifiedDate(Date modifiedDate) {
        this.modifiedDate = modifiedDate;
    }

    public String getProjectName() {
        return projectName;
    }

    public void setProjectName(String projectName) {
        this.projectName = projectName;
    }

    public int getRefHierarchyLevel() {
        return refHierarchyLevel;
    }

    public void setRefHierarchyLevel(int refHierarchyLevel) {
        this.refHierarchyLevel = refHierarchyLevel;
    }

    public int getRefRoleId() {
        return refRoleId;
    }

    public void setRefRoleId(int refRoleId) {
        this.refRoleId = refRoleId;
    }

    public String getRoleName() {
        return roleName;
    }

    public void setRoleName(String roleName) {
        this.roleName = roleName;
    }

    public List<EmployeeSkillDetails> getSkills() {
        return skills;
    }

    public void setSkills(List<EmployeeSkillDetails> skills) {
        this.skills = skills;
    }

    public String getTelephone() {
        return telephone;
    }

    public void setTelephone(String telephone) {
        this.telephone = telephone;
    }

    public class Announcement {
        @SerializedName("Id")
        private int id;
        @SerializedName("Title")
        private String title;
        @SerializedName("CarouselContent")
        private String carouselContent;
        @SerializedName("ImagePath")
        private String imagePath;

        public String getCarouselContent() {
            return carouselContent;
        }

        public void setCarouselContent(String carouselContent) {
            this.carouselContent = carouselContent;
        }

        public int getId() {
            return id;
        }

        public void setId(int id) {
            this.id = id;
        }

        public String getImagePath() {
            return imagePath;
        }

        public void setImagePath(String imagePath) {
            this.imagePath = imagePath;
        }

        public String getTitle() {
            return title;
        }

        public void setTitle(String title) {
            this.title = title;
        }
    }
}

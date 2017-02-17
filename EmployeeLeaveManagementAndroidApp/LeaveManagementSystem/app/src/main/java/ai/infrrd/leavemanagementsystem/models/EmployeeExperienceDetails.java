package ai.infrrd.leavemanagementsystem.models;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 1/2/2017.
 * <p>
 * Class to hold employee experience data
 */

public class EmployeeExperienceDetails {

    @SerializedName("Id")
    private int id;
    @SerializedName("Company")
    private String company;
    @SerializedName("Role")
    private String role;
    @SerializedName("TimePeriod")
    private String timePeriod;
    @SerializedName("FromDate")
    private Date fromDate;
    @SerializedName("ToDate")
    private Date toDate;
    @SerializedName("CompanyLogo")
    private String companyLogo;

    public String getCompany() {
        return company;
    }

    public void setCompany(String company) {
        this.company = company;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getRole() {
        return role;
    }

    public void setRole(String role) {
        this.role = role;
    }

    public String getTimePeriod() {
        return timePeriod;
    }

    public void setTimePeriod(String timePeriod) {
        this.timePeriod = timePeriod;
    }

    public Date getFromDate() {
        return fromDate;
    }

    public void setFromDate(Date fromDate) {
        this.fromDate = fromDate;
    }

    public Date getToDate() {
        return toDate;
    }

    public void setToDate(Date toDate) {
        this.toDate = toDate;
    }

    public String getCompanyLogo() {
        return companyLogo;
    }

    public void setCompanyLogo(String companyLogo) {
        this.companyLogo = companyLogo;
    }
}

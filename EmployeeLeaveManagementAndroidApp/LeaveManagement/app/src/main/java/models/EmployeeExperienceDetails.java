package models;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Anuj Dutt on 1/2/2017.
 *
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
}

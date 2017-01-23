package models;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 1/10/2017.
 * Model class to hold employee education details.
 */

public class EmployeeEducationDetails {

    @SerializedName("Id")
    private int id;
    @SerializedName("Institution")
    private String institution;
    @SerializedName("Degree")
    private String degree;
    @SerializedName("TimePeriod")
    private String timePeriod;
    @SerializedName("FromDate")
    private Date fromDate;
    @SerializedName("ToDate")
    private Date toDate;

    public String getDegree() {
        return degree;
    }

    public void setDegree(String degree) {
        this.degree = degree;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getInstitution() {
        return institution;
    }

    public void setInstitution(String institution) {
        this.institution = institution;
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
}

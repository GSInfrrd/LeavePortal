package models;

import android.support.annotation.Nullable;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 1/9/2017.
 */

public class LeaveHistoryModel {

    @SerializedName("Id")
    private long id;
    
    @SerializedName("RefEmployeeId")
    private int refEmployeeId;
    
    @SerializedName("FromDate")
    private Date fromDate;
    
    @Nullable
    @SerializedName("ToDate")
    private Date toDate;
    
    @SerializedName("CreatedDate")
    private Date createdDate;
    
    @SerializedName("RefStatus")
    private int refStatus;
    
    @SerializedName("NumberOfWorkingDays")
    private double numberOfWorkingDays;
    
    @SerializedName("RefLeaveType")
    private int refLeaveType;
    
    @SerializedName("LeaveTypeName")
    private String leaveTypeName;

    @SerializedName("StatusName")
    private String statusName;

    @SerializedName("LeaveType")
    private String leaveType;

    @SerializedName("EmployeeComment")
    private String employeeComment;

    @SerializedName("ManagerComments")
    private String managerComments;
    
    @Nullable
    @SerializedName("ModifiedDate")
    private Date modifiedDate;


    public Date getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(Date createdDate) {
        this.createdDate = createdDate;
    }

    public String getEmployeeComment() {
        return employeeComment;
    }

    public void setEmployeeComment(String employeeComment) {
        this.employeeComment = employeeComment;
    }

    public Date getFromDate() {
        return fromDate;
    }

    public void setFromDate(Date fromDate) {
        this.fromDate = fromDate;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public String getLeaveType() {
        return leaveType;
    }

    public void setLeaveType(String leaveType) {
        this.leaveType = leaveType;
    }

    public String getLeaveTypeName() {
        return leaveTypeName;
    }

    public void setLeaveTypeName(String leaveTypeName) {
        this.leaveTypeName = leaveTypeName;
    }

    public String getManagerComments() {
        return managerComments;
    }

    public void setManagerComments(String managerComments) {
        this.managerComments = managerComments;
    }

    @Nullable
    public Date getModifiedDate() {
        return modifiedDate;
    }

    public void setModifiedDate(@Nullable Date modifiedDate) {
        this.modifiedDate = modifiedDate;
    }

    public double getNumberOfWorkingDays() {
        return numberOfWorkingDays;
    }

    public void setNumberOfWorkingDays(double numberOfWorkingDays) {
        this.numberOfWorkingDays = numberOfWorkingDays;
    }

    public int getRefEmployeeId() {
        return refEmployeeId;
    }

    public void setRefEmployeeId(int refEmployeeId) {
        this.refEmployeeId = refEmployeeId;
    }

    public int getRefLeaveType() {
        return refLeaveType;
    }

    public void setRefLeaveType(int refLeaveType) {
        this.refLeaveType = refLeaveType;
    }

    public int getRefStatus() {
        return refStatus;
    }

    public void setRefStatus(int refStatus) {
        this.refStatus = refStatus;
    }

    public String getStatusName() {
        return statusName;
    }

    public void setStatusName(String statusName) {
        this.statusName = statusName;
    }

    @Nullable
    public Date getToDate() {
        return toDate;
    }

    public void setToDate(@Nullable Date toDate) {
        this.toDate = toDate;
    }
}

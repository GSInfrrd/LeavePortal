package ai.infrrd.leavemanagementsystem.models;

import android.support.annotation.Nullable;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 1/17/2017.
 * <p>
 * Model for holding API response when applying for a leave.
 */

public class EmployeeLeaveTransactionModel {

    @SerializedName("Id")
    public long id;
    @SerializedName("RefEmployeeId")
    public int refEmployeeId;
    @SerializedName("FromDate")
    public Date fromDate;
    @Nullable
    @SerializedName("ToDate")
    public Date toDate;
    @SerializedName("CreatedDate")
    public Date createdDate;
    @SerializedName("RefStatus")
    public int refStatus;
    @SerializedName("NumberOfWorkingDays")
    public double numberOfWorkingDays;
    @SerializedName("RefLeaveType")
    public int refLeaveType;
    @SerializedName("LeaveTypeName")
    public String leaveTypeName;
    @SerializedName("StatusName")
    public String statusName;
    @SerializedName("LeaveType")
    public String leaveType;
    @SerializedName("EmployeeComment")
    public String employeeComment;
    @SerializedName("ManagerComments")
    public String managerComments;
    @Nullable
    @SerializedName("ModifiedDate")
    public Date modifiedDate;
    @SerializedName("RefTransactionType")
    public int refTransactionType;

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    public int getRefEmployeeId() {
        return refEmployeeId;
    }

    public void setRefEmployeeId(int refEmployeeId) {
        this.refEmployeeId = refEmployeeId;
    }

    public Date getFromDate() {
        return fromDate;
    }

    public void setFromDate(Date fromDate) {
        this.fromDate = fromDate;
    }

    @Nullable
    public Date getToDate() {
        return toDate;
    }

    public void setToDate(@Nullable Date toDate) {
        this.toDate = toDate;
    }

    public Date getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(Date createdDate) {
        this.createdDate = createdDate;
    }

    public int getRefStatus() {
        return refStatus;
    }

    public void setRefStatus(int refStatus) {
        this.refStatus = refStatus;
    }

    public double getNumberOfWorkingDays() {
        return numberOfWorkingDays;
    }

    public void setNumberOfWorkingDays(double numberOfWorkingDays) {
        this.numberOfWorkingDays = numberOfWorkingDays;
    }

    public int getRefLeaveType() {
        return refLeaveType;
    }

    public void setRefLeaveType(int refLeaveType) {
        this.refLeaveType = refLeaveType;
    }

    public String getLeaveTypeName() {
        return leaveTypeName;
    }

    public void setLeaveTypeName(String leaveTypeName) {
        this.leaveTypeName = leaveTypeName;
    }

    public String getStatusName() {
        return statusName;
    }

    public void setStatusName(String statusName) {
        this.statusName = statusName;
    }

    public String getLeaveType() {
        return leaveType;
    }

    public void setLeaveType(String leaveType) {
        this.leaveType = leaveType;
    }

    public String getEmployeeComment() {
        return employeeComment;
    }

    public void setEmployeeComment(String employeeComment) {
        this.employeeComment = employeeComment;
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

    public int getRefTransactionType() {
        return refTransactionType;
    }

    public void setRefTransactionType(int refTransactionType) {
        this.refTransactionType = refTransactionType;
    }
}

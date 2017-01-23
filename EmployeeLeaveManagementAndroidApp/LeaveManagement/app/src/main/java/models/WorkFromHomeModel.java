package models;

import android.support.annotation.Nullable;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 1/3/2017.
 * Model for Work From Home
 */

public class WorkFromHomeModel {

    @SerializedName("Id")
    public long id;

    @SerializedName("RefEmployeeId")
    public int refEmployeeId;

    @SerializedName("Date")
    public Date date;

    @SerializedName("CreatedDate")
    public Date createdDate;

    @SerializedName("RefStatus")
    public int refStatus;

    @SerializedName("CreatedBy")
    @Nullable
    public int createdBy;

    @SerializedName("RefReason")
    public int refReason;

    @SerializedName("StatusName")
    public String statusName;

    @SerializedName("Reason")
    public String reason;

    @SerializedName("ModifiedBy")
    @Nullable
    public int modifiedBy;

    @SerializedName("ModifiedDate")
    @Nullable
    public Date modifiedDate;

    @Nullable
    public int getCreatedBy() {
        return createdBy;
    }

    public void setCreatedBy(@Nullable int createdBy) {
        this.createdBy = createdBy;
    }

    public Date getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(Date createdDate) {
        this.createdDate = createdDate;
    }

    public Date getDate() {
        return date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Nullable
    public int getModifiedBy() {
        return modifiedBy;
    }

    public void setModifiedBy(@Nullable int modifiedBy) {
        this.modifiedBy = modifiedBy;
    }

    @Nullable
    public Date getModifiedDate() {
        return modifiedDate;
    }

    public void setModifiedDate(@Nullable Date modifiedDate) {
        this.modifiedDate = modifiedDate;
    }

    public String getReason() {
        return reason;
    }

    public void setReason(String reason) {
        this.reason = reason;
    }

    public int getRefEmployeeId() {
        return refEmployeeId;
    }

    public void setRefEmployeeId(int refEmployeeId) {
        this.refEmployeeId = refEmployeeId;
    }

    public int getRefReason() {
        return refReason;
    }

    public void setRefReason(int refReason) {
        this.refReason = refReason;
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
}

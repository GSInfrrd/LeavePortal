package models;

import android.support.annotation.Nullable;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 1/5/2017.
 */

public class HolidayModel {

    @SerializedName("Id")
    public long id;

    @Nullable
    @SerializedName("Date")
    public Date date;

    @SerializedName("Description")
    public String description;

    @SerializedName("Year")
    public long year;

    @Nullable
    @SerializedName("CreatedDate")
    public Date createdDate;

    @Nullable
    @SerializedName("ModifiedDate")
    public Date modifiedDate;

    @SerializedName("CreatedBy")
    public String createdBy;

    @SerializedName("ModifiedBy")
    public String modifiedBy;

    @Nullable
    @SerializedName("IsActive")
    public boolean isActive;

    public String getCreatedBy() {
        return createdBy;
    }

    public void setCreatedBy(String createdBy) {
        this.createdBy = createdBy;
    }

    @Nullable
    public Date getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(@Nullable Date createdDate) {
        this.createdDate = createdDate;
    }

    @Nullable
    public Date getDate() {
        return date;
    }

    public void setDate(@Nullable Date date) {
        this.date = date;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public long getId() {
        return id;
    }

    public void setId(long id) {
        this.id = id;
    }

    @Nullable
    public boolean isActive() {
        return isActive;
    }

    public void setActive(@Nullable boolean active) {
        isActive = active;
    }

    public String getModifiedBy() {
        return modifiedBy;
    }

    public void setModifiedBy(String modifiedBy) {
        this.modifiedBy = modifiedBy;
    }

    @Nullable
    public Date getModifiedDate() {
        return modifiedDate;
    }

    public void setModifiedDate(@Nullable Date modifiedDate) {
        this.modifiedDate = modifiedDate;
    }

    public long getYear() {
        return year;
    }

    public void setYear(long year) {
        this.year = year;
    }
}

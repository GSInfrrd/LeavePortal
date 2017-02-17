package ai.infrrd.leavemanagementsystem.models;

import android.support.annotation.Nullable;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 2/6/2017.
 */

public class EmployeeLeaveMasterDetails {

    @SerializedName("Id")
    public int id;
    @SerializedName("RefEmployeeId")
    public int refEmployeeId;
    @Nullable
    @SerializedName("RewardedLeaveCount")
    public int rewardedLeaveCount;
    @Nullable
    @SerializedName("ModifiedDate")
    public Date modifiedDate;
    @Nullable
    @SerializedName("ModifiedBy")
    public int modifiedBy;
    @Nullable
    @SerializedName("SpentAdvanceLeave")
    public int spentAdvanceLeave;
    @Nullable
    @SerializedName("TakenLossOfPay")
    public int takenLossOfPay;
    @Nullable
    @SerializedName("EarnedCasualLeave")
    public double earnedCasualLeave;
    @Nullable
    @SerializedName("TakenCompOff")
    public int takenCompOff;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getRefEmployeeId() {
        return refEmployeeId;
    }

    public void setRefEmployeeId(int refEmployeeId) {
        this.refEmployeeId = refEmployeeId;
    }

    @Nullable
    public int getRewardedLeaveCount() {
        return rewardedLeaveCount;
    }

    public void setRewardedLeaveCount(@Nullable int rewardedLeaveCount) {
        this.rewardedLeaveCount = rewardedLeaveCount;
    }

    @Nullable
    public Date getModifiedDate() {
        return modifiedDate;
    }

    public void setModifiedDate(@Nullable Date modifiedDate) {
        this.modifiedDate = modifiedDate;
    }

    @Nullable
    public int getModifiedBy() {
        return modifiedBy;
    }

    public void setModifiedBy(@Nullable int modifiedBy) {
        this.modifiedBy = modifiedBy;
    }

    @Nullable
    public int getSpentAdvanceLeave() {
        return spentAdvanceLeave;
    }

    public void setSpentAdvanceLeave(@Nullable int spentAdvanceLeave) {
        this.spentAdvanceLeave = spentAdvanceLeave;
    }

    @Nullable
    public int getTakenLossOfPay() {
        return takenLossOfPay;
    }

    public void setTakenLossOfPay(@Nullable int takenLossOfPay) {
        this.takenLossOfPay = takenLossOfPay;
    }

    @Nullable
    public double getEarnedCasualLeave() {
        return earnedCasualLeave;
    }

    public void setEarnedCasualLeave(@Nullable double earnedCasualLeave) {
        this.earnedCasualLeave = earnedCasualLeave;
    }

    @Nullable
    public int getTakenCompOff() {
        return takenCompOff;
    }

    public void setTakenCompOff(@Nullable int takenCompOff) {
        this.takenCompOff = takenCompOff;
    }
}

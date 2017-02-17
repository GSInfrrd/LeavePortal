package ai.infrrd.leavemanagementsystem.models;

import com.google.gson.annotations.SerializedName;

/**
 * Created by E7440 on 12/26/2016.
 */

public class LeaveReportModel {
    @SerializedName("Year")
    public float year;
    @SerializedName("Jan")
    public float jan;
    @SerializedName("Feb")
    public float feb;
    @SerializedName("Mar")
    public float mar;
    @SerializedName("Apr")
    public float apr;
    @SerializedName("May")
    public float may;
    @SerializedName("Jun")
    public float jun;
    @SerializedName("Jul")
    public float jul;
    @SerializedName("Aug")
    public float aug;
    @SerializedName("Sep")
    public float sep;
    @SerializedName("Oct")
    public float oct;
    @SerializedName("Nov")
    public float nov;
    @SerializedName("Dec")
    public float dec;
    @SerializedName("LeaveCount")
    public int leaveCount;

    public float getApr() {
        return apr;
    }

    public void setApr(float apr) {
        this.apr = apr;
    }

    public float getAug() {
        return aug;
    }

    public void setAug(float aug) {
        this.aug = aug;
    }

    public float getDec() {
        return dec;
    }

    public void setDec(float dec) {
        this.dec = dec;
    }

    public float getFeb() {
        return feb;
    }

    public void setFeb(float feb) {
        this.feb = feb;
    }

    public float getJan() {
        return jan;
    }

    public void setJan(float jan) {
        this.jan = jan;
    }

    public float getJul() {
        return jul;
    }

    public void setJul(float jul) {
        this.jul = jul;
    }

    public float getJun() {
        return jun;
    }

    public void setJun(float jun) {
        this.jun = jun;
    }

    public float getMar() {
        return mar;
    }

    public void setMar(float mar) {
        this.mar = mar;
    }

    public float getMay() {
        return may;
    }

    public void setMay(float may) {
        this.may = may;
    }

    public float getNov() {
        return nov;
    }

    public void setNov(float nov) {
        this.nov = nov;
    }

    public float getOct() {
        return oct;
    }

    public void setOct(float oct) {
        this.oct = oct;
    }

    public float getSep() {
        return sep;
    }

    public void setSep(float sep) {
        this.sep = sep;
    }

    public float getYear() {
        return year;
    }

    public void setYear(float year) {
        this.year = year;
    }

    public int getLeaveCount() {
        return leaveCount;
    }

    public void setLeaveCount(int leaveCount) {
        this.leaveCount = leaveCount;
    }
}

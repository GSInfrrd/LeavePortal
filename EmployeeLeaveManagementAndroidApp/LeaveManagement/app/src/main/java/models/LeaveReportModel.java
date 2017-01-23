package models;

import com.google.gson.annotations.SerializedName;

/**
 * Created by E7440 on 12/26/2016.
 */

public class LeaveReportModel {
    @SerializedName("Year")
    public int year;
    @SerializedName("Jan")
    public int jan;
    @SerializedName("Feb")
    public int feb;
    @SerializedName("Mar")
    public int mar;
    @SerializedName("Apr")
    public int apr;
    @SerializedName("May")
    public int may;
    @SerializedName("Jun")
    public int jun;
    @SerializedName("Jul")
    public int jul;
    @SerializedName("Aug")
    public int aug;
    @SerializedName("Sep")
    public int sep;
    @SerializedName("Oct")
    public int oct;
    @SerializedName("Nov")
    public int nov;
    @SerializedName("Dec")
    public int dec;

    public int getApr() {
        return apr;
    }

    public void setApr(int apr) {
        this.apr = apr;
    }

    public int getAug() {
        return aug;
    }

    public void setAug(int aug) {
        this.aug = aug;
    }

    public int getDec() {
        return dec;
    }

    public void setDec(int dec) {
        this.dec = dec;
    }

    public int getFeb() {
        return feb;
    }

    public void setFeb(int feb) {
        this.feb = feb;
    }

    public int getJan() {
        return jan;
    }

    public void setJan(int jan) {
        this.jan = jan;
    }

    public int getJul() {
        return jul;
    }

    public void setJul(int jul) {
        this.jul = jul;
    }

    public int getJun() {
        return jun;
    }

    public void setJun(int jun) {
        this.jun = jun;
    }

    public int getMar() {
        return mar;
    }

    public void setMar(int mar) {
        this.mar = mar;
    }

    public int getMay() {
        return may;
    }

    public void setMay(int may) {
        this.may = may;
    }

    public int getNov() {
        return nov;
    }

    public void setNov(int nov) {
        this.nov = nov;
    }

    public int getOct() {
        return oct;
    }

    public void setOct(int oct) {
        this.oct = oct;
    }

    public int getSep() {
        return sep;
    }

    public void setSep(int sep) {
        this.sep = sep;
    }

    public int getYear() {
        return year;
    }

    public void setYear(int year) {
        this.year = year;
    }
}

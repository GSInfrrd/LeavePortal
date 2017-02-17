package ai.infrrd.leavemanagementsystem.models;

/**
 * Created by Anuj Dutt on 1/16/2017.
 *
 * Model to find out the available leave details for a person.
 */

public class LeaveTransactionResponse {
    private int noOfWorkingDays;
    private int availableLeaveBalance;
    private int advanceLeaveBalance;
    private int lopLeaveBalance;
    private int responseCode;

    public int getAdvanceLeaveBalance() {
        return advanceLeaveBalance;
    }

    public void setAdvanceLeaveBalance(int advanceLeaveBalance) {
        this.advanceLeaveBalance = advanceLeaveBalance;
    }

    public int getAvailableLeaveBalance() {
        return availableLeaveBalance;
    }

    public void setAvailableLeaveBalance(int availableLeaveBalance) {
        this.availableLeaveBalance = availableLeaveBalance;
    }

    public int getLopLeaveBalance() {
        return lopLeaveBalance;
    }

    public void setLopLeaveBalance(int lopLeaveBalance) {
        this.lopLeaveBalance = lopLeaveBalance;
    }

    public int getNoOfWorkingDays() {
        return noOfWorkingDays;
    }

    public void setNoOfWorkingDays(int noOfWorkingDays) {
        this.noOfWorkingDays = noOfWorkingDays;
    }

    public int getResponseCode() {
        return responseCode;
    }

    public void setResponseCode(int responseCode) {
        this.responseCode = responseCode;
    }
}

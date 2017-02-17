package ai.infrrd.leavemanagementsystem.utilities;

/**
 * Created by Anuj Dutt on 1/9/2017.
 */

public class Constants {
    public static final int LEAVE_TYPE_PLANNED = 71;
    public static final int LEAVE_TYPE_SUBMITTED = 72;
    public static final int LEAVE_TYPE_REJECTED = 73;
    public static final int LEAVE_TYPE_APPROVED = 74;
    public static final int LEAVE_TYPE_REASSIGNED = 76;

    public static final int DETAILS_EXPERIENCE_DETAILS = 2001;
    public static final int DETAILS_EDUCATION_DETAILS = 2002;
    public static final int DETAILS_SKILLS_DETAILS = 2003;
    public static final int DETAILS_PERSONAL_DETAILS = 2004;

    public static final int OK = 1000;
    public static final int DATE_ALREADY_EXISTS = 1001;
    public static final int NO_LEAVE_BALANCE = 1002;

    public static final int LEAVE_TYPE_SICK_LEAVE = 61;
    public static final int LEAVE_TYPE_CASUAL_LEAVE = 62;
    public static final int LEAVE_TYPE_COMP_OFF = 63;
    public static final int LEAVE_TYPE_ADVANCED_LEAVE = 64;
    public static final int LEAVE_TYPE_LOP = 65;
    public static final int LEAVE_TYPE_REWARD = 66;
    public static final int LEAVE_TYPE_EARNED = 67;
    public static final int LEAVE_TYPE_CREDIT = 172;

    public static final int WFH_REASON_OTHERS = 94;

    public static final String LEAVE_TYPE_SICK_LEAVE_STRING = "Sick leave";
    public static final String LEAVE_TYPE_CASUAL_LEAVE_STRING = "Casual leave";
    public static final String LEAVE_TYPE_COMP_OFF_STRING = "Comp off";
    public static final String LEAVE_TYPE_ADVANCED_LEAVE_STRING = "Advanced leave";
    public static final String LEAVE_TYPE_LOP_STRING = "Loss of pay";


    public static class PreferenceConstants {
        public static final String PREFS_NAME_PERMISSION = "permissions";
        public static final String PREFS_NAME_USER = "user";
        public static final String PREFS_PERM_DENIED = "denied";
        public static final String PREFS_USER_NAME = "userName";
        public static final String PREFS_USER_LOGIN_ID = "loginId";
        public static final String PREFS_PASSWORD = "password";
        public static final String PREFS_USER_IMAGE = "userImage";
        public static final String PREFS_EMPLOYEE_ID = "employeeId";
    }
}

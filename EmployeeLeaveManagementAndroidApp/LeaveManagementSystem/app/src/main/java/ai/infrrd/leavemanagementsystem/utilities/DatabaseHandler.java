package ai.infrrd.leavemanagementsystem.utilities;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

import ai.infrrd.leavemanagementsystem.models.EmployeeEducationDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeExperienceDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeePersonalDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeSkillDetails;
import ai.infrrd.leavemanagementsystem.models.LeaveHistoryModel;

/**
 * Created by Anuj Dutt on 1/2/2017.
 * <p>
 * Manage local database interactions
 */

public class DatabaseHandler extends SQLiteOpenHelper {
    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "LeaveManagmentSystem";
    private static final String TABLE_SKILLS = "EmployeeSkills";
    private static final String TABLE_EXPERIENCE = "EmployeeExperienceDetails";
    private static final String TABLE_EDUCATION = "EmployeeEducationDetails";
    private static final String TABLE_PERSONAL_DETAILS = "EmployeePersonalDetails";
    private static final String TABLE_LEAVE_DETAILS = "EmployeeLeaveDetails";
    private static final String KEY_ID = "id";
    private static final String KEY_REF_EMP_ID = "RefemployeeId";
    private static final String KEY_SKILL = "Skill";
    private static final String KEY_SKILL_NAME = "SkillName";
    private static final String KEY_IS_SELECTED = "IsSelected";
    private static final String KEY_COMPANY_NAME = "CompanyName";
    private static final String KEY_ROLE = "Role";
    private static final String KEY_COMPANY_LOGO = "CompanyLogo";
    private static final String KEY_DATE_RANGE = "DateRange";
    private static final String KEY_FROM_DATE = "FromDate";
    private static final String KEY_TO_DATE = "ToDate";
    private static final String KEY_DEGREE_NAME = "Degree";
    private static final String KEY_INSTITUTION = "Institution";
    private static final String KEY_FIRST_NAME = "FirstName";
    private static final String KEY_LAST_NAME = "LastName";
    private static final String KEY_DOB = "DOB";
    private static final String KEY_PHONE_NUMBER = "PhoneNumber";
    private static final String KEY_CITY = "City";
    private static final String KEY_COUNTRY = "Country";
    private static final String KEY_TWITTER = "Twitter";
    private static final String KEY_FACEBOOK = "Facebook";
    private static final String KEY_GOOGLE_PLUS = "GooglePlus";
    private static final String KEY_CREATED_DATE = "CreatedDate";
    private static final String KEY_REF_STATUS = "RefStatus";
    private static final String KEY_NO_OF_WORKING_DAYS = "NoOfWorkingDays";
    private static final String KEY_REF_LEAVE_TYPE = "RefLeaveType";
    private static final String KEY_LEAVE_TYPE_NAME = "LeaveTypeName";
    private static final String KEY_STATUS_NAME = "StatusName";
    private static final String KEY_LEAVE_TYPE = "LeaveType";
    private static final String KEY_EMPLOYEE_COMMENT = "EmployeeComment";
    private static final String KEY_MANAGER_COMMENT = "ManagerComment";
    private SimpleDateFormat simpleDateFormat = null;

    public DatabaseHandler(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        //3rd argument to be passed is CursorFactory instance
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_EXPERIENCE_TABLE = "CREATE TABLE " + TABLE_EXPERIENCE + "("
                + KEY_ID + " INTEGER PRIMARY KEY,"
                + KEY_COMPANY_NAME + " TEXT,"
                + KEY_ROLE + " TEXT,"
                + KEY_COMPANY_LOGO + " TEXT,"
                + KEY_DATE_RANGE + " TEXT,"
                + KEY_FROM_DATE + " TEXT,"
                + KEY_TO_DATE + " TEXT" + ")";
        String CREATE_EDUCATION_TABLE = "CREATE TABLE " + TABLE_EDUCATION + "("
                + KEY_ID + " INTEGER PRIMARY KEY,"
                + KEY_INSTITUTION + " TEXT,"
                + KEY_DEGREE_NAME + " TEXT,"
                + KEY_DATE_RANGE + " TEXT,"
                + KEY_FROM_DATE + " TEXT,"
                + KEY_TO_DATE + " TEXT" + ")";
        String CREATE_PERSONAL_DETAILS_TABLE = "CREATE TABLE " + TABLE_PERSONAL_DETAILS + "("
                + KEY_FIRST_NAME + " TEXT,"
                + KEY_LAST_NAME + " TEXT,"
                + KEY_DOB + " TEXT,"
                + KEY_PHONE_NUMBER + " TEXT,"
                + KEY_CITY + " TEXT,"
                + KEY_COUNTRY + " TEXT,"
                + KEY_TWITTER + " TEXT,"
                + KEY_FACEBOOK + " TEXT,"
                + KEY_GOOGLE_PLUS + " TEXT" + ")";
        String CREATE_SKILLS_TABLE = "CREATE TABLE " + TABLE_SKILLS + "("
                + KEY_ID + " INTEGER,"
                + KEY_IS_SELECTED + " TEXT,"
                + KEY_SKILL_NAME + " TEXT" + ")";
        String CREATE_LEAVE_DETAILS_TABLE = "CREATE TABLE " + TABLE_LEAVE_DETAILS + "("
                + KEY_ID + " TEXT,"
                + KEY_FROM_DATE + " TEXT,"
                + KEY_TO_DATE + " TEXT,"
                + KEY_CREATED_DATE + " TEXT,"
                + KEY_REF_STATUS + " TEXT,"
                + KEY_NO_OF_WORKING_DAYS + " TEXT,"
                + KEY_REF_LEAVE_TYPE + " TEXT,"
                + KEY_LEAVE_TYPE_NAME + " TEXT,"
                + KEY_STATUS_NAME + " TEXT,"
                + KEY_LEAVE_TYPE + " TEXT,"
                + KEY_EMPLOYEE_COMMENT + " TEXT,"
                + KEY_MANAGER_COMMENT + " TEXT" + ")";
        db.execSQL(CREATE_EXPERIENCE_TABLE);
        db.execSQL(CREATE_EDUCATION_TABLE);
        db.execSQL(CREATE_PERSONAL_DETAILS_TABLE);
        db.execSQL(CREATE_SKILLS_TABLE);
        db.execSQL(CREATE_LEAVE_DETAILS_TABLE);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXPERIENCE);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_EDUCATION);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_PERSONAL_DETAILS);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_SKILLS);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_LEAVE_DETAILS);
        // Create tables again
        onCreate(db);
    }

    public void addEmployeeExperienceDetails(EmployeeExperienceDetails experienceDetails, int refEmployeeId) {
        SQLiteDatabase db = this.getWritableDatabase();
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        ContentValues values = new ContentValues();
        values.put(KEY_ID, experienceDetails.getId());
        values.put(KEY_COMPANY_NAME, experienceDetails.getCompany());
        values.put(KEY_ROLE, experienceDetails.getRole());
        values.put(KEY_COMPANY_LOGO, experienceDetails.getCompanyLogo());
        values.put(KEY_TO_DATE, simpleDateFormat.format(experienceDetails.getToDate()));
        values.put(KEY_FROM_DATE, simpleDateFormat.format(experienceDetails.getFromDate()));
        values.put(KEY_DATE_RANGE, experienceDetails.getTimePeriod());
        db.insert(TABLE_EXPERIENCE, null, values);
        //2nd argument is String containing nullColumnHackz
        db.close();
    }

    public void addEmployeeEducationDetails(EmployeeEducationDetails employeeEducationDetails, int refEmployeeId) {
        SQLiteDatabase db = this.getWritableDatabase();
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        ContentValues values = new ContentValues();
        values.put(KEY_ID, employeeEducationDetails.getId());
        values.put(KEY_INSTITUTION, employeeEducationDetails.getInstitution());
        values.put(KEY_DEGREE_NAME, employeeEducationDetails.getDegree());
        values.put(KEY_TO_DATE, simpleDateFormat.format(employeeEducationDetails.getToDate()));
        values.put(KEY_FROM_DATE, simpleDateFormat.format(employeeEducationDetails.getFromDate()));
        values.put(KEY_DATE_RANGE, employeeEducationDetails.getTimePeriod());
        db.insert(TABLE_EDUCATION, null, values);
        //2nd argument is String containing nullColumnHack
        db.close();
    }

    public void addEmployeeSkillDetails(EmployeeSkillDetails employeeSkillDetails, int refEmployeeId) {
        SQLiteDatabase db = this.getWritableDatabase();
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        ContentValues values = new ContentValues();
        values.put(KEY_ID, employeeSkillDetails.getId());
        values.put(KEY_SKILL_NAME, employeeSkillDetails.getSkillName());
        values.put(KEY_IS_SELECTED, employeeSkillDetails.isSelected());
        db.insert(TABLE_SKILLS, null, values);
        //2nd argument is String containing nullColumnHack
        db.close();
    }

    public void addEmployeePersonalDetails(EmployeePersonalDetails employeePersonalDetails) {
        SQLiteDatabase db = this.getWritableDatabase();
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        ContentValues values = new ContentValues();
        values.put(KEY_FIRST_NAME, employeePersonalDetails.getFirstName());
        values.put(KEY_LAST_NAME, employeePersonalDetails.getLastName());
        values.put(KEY_DOB, simpleDateFormat.format(employeePersonalDetails.getDateOfBirth()));
        values.put(KEY_PHONE_NUMBER, employeePersonalDetails.getPhoneNumber());
        values.put(KEY_CITY, employeePersonalDetails.getCity());
        values.put(KEY_COUNTRY, employeePersonalDetails.getCountry());
        values.put(KEY_TWITTER, employeePersonalDetails.getTwitter());
        values.put(KEY_FACEBOOK, employeePersonalDetails.getFacebook());
        values.put(KEY_GOOGLE_PLUS, employeePersonalDetails.getGooglePlus());
        db.insert(TABLE_PERSONAL_DETAILS, null, values);
        //2nd argument is String containing nullColumnHack
        db.close();
    }

    public void dropTables() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXPERIENCE);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_EDUCATION);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_PERSONAL_DETAILS);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_SKILLS);
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_LEAVE_DETAILS);
        db.close();
    }

    public void addEmployeeExperienceDetailsList(List<EmployeeExperienceDetails> experienceDetailList) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getWritableDatabase();
        for (EmployeeExperienceDetails experienceDetails : experienceDetailList) {
            ContentValues values = new ContentValues();
            values.put(KEY_ID, experienceDetails.getId());
            values.put(KEY_COMPANY_NAME, experienceDetails.getCompany());
            values.put(KEY_ROLE, experienceDetails.getRole());
            values.put(KEY_COMPANY_LOGO, experienceDetails.getCompanyLogo());
            values.put(KEY_TO_DATE, simpleDateFormat.format(experienceDetails.getToDate()));
            values.put(KEY_FROM_DATE, simpleDateFormat.format(experienceDetails.getFromDate()));
            values.put(KEY_DATE_RANGE, experienceDetails.getTimePeriod());
            db.insert(TABLE_EXPERIENCE, null, values);
        }
        db.close();
    }

    public void addEmployeeEducationDetailsList(List<EmployeeEducationDetails> employeeEducationDetailsList) {
        SQLiteDatabase db = this.getWritableDatabase();
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        for (EmployeeEducationDetails employeeEducationDetails : employeeEducationDetailsList) {
            ContentValues values = new ContentValues();
            values.put(KEY_ID, employeeEducationDetails.getId());
            values.put(KEY_INSTITUTION, employeeEducationDetails.getInstitution());
            values.put(KEY_DEGREE_NAME, employeeEducationDetails.getDegree());
            values.put(KEY_TO_DATE, simpleDateFormat.format(employeeEducationDetails.getToDate()));
            values.put(KEY_FROM_DATE, simpleDateFormat.format(employeeEducationDetails.getFromDate()));
            values.put(KEY_DATE_RANGE, employeeEducationDetails.getTimePeriod());
            db.insert(TABLE_EDUCATION, null, values);
        }
        db.close();
    }

    public void addEmployeeSkillsList(List<EmployeeSkillDetails> employeeSkillDetailsList) {
        SQLiteDatabase db = this.getWritableDatabase();
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        for (EmployeeSkillDetails employeeSkillDetails : employeeSkillDetailsList) {
            ContentValues values = new ContentValues();
            values.put(KEY_ID, employeeSkillDetails.getId());
            values.put(KEY_SKILL_NAME, employeeSkillDetails.getSkillName());
            values.put(KEY_IS_SELECTED, String.valueOf(employeeSkillDetails.isSelected()));
            db.insert(TABLE_SKILLS, null, values);
        }
        db.close();
    }

    public void addEmployeeLeaveDetailsList(List<LeaveHistoryModel> leaveHistoryModelList) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getWritableDatabase();
        for (LeaveHistoryModel leaveHistoryModel : leaveHistoryModelList) {
            ContentValues values = new ContentValues();
            values.put(KEY_ID, leaveHistoryModel.getId());
            if (leaveHistoryModel.getToDate() != null)
                values.put(KEY_TO_DATE, simpleDateFormat.format(leaveHistoryModel.getToDate()));
            values.put(KEY_FROM_DATE, simpleDateFormat.format(leaveHistoryModel.getFromDate()));
            values.put(KEY_CREATED_DATE, simpleDateFormat.format(leaveHistoryModel.getCreatedDate()));
            values.put(KEY_REF_STATUS, leaveHistoryModel.getRefStatus());
            values.put(KEY_NO_OF_WORKING_DAYS, leaveHistoryModel.getNumberOfWorkingDays());
            values.put(KEY_REF_LEAVE_TYPE, leaveHistoryModel.getRefLeaveType());
            values.put(KEY_LEAVE_TYPE_NAME, leaveHistoryModel.getLeaveTypeName());
            values.put(KEY_STATUS_NAME, leaveHistoryModel.getStatusName());
            values.put(KEY_LEAVE_TYPE, leaveHistoryModel.getLeaveType());
            values.put(KEY_EMPLOYEE_COMMENT, leaveHistoryModel.getEmployeeComment());
            values.put(KEY_MANAGER_COMMENT, leaveHistoryModel.getManagerComments());
            db.insert(TABLE_LEAVE_DETAILS, null, values);
        }
        db.close();
    }

    public List<EmployeeExperienceDetails> getAllEmployeeExperienceDetails() {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        List<EmployeeExperienceDetails> employeeExperienceDetailsList = new ArrayList<EmployeeExperienceDetails>();
        String selectQuery = "SELECT  * FROM " + TABLE_EXPERIENCE;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        if (cursor.moveToFirst()) {
            do {
                EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
                employeeExperienceDetails.setId(Integer.parseInt(cursor.getString(0)));
                employeeExperienceDetails.setCompany(cursor.getString(1));
                employeeExperienceDetails.setRole(cursor.getString(2));
                employeeExperienceDetails.setCompanyLogo(cursor.getString(3));
                employeeExperienceDetails.setTimePeriod(cursor.getString(4));
                try {
                    employeeExperienceDetails.setFromDate(simpleDateFormat.parse(cursor.getString(5)));
                    employeeExperienceDetails.setToDate(simpleDateFormat.parse(cursor.getString(6)));
                } catch (ParseException e) {
                    e.printStackTrace();
                }
                employeeExperienceDetailsList.add(employeeExperienceDetails);
            } while (cursor.moveToNext());
        }
        return employeeExperienceDetailsList;
    }

    public List<EmployeeEducationDetails> getAllEmployeeEducationDetails() {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        List<EmployeeEducationDetails> employeeEducationDetailsList = new ArrayList<EmployeeEducationDetails>();
        String selectQuery = "SELECT  * FROM " + TABLE_EDUCATION;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        if (cursor.moveToFirst()) {
            do {
                EmployeeEducationDetails employeeEducationDetails = new EmployeeEducationDetails();
                employeeEducationDetails.setId(Integer.parseInt(cursor.getString(0)));
                employeeEducationDetails.setInstitution(cursor.getString(1));
                employeeEducationDetails.setDegree(cursor.getString(2));
                employeeEducationDetails.setTimePeriod(cursor.getString(3));
                try {
                    employeeEducationDetails.setFromDate(simpleDateFormat.parse(cursor.getString(4)));
                    employeeEducationDetails.setToDate(simpleDateFormat.parse(cursor.getString(5)));
                } catch (ParseException e) {
                    e.printStackTrace();
                }

                employeeEducationDetailsList.add(employeeEducationDetails);
            } while (cursor.moveToNext());
        }
        return employeeEducationDetailsList;
    }

    public List<EmployeeSkillDetails> getAllEmployeeSkillDetails() {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        List<EmployeeSkillDetails> employeeSkillDetailsList = new ArrayList<EmployeeSkillDetails>();
        String selectQuery = "SELECT  * FROM " + TABLE_SKILLS;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        if (cursor.moveToFirst()) {
            do {
                EmployeeSkillDetails employeeSkillDetails = new EmployeeSkillDetails();
                employeeSkillDetails.setId(Integer.parseInt(cursor.getString(0)));
                employeeSkillDetails.setSelected(Boolean.parseBoolean(cursor.getString(1)));
                employeeSkillDetails.setSkillName(cursor.getString(2));
                employeeSkillDetailsList.add(employeeSkillDetails);
            } while (cursor.moveToNext());
        }
        return employeeSkillDetailsList;
    }

    public List<LeaveHistoryModel> getAllEmployeeLeaveDetails() {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        List<LeaveHistoryModel> leaveHistoryModelList = new ArrayList<LeaveHistoryModel>();
        String selectQuery = "SELECT  * FROM " + TABLE_LEAVE_DETAILS;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        if (cursor.moveToFirst()) {
            do {
                LeaveHistoryModel leaveHistoryModel = new LeaveHistoryModel();
                leaveHistoryModel.setId(Integer.parseInt(cursor.getString(0)));
                try {
                    leaveHistoryModel.setFromDate(simpleDateFormat.parse(cursor.getString(1)));
                    if (cursor.getString(2) != null)
                        leaveHistoryModel.setToDate(simpleDateFormat.parse(cursor.getString(2)));
                    leaveHistoryModel.setCreatedDate(simpleDateFormat.parse(cursor.getString(3)));
                } catch (ParseException e) {
                    e.printStackTrace();
                }
                leaveHistoryModel.setRefStatus(Integer.parseInt(cursor.getString(4)));
                leaveHistoryModel.setNumberOfWorkingDays(Double.parseDouble(cursor.getString(5)));
                leaveHistoryModel.setRefLeaveType(Integer.parseInt(cursor.getString(6)));
                leaveHistoryModel.setLeaveTypeName(cursor.getString(7));
                leaveHistoryModel.setStatusName(cursor.getString(8));
                leaveHistoryModel.setLeaveType(cursor.getString(9));
                leaveHistoryModel.setEmployeeComment(cursor.getString(10));
                leaveHistoryModel.setManagerComments(cursor.getString(11));
                leaveHistoryModelList.add(leaveHistoryModel);
            } while (cursor.moveToNext());
        }
        return leaveHistoryModelList;
    }

    public EmployeeExperienceDetails getEmployeeExperienceDetailsById(int id) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getReadableDatabase();
        EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
        Cursor cursor = db.query(TABLE_EXPERIENCE, new String[]{KEY_ID, KEY_COMPANY_NAME,
                        KEY_ROLE, KEY_COMPANY_LOGO, KEY_DATE_RANGE, KEY_FROM_DATE, KEY_TO_DATE}, KEY_ID + "=?",
                new String[]{String.valueOf(id)}, null, null, null, null);
        if (cursor != null)
            cursor.moveToFirst();

        employeeExperienceDetails.setId(Integer.parseInt(cursor.getString(0)));
        employeeExperienceDetails.setCompany(cursor.getString(1));
        employeeExperienceDetails.setRole(cursor.getString(2));
        employeeExperienceDetails.setCompanyLogo(cursor.getString(3));
        employeeExperienceDetails.setTimePeriod(cursor.getString(4));
        try {
            employeeExperienceDetails.setFromDate(simpleDateFormat.parse(cursor.getString(5)));
            employeeExperienceDetails.setToDate(simpleDateFormat.parse(cursor.getString(6)));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return employeeExperienceDetails;
    }

    public EmployeeEducationDetails getEmployeeEducationDetailsById(int id) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getReadableDatabase();
        EmployeeEducationDetails employeeEducationDetails = new EmployeeEducationDetails();
        Cursor cursor = db.query(TABLE_EDUCATION, new String[]{KEY_ID, KEY_INSTITUTION,
                        KEY_DEGREE_NAME, KEY_DATE_RANGE, KEY_FROM_DATE, KEY_TO_DATE}, KEY_ID + "=?",
                new String[]{String.valueOf(id)}, null, null, null, null);
        if (cursor != null)
            cursor.moveToFirst();

        employeeEducationDetails.setId(Integer.parseInt(cursor.getString(0)));
        employeeEducationDetails.setInstitution(cursor.getString(1));
        employeeEducationDetails.setDegree(cursor.getString(2));
        employeeEducationDetails.setTimePeriod(cursor.getString(3));
        try {
            employeeEducationDetails.setFromDate(simpleDateFormat.parse(cursor.getString(4)));
            employeeEducationDetails.setToDate(simpleDateFormat.parse(cursor.getString(5)));
        } catch (ParseException e) {
            e.printStackTrace();
        }
        return employeeEducationDetails;
    }

    public EmployeeSkillDetails getEmployeeSkillDetailsById(int id) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getReadableDatabase();
        EmployeeSkillDetails employeeSkillDetails = new EmployeeSkillDetails();
        Cursor cursor = db.query(TABLE_SKILLS, new String[]{KEY_ID, KEY_SKILL_NAME}, KEY_ID + "=?",
                new String[]{String.valueOf(id)}, null, null, null, null);
        if (cursor != null)
            cursor.moveToFirst();

        employeeSkillDetails.setId(Integer.parseInt(cursor.getString(0)));
        employeeSkillDetails.setSkillName(cursor.getString(1));
        return employeeSkillDetails;
    }

    public EmployeePersonalDetails getEmployeePersonalDetails() {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        EmployeePersonalDetails employeePersonalDetails = new EmployeePersonalDetails();
        String selectQuery = "SELECT  * FROM " + TABLE_PERSONAL_DETAILS;
        SQLiteDatabase db = this.getWritableDatabase();
        Cursor cursor = db.rawQuery(selectQuery, null);
        if (cursor.moveToFirst()) {
            do {
                employeePersonalDetails.setFirstName(cursor.getString(0));
                employeePersonalDetails.setLastName(cursor.getString(1));
                employeePersonalDetails.setPhoneNumber(cursor.getString(3));
                employeePersonalDetails.setCity(cursor.getString(4));
                employeePersonalDetails.setCountry(cursor.getString(5));
                employeePersonalDetails.setTwitter(cursor.getString(6));
                employeePersonalDetails.setFacebook(cursor.getString(7));
                employeePersonalDetails.setGooglePlus(cursor.getString(8));
                try {
                    employeePersonalDetails.setDateOfBirth(simpleDateFormat.parse(cursor.getString(2)));
                } catch (ParseException e) {
                    e.printStackTrace();
                }
            } while (cursor.moveToNext());
        }
        return employeePersonalDetails;
    }

    public int updateEmployeeExperienceDetails(EmployeeExperienceDetails employeeExperienceDetails) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(KEY_ID, employeeExperienceDetails.getId());
        values.put(KEY_COMPANY_NAME, employeeExperienceDetails.getCompany());
        values.put(KEY_ROLE, employeeExperienceDetails.getRole());
        values.put(KEY_COMPANY_LOGO, employeeExperienceDetails.getCompanyLogo());
        values.put(KEY_TO_DATE, simpleDateFormat.format(employeeExperienceDetails.getToDate()));
        values.put(KEY_FROM_DATE, simpleDateFormat.format(employeeExperienceDetails.getFromDate()));
        values.put(KEY_DATE_RANGE, employeeExperienceDetails.getTimePeriod());
        return db.update(TABLE_EXPERIENCE, values, KEY_ID + " = ?",
                new String[]{String.valueOf(employeeExperienceDetails.getId())});
    }

    public int updateEmployeeEducationDetails(EmployeeEducationDetails employeeEducationDetails) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        SQLiteDatabase db = this.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put(KEY_ID, employeeEducationDetails.getId());
        values.put(KEY_INSTITUTION, employeeEducationDetails.getInstitution());
        values.put(KEY_DEGREE_NAME, employeeEducationDetails.getDegree());
        values.put(KEY_TO_DATE, simpleDateFormat.format(employeeEducationDetails.getToDate()));
        values.put(KEY_FROM_DATE, simpleDateFormat.format(employeeEducationDetails.getFromDate()));
        values.put(KEY_DATE_RANGE, employeeEducationDetails.getTimePeriod());
        return db.update(TABLE_EDUCATION, values, KEY_ID + " = ?",
                new String[]{String.valueOf(employeeEducationDetails.getId())});
    }

    public List<LeaveHistoryModel> getAllEmployeeLeaveDetailsByCategory(int leaveType) {
        simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        List<LeaveHistoryModel> leaveHistoryModelList = new ArrayList<LeaveHistoryModel>();
        SQLiteDatabase db = this.getReadableDatabase();
        Cursor cursor = null;

        if (leaveType == Constants.LEAVE_TYPE_CREDIT) {
            cursor = db.query(TABLE_LEAVE_DETAILS, new String[]{KEY_ID, KEY_FROM_DATE, KEY_TO_DATE,
                            KEY_CREATED_DATE, KEY_REF_STATUS, KEY_NO_OF_WORKING_DAYS, KEY_REF_LEAVE_TYPE,
                            KEY_LEAVE_TYPE_NAME, KEY_STATUS_NAME, KEY_LEAVE_TYPE, KEY_EMPLOYEE_COMMENT,
                            KEY_MANAGER_COMMENT}, KEY_REF_LEAVE_TYPE + "=?" + " OR " + KEY_REF_LEAVE_TYPE + "=?",
                    new String[]{String.valueOf(Constants.LEAVE_TYPE_REWARD), String.valueOf(Constants.LEAVE_TYPE_EARNED)}, null, null, null, null);
        } else {
            cursor = db.query(TABLE_LEAVE_DETAILS, new String[]{KEY_ID, KEY_FROM_DATE, KEY_TO_DATE,
                            KEY_CREATED_DATE, KEY_REF_STATUS, KEY_NO_OF_WORKING_DAYS, KEY_REF_LEAVE_TYPE,
                            KEY_LEAVE_TYPE_NAME, KEY_STATUS_NAME, KEY_LEAVE_TYPE, KEY_EMPLOYEE_COMMENT,
                            KEY_MANAGER_COMMENT}, KEY_REF_LEAVE_TYPE + "!=? AND " + KEY_REF_LEAVE_TYPE + "!=?",
                    new String[]{String.valueOf(Constants.LEAVE_TYPE_REWARD), String.valueOf(Constants.LEAVE_TYPE_EARNED)}, null, null, null, null);
        }
        if (cursor.moveToFirst()) {
            do {
                LeaveHistoryModel leaveHistoryModel = new LeaveHistoryModel();
                leaveHistoryModel.setId(Integer.parseInt(cursor.getString(0)));
                try {
                    leaveHistoryModel.setFromDate(simpleDateFormat.parse(cursor.getString(1)));
                    if (cursor.getString(2) != null)
                        leaveHistoryModel.setToDate(simpleDateFormat.parse(cursor.getString(2)));
                    leaveHistoryModel.setCreatedDate(simpleDateFormat.parse(cursor.getString(3)));
                } catch (ParseException e) {
                    e.printStackTrace();
                }
                leaveHistoryModel.setRefStatus(Integer.parseInt(cursor.getString(4)));
                leaveHistoryModel.setNumberOfWorkingDays(Double.parseDouble(cursor.getString(5)));
                leaveHistoryModel.setRefLeaveType(Integer.parseInt(cursor.getString(6)));
                leaveHistoryModel.setLeaveTypeName(cursor.getString(7));
                leaveHistoryModel.setStatusName(cursor.getString(8));
                leaveHistoryModel.setLeaveType(cursor.getString(9));
                leaveHistoryModel.setEmployeeComment(cursor.getString(10));
                leaveHistoryModel.setManagerComments(cursor.getString(11));
                leaveHistoryModelList.add(leaveHistoryModel);
            } while (cursor.moveToNext());
        }
        return leaveHistoryModelList;
    }

    // Deleting single experience
    public void deleteEmployeeExperienceDetail(EmployeeExperienceDetails experienceDetails) {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_EXPERIENCE, KEY_ID + " = ?",
                new String[]{String.valueOf(experienceDetails.getId())});
        db.close();
    }

    public void deleteEmployeeSkillDetail(EmployeeSkillDetails skillDetails) {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_SKILLS, KEY_ID + " = ?",
                new String[]{String.valueOf(skillDetails.getId())});
        db.close();
    }

    public void deleteAllEmployeeExperienceDetails() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_EXPERIENCE, null, null);
        db.close();
    }

    public void deleteAllEmployeeEducationDetails() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_EDUCATION, null, null);
        db.close();
    }

    public void deleteAllEmployeeSkillDetails() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_SKILLS, null, null);
        db.close();
    }

    public void deleteAllEmployeePersonalDetails() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_PERSONAL_DETAILS, null, null);
        db.close();
    }

    public void deleteAllEmployeeLeaveDetails() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_LEAVE_DETAILS, null, null);
        db.close();
    }

    // Getting experience Count
    public int getEmployeeExperienceDetailssCount() {
        String countQuery = "SELECT  * FROM " + TABLE_EXPERIENCE;
        SQLiteDatabase db = this.getReadableDatabase();
        Cursor cursor = db.rawQuery(countQuery, null);
        cursor.close();
        // return count
        return cursor.getCount();
    }
}

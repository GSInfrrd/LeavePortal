package utilities;

import android.content.ContentValues;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

import java.util.ArrayList;
import java.util.List;

import models.EmployeeExperienceDetails;

/**
 * Created by Anuj Dutt on 1/2/2017.
 *
 * Manage local database interactions
 */

public class DatabaseHandler extends SQLiteOpenHelper {
    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_NAME = "LeaveManagmentSystem";
    private static final String TABLE_SKILLS = "EmployeeSkills";
    private static final String TABLE_EXPERIENCE = "EmployeeExperienceDetails";
    private static final String KEY_ID = "id";
    private static final String KEY_REF_EMP_ID = "RefemployeeId";
    private static final String KEY_SKILL = "Skill";

    private static final String KEY_COMPANY_NAME = "CompanyName";
    private static final String KEY_ROLE = "Role";
    private static final String KEY_FROM_DATE = "FromDate";
    private static final String KEY_TO_DATE = "ToDate";

    public DatabaseHandler(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        //3rd argument to be passed is CursorFactory instance
    }

    // Creating Tables
    @Override
    public void onCreate(SQLiteDatabase db) {
        String CREATE_EXPERIENCE_TABLE = "CREATE TABLE " + TABLE_EXPERIENCE + "("
                + KEY_ID + " INTEGER PRIMARY KEY," +  KEY_COMPANY_NAME + " TEXT," + KEY_ROLE + " TEXT,"
                + KEY_FROM_DATE + " TEXT,"
                + KEY_TO_DATE + " TEXT"+ ")";
        db.execSQL(CREATE_EXPERIENCE_TABLE);
    }


    // Upgrading database
    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // Drop older table if existed
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXPERIENCE);
        // Create tables again
        onCreate(db);
    }

    public void addEmployeeExperienceDetails(EmployeeExperienceDetails experienceDetails, int refEmployeeId) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(KEY_ID, experienceDetails.getId());
        values.put(KEY_REF_EMP_ID, refEmployeeId);
        values.put(KEY_COMPANY_NAME, experienceDetails.getCompany());
        values.put(KEY_ROLE, experienceDetails.getRole());
        values.put(KEY_TO_DATE, experienceDetails.getTimePeriod());
        values.put(KEY_FROM_DATE, experienceDetails.getTimePeriod());

        db.insert(TABLE_EXPERIENCE, null, values);
        //2nd argument is String containing nullColumnHack
        db.close();
    }

    public void dropTables()
    {
        SQLiteDatabase db = this.getWritableDatabase();
        db.execSQL("DROP TABLE IF EXISTS " + TABLE_EXPERIENCE);
    }

    public void addEmployeeExperienceDetailsList(List<EmployeeExperienceDetails> experienceDetailList) {
        SQLiteDatabase db = this.getWritableDatabase();
        for(EmployeeExperienceDetails experienceDetails : experienceDetailList) {
            ContentValues values = new ContentValues();
            values.put(KEY_ID, experienceDetails.getId());
            values.put(KEY_COMPANY_NAME, experienceDetails.getCompany());
            values.put(KEY_ROLE, experienceDetails.getRole());
            values.put(KEY_TO_DATE, experienceDetails.getTimePeriod());
            values.put(KEY_FROM_DATE, experienceDetails.getTimePeriod());

            db.insert(TABLE_EXPERIENCE, null, values);
        }
        //2nd argument is String containing nullColumnHack
        db.close();
    }

    public List<EmployeeExperienceDetails> getAllEmployeeExperienceDetails() {

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
                employeeExperienceDetails.setTimePeriod(cursor.getString(4));
                employeeExperienceDetailsList.add(employeeExperienceDetails);
            } while (cursor.moveToNext());
        }
        return employeeExperienceDetailsList;
    }

    public EmployeeExperienceDetails getEmployeeExperienceDetailsById(int id) {
        SQLiteDatabase db = this.getReadableDatabase();
       EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
        Cursor cursor = db.query(TABLE_EXPERIENCE, new String[] { KEY_ID, KEY_COMPANY_NAME,
                        KEY_ROLE, KEY_TO_DATE }, KEY_ID + "=?",
                new String[] { String.valueOf(id) }, null, null, null, null);
        if (cursor != null)
            cursor.moveToFirst();

        employeeExperienceDetails.setId(Integer.parseInt(cursor.getString(0)));
        employeeExperienceDetails.setCompany(cursor.getString(1));
        employeeExperienceDetails.setRole(cursor.getString(2));
        employeeExperienceDetails.setTimePeriod(cursor.getString(3));
        return employeeExperienceDetails;
    }



    // code to update the single contact
    public int updateEmployeeExperienceDetails(EmployeeExperienceDetails employeeExperienceDetails) {
        SQLiteDatabase db = this.getWritableDatabase();

        ContentValues values = new ContentValues();
        values.put(KEY_ID, employeeExperienceDetails.getId());
        //values.put(KEY_REF_EMP_ID, employeeExperienceDetails.get());

        // updating row
        return db.update(TABLE_EXPERIENCE, values, KEY_ID + " = ?",
                new String[] { String.valueOf(employeeExperienceDetails.getId()) });
    }

    // Deleting single experience
    public void deleteEmployeeExperienceDetails(EmployeeExperienceDetails experienceDetails) {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_EXPERIENCE, KEY_ID + " = ?",
                new String[] { String.valueOf(experienceDetails.getId()) });
        db.close();
    }

    // Deleting single experience
    public void deleteAllEmployeeExperienceDetails() {
        SQLiteDatabase db = this.getWritableDatabase();
        db.delete(TABLE_EXPERIENCE, null, null);
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

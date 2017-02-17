package ai.infrrd.leavemanagementsystem.rest;

import java.util.List;

import ai.infrrd.leavemanagementsystem.models.CompanyDetailsResponse;
import ai.infrrd.leavemanagementsystem.models.EmployeeDetailsModel;
import ai.infrrd.leavemanagementsystem.models.EmployeeEducationDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeExperienceDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeLeaveMasterDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeLeaveTransactionModel;
import ai.infrrd.leavemanagementsystem.models.EmployeeSkillDetails;
import ai.infrrd.leavemanagementsystem.models.HolidayModel;
import ai.infrrd.leavemanagementsystem.models.LeaveHistoryModel;
import ai.infrrd.leavemanagementsystem.models.LeaveReportModel;
import ai.infrrd.leavemanagementsystem.models.LeaveTransactionResponse;
import ai.infrrd.leavemanagementsystem.models.Profile;
import ai.infrrd.leavemanagementsystem.models.WorkFromHomeModel;
import retrofit.Call;
import retrofit.http.Body;
import retrofit.http.GET;
import retrofit.http.POST;
import retrofit.http.Query;

/**
 * Created by Anuj Dutt on 1/3/2017.
 * Declare the API endpoints
 */

public interface ApiInterface {

    @GET("Holiday/GetHolidayList")
    Call<List<HolidayModel>> getHolidayList();

    @GET("Profile/getProfileImage/")
    Call<String> getProfileImage(@Query("empId") int empId);

    @GET("Account/GetUserDetails/")
    Call<EmployeeDetailsModel> getUserDetails(@Query("empId") int empId);

    @GET("EmployeeLeaveTrans/Get/")
    Call<List<LeaveHistoryModel>> getLeaveDetails(@Query("id") int id, @Query("leaveType") int leaveType, @Query("month") int month, @Query("transactionType") int transactionType);

    @GET("Profile/GetUserProfileDetails/")
    Call<EmployeeDetailsModel> getUserProfileDetails(@Query("empId") int empId);

    @GET("/v1/companies/suggest")
    Call<List<CompanyDetailsResponse>> getCompanyDetails(@Query("query") String query);

    @GET("Account/Login")
    Call<Profile> login(@Query("username") String username, @Query("password") String password);

    @GET("AddLeave/checkLeaveAvailabilityAndroid")
    Call<LeaveTransactionResponse> checkLeaveAvailabilityAndroid(@Query("employeeId") int employeeId, @Query("fromDateLong") long fromDate, @Query("toDateLong") long toDate, @Query("leaveType") int leaveType);

    @GET("employeeleavetrans/applyleave")
    Call<Boolean> applyLeave(@Query("id") int employeeId, @Query("leaveType") int leaveType, @Query("fromDate") String fromDate, @Query("toDate") String toDate, @Query("comments") String comments, @Query("workingDays") double workingDays);

    @POST("Profile/Post/")
    Call<Boolean> savePersonalDetails(@Body EmployeeDetailsModel model);

    @POST("Profile/EditEmployeeExperienceDetails/")
    Call<Boolean> saveExperienceDetails(@Body List<EmployeeExperienceDetails> experienceDetails, @Query("employeeId") int employeeId);

    @POST("Profile/EditEmployeeEducationDetails/")
    Call<Boolean> saveEducationDetails(@Body List<EmployeeEducationDetails> educationDetails, @Query("employeeId") int employeeId);

    @POST("Profile/EditEmployeeSkills/")
    Call<Boolean> saveSkillDetails(@Body List<EmployeeSkillDetails> skills, @Query("employeeId") int employeeId);

    @GET("workfromhome/GetWorkFromHomeReasonsList/")
    Call<List<WorkFromHomeModel>> getWorkFromHomeReasonsList();

    @GET("account/GetLeaveReportDetails")
    Call<LeaveReportModel> getLeaveReportDetails(@Query("empId") int empId, @Query("year") int year, @Query("leaveType") int leaveType );

    @POST("workfromhome/AddNewWorkFromHome")
    Call<Long> addNewWorkFromHome(@Body WorkFromHomeModel model);

    @GET("workfromhome/GetWorkFromHomeList/")
    Call<List<WorkFromHomeModel>> getWorkFromHomeList(@Query("EmpId") int empId);

    @GET("employeeleavetrans/GetEmployeeLeaveMasterDetails/")
    Call<EmployeeLeaveMasterDetails> getEmployeeLeaveMasterDetails(@Query("employeeId") int empId);
}
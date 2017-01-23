package rest;

import java.util.Date;
import java.util.List;

import models.CompanyDetailsResponse;
import models.EmployeeDetailsModel;
import models.EmployeeExperienceDetails;
import models.EmployeeLeaveTransactionModel;
import models.EmployeeSkillDetails;
import models.HolidayModel;
import models.LeaveHistoryModel;
import models.LeaveTransactionResponse;
import models.Profile;
import models.WorkFromHomeModel;
import retrofit.Call;
import retrofit.http.Body;
import retrofit.http.Field;
import retrofit.http.FormUrlEncoded;
import retrofit.http.GET;
import retrofit.http.POST;
import retrofit.http.Query;

/**
 * Created by Anuj Dutt on 1/3/2017.
 * Declare the API endpoints
 */

public interface ApiInterface {

    @POST("WorkFromHome/AddNewWorkFromHome")
    Call<WorkFromHomeModel> addNewWorkFromHome(@Body WorkFromHomeModel workFromHomeModel);

    @GET("Holiday/GetHolidayList")
    Call<List<HolidayModel>> getHolidayList();

    @GET("Profile/getProfileImage/")
    Call<String> getProfileImage(@Query("empId") int empId);

    @GET("Account/GetUserDetails/")
    Call<EmployeeDetailsModel> getUserDetails(@Query("empId") int empId);

    @GET("EmployeeLeaveTrans/Get/")
    Call<List<LeaveHistoryModel>> getLeaveDetails(@Query("id") int id);

    @GET("Profile/GetUserProfileDetails/")
    Call<EmployeeDetailsModel> getUserProfileDetails(@Query("empId") int empId);

    @GET("/v1/companies/suggest")
    Call<List<CompanyDetailsResponse>> getCompanyDetails(@Query("query") String query);

    @GET("Account/Login")
    Call<Profile> login(@Query("username") String username, @Query("password") String password);

    @GET("AddLeave/checkLeaveAvailabilityAndroid")
    Call<LeaveTransactionResponse> checkLeaveAvailabilityAndroid(@Query("employeeId") int employeeId, @Query("fromDateLong") long fromDate, @Query("toDateLong") long toDate, @Query("leaveType") int leaveType);

    @GET("employeeleavetrans/applyleave")
    Call<EmployeeLeaveTransactionModel> applyLeave(@Query("id") int employeeId, @Query("leaveType") int leaveType, @Query("fromDate") String fromDate, @Query("toDate") String toDate, @Query("comments") String comments, @Query("workingDays") double workingDays);

    @POST("Profile/Post/")
    Call<Boolean> savePersonalDetails(@Body EmployeeDetailsModel model);

    @POST("Profile/EditEmployeeExperienceDetails/")
    Call<Boolean> saveExperienceDetails(@Body List<EmployeeExperienceDetails> experienceDetails, @Query("employeeId") int employeeId);

    @POST("Profile/EditEmployeeSkills/")
    Call<Boolean> saveSkillDetails(@Body List<EmployeeSkillDetails> skills, @Query("employeeId") int employeeId);

    @GET("workfromhome/GetWorkFromHomeReasonsList/")
    Call<List<WorkFromHomeModel>> getWorkFromHomeReasonsList();
}
package rest;

import java.util.List;

import models.CompanyDetailsResponse;
import models.EmployeeDetailsModel;
import models.HolidayModel;
import models.LeaveHistoryModel;
import models.Profile;
import models.WorkFromHomeModel;
import retrofit.Call;
import retrofit.http.Body;
import retrofit.http.GET;
import retrofit.http.POST;
import retrofit.http.Path;
import retrofit.http.Query;
import retrofit.http.Url;

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
}

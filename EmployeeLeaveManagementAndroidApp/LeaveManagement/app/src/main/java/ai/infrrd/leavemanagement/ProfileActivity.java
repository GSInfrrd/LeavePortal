package ai.infrrd.leavemanagement;

import android.content.Intent;
import android.net.Uri;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLDecoder;
import java.util.Date;

import models.*;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.DatabaseHandler;
import utilities.DateDeserializer;
import utilities.EducationAdapter;
import utilities.ExperienceAdapter;
import utilities.SharedPreferenceManager;
import utilities.SkillsAdapter;

public class ProfileActivity extends AppCompatActivity {

    RecyclerView mExperienceRecyclerView = null;
    RecyclerView mEducationRecyclerView = null;
    RecyclerView mSkillsRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;
    String LOG_TAG = "";
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        LOG_TAG = getClass().getSimpleName();

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setTitle(SharedPreferenceManager.getPreference(this, getString(R.string.user_preferences), getString(R.string.user_name), getString(R.string.user_name)));
        String userId = SharedPreferenceManager.getPreference(ProfileActivity.this,getString(R.string.user_preferences),getString(R.string.emp_id),getString(R.string.emp_id));

        mExperienceRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_experience);
        mExperienceRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(ProfileActivity.this);
        mExperienceRecyclerView.setLayoutManager(mLayoutManager);
        mSkillsRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_skills);
        mSkillsRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(ProfileActivity.this);
        mSkillsRecyclerView.setLayoutManager(mLayoutManager);
        mEducationRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_education);
        mEducationRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(ProfileActivity.this);
        mEducationRecyclerView.setLayoutManager(mLayoutManager);

        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<EmployeeDetailsModel> call = apiService.getUserProfileDetails(Integer.parseInt(userId));

        final DatabaseHandler databaseHandler = new DatabaseHandler(ProfileActivity.this);
        //databaseHandler.dropTables();

        call.enqueue(new Callback<EmployeeDetailsModel>() {
            @Override
            public void onResponse(Response<EmployeeDetailsModel> response, Retrofit retrofit) {
                Log.i(LOG_TAG,"Fetched user profile details");
                EmployeeDetailsModel employeeDetailsModel = response.body();

                ExperienceAdapter experienceAdapter = new ExperienceAdapter(employeeDetailsModel.getEmployeeExperienceDetails(),false,R.layout.experience_recyclerview_item, ProfileActivity.this);
                mExperienceRecyclerView.setAdapter(experienceAdapter);
                databaseHandler.deleteAllEmployeeExperienceDetails();
                databaseHandler.addEmployeeExperienceDetailsList(employeeDetailsModel.getEmployeeExperienceDetails());

                SkillsAdapter skillsAdapter = new SkillsAdapter(employeeDetailsModel.getSkills());
                mSkillsRecyclerView.setAdapter(skillsAdapter);


                EducationAdapter educationAdapter = new EducationAdapter(employeeDetailsModel.getEmployeeEducationDetails());
                mEducationRecyclerView.setAdapter(educationAdapter);

            }
            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message),t);
            }
        });

    }

    public void editDetailsListener(View view)
    {
        Intent intent = new Intent(ProfileActivity.this,EditActivity.class);
        intent.putExtra("viewId", view.getId());
        startActivity(intent);
    }
}
package ai.infrrd.leavemanagementsystem;

import android.content.Intent;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

import models.EmployeeDetailsModel;
import models.EmployeePersonalDetails;
import models.EmployeeSkillDetails;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.Constants;
import utilities.DatabaseHandler;
import utilities.EducationAdapter;
import utilities.ExperienceAdapter;
import utilities.SharedPreferenceManager;
import utilities.SkillsAdapter;
import utilities.Utilities;

public class ProfileActivity extends AppCompatActivity {

    private RecyclerView mExperienceRecyclerView = null;
    private RecyclerView mEducationRecyclerView = null;
    private RecyclerView mSkillsRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;
    private TextView mEmailTextview = null;
    private TextView mBirthdayTextview = null;
    private TextView mCityTextview = null;
    private TextView mInterestsTextview = null;
    private TextView mPhoneTextview = null;
    private TextView mWebsiteTextview = null;
    private String LOG_TAG = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        LOG_TAG = getClass().getSimpleName();

        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setTitle(SharedPreferenceManager.getPreference(this, getString(R.string.user_preferences), getString(R.string.user_name), getString(R.string.user_name)));
        String userId = SharedPreferenceManager.getPreference(ProfileActivity.this, getString(R.string.user_preferences), getString(R.string.emp_id), getString(R.string.emp_id));

        mEmailTextview = (TextView) findViewById(R.id.profile_email);
        mBirthdayTextview = (TextView) findViewById(R.id.profile_birthday);
        mCityTextview = (TextView) findViewById(R.id.profile_city);
        mInterestsTextview = (TextView) findViewById(R.id.profile_interests);
        mPhoneTextview = (TextView) findViewById(R.id.profile_phone);
        mWebsiteTextview = (TextView) findViewById(R.id.profile_website);

        mExperienceRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_experience);
        mExperienceRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(ProfileActivity.this);
        mExperienceRecyclerView.setLayoutManager(mLayoutManager);
        mSkillsRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_skills);
        mSkillsRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(ProfileActivity.this, LinearLayoutManager.HORIZONTAL, false);
        mSkillsRecyclerView.setLayoutManager(mLayoutManager);
        mEducationRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_education);
        mEducationRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(ProfileActivity.this);
        mEducationRecyclerView.setLayoutManager(mLayoutManager);

        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<EmployeeDetailsModel> call = apiService.getUserProfileDetails(Integer.parseInt(userId));


        //databaseHandler.dropTables();

        call.enqueue(new Callback<EmployeeDetailsModel>() {
            @Override
            public void onResponse(Response<EmployeeDetailsModel> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched user profile details");
                EmployeeDetailsModel employeeDetailsModel = response.body();
                List<EmployeeSkillDetails> employeeSkillDetailsList = new ArrayList<EmployeeSkillDetails>();
                mEmailTextview.setText(employeeDetailsModel.getEmail());
                mBirthdayTextview.setText(employeeDetailsModel.getDateOfBirthAsString());
                mCityTextview.setText(employeeDetailsModel.getCity());
                mInterestsTextview.setText(employeeDetailsModel.getBio());
                mPhoneTextview.setText(employeeDetailsModel.getTelephone());
                mWebsiteTextview.setText("FIX THIS");
                ExperienceAdapter experienceAdapter = new ExperienceAdapter(employeeDetailsModel.getEmployeeExperienceDetails(), false, R.layout.experience_recyclerview_item, ProfileActivity.this);
                mExperienceRecyclerView.setAdapter(experienceAdapter);
                for(EmployeeSkillDetails employeeSkillDetails: employeeDetailsModel.getSkills())
                {
                    if(employeeSkillDetails.isSelected())
                    {
                        employeeSkillDetailsList.add(employeeSkillDetails);
                    }
                }
                SkillsAdapter skillsAdapter = new SkillsAdapter(ProfileActivity.this,false, R.layout.skills_recycler_view_item ,employeeSkillDetailsList);
                mSkillsRecyclerView.setAdapter(skillsAdapter);
                EducationAdapter educationAdapter = new EducationAdapter(employeeDetailsModel.getEmployeeEducationDetails(),false,R.layout.education_recyclerview_item, ProfileActivity.this);
                mEducationRecyclerView.setAdapter(educationAdapter);
                insertDetailsIntoLocalDatabase(employeeDetailsModel);
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                Utilities.showInfoSnackbar(mEmailTextview, getString(R.string.retrofit_error_message), 1000);
            }
        });
    }
    public void editDetailsListener(View view)
    {
        Intent intent = null;
        switch (view.getId())
        {
            case R.id.edit_personal:
                intent = new Intent(ProfileActivity.this,AddActivity.class);
                intent.putExtra("type", Constants.DETAILS_PERSONAL_DETAILS);
                break;
            case R.id.edit_skill:
                intent = new Intent(ProfileActivity.this,AddActivity.class);
                intent.putExtra("type", Constants.DETAILS_SKILLS_DETAILS);
                break;
            default:
                intent = new Intent(ProfileActivity.this,EditActivity.class);
        }

        intent.putExtra("viewId", view.getId());
        startActivity(intent);
    }

    /**
     * Insert employee details into the database for editing later.
     * @param employeeDetailsModel
     */
    public void insertDetailsIntoLocalDatabase(EmployeeDetailsModel employeeDetailsModel)
    {
        final DatabaseHandler databaseHandler = new DatabaseHandler(ProfileActivity.this);
        EmployeePersonalDetails employeePersonalDetails = new EmployeePersonalDetails();
        employeePersonalDetails.setFirstName(employeeDetailsModel.getFirstName());
        employeePersonalDetails.setLastName(employeeDetailsModel.getLastName());
        employeePersonalDetails.setDateOfBirth(employeeDetailsModel.getDateOfBirth());
        employeePersonalDetails.setPhoneNumber(employeeDetailsModel.getTelephone());
        employeePersonalDetails.setCity(employeeDetailsModel.getCity());
        employeePersonalDetails.setCountry(employeeDetailsModel.getCountry());
        employeePersonalDetails.setTwitter(employeeDetailsModel.getTwitterLink());
        employeePersonalDetails.setFacebook(employeeDetailsModel.getFacebookLink());
        employeePersonalDetails.setGooglePlus(employeeDetailsModel.getGooglePlusLink());
        databaseHandler.deleteAllEmployeeExperienceDetails();
        databaseHandler.deleteAllEmployeeEducationDetails();
        databaseHandler.deleteAllEmployeeSkillDetails();
        databaseHandler.deleteAllEmployeePersonalDetails();
        databaseHandler.addEmployeeExperienceDetailsList(employeeDetailsModel.getEmployeeExperienceDetails());
        databaseHandler.addEmployeeEducationDetailsList(employeeDetailsModel.getEmployeeEducationDetails());
        databaseHandler.addEmployeeSkillsList(employeeDetailsModel.getSkills());
        databaseHandler.addEmployeePersonalDetails(employeePersonalDetails);
    }
}
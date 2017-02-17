package ai.infrrd.leavemanagementsystem;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Base64;
import android.util.Log;
import android.view.View;
import android.widget.TextView;

import com.bumptech.glide.Glide;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;

import ai.infrrd.leavemanagementsystem.adapters.EducationAdapter;
import ai.infrrd.leavemanagementsystem.adapters.ExperienceAdapter;
import ai.infrrd.leavemanagementsystem.adapters.SkillsAdapter;
import ai.infrrd.leavemanagementsystem.models.EmployeeDetailsModel;
import ai.infrrd.leavemanagementsystem.models.EmployeePersonalDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeSkillDetails;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import de.hdodenhof.circleimageview.CircleImageView;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

public class ProfileActivity extends AppCompatActivity {

    private RecyclerView mExperienceRecyclerView = null;
    private RecyclerView mEducationRecyclerView = null;
    private RecyclerView mSkillsRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;
    private TextView mDobTextview = null;
    private TextView mMobileNumberTextview = null;
    private TextView mCityTextview = null;
    private TextView mCountryTextview = null;
    private TextView mTwitterTextview = null;
    private TextView mFacebookTextview = null;
    private TextView mGooglePlusTextview = null;
    private CircleImageView mEmployeeImage = null;
    private String LOG_TAG = "";
    private String mUserId = "";
    private String mUserImage = "";
    private ProgressDialog mProgressDialog = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_profile);
        initialiseUIComponents();
        getUserProfileDetails();

    }

    public void editDetailsListener(View view) {
        Intent intent = null;
        switch (view.getId()) {
            case R.id.edit_personal:
                intent = new Intent(ProfileActivity.this, AddActivity.class);
                intent.putExtra("type", Constants.DETAILS_PERSONAL_DETAILS);
                break;
            case R.id.edit_skill:
                intent = new Intent(ProfileActivity.this, AddActivity.class);
                intent.putExtra("type", Constants.DETAILS_SKILLS_DETAILS);
                break;
            default:
                intent = new Intent(ProfileActivity.this, EditActivity.class);
        }

        intent.putExtra("viewId", view.getId());
        startActivity(intent);
    }

    /**
     * Insert employee details into the database for editing later.
     *
     * @param employeeDetailsModel
     */
    public void insertDetailsIntoLocalDatabase(EmployeeDetailsModel employeeDetailsModel) {
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

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        LOG_TAG = getClass().getSimpleName();
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        SharedPreferences userPreferences = ProfileActivity.this.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        getSupportActionBar().setTitle(userPreferences.getString(Constants.PreferenceConstants.PREFS_USER_NAME, "Infrrd Employee"));
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mEmployeeImage = (CircleImageView) findViewById(R.id.toolbarImage);
        mDobTextview = (TextView) findViewById(R.id.profile_dob);
        mMobileNumberTextview = (TextView) findViewById(R.id.profile_mobile_number);
        mCityTextview = (TextView) findViewById(R.id.profile_city);
        mCountryTextview = (TextView) findViewById(R.id.profile_country);
        mTwitterTextview = (TextView) findViewById(R.id.profile_twitter);
        mFacebookTextview = (TextView) findViewById(R.id.profile_facebook);
        mGooglePlusTextview = (TextView) findViewById(R.id.profile_google_plus);
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
        mUserImage = userPreferences.getString(Constants.PreferenceConstants.PREFS_USER_IMAGE, null);
        if (mUserImage != null) {
            byte[] imageByteArray = Base64.decode(mUserImage, Base64.DEFAULT);
            Glide.with(ProfileActivity.this)
                    .load(imageByteArray)
                    .asBitmap()
                    .placeholder(R.drawable.default_image).fitCenter()
                    .into(mEmployeeImage);
        }
    }

    /**
     * Gets the user profile details.
     */
    public void getUserProfileDetails() {
        mProgressDialog = Utilities.showProgressDialog(ProfileActivity.this, getString(R.string.fetching_profile_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<EmployeeDetailsModel> call = apiService.getUserProfileDetails(Integer.parseInt(mUserId));
        call.enqueue(new Callback<EmployeeDetailsModel>() {
            @Override
            public void onResponse(Response<EmployeeDetailsModel> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched user profile details");
                SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMM dd, yyyy");
                EmployeeDetailsModel employeeDetailsModel = response.body();
                List<EmployeeSkillDetails> employeeSkillDetailsList = new ArrayList<EmployeeSkillDetails>();
                mDobTextview.setText(simpleDateFormat.format(employeeDetailsModel.getDateOfBirth()));
                mMobileNumberTextview.setText(employeeDetailsModel.getTelephone());
                mCityTextview.setText(employeeDetailsModel.getCity());
                mCountryTextview.setText(employeeDetailsModel.getCountry());
                mTwitterTextview.setText(employeeDetailsModel.getTwitterLink());
                mFacebookTextview.setText(employeeDetailsModel.getFacebookLink());
                mGooglePlusTextview.setText(employeeDetailsModel.getGooglePlusLink());
                ExperienceAdapter experienceAdapter = new ExperienceAdapter(employeeDetailsModel.getEmployeeExperienceDetails(), false, R.layout.experience_recyclerview_item, ProfileActivity.this);
                mExperienceRecyclerView.setAdapter(experienceAdapter);
                for (EmployeeSkillDetails employeeSkillDetails : employeeDetailsModel.getSkills()) {
                    if (employeeSkillDetails.isSelected()) {
                        employeeSkillDetailsList.add(employeeSkillDetails);
                    }
                }
                SkillsAdapter skillsAdapter = new SkillsAdapter(ProfileActivity.this, false, R.layout.skills_recycler_view_item, employeeSkillDetailsList);
                mSkillsRecyclerView.setAdapter(skillsAdapter);
                EducationAdapter educationAdapter = new EducationAdapter(employeeDetailsModel.getEmployeeEducationDetails(), false, R.layout.education_recyclerview_item, ProfileActivity.this);
                mEducationRecyclerView.setAdapter(educationAdapter);
                insertDetailsIntoLocalDatabase(employeeDetailsModel);
                mProgressDialog.hide();
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                ConstructAlertDialogs.errorAlertDialog(ProfileActivity.this, getString(R.string.retrofit_error_message), false);
                mProgressDialog.hide();
            }
        });
    }
}
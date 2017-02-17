package ai.infrrd.leavemanagementsystem;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.util.Base64;
import android.util.Log;
import android.view.MenuItem;
import android.view.View;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bumptech.glide.Glide;

import ai.infrrd.leavemanagementsystem.fragments.ApplyLeave;
import ai.infrrd.leavemanagementsystem.fragments.ApplyWorkFromHome;
import ai.infrrd.leavemanagementsystem.fragments.Dashboard;
import ai.infrrd.leavemanagementsystem.fragments.LeaveHistory;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import de.hdodenhof.circleimageview.CircleImageView;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

public class NavigationActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    private Toolbar mToolbar = null;
    private CircleImageView mProfilePicture = null;
    private TextView mUserName = null;
    private String LOG_TAG = "";
    private String mUserId = "";
    private ProgressDialog mProgressDialog = null;
    private LinearLayout mLogOutLayout = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_navigation);
        initialiseUIComponents();
        setListeners();
        if (getFragmentManager().findFragmentById(R.id.content_frame) == null) {
            getSupportActionBar().setTitle(getString(R.string.dashboard));
            selectFragmentForNavigation(R.id.nav_dashboard);
        }
    }

    @Override
    public void onBackPressed() {
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawer(GravityCompat.START);
        } else {
            //
        }
    }

    @Override
    public boolean onNavigationItemSelected(MenuItem item) {

        selectFragmentForNavigation(item.getItemId());

        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        drawer.closeDrawer(GravityCompat.START);
        return true;
    }

    /**
     * @param resourceId Resource ID for the fragment to be loaded
     * @author Anuj Dutt
     */

    public void selectFragmentForNavigation(int resourceId) {
        android.support.v4.app.Fragment selectedFragment = null;
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        switch (resourceId) {
            case R.id.nav_dashboard:
                mToolbar.setTitle(getString(R.string.dashboard));
                selectedFragment = new Dashboard();
                break;
            case R.id.nav_apply_leave:
                mToolbar.setTitle(getString(R.string.apply_leave));
                selectedFragment = new ApplyLeave();
                break;
            case R.id.nav_apply_wfh:
                mToolbar.setTitle(getString(R.string.apply_wfh));
                selectedFragment = new ApplyWorkFromHome();
                break;
            case R.id.nav_leave_history:
                mToolbar.setTitle(getString(R.string.leave_history));
                selectedFragment = new LeaveHistory();
                break;
            default:
                mToolbar.setTitle(getString(R.string.dashboard));
                selectedFragment = new Dashboard();
        }

        fragmentTransaction.replace(R.id.content_frame, selectedFragment);
        fragmentTransaction.commit();
        drawer.closeDrawer(GravityCompat.START);
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        SharedPreferences userPreferences = NavigationActivity.this.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mToolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(mToolbar);
        LOG_TAG = getClass().getSimpleName();
        DrawerLayout drawer = (DrawerLayout) findViewById(R.id.drawer_layout);
        ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(
                this, drawer, mToolbar, R.string.navigation_drawer_open, R.string.navigation_drawer_close);
        drawer.setDrawerListener(toggle);
        toggle.syncState();
        NavigationView navigationView = (NavigationView) findViewById(R.id.nav_view);
        navigationView.setNavigationItemSelectedListener(this);
        mProfilePicture = (CircleImageView) navigationView.getHeaderView(0).findViewById(R.id.profile_image);
        mUserName = (TextView) navigationView.getHeaderView(0).findViewById(R.id.nav_header_user_name);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mUserName.setText(userPreferences.getString(Constants.PreferenceConstants.PREFS_USER_NAME, "Infrrd Employee"));
        mLogOutLayout = (LinearLayout) navigationView.findViewById(R.id.layout_log_out);
    }

    /**
     * Set listeners for relevant UI components.
     */
    public void setListeners() {
        mProfilePicture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(NavigationActivity.this, ProfileActivity.class);
                startActivity(intent);
            }
        });
        mLogOutLayout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                SharedPreferences userPreferences = getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER
                        , MODE_PRIVATE);
                userPreferences.edit().clear().apply();
                Intent loginIntent = new Intent(NavigationActivity.this, LoginActivity.class);
                startActivity(loginIntent);
            }
        });
    }

    /**
     * Get user profile image from Db.
     */
    public void getProfileImage() {
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<String> call = apiService.getProfileImage(Integer.parseInt(mUserId));
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Response<String> response, Retrofit retrofit) {
                String imageByteString = response.body();
                if (imageByteString != null) {

                    SharedPreferences userPreferences = NavigationActivity.this.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
                    userPreferences.edit().putString(Constants.PreferenceConstants.PREFS_USER_IMAGE, imageByteString).apply();
                    byte[] imageByteArray = Base64.decode(imageByteString, Base64.DEFAULT);
                    Glide.with(NavigationActivity.this)
                            .load(imageByteArray)
                            .asBitmap()
                            .placeholder(R.drawable.default_image).fitCenter()
                            .into(mProfilePicture);
                }
                Log.d("TAG", "Image received");
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, "failure", t);
            }
        });
    }
}

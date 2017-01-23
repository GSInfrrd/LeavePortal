package ai.infrrd.leavemanagementsystem;

import android.content.Intent;
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
import android.widget.TextView;

import com.bumptech.glide.Glide;

import de.hdodenhof.circleimageview.CircleImageView;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.SharedPreferenceManager;

public class NavigationActivity extends AppCompatActivity
        implements NavigationView.OnNavigationItemSelectedListener {

    private Toolbar mToolbar = null;
    private CircleImageView mProfilePicture = null;
    private TextView mUserName = null;
    private String LOG_TAG = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_navigation);

        SharedPreferenceManager.addPreference(NavigationActivity.this, getString(R.string.user_preferences), getString(R.string.user_name), "Pruthvi");
        SharedPreferenceManager.addPreference(NavigationActivity.this, getString(R.string.user_preferences), getString(R.string.emp_id), "3");
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
        mProfilePicture.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Intent intent = new Intent(NavigationActivity.this, ProfileActivity.class);
                startActivity(intent);
            }
        });
        mUserName.setText(SharedPreferenceManager.getPreference(this, getString(R.string.user_preferences), getString(R.string.user_name), getString(R.string.user_name)));
        String userId = SharedPreferenceManager.getPreference(NavigationActivity.this,getString(R.string.user_preferences),getString(R.string.emp_id),getString(R.string.emp_id));

        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<String> call = apiService.getProfileImage(Integer.parseInt(userId));

        call.enqueue(new Callback<String>() {

            @Override
            public void onResponse(Response<String> response, Retrofit retrofit) {
                String imageByteString = response.body();
                byte[] imageByteArray = Base64.decode(imageByteString, Base64.DEFAULT);

                Log.i("TAG","IMAGE");
                Log.i("TAG",imageByteArray.toString());
                Glide.with(NavigationActivity.this)
                        .load(imageByteArray)
                        .asBitmap()
                        .placeholder(R.drawable.default_image).fitCenter()
                        .into(mProfilePicture);

                Log.d("TAG", "Image received");
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, "failure",t);
            }
        });



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
            super.onBackPressed();
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
}

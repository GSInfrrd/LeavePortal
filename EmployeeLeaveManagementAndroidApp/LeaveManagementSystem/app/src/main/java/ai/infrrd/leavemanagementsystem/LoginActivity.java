package ai.infrrd.leavemanagementsystem;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;

import models.Profile;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.SharedPreferenceManager;
import utilities.Utilities;

public class LoginActivity extends AppCompatActivity {
    private GoogleApiClient mGoogleApiClient;
    private GoogleSignInOptions mGoogleSignInOptions;
    private ImageView mInfrrdLogoImageview = null;
    private Animation mAnimRotate = null;
    private View mRootView = null;
    private static String LOG_TAG = "";
    private final int RC_SIGN_IN = 5200;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_login);
        LOG_TAG = getClass().getSimpleName();
        mRootView = (View) findViewById(android.R.id.content);
        mInfrrdLogoImageview = (ImageView) findViewById(R.id.infrrd_logo_imageview);
        mAnimRotate = AnimationUtils.loadAnimation(getApplicationContext(),
                R.anim.rotate);
        mAnimRotate.setAnimationListener(new Animation.AnimationListener() {
            @Override
            public void onAnimationStart(Animation animation) {

            }

            @Override
            public void onAnimationEnd(Animation animation) {

            }

            @Override
            public void onAnimationRepeat(Animation animation) {

            }
        });

        mGoogleSignInOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN).setHostedDomain(getString(R.string.infrrd_domain))
                .requestEmail().requestProfile().build();

        mGoogleApiClient = new GoogleApiClient.Builder(this)
                .enableAutoManage(this, new GoogleApiClient.OnConnectionFailedListener() {
                    @Override
                    public void onConnectionFailed(ConnectionResult connectionResult) {
                        Utilities.showInfoSnackbar(mRootView, getString(R.string.retrofit_error_message), 1000);
                    }
                })
                .addApi(Auth.GOOGLE_SIGN_IN_API, mGoogleSignInOptions)
                .build();
        signInUsingGoogle();
    }

    private void signInUsingGoogle() {

        Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(mGoogleApiClient);
        startActivityForResult(signInIntent, RC_SIGN_IN);
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == RC_SIGN_IN) {
            GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);
            handleSignInResult(result);
        }
    }

    private void handleSignInResult(GoogleSignInResult result) {
        if (result.isSuccess()) {
            mInfrrdLogoImageview.startAnimation(mAnimRotate);
            ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Call<Profile> getLoginDetails = apiInterface.login(result.getSignInAccount().getEmail(), "");
            getLoginDetails.enqueue(new Callback<Profile>() {
                @Override
                public void onResponse(Response<Profile> response, Retrofit retrofit) {
                    Profile profile = response.body();
                    if (profile.getRefEmployeeId() != 0) {
                        SharedPreferenceManager.addPreference(LoginActivity.this, getString(R.string.user_preferences), getString(R.string.user_name), profile.getUserName());
                        SharedPreferenceManager.addPreference(LoginActivity.this, getString(R.string.user_preferences), getString(R.string.emp_id), String.valueOf(profile.getRefEmployeeId()));
                    }
                    Intent intent = new Intent(LoginActivity.this, NavigationActivity.class);
                    startActivity(intent);
                }

                @Override
                public void onFailure(Throwable t) {
                    Log.i(LOG_TAG, getString(R.string.retrofit_error_message));
                    mInfrrdLogoImageview.clearAnimation();
                    Utilities.showInfoSnackbar(mRootView, getString(R.string.login_details_error), 2000);
                }
            });

        } else {
            signInUsingGoogle();
        }
    }
}

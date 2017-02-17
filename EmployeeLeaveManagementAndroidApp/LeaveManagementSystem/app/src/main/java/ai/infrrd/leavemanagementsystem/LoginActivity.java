package ai.infrrd.leavemanagementsystem;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.TextInputEditText;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.AppCompatButton;
import android.util.Log;
import android.util.Patterns;
import android.view.View;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.SignInButton;
import com.google.android.gms.common.api.GoogleApiClient;

import ai.infrrd.leavemanagementsystem.models.Profile;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

public class LoginActivity extends AppCompatActivity {
    private GoogleApiClient mGoogleApiClient;
    private GoogleSignInOptions mGoogleSignInOptions;
    private TextView mGoogleCredentialsTextview = null;
    private ImageView mInfrrdLogoImageview = null;
    private TextInputEditText mUsername = null;
    private TextInputEditText mPassword = null;
    private LinearLayout mUserInputsLayout = null;
    private Animation mAnimRotate = null;
    private AppCompatButton mLoginButton = null;
    private SignInButton mGoogleLoginButton = null;
    private View mRootView = null;
    private static String LOG_TAG = "";
    private final int RC_SIGN_IN = 5200;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_login);
        initialiseUIComponents();
        setListeners();
        checkPreviousLogin();

//        if (NetworkConnectivityChecker.isNetworkAvailable(LoginActivity.this)) {
//            signInUsingGoogle();
//        } else {
//            AlertDialog alertDialog = ConstructAlertDialogs.retryAlertDialog(LoginActivity.this, getString(R.string.no_internet_connection_error), getIntent());
//            alertDialog.show();
//        }
    }

    /**
     * Set listeners for components.
     */
    private void setListeners() {
        mLoginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (validateUserInput()) {
                    signInTheUser(mUsername.getText().toString(), mPassword.getText().toString());
                }
            }
        });
    }

    /**
     * Login the user.
     */
    private void signInTheUser(final String username, final String password) {
        mInfrrdLogoImageview.startAnimation(mAnimRotate);
        mGoogleCredentialsTextview.setVisibility(View.VISIBLE);
        mUserInputsLayout.setVisibility(View.INVISIBLE);
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<Profile> getLoginDetails = apiInterface.login(username, password);
        getLoginDetails.enqueue(new Callback<Profile>() {
            @Override
            public void onResponse(Response<Profile> response, Retrofit retrofit) {
                Profile profile = response.body();
                if (profile.getRefEmployeeId() != 0) {
                    initialisePreferences(profile.getUserName(), profile.getRefEmployeeId(), username, password);
                    Intent intent = new Intent(LoginActivity.this, NavigationActivity.class);
                    startActivity(intent);
                } else {
                    mGoogleCredentialsTextview.setVisibility(View.INVISIBLE);
                    mUserInputsLayout.setVisibility(View.VISIBLE);
                    mInfrrdLogoImageview.clearAnimation();
                    ConstructAlertDialogs.errorAlertDialog(LoginActivity.this, getString(R.string.wrong_login_credentials_error), false);
                }
            }

            @Override
            public void onFailure(Throwable t) {
                Log.i(LOG_TAG, getString(R.string.retrofit_error_message));
                mGoogleCredentialsTextview.setVisibility(View.INVISIBLE);
                mInfrrdLogoImageview.clearAnimation();
                mUserInputsLayout.setVisibility(View.VISIBLE);
                ConstructAlertDialogs.errorAlertDialog(LoginActivity.this, getString(R.string.login_details_error), false);
            }
        });
    }

    /**
     * Validate user input
     */
    private boolean validateUserInput() {
        boolean valid = true;
        if (mUsername.getText().toString().isEmpty()) {
            mUsername.setError(getString(R.string.please_enter_user_name));
            valid = false;
        } else {
            mUsername.setError(null);
        }

        if (mPassword.getText().toString().isEmpty()) {
            mPassword.setError(getString(R.string.please_enter_password));
            valid = false;
        } else {
            mPassword.setError(null);
        }

        if (!mUsername.getText().toString().isEmpty()) {
            if (!Patterns.EMAIL_ADDRESS.matcher(mUsername.getText().toString()).matches()) {
                mUsername.setError(getString(R.string.please_enter_user_name));
                valid = false;
            } else {
                mUsername.setError(null);
            }
        }
        return valid;
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        LOG_TAG = getClass().getSimpleName();
        mRootView = (View) findViewById(android.R.id.content);
        mInfrrdLogoImageview = (ImageView) findViewById(R.id.infrrd_logo_imageview);
        mGoogleCredentialsTextview = (TextView) findViewById(R.id.google_connection_indicator_textview);
        mUserInputsLayout = (LinearLayout) findViewById(R.id.layout_user_inputs);
        mUsername = (TextInputEditText) findViewById(R.id.text_input_username);
        mPassword = (TextInputEditText) findViewById(R.id.text_input_password);
        mLoginButton = (AppCompatButton) findViewById(R.id.sign_in_button);
        mGoogleLoginButton = (SignInButton) findViewById(R.id.google_sign_in_button);
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
    }

    /**
     * Initiate the google sign-in process.
     */
    private void signInUsingGoogle() {
        Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(mGoogleApiClient);
        mGoogleCredentialsTextview.setVisibility(View.VISIBLE);
        startActivityForResult(signInIntent, RC_SIGN_IN);
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == RC_SIGN_IN) {
            GoogleSignInResult result = Auth.GoogleSignInApi.getSignInResultFromIntent(data);
            mGoogleCredentialsTextview.setVisibility(View.GONE);
            handleSignInResult(result);
        }
    }

    /**
     * Handle the google sign-in result and proceed with application flow.
     *
     * @param result - The google sign-in result
     */
    private void handleSignInResult(GoogleSignInResult result) {
        if (result.isSuccess()) {
            signInTheUser(result.getSignInAccount().getEmail(), "");

        } else {
            signInUsingGoogle();
        }
    }

    /**
     * Put relevant shared preference values
     *
     * @param userName
     * @param employeeId
     */
    public void initialisePreferences(String userName, int employeeId, String userId, String password) {
        SharedPreferences userPreferences = LoginActivity.this.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        userPreferences.edit().putString(Constants.PreferenceConstants.PREFS_USER_NAME, userName).apply();
        userPreferences.edit().putString(Constants.PreferenceConstants.PREFS_PASSWORD, password).apply();
        userPreferences.edit().putString(Constants.PreferenceConstants.PREFS_USER_LOGIN_ID, userId).apply();
        userPreferences.edit().putString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, String.valueOf(employeeId)).apply();
        userPreferences.edit().putString(Constants.PreferenceConstants.PREFS_USER_IMAGE, "").apply();
        SharedPreferences permissionPreferences = LoginActivity.this.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_PERMISSION, Context.MODE_PRIVATE);
        permissionPreferences.edit().putBoolean(Constants.PreferenceConstants.PREFS_PERM_DENIED, true).apply();
    }

    public void checkPreviousLogin() {
        SharedPreferences userPreferences = LoginActivity.this.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        String username = userPreferences.getString(Constants.PreferenceConstants.PREFS_USER_LOGIN_ID, "");
        String password = userPreferences.getString(Constants.PreferenceConstants.PREFS_PASSWORD, "");

        if (!username.isEmpty()) {
            signInTheUser(username, password);
        }

    }
}

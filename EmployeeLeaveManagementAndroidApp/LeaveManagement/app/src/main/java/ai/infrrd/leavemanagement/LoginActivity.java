package ai.infrrd.leavemanagement;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.Toast;

import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.auth.api.signin.GoogleSignInResult;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.SignInButton;
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

    private static String LOG_TAG = "";
    private EditText mLoginId;
    private EditText mPassword;
    private Button mLoginButton;

    private GoogleApiClient mGoogleApiClient;
    private GoogleSignInOptions googleSignInOptions;
    private SignInButton mGoogleSignInButton;

    private final int RC_SIGN_IN = 5200;

    private Animation mAnimRotate = null;
    private View mRootView = null;
    private ImageView mInfrrdImageview = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_login);
        LOG_TAG = getLocalClassName().getClass().getSimpleName();
        mLoginButton = (Button) findViewById(R.id.btn_login);
        mGoogleSignInButton = (SignInButton) findViewById(R.id.btn_login_google);
        mRootView = (View) findViewById(android.R.id.content);
        mInfrrdImageview = (ImageView) findViewById(R.id.infrrd_logo_imageview);
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
        mLoginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onPressLoginButton(view);
            }
        });


        Utilities.setGooglePlusButtonText(mGoogleSignInButton, getString(R.string.login_google).toUpperCase());


        googleSignInOptions = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN).setHostedDomain(getString(R.string.infrrd_domain))
                .requestEmail().requestProfile().build();

        mGoogleApiClient = new GoogleApiClient.Builder(this)
                .enableAutoManage(this, new GoogleApiClient.OnConnectionFailedListener() {
                    @Override
                    public void onConnectionFailed(ConnectionResult connectionResult) {
                        Utilities.showInfoSnackbar(mLoginButton, getString(R.string.retrofit_error_message), 1000);
                    }
                })
                .addApi(Auth.GOOGLE_SIGN_IN_API, googleSignInOptions)
                .build();

        signInUsingGoogle();
    }


    private void signInUsingGoogle() {

        mInfrrdImageview.startAnimation(mAnimRotate);
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
                    Utilities.showInfoSnackbar(mRootView, getString(R.string.login_details_error), 2000);
                }
            });

        } else {
            //If login fails
            Toast.makeText(this, "Login Failed", Toast.LENGTH_LONG).show();
        }
    }


    /**
     * Anuj Dutt
     * Preventing exiting the app by pressing back key from login page.
     */
    @Override
    public void onBackPressed() {
        Log.i(LOG_TAG, "Preventing application exit by pressing back key.");
    }

    /**
     * Callback for login procedure
     *
     * @param view
     */
    public void onPressLoginButton(View view) {
        mLoginId = (EditText) findViewById(R.id.input_email);
        mPassword = (EditText) findViewById(R.id.input_password);

        if (!validate(mLoginId.getText().toString(), mPassword.getText().toString())) {
            onLoginFailed();
            return;
        }

        mLoginButton.setEnabled(false);

        final ProgressDialog progressDialog = new ProgressDialog(LoginActivity.this,
                R.style.AppTheme_Dark_Dialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage("Authenticating...");
        progressDialog.show();
//        FetchUserData fetchUserData = new FetchUserData();
//        fetchUserData.execute(mLoginId.getText().toString(),mPassword.getText().toString());
    }


    public void onLoginFailed() {
        Toast.makeText(getBaseContext(), "Login failed", Toast.LENGTH_LONG).show();
        mLoginButton.setEnabled(true);
    }

    /**
     * @param - The email and password
     * @author Anuj Dutt
     * A method to validate our email ID and password
     */
    public boolean validate(String email, String password) {
        boolean valid = true;

        if (email.isEmpty() || !android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches()) {
            mLoginId.setError("Enter a valid email address");
            valid = false;
        } else {
            mLoginId.setError(null);
        }

        if (password.isEmpty() || password.length() < 4 || password.length() > 10) {
            mPassword.setError("Password must be between 4 and 10 alphanumeric characters");
            valid = false;
        } else {
            mPassword.setError(null);
        }

        return valid;
    }

//    /**
//     * @author Anuj Dutt
//     * An AsyncTask to send login details and get user data in response.
//     *
//     */
//    public class FetchUserData extends AsyncTask<String,Void,Profile>
//    {
//        private String LOG_TAG = "";
//        private URL apiLoginUrl = null;
//        private final String username = getString(R.string.username);
//        private final String password = getString(R.string.password);
//
//        private InputStream inputStream = null;
//        private HttpURLConnection httpURLConnection =  null;
//
//        /**
//         * The piece of code that will execute in the background asynchronously when the user taps on Login button.
//         * @param strings the email and password of the user.
//         * @return user data
//         */
//        @Override
//        protected Profile doInBackground(String... strings) {
//            LOG_TAG = getLocalClassName();
//
//            try{
//                Uri builtUri = Uri.parse(getString(R.string.api_base_address) + getString(R.string.api_address_login)).buildUpon().appendQueryParameter(username, strings[0]).appendQueryParameter(password,strings[1]).build();
//                apiLoginUrl = new URL(URLDecoder.decode(builtUri.toString(),"utf-8"));
//                HttpURLConnection httpURLConnection = (HttpURLConnection) apiLoginUrl.openConnection();
//                httpURLConnection.setRequestMethod("GET");
//                httpURLConnection.connect();
//                inputStream = httpURLConnection.getInputStream();
//                BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(inputStream));
//                GsonBuilder gsonBuilder = new GsonBuilder();
//                Profile profile = new Profile();
//                gsonBuilder.registerTypeAdapter(Date.class,new DateDeserializer());
//                Gson gson = gsonBuilder.create();
//                profile = gson.fromJson(bufferedReader, Profile.class);
//                return profile;
//            }catch(MalformedURLException ex)
//            {
//                Log.e(LOG_TAG,ex.getClass().getSimpleName(),ex);
//
//            }catch(IOException ex)
//            {
//                Log.e(LOG_TAG,ex.getClass().getSimpleName(),ex);
//            }catch(Exception ex)
//            {
//                Log.e(LOG_TAG,ex.getClass().getSimpleName(),ex);
//            }finally {
//                if (inputStream != null) {
//                    try {
//                        inputStream.close();
//                    } catch (final IOException ex) {
//                        Log.e(LOG_TAG,ex.getClass().getSimpleName(),ex);
//                    }
//                }
//                if (httpURLConnection != null) {
//                    httpURLConnection.disconnect();
//                }
//            }
//            return null;
//        }
//
//        @Override
//        protected void onPreExecute() {
//            super.onPreExecute();
//        }
//
//        @Override
//        protected void onPostExecute(Profile profile) {
//
//            if(profile.getRefEmployeeId()!=0)
//            {
//                SharedPreferenceManager.addPreference(LoginActivity.this, getString(R.string.user_preferences),getString(R.string.user_name),profile.getUserName());
//                SharedPreferenceManager.addPreference(LoginActivity.this, getString(R.string.user_preferences),getString(R.string.emp_id),String.valueOf(profile.getRefEmployeeId()));
//            }
//            Intent intent = new Intent(LoginActivity.this,NavigationActivity.class);
//            startActivity(intent);
//        }
//    }
}

package com.example.e7440.leavemanagementsystem;

import android.os.AsyncTask;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.EditText;

public class LoginActivity extends AppCompatActivity {

    private static String LOG_TAG = "";
    private EditText mLoginId;
    private EditText mPassword;
    private Button mLoginButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        setContentView(R.layout.activity_login);
        getWindow().setFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN,
                WindowManager.LayoutParams.FLAG_FULLSCREEN);
        LOG_TAG = getLocalClassName().getClass().getSimpleName();
        mLoginButton = (Button) findViewById(R.id.btn_login);
        mLoginButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                onPressLoginButton(view);
            }
        });
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
     * Anuj Dutt
     * Callback for login procedure
     * @param view
     */
    public void onPressLoginButton(View view)
    {

    }

    public class FetchUSerData extends AsyncTask<String,Void,String>
    {
        @Override
        protected String doInBackground(String... strings) {
            return null;
        }

        @Override
        protected void onPreExecute() {
            super.onPreExecute();
        }


    }
}

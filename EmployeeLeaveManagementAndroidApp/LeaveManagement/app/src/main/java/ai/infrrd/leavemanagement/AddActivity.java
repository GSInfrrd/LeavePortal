package ai.infrrd.leavemanagement;

import android.app.DatePickerDialog;
import android.content.Intent;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.DatePicker;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.Switch;
import android.widget.TextView;

import com.bumptech.glide.Glide;

import java.text.SimpleDateFormat;
import java.util.Calendar;

import components.DelayAutoCompleteTextView;
import models.CompanyDetailsResponse;
import models.EmployeeExperienceDetails;
import utilities.CompanyDetailsAutoCompleteAdapter;
import utilities.Constants;
import utilities.DatabaseHandler;
import utilities.Utilities;

public class AddActivity extends AppCompatActivity {

    FragmentTransaction mFragmentTransaction = null;
    Fragment mFragment = null;
    ImageView mEmployerImage = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add);
        Intent intent = getIntent();

//        switch (intent.getIntExtra("type",0))
//        {
//            case Constants.DETAILS_EXPERIENCE_DETAILS:
//                mFragmentTransaction = getSupportFragmentManager().beginTransaction();
//                mFragment = new AddEditExperienceDetails();
//                Bundle bundle = new Bundle();
//                bundle.putString("id", String.valueOf(intent.getIntExtra("id",0)));
//                mFragment.setArguments(bundle);
//                mFragmentTransaction.replace(R.id.include_edit_details,mFragment);
//                mFragmentTransaction.commit();
//        }

        final DelayAutoCompleteTextView bookTitle = (DelayAutoCompleteTextView) findViewById(R.id.et_book_title);
        mEmployerImage = (ImageView) findViewById(R.id.employer_display_picture);
        bookTitle.setThreshold(4);
        bookTitle.setAdapter(new CompanyDetailsAutoCompleteAdapter(this)); // 'this' is Activity instance
        bookTitle.setLoadingIndicator(
                (android.widget.ProgressBar) findViewById(R.id.pb_loading_indicator));
        bookTitle.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position, long id) {
                CompanyDetailsResponse companyDetailsResponse = (CompanyDetailsResponse) adapterView.getItemAtPosition(position);
                bookTitle.setText(companyDetailsResponse.getName());
                Glide.with(AddActivity.this)
                        .load(companyDetailsResponse.getLogo())
                        .placeholder(R.drawable.profile_default).fitCenter()
                        .into(mEmployerImage);
            }
        });
    }
}
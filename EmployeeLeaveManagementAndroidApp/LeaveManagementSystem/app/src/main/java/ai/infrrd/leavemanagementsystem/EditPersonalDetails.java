package ai.infrrd.leavemanagementsystem;

import android.app.DatePickerDialog;
import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.DatePicker;
import android.widget.EditText;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;

import models.EmployeeDetailsModel;
import models.EmployeePersonalDetails;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.DatabaseHandler;
import utilities.SharedPreferenceManager;
import utilities.Utilities;

/**
 * Created by Anuj Dutt on 1/17/2017.
 * <p>
 * Class to return fragment to edit personal details
 */

public class EditPersonalDetails extends Fragment {

    private View mFragmentView = null;
    private EditText mFirstNameEditText = null;
    private EditText mLastNameEditText = null;
    private EditText mDobEditText = null;
    private EditText mPhoneNumberEditText = null;
    private EditText mCityEditText = null;
    private EditText mCountryEditText = null;
    private EditText mTwitterEditText = null;
    private EditText mFacebookEditText = null;
    private EditText mGooglePlusEditText = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_edit_personal_details, container, false);
        setHasOptionsMenu(true);
        mFirstNameEditText = (EditText) mFragmentView.findViewById(R.id.first_name_edittext);
        mLastNameEditText = (EditText) mFragmentView.findViewById(R.id.last_name_edittext);
        mDobEditText = (EditText) mFragmentView.findViewById(R.id.dob_edittext);
        mPhoneNumberEditText = (EditText) mFragmentView.findViewById(R.id.phone_number_edittext);
        mCityEditText = (EditText) mFragmentView.findViewById(R.id.city_edittext);
        mCountryEditText = (EditText) mFragmentView.findViewById(R.id.country_edittext);
        mTwitterEditText = (EditText) mFragmentView.findViewById(R.id.twitter_edittext);
        mFacebookEditText = (EditText) mFragmentView.findViewById(R.id.facebook_edittext);
        mGooglePlusEditText = (EditText) mFragmentView.findViewById(R.id.googleplus_edittext);
        mDobEditText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Calendar now = Calendar.getInstance();
                DatePickerDialog datePickerDialog = new DatePickerDialog(getActivity(), new dateSetListener(),
                        now.get(Calendar.YEAR),
                        now.get(Calendar.MONTH),
                        now.get(Calendar.DAY_OF_MONTH));
                datePickerDialog.getDatePicker().setMaxDate(Calendar.getInstance().getTime().getTime());
                datePickerDialog.show();
            }
        });

        setPassedValues();
        return mFragmentView;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            savePersonalDetails();
        }
        return super.onOptionsItemSelected(item);
    }

    /**
     * Prepopulates the stored employee data
     */

    public void setPassedValues() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        EmployeePersonalDetails employeePersonalDetails = databaseHandler.getEmployeePersonalDetails();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
        mFirstNameEditText.setText(employeePersonalDetails.getFirstName());
        mLastNameEditText.setText(employeePersonalDetails.getLastName());
        mDobEditText.setText(simpleDateFormat.format(employeePersonalDetails.getDateOfBirth()));
        mPhoneNumberEditText.setText(employeePersonalDetails.getPhoneNumber());
        mCityEditText.setText(employeePersonalDetails.getCity());
        mCountryEditText.setText(employeePersonalDetails.getCountry());
        mTwitterEditText.setText(employeePersonalDetails.getTwitter());
        mFacebookEditText.setText(employeePersonalDetails.getFacebook());
        mGooglePlusEditText.setText(employeePersonalDetails.getGooglePlus());
    }

    public class dateSetListener implements android.app.DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker datePicker, int year, int monthOfYear, int dayOfMonth) {
            Calendar date = Calendar.getInstance();
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
            date.set(year, monthOfYear, dayOfMonth, 0, 0, 0);
            mDobEditText.setText(simpleDateFormat.format(date.getTime()));
        }
    }

    /**
     * Validates user inputs.
     *
     * @param firstName - User's first name
     * @param lastName - User's last name
     * @return
     */
    public boolean validate(String firstName, String lastName) {
        boolean valid = true;

        if (firstName.isEmpty()) {
            mFirstNameEditText.setError(getString(R.string.please_enter_first_name));
            valid = false;
        } else {
            mFirstNameEditText.setError(null);
        }

        if (lastName.isEmpty()) {
            mLastNameEditText.setError(getString(R.string.please_enter_last_name));
            valid = false;
        } else {
            mLastNameEditText.setError(null);
        }

        return valid;
    }

    public void savePersonalDetails()
    {
        EmployeeDetailsModel employeeDetailsModel = new EmployeeDetailsModel();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
        String userId = SharedPreferenceManager.getPreference(getActivity(), getString(R.string.user_preferences), getString(R.string.emp_id), getString(R.string.emp_id));
        if(validate(mFirstNameEditText.getText().toString(),mLastNameEditText.getText().toString()))
        {
            employeeDetailsModel.setId(Integer.parseInt(userId));
            employeeDetailsModel.setFirstName(mFirstNameEditText.getText().toString());
            employeeDetailsModel.setLastName(mLastNameEditText.getText().toString());
            try {
                employeeDetailsModel.setDateOfBirth(simpleDateFormat.parse(mDobEditText.getText().toString()));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            employeeDetailsModel.setTelephone(mPhoneNumberEditText.getText().toString());
            employeeDetailsModel.setCity(mCityEditText.getText().toString());
            employeeDetailsModel.setCountry(mCountryEditText.getText().toString());
            employeeDetailsModel.setTwitterLink(mTwitterEditText.getText().toString());
            employeeDetailsModel.setFacebookLink(mFacebookEditText.getText().toString());
            employeeDetailsModel.setGooglePlusLink(mGooglePlusEditText.getText().toString());
            final ProgressDialog progressDialog = new ProgressDialog(getActivity(),
                    R.style.AppTheme_Dark_Dialog);
            progressDialog.setIndeterminate(true);
            progressDialog.setMessage(getString(R.string.saving_your_personal_details));
            progressDialog.show();
            ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Call<Boolean> call = apiInterface.savePersonalDetails(employeeDetailsModel);
            call.enqueue(new Callback<Boolean>() {
                @Override
                public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                    progressDialog.hide();
                    Utilities.showInfoSnackbar(mFragmentView,getString(R.string.personal_details_saved),2000);
                    Intent intent = new Intent(getActivity(),ProfileActivity.class);
                    startActivity(intent);
                }

                @Override
                public void onFailure(Throwable t) {
                    progressDialog.hide();
                    Utilities.showInfoSnackbar(mFragmentView,getString(R.string.retrofit_error_message),2000);
                }
            });
        }
    }
}

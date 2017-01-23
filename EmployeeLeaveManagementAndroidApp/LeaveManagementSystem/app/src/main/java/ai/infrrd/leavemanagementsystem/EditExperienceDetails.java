package ai.infrrd.leavemanagementsystem;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.borax12.materialdaterangepicker.date.DatePickerDialog;
import com.bumptech.glide.Glide;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;

import components.DelayAutoCompleteTextView;
import models.CompanyDetailsResponse;
import models.EmployeeDetailsModel;
import models.EmployeeExperienceDetails;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.CompanyDetailsAutoCompleteAdapter;
import utilities.DatabaseHandler;
import utilities.SharedPreferenceManager;
import utilities.Utilities;

/**
 * Created by Anuj Dutt on 1/17/2017.
 * <p>
 * Class to return fragment to edit experience details
 */

public class EditExperienceDetails extends Fragment {

    private ImageView mEmployerImage = null;
    private ImageView mEditDateRange = null;
    private TextView mStartDateString = null;
    private TextView mEndDateString = null;
    private TextView mStartDateLong = null;
    private TextView mEndDateLong = null;
    private EditText mDesignation = null;
    private View mFragmentView = null;
    private int experienceId;
    private DelayAutoCompleteTextView mCompanyName = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_edit_experience_details, container, false);
        setHasOptionsMenu(true);
        mCompanyName = (DelayAutoCompleteTextView) mFragmentView.findViewById(R.id.company_name_textview);
        mDesignation = (EditText) mFragmentView.findViewById(R.id.company_role_textview);
        mEmployerImage = (ImageView) mFragmentView.findViewById(R.id.employer_display_picture);
        mEditDateRange = (ImageView) mFragmentView.findViewById(R.id.edit_date_range);
        mStartDateString = (TextView) mFragmentView.findViewById(R.id.textview_start_date_string);
        mStartDateLong = (TextView) mFragmentView.findViewById(R.id.textview_start_date_long);
        mEndDateString = (TextView) mFragmentView.findViewById(R.id.textview_end_date_string);
        mEndDateLong = (TextView) mFragmentView.findViewById(R.id.textview_end_date_long);

        resetDates();
        Bundle bundle = getArguments();
        experienceId = bundle.getInt("id", 0);
        if (experienceId != 0) {
            setPassedValues(experienceId);
        }
        mCompanyName.setThreshold(4);
        mCompanyName.setAdapter(new CompanyDetailsAutoCompleteAdapter(getActivity()));
        mCompanyName.setLoadingIndicator(
                (android.widget.ProgressBar) mFragmentView.findViewById(R.id.pb_loading_indicator));
        mCompanyName.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position, long id) {
                CompanyDetailsResponse companyDetailsResponse = (CompanyDetailsResponse) adapterView.getItemAtPosition(position);
                mCompanyName.setText(companyDetailsResponse.getName());
                Glide.with(getActivity())
                        .load(companyDetailsResponse.getLogo())
                        .placeholder(R.drawable.default_image).fitCenter()
                        .into(mEmployerImage);
            }
        });

        mEditDateRange.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Calendar now = Calendar.getInstance();
                DatePickerDialog datepickerdialog = com.borax12.materialdaterangepicker.date.DatePickerDialog.newInstance(
                        new dateSetListener(),
                        now.get(Calendar.YEAR),
                        now.get(Calendar.MONTH),
                        now.get(Calendar.DAY_OF_MONTH)
                );
                datepickerdialog.setAutoHighlight(true);
                datepickerdialog.setMinDate(now);
                datepickerdialog.show(getActivity().getFragmentManager(), "Datepickerdialog");
            }
        });

        return mFragmentView;

    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            if (experienceId != 0) {
                saveExperienceDetails();
            }
            else
            {
                addAndSaveExperienceDetails();

            }
        }
        return super.onOptionsItemSelected(item);
    }

    public class dateSetListener implements DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePickerDialog view, int year, int monthOfYear, int dayOfMonth, int yearEnd, int monthOfYearEnd, int dayOfMonthEnd) {
            Calendar startDate = Calendar.getInstance();
            startDate.set(year, monthOfYear, dayOfMonth, 0, 0, 0);
            Calendar endDate = Calendar.getInstance();
            endDate.set(yearEnd, monthOfYearEnd, dayOfMonthEnd, 0, 0, 0);

            if (startDate.getTime().getTime() > endDate.getTime().getTime()) {
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.date_range_error), 2000);
                Calendar now = Calendar.getInstance();
                DatePickerDialog datepickerdialog = com.borax12.materialdaterangepicker.date.DatePickerDialog.newInstance(
                        new dateSetListener(),
                        now.get(Calendar.YEAR),
                        now.get(Calendar.MONTH),
                        now.get(Calendar.DAY_OF_MONTH)
                );
                datepickerdialog.setAutoHighlight(true);
                datepickerdialog.setMinDate(now);
                datepickerdialog.show(getActivity().getFragmentManager(), "Datepickerdialog");
            } else {
                setDateDetails(startDate, endDate);
            }
        }
    }

    public void setDateDetails(Calendar startDate, Calendar endDate) {
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");

        mStartDateLong.setText(String.valueOf(startDate.getTime().getTime()));
        mStartDateString.setText(simpleDateFormat.format(startDate.getTime()));
        mEndDateLong.setText(String.valueOf(endDate.getTime().getTime()));
        mEndDateString.setText(simpleDateFormat.format(endDate.getTime()));
    }

    public void resetDates() {
        Calendar calendar = Calendar.getInstance();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        mStartDateLong.setText("");
        mStartDateString.setText(simpleDateFormat.format(calendar.getTime()));
        mEndDateLong.setText("");
        mEndDateString.setText(simpleDateFormat.format(calendar.getTime()));
    }

    public void setPassedValues(int experienceId) {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        EmployeeExperienceDetails employeeExperienceDetails = databaseHandler.getEmployeeExperienceDetailsById(experienceId);
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        mStartDateLong.setText(String.valueOf(employeeExperienceDetails.getFromDate().getTime()));
        mStartDateString.setText(simpleDateFormat.format(employeeExperienceDetails.getFromDate()));
        mEndDateLong.setText(String.valueOf(employeeExperienceDetails.getToDate().getTime()));
        mEndDateString.setText(simpleDateFormat.format(employeeExperienceDetails.getToDate()));
        mCompanyName.setText(employeeExperienceDetails.getCompany());
        mDesignation.setText(employeeExperienceDetails.getRole());
    }

    /**
     *
     * @param companyName - User's company name input
     * @param designation - User's designation input
     * @param startDate - User's start date input
     * @param endDate - User's end date input
     * @return
     */
    public boolean validate(String companyName, String designation, String startDate, String endDate) {
        boolean valid = true;

        if (companyName.isEmpty()) {
            mCompanyName.setError(getString(R.string.please_enter_first_name));
            valid = false;
        } else {
            mCompanyName.setError(null);
        }

        if (designation.isEmpty()) {
            mDesignation.setError(getString(R.string.please_enter_last_name));
            valid = false;
        } else {
            mDesignation.setError(null);
        }
        if (startDate.isEmpty() || endDate.isEmpty()) {
            Utilities.showInfoSnackbar(mFragmentView, getString(R.string.enter_start_end_dates), 2000);
            valid = false;
        }

        return valid;
    }

    public void saveExperienceDetails()
    {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeExperienceDetails> employeeExperienceDetailsList = null;
        EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
        String userId = SharedPreferenceManager.getPreference(getActivity(), getString(R.string.user_preferences), getString(R.string.emp_id), getString(R.string.emp_id));
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        if(validate(mCompanyName.getText().toString(),mDesignation.getText().toString(),mStartDateLong.getText().toString(),mEndDateLong.getText().toString()))
        {
            employeeExperienceDetails.setId(experienceId);
            employeeExperienceDetails.setCompany(mCompanyName.getText().toString());
            employeeExperienceDetails.setRole(mDesignation.getText().toString());
            try {
                employeeExperienceDetails.setFromDate(simpleDateFormat.parse(mStartDateString.getText().toString()));
                employeeExperienceDetails.setToDate(simpleDateFormat.parse(mEndDateString.getText().toString()));
            }catch (ParseException e)
            {
                e.printStackTrace();
            }
            if(experienceId !=0)
            {
                databaseHandler.updateEmployeeExperienceDetails(employeeExperienceDetails);
            }
            else
            {
                databaseHandler.addEmployeeExperienceDetails(employeeExperienceDetails,Integer.parseInt(userId));
            }

            employeeExperienceDetailsList = databaseHandler.getAllEmployeeExperienceDetails();
            final ProgressDialog progressDialog = new ProgressDialog(getActivity(),
                    R.style.AppTheme_Dark_Dialog);
            progressDialog.setIndeterminate(true);
            progressDialog.setMessage(getString(R.string.saving_your_experience_details));
            progressDialog.show();
            ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Call<Boolean> call = apiInterface.saveExperienceDetails(employeeExperienceDetailsList,Integer.parseInt(userId));
            call.enqueue(new Callback<Boolean>() {
                @Override
                public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                    progressDialog.hide();
                    Utilities.showInfoSnackbar(mFragmentView,getString(R.string.experience_details_saved),2000);
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

    public void addAndSaveExperienceDetails()
    {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeExperienceDetails> employeeExperienceDetailsList = null;
        EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
        String userId = SharedPreferenceManager.getPreference(getActivity(), getString(R.string.user_preferences), getString(R.string.emp_id), getString(R.string.emp_id));
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        if(validate(mCompanyName.getText().toString(),mDesignation.getText().toString(),mStartDateLong.getText().toString(),mEndDateLong.getText().toString()))
        {
            employeeExperienceDetails.setId(experienceId);
            employeeExperienceDetails.setCompany(mCompanyName.getText().toString());
            employeeExperienceDetails.setRole(mDesignation.getText().toString());
            try {
                employeeExperienceDetails.setFromDate(simpleDateFormat.parse(mStartDateString.getText().toString()));
                employeeExperienceDetails.setToDate(simpleDateFormat.parse(mEndDateString.getText().toString()));
            }catch (ParseException e)
            {
                e.printStackTrace();
            }
            if(experienceId !=0)
            {
                databaseHandler.updateEmployeeExperienceDetails(employeeExperienceDetails);
            }
            else
            {
                databaseHandler.addEmployeeExperienceDetails(employeeExperienceDetails,Integer.parseInt(userId));
            }

            employeeExperienceDetailsList = databaseHandler.getAllEmployeeExperienceDetails();
            final ProgressDialog progressDialog = new ProgressDialog(getActivity(),
                    R.style.AppTheme_Dark_Dialog);
            progressDialog.setIndeterminate(true);
            progressDialog.setMessage(getString(R.string.saving_your_experience_details));
            progressDialog.show();
            ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Call<Boolean> call = apiInterface.saveExperienceDetails(employeeExperienceDetailsList,Integer.parseInt(userId));
            call.enqueue(new Callback<Boolean>() {
                @Override
                public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                    progressDialog.hide();
                    Utilities.showInfoSnackbar(mFragmentView,getString(R.string.experience_details_saved),2000);
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

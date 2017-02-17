package ai.infrrd.leavemanagementsystem.fragments;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
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

import ai.infrrd.leavemanagementsystem.ProfileActivity;
import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.adapters.CompanyDetailsAutoCompleteAdapter;
import ai.infrrd.leavemanagementsystem.components.DelayAutoCompleteTextView;
import ai.infrrd.leavemanagementsystem.models.CompanyDetailsResponse;
import ai.infrrd.leavemanagementsystem.models.EmployeeExperienceDetails;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/17/2017.
 * <p>
 * Class to return fragment to edit experience details
 */

public class EditExperienceDetails extends Fragment {
    private ProgressDialog mProgressDialog = null;
    private ImageView mEmployerImage = null;
    private ImageView mEditDateRange = null;
    private TextView mStartDateString = null;
    private TextView mEndDateString = null;
    private TextView mStartDateLong = null;
    private TextView mEndDateLong = null;
    private TextView mEmployerImageSrcTextview = null;
    private EditText mDesignation = null;
    private View mFragmentView = null;
    private int experienceId;
    private String mUserId = "";
    private DelayAutoCompleteTextView mCompanyName = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_edit_experience_details, container, false);
        initialiseUIComponents();
        resetDates();
        Bundle bundle = getArguments();
        experienceId = bundle.getInt("id", 0);
        if (experienceId != 0) {
            setPassedValues(experienceId);
        }
        setListeners();
        return mFragmentView;

    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            if (experienceId != 0) {
                saveExperienceDetails();
            } else {
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
                datepickerdialog.setMaxDate(now);
                datepickerdialog.show(getActivity().getFragmentManager(), "Datepickerdialog");
            } else {
                setDateDetails(startDate, endDate);
            }
        }
    }

    /**
     * Set dates entered by the user into the UI.
     *
     * @param startDate - Start date entered by the user.
     * @param endDate   - End date entered by the user.
     */
    public void setDateDetails(Calendar startDate, Calendar endDate) {
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        mStartDateLong.setText(String.valueOf(startDate.getTime().getTime()));
        mStartDateString.setText(simpleDateFormat.format(startDate.getTime()));
        mEndDateLong.setText(String.valueOf(endDate.getTime().getTime()));
        mEndDateString.setText(simpleDateFormat.format(endDate.getTime()));
    }

    /**
     * Reset dates back to default values.
     */
    public void resetDates() {
        Calendar calendar = Calendar.getInstance();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        mStartDateLong.setText("");
        mStartDateString.setText(simpleDateFormat.format(calendar.getTime()));
        mEndDateLong.setText("");
        mEndDateString.setText(simpleDateFormat.format(calendar.getTime()));
    }

    /**
     * Set passed values into the UI.
     *
     * @param experienceId - The experience ID of the experience set.
     */
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
        mEmployerImageSrcTextview.setText(employeeExperienceDetails.getCompanyLogo());
        Glide.with(getActivity())
                .load(employeeExperienceDetails.getCompanyLogo())
                .placeholder(R.drawable.default_image).fitCenter()
                .into(mEmployerImage);
    }

    /**
     * @param companyName - User's company name input
     * @param designation - User's designation input
     * @param startDate   - User's start date input
     * @param endDate     - User's end date input
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

    public void saveExperienceDetails() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeExperienceDetails> employeeExperienceDetailsList = null;
        EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        if (validate(mCompanyName.getText().toString(), mDesignation.getText().toString(), mStartDateLong.getText().toString(), mEndDateLong.getText().toString())) {
            employeeExperienceDetails.setId(experienceId);
            employeeExperienceDetails.setCompany(mCompanyName.getText().toString());
            employeeExperienceDetails.setRole(mDesignation.getText().toString());
            employeeExperienceDetails.setCompanyLogo(mEmployerImageSrcTextview.getText().toString());
            try {
                employeeExperienceDetails.setFromDate(simpleDateFormat.parse(mStartDateString.getText().toString()));
                employeeExperienceDetails.setToDate(simpleDateFormat.parse(mEndDateString.getText().toString()));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if (experienceId != 0) {
                databaseHandler.updateEmployeeExperienceDetails(employeeExperienceDetails);
            } else {
                databaseHandler.addEmployeeExperienceDetails(employeeExperienceDetails, Integer.parseInt(mUserId));
            }

            employeeExperienceDetailsList = databaseHandler.getAllEmployeeExperienceDetails();
            saveExperienceDetailsIntoDB(employeeExperienceDetailsList);
        }
    }

    public void addAndSaveExperienceDetails() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeExperienceDetails> employeeExperienceDetailsList = null;
        EmployeeExperienceDetails employeeExperienceDetails = new EmployeeExperienceDetails();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        if (validate(mCompanyName.getText().toString(), mDesignation.getText().toString(), mStartDateLong.getText().toString(), mEndDateLong.getText().toString())) {
            employeeExperienceDetails.setId(experienceId);
            employeeExperienceDetails.setCompany(mCompanyName.getText().toString());
            employeeExperienceDetails.setRole(mDesignation.getText().toString());
            employeeExperienceDetails.setCompanyLogo(mEmployerImageSrcTextview.getText().toString());
            try {
                employeeExperienceDetails.setFromDate(simpleDateFormat.parse(mStartDateString.getText().toString()));
                employeeExperienceDetails.setToDate(simpleDateFormat.parse(mEndDateString.getText().toString()));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if (experienceId != 0) {
                databaseHandler.updateEmployeeExperienceDetails(employeeExperienceDetails);
            } else {
                databaseHandler.addEmployeeExperienceDetails(employeeExperienceDetails, Integer.parseInt(mUserId));
            }

            employeeExperienceDetailsList = databaseHandler.getAllEmployeeExperienceDetails();
            saveExperienceDetailsIntoDB(employeeExperienceDetailsList);
        }
    }

    /**
     * Saves the experience details into the DB.
     *
     * @param employeeExperienceDetailsList - Experience data entered by user.
     */
    public void saveExperienceDetailsIntoDB(List<EmployeeExperienceDetails> employeeExperienceDetailsList) {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.saving_your_experience_details));
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<Boolean> call = apiInterface.saveExperienceDetails(employeeExperienceDetailsList, Integer.parseInt(mUserId));
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                mProgressDialog.hide();
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.experience_details_saved), 2000);
                Intent intent = new Intent(getActivity(), ProfileActivity.class);
                startActivity(intent);
            }

            @Override
            public void onFailure(Throwable t) {
                mProgressDialog.hide();
                ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
            }
        });
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        setHasOptionsMenu(true);
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mCompanyName = (DelayAutoCompleteTextView) mFragmentView.findViewById(R.id.company_name_textview);
        mDesignation = (EditText) mFragmentView.findViewById(R.id.company_role_textview);
        mEmployerImage = (ImageView) mFragmentView.findViewById(R.id.employer_display_picture);
        mEditDateRange = (ImageView) mFragmentView.findViewById(R.id.edit_date_range);
        mStartDateString = (TextView) mFragmentView.findViewById(R.id.textview_start_date_string);
        mStartDateLong = (TextView) mFragmentView.findViewById(R.id.textview_start_date_long);
        mEndDateString = (TextView) mFragmentView.findViewById(R.id.textview_end_date_string);
        mEndDateLong = (TextView) mFragmentView.findViewById(R.id.textview_end_date_long);
        mEmployerImageSrcTextview = (TextView) mFragmentView.findViewById(R.id.textview_employer_image_src);
    }

    /**
     * Set listeners for relevant UI components.
     */
    public void setListeners() {
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
                mEmployerImageSrcTextview.setText(companyDetailsResponse.getLogo());
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
                datepickerdialog.setMaxDate(now);
                datepickerdialog.show(getActivity().getFragmentManager(), "Datepickerdialog");
            }
        });

    }
}

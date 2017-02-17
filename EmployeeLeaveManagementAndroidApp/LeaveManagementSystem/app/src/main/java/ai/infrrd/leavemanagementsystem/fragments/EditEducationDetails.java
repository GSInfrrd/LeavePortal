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
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import com.borax12.materialdaterangepicker.date.DatePickerDialog;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.List;

import ai.infrrd.leavemanagementsystem.ProfileActivity;
import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.EmployeeEducationDetails;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;
import ai.infrrd.leavemanagementsystem.utilities.SharedPreferenceManager;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/17/2017.
 * <p>
 * Class to return fragment to edit education details
 */

public class EditEducationDetails extends Fragment {
    private ProgressDialog mProgressDialog = null;
    private ImageView mEditDateRange = null;
    private TextView mStartDateString = null;
    private TextView mEndDateString = null;
    private TextView mStartDateLong = null;
    private TextView mEndDateLong = null;
    private EditText mInstitution = null;
    private EditText mDegree = null;
    private View mFragmentView = null;
    private int educationId;
    private String mUserId = "";

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_edit_education_details, container, false);
        initialiseUIComponents();
        resetDates();
        Bundle bundle = getArguments();
        educationId = bundle.getInt("id", 0);
        if (educationId != 0) {
            setPassedValues(educationId);
        }
        setListeners();
        return mFragmentView;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            if (educationId != 0) {
                saveEducationDetails();
            } else {
                addAndSaveEducationDetails();

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

    public void setPassedValues(int educationId) {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        EmployeeEducationDetails employeeEducationDetails = databaseHandler.getEmployeeEducationDetailsById(educationId);
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        mStartDateLong.setText(String.valueOf(employeeEducationDetails.getFromDate().getTime()));
        mStartDateString.setText(simpleDateFormat.format(employeeEducationDetails.getFromDate()));
        mEndDateLong.setText(String.valueOf(employeeEducationDetails.getToDate().getTime()));
        mEndDateString.setText(simpleDateFormat.format(employeeEducationDetails.getToDate()));
        mInstitution.setText(employeeEducationDetails.getInstitution());
        mDegree.setText(employeeEducationDetails.getDegree());
    }

    /**
     * @param collegeName - User's college name input
     * @param degree      - User's degree input
     * @param startDate   - User's start date input
     * @param endDate     - User's end date input
     * @return
     */
    public boolean validate(String collegeName, String degree, String startDate, String endDate) {
        boolean valid = true;

        if (collegeName.isEmpty()) {
            mInstitution.setError(getString(R.string.please_enter_first_name));
            valid = false;
        } else {
            mInstitution.setError(null);
        }

        if (degree.isEmpty()) {
            mDegree.setError(getString(R.string.please_enter_last_name));
            valid = false;
        } else {
            mDegree.setError(null);
        }
        if (startDate.isEmpty() || endDate.isEmpty()) {
            Utilities.showInfoSnackbar(mFragmentView, getString(R.string.enter_start_end_dates), 2000);
            valid = false;
        }

        return valid;
    }

    public void saveEducationDetails() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeEducationDetails> employeeEducationDetailsList = null;
        EmployeeEducationDetails employeeEducationDetails = new EmployeeEducationDetails();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        if (validate(mInstitution.getText().toString(), mDegree.getText().toString(), mStartDateLong.getText().toString(), mEndDateLong.getText().toString())) {
            employeeEducationDetails.setId(educationId);
            employeeEducationDetails.setInstitution(mInstitution.getText().toString());
            employeeEducationDetails.setDegree(mDegree.getText().toString());
            try {
                employeeEducationDetails.setFromDate(simpleDateFormat.parse(mStartDateString.getText().toString()));
                employeeEducationDetails.setToDate(simpleDateFormat.parse(mEndDateString.getText().toString()));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if (educationId != 0) {
                databaseHandler.updateEmployeeEducationDetails(employeeEducationDetails);
            } else {
                databaseHandler.addEmployeeEducationDetails(employeeEducationDetails, Integer.parseInt(mUserId));
            }
            employeeEducationDetailsList = databaseHandler.getAllEmployeeEducationDetails();
            saveEducationDetailsIntoDB(employeeEducationDetailsList);
        }
    }

    public void addAndSaveEducationDetails() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeEducationDetails> employeeEducationDetailsList = null;
        EmployeeEducationDetails employeeEducationDetails = new EmployeeEducationDetails();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("MMMM dd, yyyy");
        if (validate(mInstitution.getText().toString(), mDegree.getText().toString(), mStartDateLong.getText().toString(), mEndDateLong.getText().toString())) {
            employeeEducationDetails.setId(educationId);
            employeeEducationDetails.setInstitution(mInstitution.getText().toString());
            employeeEducationDetails.setDegree(mDegree.getText().toString());
            try {
                employeeEducationDetails.setFromDate(simpleDateFormat.parse(mStartDateString.getText().toString()));
                employeeEducationDetails.setToDate(simpleDateFormat.parse(mEndDateString.getText().toString()));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            if (educationId != 0) {
                databaseHandler.updateEmployeeEducationDetails(employeeEducationDetails);
            } else {
                databaseHandler.addEmployeeEducationDetails(employeeEducationDetails, Integer.parseInt(mUserId));
            }

            employeeEducationDetailsList = databaseHandler.getAllEmployeeEducationDetails();
            saveEducationDetailsIntoDB(employeeEducationDetailsList);
        }
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        setHasOptionsMenu(true);
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mInstitution = (EditText) mFragmentView.findViewById(R.id.textview_institution_name);
        mDegree = (EditText) mFragmentView.findViewById(R.id.textview_degree_name);
        mEditDateRange = (ImageView) mFragmentView.findViewById(R.id.edit_date_range);
        mStartDateString = (TextView) mFragmentView.findViewById(R.id.textview_start_date_string);
        mStartDateLong = (TextView) mFragmentView.findViewById(R.id.textview_start_date_long);
        mEndDateString = (TextView) mFragmentView.findViewById(R.id.textview_end_date_string);
        mEndDateLong = (TextView) mFragmentView.findViewById(R.id.textview_end_date_long);
    }

    /**
     * Set listeners for relevant UI components.
     */
    public void setListeners() {
        mEditDateRange.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Calendar now = Calendar.getInstance();
                DatePickerDialog datepickerdialog = com.borax12.materialdaterangepicker.date.DatePickerDialog.newInstance(
                        new EditEducationDetails.dateSetListener(),
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

    /**
     * Saves the experience details into the DB.
     *
     * @param employeeEducationDetailsList - Education data entered by user.
     */
    public void saveEducationDetailsIntoDB(List<EmployeeEducationDetails> employeeEducationDetailsList) {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.saving_your_experience_details));
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<Boolean> call = apiInterface.saveEducationDetails(employeeEducationDetailsList, Integer.parseInt(mUserId));
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                mProgressDialog.hide();
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.education_details_saved), 2000);
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
}

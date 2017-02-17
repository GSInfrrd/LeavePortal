package ai.infrrd.leavemanagementsystem.fragments;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import com.borax12.materialdaterangepicker.date.DatePickerDialog;

import java.text.SimpleDateFormat;
import java.util.Calendar;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.LeaveTransactionResponse;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 12/28/2016.
 */

public class ApplyLeave extends Fragment {
    private ProgressDialog mProgressDialog = null;
    private View mFragmentView = null;
    private LinearLayout mLeaveCompositionLayout = null;
    private LinearLayout mLeaveDetailsLayout = null;
    private LinearLayout mStartDateLayout = null;
    private LinearLayout mEndDateLayout = null;
    private LinearLayout mStartDateSelectorLayout = null;
    private LinearLayout mStartDatePlaceholderLayout = null;
    private LinearLayout mEndDateSelectorLayout = null;
    private LinearLayout mEndDatePlaceholderLayout = null;
    private RadioGroup mHalfOrFullDayLeaveSelector = null;
    private RadioButton mRadioHalfDay = null;
    private RadioButton mRadioFullDay = null;
    private ImageView mImgSickLeave = null;
    private ImageView mImgCasualLeave = null;
    private ImageView mImgCompOff = null;
    private ImageView mImgAdvancedLeave = null;
    private ImageView mImgLop = null;
    private ImageView mImgEditDateRange = null;
    private TextView mNoOfWorkingDatesTextView = null;
    private TextView mStartDatePlaceholderTextView = null;
    private TextView mEndDatePlaceholderTextView = null;
    private TextView mStartDateStringTextView = null;
    private TextView mEndDateStringTextView = null;
    private TextView mStartDateDay = null;
    private TextView mStartDateMonth = null;
    private TextView mStartDate = null;
    private TextView mEndDateDay = null;
    private TextView mEndDateMonth = null;
    private TextView mEndDate = null;
    private EditText mLeaveCommentsEditText = null;
    private String LOG_TAG = null;
    private int mLeaveType = 0;
    private String mUserId = "";

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_apply_leave, container, false);
        initialiseUIComponents();
        setListeners();
        return mFragmentView;
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
                getLeaveDetailsForEmployee(startDate, endDate);
            }
        }
    }

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
        inflater.inflate(R.menu.menu_apply_leave, menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.action_apply:
                applyLeave();
                break;
        }
        return super.onOptionsItemSelected(item);
    }

    public class dateSetListenerDefault implements android.app.DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker datePicker, int year, int monthOfYear, int dayOfMonth) {
            Calendar date = Calendar.getInstance();
            date.set(year, monthOfYear, dayOfMonth, 0, 0, 0);
            getLeaveDetailsForEmployee(date, date);
        }
    }

    public void selectLeaveType(View view) {
        Calendar calendar = Calendar.getInstance();
        selectImageAnimation(view);
        resetDateSelectorLayouts(view);
        resetDates();
        mImgEditDateRange.setOnClickListener(new View.OnClickListener() {
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
                datepickerdialog.show(getActivity().getFragmentManager(), "Datepickerdialog");
            }
        });

        switch (view.getId()) {
            case R.id.img_sick_leave:
                mLeaveType = Constants.LEAVE_TYPE_SICK_LEAVE;
                mStartDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));
                mEndDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));
                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                mImgEditDateRange.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        Calendar now = Calendar.getInstance();
                        android.app.DatePickerDialog datePickerDialog = new android.app.DatePickerDialog(getActivity(), new dateSetListenerDefault(),
                                now.get(Calendar.YEAR),
                                now.get(Calendar.MONTH),
                                now.get(Calendar.DAY_OF_MONTH));
                        datePickerDialog.getDatePicker().setMaxDate(Calendar.getInstance().getTime().getTime());
                        datePickerDialog.show();
                    }
                });

                break;
            case R.id.img_comp_off:
                mLeaveType = Constants.LEAVE_TYPE_COMP_OFF;
                mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);
                mImgEditDateRange.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        Calendar now = Calendar.getInstance();
                        android.app.DatePickerDialog datePickerDialog = new android.app.DatePickerDialog(getActivity(), new dateSetListenerDefault(),
                                now.get(Calendar.YEAR),
                                now.get(Calendar.MONTH),
                                now.get(Calendar.DAY_OF_MONTH));
                        datePickerDialog.getDatePicker().setMinDate(Calendar.getInstance().getTime().getTime());
                        datePickerDialog.show();
                    }
                });
                break;
            case R.id.img_casual_leave:
                mLeaveType = Constants.LEAVE_TYPE_CASUAL_LEAVE;
                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                break;
            case R.id.img_advanced_leave:
                mLeaveType = Constants.LEAVE_TYPE_ADVANCED_LEAVE;
                mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);
                break;
            case R.id.img_lop:
                mLeaveType = Constants.LEAVE_TYPE_LOP;
                mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);
                break;

        }
    }

    /**
     * Reset dates to default values.
     */
    public void resetDates() {
        Calendar calendar = Calendar.getInstance();
        Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
        fadeIn.setDuration(1000);
        mStartDateStringTextView.setText("");
        mEndDateStringTextView.setText("");
        mStartDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
        mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
        mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
        mEndDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
        mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
        mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
        mNoOfWorkingDatesTextView.setText(String.valueOf(0));
        mNoOfWorkingDatesTextView.startAnimation(fadeIn);
    }

    /**
     * Check for leave availability for user selections.
     *
     * @param startDate - Start date entered by the employee.
     * @param endDate   - End date entered by the employee.
     */
    public void getLeaveDetailsForEmployee(final Calendar startDate, final Calendar endDate) {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.fetching_leave_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<LeaveTransactionResponse> call = apiService.checkLeaveAvailabilityAndroid(Integer.parseInt(mUserId), Utilities.setTimeToMidnight(startDate.getTime().getTime()), Utilities.setTimeToMidnight(endDate.getTime().getTime()), mLeaveType);
        call.enqueue(new Callback<LeaveTransactionResponse>() {
            @Override
            public void onResponse(Response<LeaveTransactionResponse> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched date details");
                LeaveTransactionResponse leaveTransactionResponse = response.body();
                mProgressDialog.hide();
                analyseLeaveTransactionResponse(leaveTransactionResponse, startDate, endDate);
            }

            @Override
            public void onFailure(Throwable t) {
                mProgressDialog.hide();
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
            }
        });
    }

    /**
     * Reset image size to default.
     *
     * @param imageView - The image been selected
     */
    public void setDefaultLayoutParams(ImageView imageView) {
        ViewGroup.LayoutParams layoutParams = imageView.getLayoutParams();
        layoutParams.width = R.dimen.dimen_32_dp;
        layoutParams.width = R.dimen.dimen_32_dp;
        imageView.setLayoutParams(layoutParams);
    }

    /**
     * Animate image selection.
     *
     * @param view - The image been selected
     */
    public void selectImageAnimation(View view) {
        setDefaultLayoutParams(mImgSickLeave);
        setDefaultLayoutParams(mImgCasualLeave);
        setDefaultLayoutParams(mImgCompOff);
        setDefaultLayoutParams(mImgAdvancedLeave);
        setDefaultLayoutParams(mImgLop);
        switch (view.getId()) {
            case R.id.img_sick_leave:
                mImgSickLeave.animate().scaleX(1.1f);
                mImgSickLeave.animate().scaleY(1.1f);
                mImgCasualLeave.animate().scaleX(.8f);
                mImgCasualLeave.animate().scaleY(.8f);
                mImgCompOff.animate().scaleX(.8f);
                mImgCompOff.animate().scaleY(.8f);
                mImgAdvancedLeave.animate().scaleX(.8f);
                mImgAdvancedLeave.animate().scaleY(.8f);
                mImgLop.animate().scaleX(.8f);
                mImgLop.animate().scaleY(.8f);
                break;
            case R.id.img_casual_leave:
                mImgCasualLeave.animate().scaleX(1.1f);
                mImgCasualLeave.animate().scaleY(1.1f);
                mImgSickLeave.animate().scaleX(.8f);
                mImgSickLeave.animate().scaleY(.8f);
                mImgCompOff.animate().scaleX(.8f);
                mImgCompOff.animate().scaleY(.8f);
                mImgAdvancedLeave.animate().scaleX(.8f);
                mImgAdvancedLeave.animate().scaleY(.8f);
                mImgLop.animate().scaleX(.8f);
                mImgLop.animate().scaleY(.8f);
                break;
            case R.id.img_comp_off:
                mImgCompOff.animate().scaleX(1.1f);
                mImgCompOff.animate().scaleY(1.1f);
                mImgCasualLeave.animate().scaleX(.8f);
                mImgCasualLeave.animate().scaleY(.8f);
                mImgSickLeave.animate().scaleX(.8f);
                mImgSickLeave.animate().scaleY(.8f);
                mImgAdvancedLeave.animate().scaleX(.8f);
                mImgAdvancedLeave.animate().scaleY(.8f);
                mImgLop.animate().scaleX(.8f);
                mImgLop.animate().scaleY(.8f);
                break;
            case R.id.img_advanced_leave:
                mImgAdvancedLeave.animate().scaleX(1.1f);
                mImgAdvancedLeave.animate().scaleY(1.1f);
                mImgCasualLeave.animate().scaleX(.8f);
                mImgCasualLeave.animate().scaleY(.8f);
                mImgSickLeave.animate().scaleX(.8f);
                mImgSickLeave.animate().scaleY(.8f);
                mImgCompOff.animate().scaleX(.8f);
                mImgCompOff.animate().scaleY(.8f);
                mImgLop.animate().scaleX(.8f);
                mImgLop.animate().scaleY(.8f);
                break;
            case R.id.img_lop:
                mImgLop.animate().scaleX(1.1f);
                mImgLop.animate().scaleY(1.1f);
                mImgCasualLeave.animate().scaleX(.8f);
                mImgCasualLeave.animate().scaleY(.8f);
                mImgSickLeave.animate().scaleX(.8f);
                mImgSickLeave.animate().scaleY(.8f);
                mImgCompOff.animate().scaleX(.8f);
                mImgCompOff.animate().scaleY(.8f);
                mImgAdvancedLeave.animate().scaleX(.8f);
                mImgAdvancedLeave.animate().scaleY(.8f);
                break;
        }
    }

    /**
     * Reset UI to defaults.
     *
     * @param view - Selected leave type
     */
    public void resetDateSelectorLayouts(View view) {
        mHalfOrFullDayLeaveSelector.check(R.id.radio_full_day);
        switch (view.getId()) {
            case R.id.img_advanced_leave:
            case R.id.img_lop:
            case R.id.img_casual_leave:
                mImgEditDateRange.setVisibility(View.VISIBLE);
                mLeaveDetailsLayout.setVisibility(View.VISIBLE);
                mEndDatePlaceholderLayout.setVisibility(View.VISIBLE);
                mEndDateSelectorLayout.setVisibility(View.GONE);
                mStartDatePlaceholderTextView.setText(getString(R.string.start));
                mEndDatePlaceholderTextView.setText(getString(R.string.end));
                mStartDatePlaceholderLayout.setVisibility(View.VISIBLE);
                mStartDateSelectorLayout.setVisibility(View.GONE);
                mLeaveCompositionLayout.setVisibility(View.VISIBLE);
                break;
            case R.id.img_sick_leave:
            case R.id.img_comp_off:
                mImgEditDateRange.setVisibility(View.VISIBLE);
                mLeaveDetailsLayout.setVisibility(View.VISIBLE);
                mEndDatePlaceholderLayout.setVisibility(View.GONE);
                mEndDateSelectorLayout.setVisibility(View.GONE);
                mStartDatePlaceholderTextView.setText(getString(R.string.start));
                mEndDatePlaceholderTextView.setText(getString(R.string.end));
                mStartDatePlaceholderLayout.setVisibility(View.VISIBLE);
                mStartDateSelectorLayout.setVisibility(View.GONE);
                mLeaveCompositionLayout.setVisibility(View.VISIBLE);
                break;
        }
    }

    public void analyseLeaveTransactionResponse(LeaveTransactionResponse leaveTransactionResponse, final Calendar startDate, final Calendar endDate) {
        switch (leaveTransactionResponse.getResponseCode()) {
            case Constants.OK:
                if (mLeaveType == Constants.LEAVE_TYPE_SICK_LEAVE) {
                    mStartDateDay.setText(new SimpleDateFormat("EEE").format(startDate.getTime()).toUpperCase());
                    mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(startDate.getTime()).toUpperCase());
                    mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(startDate.getTime()).toUpperCase())));
                    mStartDateStringTextView.setText(String.valueOf(startDate.getTime().getTime()));
                    mEndDatePlaceholderLayout.setVisibility(View.GONE);
                    mEndDateSelectorLayout.setVisibility(View.GONE);
                    mStartDatePlaceholderLayout.setVisibility(View.GONE);
                    mStartDateSelectorLayout.setVisibility(View.VISIBLE);
                    mNoOfWorkingDatesTextView.setText(String.valueOf(leaveTransactionResponse.getNoOfWorkingDays()));
                    Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
                    fadeIn.setDuration(1000);
                    mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                    Utilities.showInfoSnackbar(mFragmentView, getString(R.string.select_your_leave_composition), 2000);

                } else if (mLeaveType == Constants.LEAVE_TYPE_CASUAL_LEAVE) {
                    mStartDateDay.setText(new SimpleDateFormat("EEE").format(startDate.getTime()).toUpperCase());
                    mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(startDate.getTime()).toUpperCase());
                    mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(startDate.getTime()).toUpperCase())));
                    mStartDateStringTextView.setText(String.valueOf(startDate.getTime().getTime()));

                    mEndDateDay.setText(new SimpleDateFormat("EEE").format(endDate.getTime()).toUpperCase());
                    mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(endDate.getTime()).toUpperCase());
                    mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(endDate.getTime()).toUpperCase())));
                    mEndDateStringTextView.setText(String.valueOf(endDate.getTime().getTime()));

                    mEndDatePlaceholderLayout.setVisibility(View.GONE);
                    mEndDateSelectorLayout.setVisibility(View.VISIBLE);
                    mStartDatePlaceholderLayout.setVisibility(View.GONE);
                    mStartDateSelectorLayout.setVisibility(View.VISIBLE);

                    mNoOfWorkingDatesTextView.setText(String.valueOf(leaveTransactionResponse.getNoOfWorkingDays()));
                    Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
                    fadeIn.setDuration(1000);
                    mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                    if (Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mEndDateStringTextView.getText()))) != Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mStartDateStringTextView.getText())))) {
                        mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);

                    } else {
                        mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                    }

                    Utilities.showInfoSnackbar(mFragmentView, getString(R.string.select_your_leave_composition), 2000);

                } else {
                    mStartDateDay.setText(new SimpleDateFormat("EEE").format(startDate.getTime()).toUpperCase());
                    mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(startDate.getTime()).toUpperCase());
                    mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(startDate.getTime()).toUpperCase())));
                    mStartDateStringTextView.setText(String.valueOf(startDate.getTime().getTime()));

                    mEndDateDay.setText(new SimpleDateFormat("EEE").format(endDate.getTime()).toUpperCase());
                    mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(endDate.getTime()).toUpperCase());
                    mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(endDate.getTime()).toUpperCase())));
                    mEndDateStringTextView.setText(String.valueOf(endDate.getTime().getTime()));

                    mEndDatePlaceholderLayout.setVisibility(View.GONE);
                    mEndDateSelectorLayout.setVisibility(View.VISIBLE);
                    mStartDatePlaceholderLayout.setVisibility(View.GONE);
                    mStartDateSelectorLayout.setVisibility(View.VISIBLE);

                    mNoOfWorkingDatesTextView.setText(String.valueOf(leaveTransactionResponse.getNoOfWorkingDays()));
                    Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
                    fadeIn.setDuration(1000);
                    mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                }

                break;
            case Constants.DATE_ALREADY_EXISTS:
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.date_already_exists_error), 2000);
                if (mLeaveType == Constants.LEAVE_TYPE_SICK_LEAVE) {
                    mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);
                } else if (mLeaveType == Constants.LEAVE_TYPE_COMP_OFF) {
                    Calendar now = Calendar.getInstance();
                    android.app.DatePickerDialog datePickerDialog = new android.app.DatePickerDialog(getActivity(), new dateSetListenerDefault(),
                            now.get(Calendar.YEAR),
                            now.get(Calendar.MONTH),
                            now.get(Calendar.DAY_OF_MONTH));


                    datePickerDialog.getDatePicker().setMinDate(Calendar.getInstance().getTime().getTime());
                    datePickerDialog.show();
                } else {
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

                break;
            case Constants.NO_LEAVE_BALANCE:
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.insufficient_leave_balance), 2000);
                if (mLeaveType == Constants.LEAVE_TYPE_SICK_LEAVE) {
                    mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                    mLeaveDetailsLayout.setVisibility(View.VISIBLE);
                    mEndDatePlaceholderLayout.setVisibility(View.VISIBLE);
                    mEndDateSelectorLayout.setVisibility(View.GONE);
                    mEndDatePlaceholderTextView.setText(getString(R.string.today));
                    mStartDatePlaceholderLayout.setVisibility(View.GONE);
                    mStartDateSelectorLayout.setVisibility(View.VISIBLE);
                    mLeaveCompositionLayout.setVisibility(View.GONE);
                    mNoOfWorkingDatesTextView.setText(String.valueOf(leaveTransactionResponse.getNoOfWorkingDays()));
                    Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
                    fadeIn.setDuration(1000);
                    mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                    Utilities.showInfoSnackbar(mFragmentView, getString(R.string.select_your_leave_composition), 2000);
                } else {
                    mStartDateDay.setText(new SimpleDateFormat("EEE").format(startDate.getTime()).toUpperCase());
                    mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(startDate.getTime()).toUpperCase());
                    mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(startDate.getTime()).toUpperCase())));
                    mStartDateStringTextView.setText(String.valueOf(startDate.getTime().getTime()));

                    mEndDateDay.setText(new SimpleDateFormat("EEE").format(endDate.getTime()).toUpperCase());
                    mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(endDate.getTime()).toUpperCase());
                    mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(endDate.getTime()).toUpperCase())));
                    mEndDateStringTextView.setText(String.valueOf(endDate.getTime().getTime()));

                    mEndDatePlaceholderLayout.setVisibility(View.GONE);
                    mEndDateSelectorLayout.setVisibility(View.VISIBLE);
                    mStartDatePlaceholderLayout.setVisibility(View.GONE);
                    mStartDateSelectorLayout.setVisibility(View.VISIBLE);

                    mNoOfWorkingDatesTextView.setText(String.valueOf(leaveTransactionResponse.getNoOfWorkingDays()));
                    Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
                    fadeIn.setDuration(1000);
                    mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                    Utilities.showInfoSnackbar(mFragmentView, getString(R.string.select_your_leave_composition), 2000);


                    if (Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mEndDateStringTextView.getText()))) != Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mStartDateStringTextView.getText())))) {
                        mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);

                    } else {
                        mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                    }
                }
                break;
        }
    }

    /**
     * Apply for leave.
     */
    public void applyLeave() {
        if (validate(mLeaveCommentsEditText.getText().toString(), mStartDateStringTextView.getText().toString(), mEndDateStringTextView.getText().toString())) {
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
            ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
            Calendar startDate = Calendar.getInstance();
            Calendar endDate = Calendar.getInstance();
            startDate.setTimeInMillis(Long.parseLong(mStartDateStringTextView.getText().toString()));
            endDate.setTimeInMillis(Long.parseLong(mEndDateStringTextView.getText().toString()));
            mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.applying_for_leave));
            Call<Boolean> call = apiInterface.applyLeave(Integer.parseInt(mUserId), mLeaveType, simpleDateFormat.format(startDate.getTime()), simpleDateFormat.format(endDate.getTime()), mLeaveCommentsEditText.getText().toString(), Double.parseDouble(mNoOfWorkingDatesTextView.getText().toString()));
            call.enqueue(new Callback<Boolean>() {
                @Override
                public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                    if (response.body()) {
                        mProgressDialog.hide();
                        Utilities.showInfoSnackbar(mFragmentView, getString(R.string.leave_applied_successfully), 1000);
                        FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                        FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                        fragmentTransaction.replace(R.id.content_frame, new LeaveHistory());
                        fragmentTransaction.commit();
                    } else {
                        ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
                        mProgressDialog.hide();
                    }

                }

                @Override
                public void onFailure(Throwable t) {
                    Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                    ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
                    mProgressDialog.hide();
                }
            });
        }
    }

    /**
     * @param comments  - Leave comments entered
     * @param startDate - Start date of leave applied
     * @param endDate   - End date of leave applied
     * @return - True/false depending on if validated or not
     */
    public boolean validate(String comments, String startDate, String endDate) {
        boolean valid = true;

        if (comments.isEmpty()) {
            mLeaveCommentsEditText.setError(getString(R.string.please_enter_a_comment));
            mLeaveCommentsEditText.requestFocus();
            valid = false;
        } else {
            mLeaveCommentsEditText.setError(null);
        }

        if (startDate.isEmpty() || endDate.isEmpty()) {
            Utilities.showInfoSnackbar(mFragmentView, getString(R.string.enter_start_end_dates), 2000);
            valid = false;
        }

        return valid;
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        LOG_TAG = getClass().getSimpleName();
        setHasOptionsMenu(true);
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mStartDateLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_start_date);
        mEndDateLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_end_date);
        mEndDateSelectorLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_end_date_selector);
        mEndDatePlaceholderLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_end_date_placeholder);
        mStartDateSelectorLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_start_date_selector);
        mStartDatePlaceholderLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_start_date_placeholder);
        mLeaveDetailsLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_leave_details);
        mLeaveCompositionLayout = (LinearLayout) mFragmentView.findViewById(R.id.layout_leave_composition);
        mImgSickLeave = (ImageView) mFragmentView.findViewById(R.id.img_sick_leave);
        mImgCasualLeave = (ImageView) mFragmentView.findViewById(R.id.img_casual_leave);
        mImgCompOff = (ImageView) mFragmentView.findViewById(R.id.img_comp_off);
        mImgAdvancedLeave = (ImageView) mFragmentView.findViewById(R.id.img_advanced_leave);
        mImgLop = (ImageView) mFragmentView.findViewById(R.id.img_lop);
        mImgEditDateRange = (ImageView) mFragmentView.findViewById(R.id.edit_date_range);
        mNoOfWorkingDatesTextView = (TextView) mFragmentView.findViewById(R.id.no_of_working_days);
        mStartDateDay = (TextView) mFragmentView.findViewById(R.id.txt_view_start_day);
        mStartDateMonth = (TextView) mFragmentView.findViewById(R.id.txt_view_start_month);
        mStartDate = (TextView) mFragmentView.findViewById(R.id.txt_view_start_date);
        mEndDateDay = (TextView) mFragmentView.findViewById(R.id.txt_view_end_day);
        mEndDateMonth = (TextView) mFragmentView.findViewById(R.id.txt_view_end_month);
        mEndDate = (TextView) mFragmentView.findViewById(R.id.txt_view_end_date);
        mStartDatePlaceholderTextView = (TextView) mFragmentView.findViewById(R.id.textview_start_date_placeholder);
        mEndDatePlaceholderTextView = (TextView) mFragmentView.findViewById(R.id.textview_end_date_placeholder);
        mStartDateStringTextView = (TextView) mFragmentView.findViewById(R.id.start_date_string);
        mEndDateStringTextView = (TextView) mFragmentView.findViewById(R.id.end_date_string);
        mHalfOrFullDayLeaveSelector = (RadioGroup) mFragmentView.findViewById(R.id.half_full_leave_selector);
        mRadioHalfDay = (RadioButton) mFragmentView.findViewById(R.id.radio_half_day);
        mRadioFullDay = (RadioButton) mFragmentView.findViewById(R.id.radio_full_day);
        mLeaveCommentsEditText = (EditText) mFragmentView.findViewById(R.id.leave_comments);
    }

    /**
     * Set listeners for relevant UI components.
     */
    public void setListeners() {
        mImgSickLeave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectLeaveType(view);
            }
        });
        mImgCasualLeave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectLeaveType(view);
            }
        });
        mImgCompOff.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectLeaveType(view);
            }
        });
        mImgAdvancedLeave.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectLeaveType(view);
            }
        });
        mImgLop.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectLeaveType(view);
            }
        });

        mHalfOrFullDayLeaveSelector.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                Animation fadeIn = new AlphaAnimation(0.0f, 1.0f);
                fadeIn.setDuration(1000);
                switch (checkedId) {
                    case R.id.radio_half_day:
                        mNoOfWorkingDatesTextView.setText(String.valueOf(0.5));
                        mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                        break;
                    case R.id.radio_full_day:
                        mNoOfWorkingDatesTextView.setText(String.valueOf(1));
                        mNoOfWorkingDatesTextView.startAnimation(fadeIn);
                        break;
                }
            }
        });
    }
}
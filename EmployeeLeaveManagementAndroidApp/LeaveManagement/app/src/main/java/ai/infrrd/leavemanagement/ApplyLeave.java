package ai.infrrd.leavemanagement;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.AppBarLayout;
import android.support.design.widget.Snackbar;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.support.v7.widget.CardView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.DatePicker;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import models.HolidayModel;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.Utilities;

/**
 * Created by Anuj Dutt on 12/28/2016.
 */

public class ApplyLeave extends Fragment {

    private View view = null;
    private DatePickerDialog startDatePicker = null;
    private DatePickerDialog endDatePicker = null;
    private LinearLayout mLeaveDetailsLayout = null;
    private LinearLayout mStartDateLayout = null;
    private LinearLayout mEndDateLayout = null;
    private LinearLayout mEndDateSelectorLayout = null;
    private LinearLayout mEndDatePlaceholderLayout = null;
    private RadioGroup mHalfOrFullDayLeaveSelector = null;
    private RadioButton mRadioHalfDay = null;
    private RadioButton mRadioFullDay = null;
    private ImageView mImgSickLeave = null;
    private ImageView mImgCasualLeave = null;
    //private ImageView mImgAdvancedLeave = null;
    private ImageView mImgCompOff = null;
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
    private int selectedStartYear;
    private int selectedStartDay;
    private int selectedStartMonth;
    private int selectedEndDay;
    private int selectedEndMonth;
    private int selectedEndYear;


    private List<HolidayModel> holidays = null;
    private CardView mNoOfWorkingDatesCardview = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        view = inflater.inflate(R.layout.fragment_apply_leave, container, false);
        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<List<HolidayModel>> call = apiService.getHolidayList();

        call.enqueue(new Callback<List<HolidayModel>>() {

            @Override
            public void onResponse(Response<List<HolidayModel>> response, Retrofit retrofit) {
                holidays = response.body();
                Log.d("TAG", "Number of holidays received: " + holidays.size());
            }

            @Override
            public void onFailure(Throwable t) {
                Utilities.showInfoSnackbar(getView(),getString(R.string.retrofit_error_message),1000);
            }
        });

        mStartDateLayout = (LinearLayout) view.findViewById(R.id.layout_start_date);
        mEndDateLayout = (LinearLayout) view.findViewById(R.id.layout_end_date);
        mEndDateSelectorLayout = (LinearLayout) view.findViewById(R.id.layout_end_date_selector);
        mEndDatePlaceholderLayout = (LinearLayout) view.findViewById(R.id.layout_end_date_placeholder);
        mLeaveDetailsLayout = (LinearLayout) view.findViewById(R.id.layout_leave_details);
        mNoOfWorkingDatesCardview = (CardView) view.findViewById(R.id.card_view_no_of_working_dates);

        mImgSickLeave = (ImageView) view.findViewById(R.id.img_sick_leave);
        mImgCasualLeave = (ImageView) view.findViewById(R.id.img_casual_leave);
        //mImgAdvancedLeave = (ImageView) view.findViewById(R.id.img_advanced_leave);
        mImgCompOff = (ImageView) view.findViewById(R.id.img_comp_off);

        mNoOfWorkingDatesTextView = (TextView) view.findViewById(R.id.no_of_working_days);
        mStartDateDay = (TextView) view.findViewById(R.id.txt_view_start_day);
        mStartDateMonth = (TextView) view.findViewById(R.id.txt_view_start_month);
        mStartDate = (TextView) view.findViewById(R.id.txt_view_start_date);
        mEndDateDay = (TextView) view.findViewById(R.id.txt_view_end_day);
        mEndDateMonth = (TextView) view.findViewById(R.id.txt_view_end_month);
        mEndDate = (TextView) view.findViewById(R.id.txt_view_end_date);
        mStartDatePlaceholderTextView = (TextView) view.findViewById(R.id.textview_start_date_placeholder);
        mEndDatePlaceholderTextView = (TextView) view.findViewById(R.id.textview_end_date_placeholder);
        mStartDateStringTextView = (TextView) view.findViewById(R.id.start_date_string);
        mEndDateStringTextView = (TextView) view.findViewById(R.id.end_date_string);

        mHalfOrFullDayLeaveSelector = (RadioGroup) view.findViewById(R.id.half_full_leave_selector);
        mRadioHalfDay = (RadioButton) view.findViewById(R.id.radio_half_day);
        mRadioFullDay = (RadioButton) view.findViewById(R.id.radio_full_day);

        final Calendar calendar = Calendar.getInstance();
        selectedEndDay = selectedStartDay = calendar.get(Calendar.DAY_OF_MONTH);
        selectedEndMonth = selectedStartMonth = calendar.get(Calendar.MONTH);
        selectedEndYear = selectedStartYear = calendar.get(Calendar.YEAR);
        mStartDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
        mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
        mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
        mEndDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
        mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
        mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
        mStartDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));
        mEndDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));

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
//        mImgAdvancedLeave.setOnClickListener(new View.OnClickListener() {
//            @Override
//            public void onClick(View view) {
//                selectLeaveType(view);
//            }
//        });
        mImgCompOff.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                selectLeaveType(view);
            }
        });

        resetDates();

        return view;
    }

    public void resetDates() {
        Calendar calendar = Calendar.getInstance();

        mStartDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));
        mEndDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));

        mStartDateLayout.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                startDatePicker = new DatePickerDialog(getActivity(), new StartDateImplementation(),
                        selectedStartYear, selectedStartMonth, selectedStartDay);
                startDatePicker.getDatePicker().setMinDate(Calendar.getInstance().getTime().getTime());
                startDatePicker.show();
            }
        });

        mEndDateLayout.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                Utilities.showInfoSnackbar(getView(),getString(R.string.select_start_date_first),1000);
            }
        });

        selectedEndDay = selectedStartDay = calendar.get(Calendar.DAY_OF_MONTH);
        selectedEndMonth = selectedStartMonth = calendar.get(Calendar.MONTH);
        selectedEndYear = selectedStartYear = calendar.get(Calendar.YEAR);
        mStartDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
        mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
        mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
        mEndDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
        mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
        mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
    }

    public void selectLeaveType(View view) {

        Calendar calendar = Calendar.getInstance();

        switch (view.getId()) {
            case R.id.img_sick_leave:

                mNoOfWorkingDatesTextView.setText("1");
                mNoOfWorkingDatesCardview.setVisibility(View.VISIBLE);
                selectImageAnimation(view);
                Utilities.showInfoSnackbar(getView(),getString(R.string.sick_leave_warning),1000);
                resetDates();
                mStartDateLayout.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        Utilities.showInfoSnackbar(getView(),getString(R.string.sick_leave_warning),1000);
                    }
                });
                mEndDateLayout.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        Utilities.showInfoSnackbar(getView(),getString(R.string.sick_leave_warning),1000);
                    }
                });
                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                mLeaveDetailsLayout.setVisibility(View.VISIBLE);
                mEndDatePlaceholderLayout.setVisibility(View.VISIBLE);
                mEndDateSelectorLayout.setVisibility(View.GONE);
                mEndDatePlaceholderTextView.setText(getString(R.string.today));

                break;
            case R.id.img_comp_off:

                mNoOfWorkingDatesTextView.setText("0");
                mNoOfWorkingDatesCardview.setVisibility(View.GONE);
                selectImageAnimation(view);
                resetDates();
                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                mLeaveDetailsLayout.setVisibility(View.VISIBLE);
                mEndDateSelectorLayout.setVisibility(View.GONE);
                mEndDatePlaceholderTextView.setText(getString(R.string.today));

                mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);

                break;
            case R.id.img_casual_leave:

                mNoOfWorkingDatesTextView.setText("0");
                mNoOfWorkingDatesCardview.setVisibility(View.VISIBLE);
                selectImageAnimation(view);
                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
                mLeaveDetailsLayout.setVisibility(View.VISIBLE);
                mEndDatePlaceholderLayout.setVisibility(View.GONE);
                mEndDateSelectorLayout.setVisibility(View.VISIBLE);
                resetDates();

                break;
//            case R.id.img_advanced_leave:
//
//                mNoOfWorkingDatesTextView.setText("0");
//                mNoOfWorkingDatesCardview.setVisibility(View.VISIBLE);
//                selectImageAnimation(view);
//                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
//                mLeaveDetailsLayout.setVisibility(View.VISIBLE);
//                mEndDatePlaceholderLayout.setVisibility(View.GONE);
//                mEndDateSelectorLayout.setVisibility(View.VISIBLE);
//                resetDates();
//
//                break;
        }
    }

    private class StartDateImplementation implements DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
            Calendar calendar = Calendar.getInstance();
            calendar.set(year, monthOfYear, dayOfMonth);
            TextView mStartDateDay = (TextView) getActivity().findViewById(R.id.txt_view_start_day);
            TextView mStartDateMonth = (TextView) getActivity().findViewById(R.id.txt_view_start_month);
            TextView mStartDate = (TextView) getActivity().findViewById(R.id.txt_view_start_date);
            mStartDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
            mStartDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
            mStartDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));

            TextView mEndDateDay = (TextView) getActivity().findViewById(R.id.txt_view_end_day);
            TextView mEndDateMonth = (TextView) getActivity().findViewById(R.id.txt_view_end_month);
            TextView mEndDate = (TextView) getActivity().findViewById(R.id.txt_view_end_date);
            mEndDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
            mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
            mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));

            mStartDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));
            mEndDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));

            mEndDateLayout.setOnClickListener(new View.OnClickListener() {

                @Override
                public void onClick(View v) {
                    endDatePicker = new DatePickerDialog(getActivity(), new EndDateImplementation(),
                            selectedEndYear, selectedEndMonth, selectedEndDay);
                    endDatePicker.getDatePicker().setMinDate(Long.parseLong(String.valueOf( mStartDateStringTextView.getText())));

                    endDatePicker.show();
                }
            });
        }
    }

    private class EndDateImplementation implements DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {

            Calendar calendar = Calendar.getInstance();
            calendar.set(year, monthOfYear, dayOfMonth);
            TextView mEndDateDay = (TextView) getActivity().findViewById(R.id.txt_view_end_day);
            TextView mEndDateMonth = (TextView) getActivity().findViewById(R.id.txt_view_end_month);
            TextView mEndDate = (TextView) getActivity().findViewById(R.id.txt_view_end_date);
            mEndDateDay.setText(new SimpleDateFormat("EEE").format(calendar.getTime()).toUpperCase());
            mEndDateMonth.setText(new SimpleDateFormat("MMMM").format(calendar.getTime()).toUpperCase());
            mEndDate.setText(Utilities.getDayNumberSuffix(Integer.parseInt(new SimpleDateFormat("d").format(calendar.getTime()).toUpperCase())));
            mEndDateStringTextView.setText(String.valueOf(calendar.getTime().getTime()));
            if(Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mEndDateStringTextView.getText()))) != Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mStartDateStringTextView.getText()))))
            {
                mHalfOrFullDayLeaveSelector.setVisibility(View.GONE);

            }
            else
            {
                mHalfOrFullDayLeaveSelector.setVisibility(View.VISIBLE);
            }
            findLeavecount();
        }
    }

    public void selectImageAnimation(View view)
    {
        ViewGroup.LayoutParams layoutParams = null;
        layoutParams = mImgSickLeave.getLayoutParams();
        layoutParams.width = R.dimen.dimen_48_dp;
        layoutParams.width = R.dimen.dimen_48_dp;
        mImgSickLeave.setLayoutParams(layoutParams);

        layoutParams = mImgCasualLeave.getLayoutParams();
        layoutParams.width = R.dimen.dimen_48_dp;
        layoutParams.width = R.dimen.dimen_48_dp;
        mImgCasualLeave.setLayoutParams(layoutParams);

        layoutParams = mImgCompOff.getLayoutParams();
        layoutParams.width = R.dimen.dimen_48_dp;
        layoutParams.width = R.dimen.dimen_48_dp;
        mImgCompOff.setLayoutParams(layoutParams);

        switch (view.getId()) {
            case R.id.img_sick_leave:
                mImgSickLeave.animate().scaleX(1.1f);
                mImgSickLeave.animate().scaleY(1.1f);
                mImgCasualLeave.animate().scaleX(.8f);
                mImgCasualLeave.animate().scaleY(.8f);
                mImgCompOff.animate().scaleX(.8f);
                mImgCompOff.animate().scaleY(.8f);
                break;
            case R.id.img_casual_leave:
                mImgCasualLeave.animate().scaleX(1.1f);
                mImgCasualLeave.animate().scaleY(1.1f);
                mImgSickLeave.animate().scaleX(.8f);
                mImgSickLeave.animate().scaleY(.8f);
                mImgCompOff.animate().scaleX(.8f);
                mImgCompOff.animate().scaleY(.8f);
                break;
            case R.id.img_comp_off:
                mImgCompOff.animate().scaleX(1.1f);
                mImgCompOff.animate().scaleY(1.1f);
                mImgCasualLeave.animate().scaleX(.8f);
                mImgCasualLeave.animate().scaleY(.8f);
                mImgSickLeave.animate().scaleX(.8f);
                mImgSickLeave.animate().scaleY(.8f);
                break;
        }
    }

    public void findLeavecount()
    {
        List<Calendar> listOfDatesSelected = new ArrayList<Calendar>();
        int leaves = 0;
        Calendar startDate = Calendar.getInstance();
        Calendar endDate = Calendar.getInstance();
        startDate.setTimeInMillis(Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mStartDateStringTextView.getText()))));
        endDate.setTimeInMillis(Utilities.setTimeToMidnight(Long.parseLong(String.valueOf(mEndDateStringTextView.getText()))));

        long difference = endDate.getTime().getTime() - startDate.getTime().getTime();


        for (Calendar date = startDate; startDate.before(endDate) || startDate.equals(endDate); startDate.add(Calendar.DATE, 1)) {


            if(!(new SimpleDateFormat("EEE").format(date.getTime()).toUpperCase().equals("SAT"))&& !(new SimpleDateFormat("EEE").format(date.getTime()).toUpperCase().equals("SUN")))
            {
                Log.i("TAG",new SimpleDateFormat("EEE").format(date.getTime()).toUpperCase());
                listOfDatesSelected.add(date);
            }
            System.out.println(date);
        }

        Log.i("TAG",String.valueOf(leaves));


        for(Calendar dateSelected: listOfDatesSelected)
        {
            for(HolidayModel holiday : holidays)
            {
                if(!(Utilities.setTimeToMidnight(holiday.getDate().getTime()) == Utilities.setTimeToMidnight(dateSelected.getTime().getTime())))
                {
                    leaves++;
                }

            }

        }

        mNoOfWorkingDatesTextView.setText(String.valueOf(leaves));
    }
}
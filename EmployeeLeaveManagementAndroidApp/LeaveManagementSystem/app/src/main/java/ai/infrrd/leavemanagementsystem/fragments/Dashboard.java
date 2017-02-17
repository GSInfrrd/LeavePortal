package ai.infrrd.leavemanagementsystem.fragments;

import android.app.Activity;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.graphics.DashPathEffect;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.AppCompatSpinner;
import android.support.v7.widget.CardView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import com.github.mikephil.charting.charts.LineChart;
import com.github.mikephil.charting.components.Description;
import com.github.mikephil.charting.components.Legend;
import com.github.mikephil.charting.components.XAxis;
import com.github.mikephil.charting.components.YAxis;
import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.LineData;
import com.github.mikephil.charting.data.LineDataSet;
import com.github.mikephil.charting.interfaces.datasets.ILineDataSet;
import com.github.mikephil.charting.utils.Utils;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.EmployeeDetailsModel;
import ai.infrrd.leavemanagementsystem.models.LeaveReportModel;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.MonthAxisFormatter;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * @author Anuj Dutt
 *         Class for creating the fragment for Dashboard
 */

public class Dashboard extends Fragment {

    private View mFragmentView = null;
    private Context mContext = null;
    private ProgressDialog mProgressDialog = null;
    private TextView mLeaveBalanceTextview = null;
    private TextView mWorkFromHomeTextview = null;
    private TextView mLeaveTakenTextView = null;
    private TextView mCompOffTakenTextView = null;
    private TextView mNoOfRecordsTextView = null;
    private CardView mAppliedLeavesCardview = null;
    private CardView mLeaveBalanceCardview = null;
    private CardView mWorkFromHomeCardview = null;
    private CardView mCompOffTakenCardview = null;
    private LineChart mChart = null;
    private Animation mAnimZoomIn = null;
    private AppCompatSpinner mYearSpinner = null;
    private AppCompatSpinner mLeaveTypeSpinner = null;
    private String mUserId = null;
    String LOG_TAG = "";

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_dashboard, container, false);
        initialiseUIComponents();
        getUserDashboardDetails();
        return mFragmentView;
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        LOG_TAG = getClass().getSimpleName();
        mContext = getActivity();
        SharedPreferences userPreferences = mContext.getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mAnimZoomIn = AnimationUtils.loadAnimation(getActivity(), R.anim.anim_zoom_in);
        mLeaveBalanceTextview = (TextView) mFragmentView.findViewById(R.id.count_leave_balance);
        mWorkFromHomeTextview = (TextView) mFragmentView.findViewById(R.id.count_work_from_home);
        mLeaveTakenTextView = (TextView) mFragmentView.findViewById(R.id.count_leaves_taken);
        mCompOffTakenTextView = (TextView) mFragmentView.findViewById(R.id.count_comp_off_taken);
        mNoOfRecordsTextView = (TextView) mFragmentView.findViewById(R.id.textview_no_of_records);
        mAppliedLeavesCardview = (CardView) mFragmentView.findViewById(R.id.cardview_applied_leaves);
        mLeaveBalanceCardview = (CardView) mFragmentView.findViewById(R.id.cardview_leave_balance);
        mWorkFromHomeCardview = (CardView) mFragmentView.findViewById(R.id.cardview_work_from_home);
        mCompOffTakenCardview = (CardView) mFragmentView.findViewById(R.id.cardview_comp_off_taken);
        mYearSpinner = (AppCompatSpinner) mFragmentView.findViewById(R.id.spinner_year);
        mLeaveTypeSpinner = (AppCompatSpinner) mFragmentView.findViewById(R.id.spinner_leave_type);
        mChart = (LineChart) mFragmentView.findViewById(R.id.graphview_leave_history);
        GradientDrawable drawable = (GradientDrawable) mNoOfRecordsTextView.getBackground();
        drawable.setColor(getResources().getColor(R.color.colorAccent));
        mNoOfRecordsTextView.setBackground(drawable);
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
    }


    public void fillLeaveGraphDetails(LeaveReportModel leaveReportModel) {
        Log.i(LOG_TAG, "Filling leave graph details.");
        List<Entry> entries = new ArrayList<Entry>();
        entries.add(new Entry(0, leaveReportModel.getJan()));
        entries.add(new Entry(1, leaveReportModel.getFeb()));
        entries.add(new Entry(2, leaveReportModel.getMar()));
        entries.add(new Entry(3, leaveReportModel.getApr()));
        entries.add(new Entry(4, leaveReportModel.getMay()));
        entries.add(new Entry(5, leaveReportModel.getJun()));
        entries.add(new Entry(6, leaveReportModel.getJul()));
        entries.add(new Entry(7, leaveReportModel.getAug()));
        entries.add(new Entry(8, leaveReportModel.getSep()));
        entries.add(new Entry(9, leaveReportModel.getOct()));
        entries.add(new Entry(10, leaveReportModel.getNov()));
        entries.add(new Entry(11, leaveReportModel.getDec()));
        LineDataSet dataSet = new LineDataSet(entries, "Leaves");
        dataSet.enableDashedLine(10f, 5f, 0f);
        dataSet.enableDashedHighlightLine(10f, 5f, 0f);
        dataSet.setColor(Color.BLACK);
        dataSet.setCircleColor(Color.BLACK);
        dataSet.setLineWidth(1f);
        dataSet.setCircleRadius(3f);
        dataSet.setDrawCircleHole(false);
        dataSet.setValueTextSize(9f);
        dataSet.setDrawFilled(true);
        dataSet.setFormLineWidth(1f);
        dataSet.setFormLineDashEffect(new DashPathEffect(new float[]{10f, 5f}, 0f));
        dataSet.setFormSize(15.f);
        if (Utils.getSDKInt() >= 18) {
            Drawable drawable = ContextCompat.getDrawable(getActivity(), R.drawable.fade_green);
            dataSet.setFillDrawable(drawable);
        } else {
            dataSet.setFillColor(Color.BLACK);
        }
        ArrayList<ILineDataSet> dataSets = new ArrayList<ILineDataSet>();
        dataSets.add(dataSet);
        LineData lineData = new LineData(dataSets);
        XAxis xAxis = mChart.getXAxis();
        xAxis.setValueFormatter(new MonthAxisFormatter());
        xAxis.setLabelCount(12, true);
        xAxis.setPosition(XAxis.XAxisPosition.BOTTOM);
        xAxis.setTextColor(R.color.colorAccent);
        YAxis yAxisLeft = mChart.getAxisLeft();
        YAxis yAxisRight = mChart.getAxisRight();
        yAxisRight.setDrawLabels(false);
        yAxisLeft.setDrawLabels(false);
        mChart.setData(lineData);
        Description description = new Description();
        description.setText("");
        mChart.setDescription(description);
        mChart.animateX(1000);
        mChart.getLegend().setHorizontalAlignment(Legend.LegendHorizontalAlignment.RIGHT);
        mChart.invalidate();
    }

    public void setListeners() {
        mYearSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                fetchLeaveReport();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });

        mLeaveTypeSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                fetchLeaveReport();
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {

            }
        });
    }

    public void setSpinners(Date joiningDate) {
        String[] leaveTypeArray = {Constants.LEAVE_TYPE_CASUAL_LEAVE_STRING, Constants.LEAVE_TYPE_SICK_LEAVE_STRING, Constants.LEAVE_TYPE_COMP_OFF_STRING, Constants.LEAVE_TYPE_ADVANCED_LEAVE_STRING, Constants.LEAVE_TYPE_LOP_STRING};
        ArrayAdapter leaveTypeArrayAdapter = new ArrayAdapter(getActivity(), R.layout.support_simple_spinner_dropdown_item, leaveTypeArray);
        mLeaveTypeSpinner.setAdapter(leaveTypeArrayAdapter);
        mLeaveTypeSpinner.setSelection(0, false);
        int presentYear = Calendar.getInstance().get(Calendar.YEAR);
        Calendar joiningYear = Calendar.getInstance();
        joiningYear.setTime(joiningDate);
        List<String> yearsList = new ArrayList<String>();
        for (int i = joiningYear.get(Calendar.YEAR); i <= Calendar.getInstance().get(Calendar.YEAR); i++) {
            yearsList.add(String.valueOf(i));
        }
        ArrayAdapter yearArrayAdapter = new ArrayAdapter(getActivity(), R.layout.support_simple_spinner_dropdown_item, yearsList.toArray());
        mYearSpinner.setAdapter(yearArrayAdapter);
        mYearSpinner.setSelection(yearsList.size() - 1, false);
        setListeners();
    }

    /**
     * Get leave report for selected leave type and year.
     */
    public void fetchLeaveReport() {
        int leaveType = 0;
        int year = 0;
        switch (mLeaveTypeSpinner.getSelectedItemPosition()) {
            case 0:
                leaveType = Constants.LEAVE_TYPE_CASUAL_LEAVE;
                year = Integer.parseInt(mYearSpinner.getSelectedItem().toString());
                break;
            case 1:
                leaveType = Constants.LEAVE_TYPE_SICK_LEAVE;
                year = Integer.parseInt(mYearSpinner.getSelectedItem().toString());
                break;
            case 2:
                leaveType = Constants.LEAVE_TYPE_COMP_OFF;
                year = Integer.parseInt(mYearSpinner.getSelectedItem().toString());
                break;
            case 3:
                leaveType = Constants.LEAVE_TYPE_ADVANCED_LEAVE;
                year = Integer.parseInt(mYearSpinner.getSelectedItem().toString());
                break;
            case 4:
                leaveType = Constants.LEAVE_TYPE_LOP;
                year = Integer.parseInt(mYearSpinner.getSelectedItem().toString());
                break;
            default:
                year = Integer.parseInt(mYearSpinner.getSelectedItem().toString());
                break;
        }
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<LeaveReportModel> call = apiService.getLeaveReportDetails(Integer.parseInt(mUserId), year, leaveType);
        mProgressDialog = Utilities.showProgressDialog(mContext, getString(R.string.fetching_dashboard_details));
        call.enqueue(new Callback<LeaveReportModel>() {
            @Override
            public void onResponse(Response<LeaveReportModel> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched user details");
                final LeaveReportModel leaveReportModel = response.body();
                fillLeaveGraphDetails(leaveReportModel);
                mNoOfRecordsTextView.setText(getString(R.string.records_label) + " " + String.valueOf(leaveReportModel.getLeaveCount()));
                mNoOfRecordsTextView.setAnimation(mAnimZoomIn);
                mProgressDialog.hide();
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.retrofit_error_message), 2000);
                mNoOfRecordsTextView.setText(getString(R.string.records_label) + " " + String.valueOf(0));
                LeaveReportModel leaveReportModel = new LeaveReportModel();
                leaveReportModel.setJan(0);
                leaveReportModel.setFeb(0);
                leaveReportModel.setMar(0);
                leaveReportModel.setApr(0);
                leaveReportModel.setMay(0);
                leaveReportModel.setJun(0);
                leaveReportModel.setJul(0);
                leaveReportModel.setAug(0);
                leaveReportModel.setSep(0);
                leaveReportModel.setOct(0);
                leaveReportModel.setNov(0);
                leaveReportModel.setDec(0);
                fillLeaveGraphDetails(leaveReportModel);
                mNoOfRecordsTextView.setAnimation(mAnimZoomIn);
                mProgressDialog.hide();
            }
        });
    }

    /**
     * Fetch user dashboard details.
     */
    public void getUserDashboardDetails() {
        mProgressDialog = Utilities.showProgressDialog(mContext, getString(R.string.fetching_dashboard_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<EmployeeDetailsModel> call = apiService.getUserDetails(Integer.parseInt(mUserId));
        call.enqueue(new Callback<EmployeeDetailsModel>() {
            @Override
            public void onResponse(Response<EmployeeDetailsModel> response, Retrofit retrofit) {
                Log.i(LOG_TAG, getString(R.string.fetched_dashboard_details_successfully));
                final EmployeeDetailsModel employeeDetailsModel = response.body();
                mProgressDialog.hide();
                Runnable runnable = new Runnable() {
                    @Override
                    public void run() {
                        mWorkFromHomeTextview.setText(String.valueOf(employeeDetailsModel.getTotalWorkFromHome()));
                        mLeaveTakenTextView.setText(String.valueOf(employeeDetailsModel.getTotalSpent()));
                        mLeaveBalanceTextview.setText(String.valueOf(employeeDetailsModel.getTotalLeaveCount()));
                        mCompOffTakenTextView.setText(String.valueOf(Integer.valueOf(employeeDetailsModel.getCompOffTaken()) != null ? employeeDetailsModel.getCompOffTaken() : 0));
                        mLeaveBalanceCardview.setVisibility(View.VISIBLE);
                        mLeaveBalanceCardview.setAnimation(mAnimZoomIn);
                        mAppliedLeavesCardview.setVisibility(View.VISIBLE);
                        mAppliedLeavesCardview.setAnimation(mAnimZoomIn);
                        mWorkFromHomeCardview.setVisibility(View.VISIBLE);
                        mWorkFromHomeCardview.setAnimation(mAnimZoomIn);
                        mCompOffTakenCardview.setVisibility(View.VISIBLE);
                        mCompOffTakenCardview.setAnimation(mAnimZoomIn);
                        mNoOfRecordsTextView.setText(getString(R.string.records_label) + " " + String.valueOf(employeeDetailsModel.getLeaveDetails().getLeaveCount()));
                        mLeaveTypeSpinner.setVisibility(View.VISIBLE);
                        mLeaveTypeSpinner.setAnimation(mAnimZoomIn);
                        mYearSpinner.setVisibility(View.VISIBLE);
                        mYearSpinner.setAnimation(mAnimZoomIn);
                        mNoOfRecordsTextView.setVisibility(View.VISIBLE);
                        mNoOfRecordsTextView.setAnimation(mAnimZoomIn);
                    }
                };
                runnable.run();
                fillLeaveGraphDetails(employeeDetailsModel.getLeaveDetails());
                setSpinners(employeeDetailsModel.getDateOfJoining());
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                handleApiFailureCase();
            }
        });
    }

    /**
     * Set UI in case of API errors.
     */
    public void handleApiFailureCase() {
        Utilities.showInfoSnackbar(mFragmentView, getString(R.string.retrofit_error_message), 2000);
        LeaveReportModel leaveReportModel = new LeaveReportModel();
        mProgressDialog.hide();
        Runnable runnable = new Runnable() {
            @Override
            public void run() {
                mWorkFromHomeTextview.setText(String.valueOf(0));
                mLeaveTakenTextView.setText(String.valueOf(0));
                mLeaveBalanceTextview.setText(String.valueOf(0));
                mCompOffTakenTextView.setText(String.valueOf(0));
                mNoOfRecordsTextView.setText(getString(R.string.records_label) + " " + String.valueOf(0));
                mLeaveBalanceCardview.setVisibility(View.VISIBLE);
                mLeaveBalanceCardview.setAnimation(mAnimZoomIn);
                mAppliedLeavesCardview.setVisibility(View.VISIBLE);
                mAppliedLeavesCardview.setAnimation(mAnimZoomIn);
                mWorkFromHomeCardview.setVisibility(View.VISIBLE);
                mWorkFromHomeCardview.setAnimation(mAnimZoomIn);
                mCompOffTakenCardview.setVisibility(View.VISIBLE);
                mCompOffTakenCardview.setAnimation(mAnimZoomIn);
                mLeaveTypeSpinner.setVisibility(View.VISIBLE);
                mLeaveTypeSpinner.setAnimation(mAnimZoomIn);
                mYearSpinner.setVisibility(View.VISIBLE);
                mYearSpinner.setAnimation(mAnimZoomIn);
                mNoOfRecordsTextView.setVisibility(View.VISIBLE);
                mNoOfRecordsTextView.setAnimation(mAnimZoomIn);
            }
        };
        runnable.run();
        leaveReportModel.setJan(0);
        leaveReportModel.setFeb(0);
        leaveReportModel.setMar(0);
        leaveReportModel.setApr(0);
        leaveReportModel.setMay(0);
        leaveReportModel.setJun(0);
        leaveReportModel.setJul(0);
        leaveReportModel.setAug(0);
        leaveReportModel.setSep(0);
        leaveReportModel.setOct(0);
        leaveReportModel.setNov(0);
        leaveReportModel.setDec(0);
        fillLeaveGraphDetails(leaveReportModel);
        setSpinners(Calendar.getInstance().getTime());
    }
}
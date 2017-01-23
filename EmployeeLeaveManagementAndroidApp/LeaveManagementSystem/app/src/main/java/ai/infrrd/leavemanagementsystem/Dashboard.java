package ai.infrrd.leavemanagementsystem;

import android.app.Activity;
import android.graphics.Color;
import android.graphics.DashPathEffect;
import android.graphics.drawable.Drawable;
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
import java.util.List;

import models.EmployeeDetailsModel;
import models.LeaveReportModel;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.MonthAxisFormatter;
import utilities.SharedPreferenceManager;

/**
 * @author Anuj Dutt
 *         Class for creating the fragment for Dashboard
 */

public class Dashboard extends Fragment {

    private AppCompatSpinner mAppCompatSpinner = null;
    private TextView mLeaveBalanceTextview = null;
    private TextView mWorkFromHomeTextview = null;
    private TextView mAppliedLeavesTextView = null;
    private CardView mAppliedLeavesCardview = null;
    private CardView mLeaveBalanceCardview = null;
    private CardView mWorkFromHomeCardview = null;
    private LineChart mChart = null;
    private Animation mAnimSlideInRight = null;
    String LOG_TAG = "";

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        LOG_TAG = getClass().getSimpleName();
        View view = inflater.inflate(R.layout.fragment_dashboard, container, false);

        mAnimSlideInRight = AnimationUtils.loadAnimation(getActivity(), R.anim.anim_zoom_in);
        mLeaveBalanceTextview = (TextView) view.findViewById(R.id.count_leave_balance);
        mWorkFromHomeTextview = (TextView) view.findViewById(R.id.count_work_from_home);
        mAppliedLeavesTextView = (TextView) view.findViewById(R.id.count_applied_leaves);
        mAppliedLeavesCardview = (CardView) view.findViewById(R.id.cardview_applied_leaves);
        mLeaveBalanceCardview = (CardView) view.findViewById(R.id.cardview_leave_balance);
        mWorkFromHomeCardview = (CardView) view.findViewById(R.id.cardview_work_from_home);

        mChart = (LineChart) view.findViewById(R.id.graphview_leave_history);

        String userId = SharedPreferenceManager.getPreference(getContext(), getString(R.string.user_preferences), getString(R.string.emp_id), getString(R.string.emp_id));
        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<EmployeeDetailsModel> call = apiService.getUserDetails(Integer.parseInt(userId));

        call.enqueue(new Callback<EmployeeDetailsModel>() {
            @Override
            public void onResponse(Response<EmployeeDetailsModel> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched user details");
                final EmployeeDetailsModel employeeDetailsModel = response.body();

                Runnable runnable = new Runnable() {
                    @Override
                    public void run() {
                        mWorkFromHomeTextview.setText(String.valueOf(employeeDetailsModel.getTotalLeaveCount()));
                        mAppliedLeavesTextView.setText(String.valueOf(employeeDetailsModel.getTotalSpent()));
                        mLeaveBalanceTextview.setText(String.valueOf(employeeDetailsModel.getTotalLeaveCount() - employeeDetailsModel.getTotalSpent()));
                        mLeaveBalanceCardview.setVisibility(View.VISIBLE);
                        mLeaveBalanceCardview.setAnimation(mAnimSlideInRight);
                        mAppliedLeavesCardview.setVisibility(View.VISIBLE);
                        mAppliedLeavesCardview.setAnimation(mAnimSlideInRight);
                        mWorkFromHomeCardview.setVisibility(View.VISIBLE);
                        mWorkFromHomeCardview.setAnimation(mAnimSlideInRight);
                    }
                };
                runnable.run();
                fillLeaveGraphDetails(employeeDetailsModel.getLeaveDetails());
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
            }
        });

        return view;
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
    }


    public void fillLeaveGraphDetails(LeaveReportModel leaveReportModel) {
        Log.i(LOG_TAG, "Filling leave graph details.");
        List<Entry> entries = new ArrayList<Entry>();
        entries.add(new Entry(0, 1));
        entries.add(new Entry(1, 1));
        entries.add(new Entry(2, 2));
        entries.add(new Entry(3, 3));
        entries.add(new Entry(4, 0));
        entries.add(new Entry(5, 2));
        entries.add(new Entry(6, 3));
        entries.add(new Entry(7, 4));
        entries.add(new Entry(8, 5));
        entries.add(new Entry(9, 0));
        entries.add(new Entry(10, 2));
        entries.add(new Entry(11, 1));
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
}
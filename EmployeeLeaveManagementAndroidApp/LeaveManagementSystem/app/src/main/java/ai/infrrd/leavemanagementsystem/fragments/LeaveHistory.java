package ai.infrrd.leavemanagementsystem.fragments;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.adapters.LeaveTransactionsViewPagerAdapter;
import ai.infrrd.leavemanagementsystem.models.EmployeeLeaveMasterDetails;
import ai.infrrd.leavemanagementsystem.models.LeaveHistoryModel;
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
 * @author Anuj Dutt
 *         Class for creating the fragment for LeaveHistory
 */

public class LeaveHistory extends Fragment implements TabLayout.OnTabSelectedListener {

    String LOG_TAG = "";
    private View mFragmemt = null;
    private TabLayout mTransactionTypeTabLayout = null;
    private ViewPager mTransactionTypeViewPager = null;
    private TextView mLeaveBalanceTextview = null;
    private ProgressDialog mProgressDialog = null;
    private String mUserId = "";

    @Override
    public void onTabSelected(TabLayout.Tab tab) {
        mTransactionTypeViewPager.setCurrentItem(tab.getPosition());
    }

    @Override
    public void onTabUnselected(TabLayout.Tab tab) {

    }

    @Override
    public void onTabReselected(TabLayout.Tab tab) {

    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmemt = inflater.inflate(R.layout.fragment_leave_history, container, false);
        initialiseUIComponents();
        setListeners();
        getLeaveDetails();
        return mFragmemt;
    }

    /**
     * Get leave balance of the user.
     */
    public void getLeaveBalance() {
        //mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.fetching_leave_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<EmployeeLeaveMasterDetails> call = apiService.getEmployeeLeaveMasterDetails(Integer.parseInt(mUserId));
        call.enqueue(new Callback<EmployeeLeaveMasterDetails>() {
            @Override
            public void onResponse(Response<EmployeeLeaveMasterDetails> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched user leave balance details");
                EmployeeLeaveMasterDetails employeeLeaveMasterDetails = response.body();
                mLeaveBalanceTextview.setText(String.valueOf(employeeLeaveMasterDetails.getEarnedCasualLeave()));
                mProgressDialog.hide();
            }

            @Override
            public void onFailure(Throwable t) {
                ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
                mProgressDialog.hide();
                mLeaveBalanceTextview.setText(String.valueOf(0.0));
            }
        });

    }

    /**
     * Get leave details of the user.
     */
    public void getLeaveDetails() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.fetching_leave_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<List<LeaveHistoryModel>> call = apiService.getLeaveDetails(Integer.parseInt(mUserId), 0, 0, 0);
        call.enqueue(new Callback<List<LeaveHistoryModel>>() {
            @Override
            public void onResponse(Response<List<LeaveHistoryModel>> response, Retrofit retrofit) {
                Log.i(LOG_TAG, "Fetched user leave details");
                List<LeaveHistoryModel> leaveHistoryModelList = response.body();
                databaseHandler.deleteAllEmployeeLeaveDetails();
                databaseHandler.addEmployeeLeaveDetailsList(leaveHistoryModelList);
                LeaveTransactionsViewPagerAdapter leaveTransactionsViewPagerAdapter = new LeaveTransactionsViewPagerAdapter(getFragmentManager(), mTransactionTypeTabLayout.getTabCount());
                mTransactionTypeViewPager.setAdapter(leaveTransactionsViewPagerAdapter);
                mTransactionTypeTabLayout.addOnTabSelectedListener(LeaveHistory.this);
                mTransactionTypeViewPager.addOnPageChangeListener(new ViewPager.OnPageChangeListener() {
                    @Override
                    public void onPageSelected(int position) {
                        mTransactionTypeTabLayout.setScrollPosition(position, 0, true);
                        mTransactionTypeTabLayout.setSelected(true);
                    }

                    @Override
                    public void onPageScrolled(int arg0, float arg1, int arg2) {
                    }

                    @Override
                    public void onPageScrollStateChanged(int arg0) {
                    }
                });
                getLeaveBalance();
            }

            @Override
            public void onFailure(Throwable t) {
                databaseHandler.deleteAllEmployeeLeaveDetails();
                ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
                LeaveTransactionsViewPagerAdapter leaveTransactionsViewPagerAdapter = new LeaveTransactionsViewPagerAdapter(getFragmentManager(), mTransactionTypeTabLayout.getTabCount());
                mTransactionTypeViewPager.setAdapter(leaveTransactionsViewPagerAdapter);
                mTransactionTypeTabLayout.addOnTabSelectedListener(LeaveHistory.this);
                mTransactionTypeViewPager.addOnPageChangeListener(new ViewPager.OnPageChangeListener() {
                    @Override
                    public void onPageSelected(int position) {
                        mTransactionTypeTabLayout.setScrollPosition(position, 0, true);
                        mTransactionTypeTabLayout.setSelected(true);
                    }

                    @Override
                    public void onPageScrolled(int arg0, float arg1, int arg2) {
                    }

                    @Override
                    public void onPageScrollStateChanged(int arg0) {
                    }
                });
                getLeaveBalance();
            }
        });
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        LOG_TAG = getClass().getSimpleName();
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mLeaveBalanceTextview = (TextView) mFragmemt.findViewById(R.id.count_leave_balance);
        mTransactionTypeTabLayout = (TabLayout) mFragmemt.findViewById(R.id.tablayout_leave_history);
        mTransactionTypeViewPager = (ViewPager) mFragmemt.findViewById(R.id.viewpager_leave_history);
        mTransactionTypeTabLayout.addTab(mTransactionTypeTabLayout.newTab().setText(getString(R.string.all)));
        mTransactionTypeTabLayout.addTab(mTransactionTypeTabLayout.newTab().setText(getString(R.string.spent)));
        mTransactionTypeTabLayout.addTab(mTransactionTypeTabLayout.newTab().setText(getString(R.string.credited)));
        mTransactionTypeTabLayout.setTabGravity(TabLayout.GRAVITY_FILL);
    }

    /**
     * Set listeners for relevant UI components.
     */
    public void setListeners() {
        //No listeners to be set
    }
}

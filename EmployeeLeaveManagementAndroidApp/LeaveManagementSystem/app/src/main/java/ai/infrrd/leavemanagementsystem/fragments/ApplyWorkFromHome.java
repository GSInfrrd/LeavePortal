package ai.infrrd.leavemanagementsystem.fragments;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.widget.AppCompatSpinner;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.EditText;
import android.widget.LinearLayout;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.adapters.WfhHistoryAdapter;
import ai.infrrd.leavemanagementsystem.models.LeaveTransactionResponse;
import ai.infrrd.leavemanagementsystem.models.WorkFromHomeModel;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/18/2017.
 * <p>
 * Loads the WFH fragment into the navigation drawer content
 */

public class ApplyWorkFromHome extends Fragment {

    private View mFragmentView = null;
    private AppCompatSpinner mWfhReasonSpinner = null;
    private EditText mLeaveCommentsEditText = null;
    private Map<Integer, String> mReasonMap = null;
    private List<String> mReasonList = null;
    private ArrayAdapter<String> mReasonSpinnerAdapter = null;
    private ProgressDialog mProgressDialog = null;
    private RecyclerView mWfhRecyclerView = null;
    private LinearLayoutManager mLayoutManager = null;
    private LinearLayout mPlaceholderLayout = null;
    private String LOG_TAG = "";
    private String mUserId = "";

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        LOG_TAG = getClass().getSimpleName();
        mFragmentView = inflater.inflate(R.layout.fragment_apply_wfh, container, false);
        initialiseUIComponents();
        getWfhList();
        return mFragmentView;
    }

    @Override
    public void onCreateOptionsMenu(Menu menu, MenuInflater inflater) {
        inflater.inflate(R.menu.menu_edit_profile, menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.action_add:
                checkWfhAvailability();
                break;
        }
        return super.onOptionsItemSelected(item);
    }

    /**
     * Validate data entered by the user
     *
     * @return - true/false depending on if validation suceeds or not
     */
    public boolean validate() {
        boolean valid = true;
        if (mWfhReasonSpinner.getSelectedItem().toString().equals(mReasonMap.get(Constants.WFH_REASON_OTHERS))) {
            if (mLeaveCommentsEditText.getText().toString().isEmpty()) {
                mLeaveCommentsEditText.setError(getString(R.string.please_enter_a_comment));
                valid = false;
            } else {
                mLeaveCommentsEditText.setError(null);
            }
        }
        return valid;
    }

    /**
     * Apply for WFH today
     */
    public void applyForWfh(final DialogInterface dialogInterface) {
        if (validate()) {
            mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.applying_for_leave));
            ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
            WorkFromHomeModel workFromHomeModel = new WorkFromHomeModel();
            for (Map.Entry<Integer, String> entry : mReasonMap.entrySet()) {
                if (entry.getValue().equals(mWfhReasonSpinner.getSelectedItem().toString())) {
                    workFromHomeModel.setRefReason(entry.getKey());
                    workFromHomeModel.setReason(entry.getValue());
                }
            }

            workFromHomeModel.setRefEmployeeId(Integer.parseInt(mUserId));
            workFromHomeModel.setOtherReason(mLeaveCommentsEditText.getText().toString());
            workFromHomeModel.setDate(Calendar.getInstance().getTime());
            workFromHomeModel.setRefStatus(Constants.LEAVE_TYPE_APPROVED);
            Call<Long> call = apiInterface.addNewWorkFromHome(workFromHomeModel);
            call.enqueue(new Callback<Long>() {
                @Override
                public void onResponse(Response<Long> response, Retrofit retrofit) {
                    mProgressDialog.hide();
                    dialogInterface.cancel();
                    Utilities.showInfoSnackbar(mFragmentView, getString(R.string.wfh_applied_successfully), 1000);
                    FragmentManager fragmentManager = getActivity().getSupportFragmentManager();
                    FragmentTransaction fragmentTransaction = fragmentManager.beginTransaction();
                    fragmentTransaction.replace(R.id.content_frame, new ApplyWorkFromHome());
                    fragmentTransaction.commit();
                }

                @Override
                public void onFailure(Throwable t) {
                    Utilities.showInfoSnackbar(mFragmentView, getString(R.string.retrofit_error_message), 1000);
                    mProgressDialog.hide();
                }
            });
        }
    }

    /**
     * Initialise UI components
     */
    public void initialiseUIComponents() {
        setHasOptionsMenu(true);
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mReasonMap = new HashMap<Integer, String>();
        mReasonList = new ArrayList<String>();
        mWfhRecyclerView = (RecyclerView) mFragmentView.findViewById(R.id.wfh_recyclerview);
        mWfhRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(getActivity());
        mWfhRecyclerView.setLayoutManager(mLayoutManager);
        mPlaceholderLayout = (LinearLayout) mFragmentView.findViewById(R.id.placeholder_layout);
    }


    /**
     * Set an alert dialog to apply for WFH.
     */
    public AlertDialog.Builder setApplyWfhDialog() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());
        builder.setTitle(getString(R.string.enter_your_reason));
        builder
                .setCancelable(false)
                .setPositiveButton(getString(R.string.submit), new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {
                        applyForWfh(dialog);
                    }
                })
                .setNegativeButton(getString(R.string.cancel), new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int id) {

                        dialog.cancel();
                    }
                });
        return builder;
    }

    /**
     * Get WFH reason list from API.
     */
    public void getWfhReasonList(final ProgressDialog mProgressDialog) {
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<List<WorkFromHomeModel>> call = apiInterface.getWorkFromHomeReasonsList();
        //mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.fetching_wfh_details));
        call.enqueue(new Callback<List<WorkFromHomeModel>>() {
            @Override
            public void onResponse(Response<List<WorkFromHomeModel>> response, Retrofit retrofit) {
                List<WorkFromHomeModel> workFromHomeModelList = response.body();
                for (WorkFromHomeModel workFromHomeModel : workFromHomeModelList) {
                    mReasonMap.put(workFromHomeModel.getRefReason(), workFromHomeModel.getReason());
                    mReasonList.add(workFromHomeModel.getReason());
                }
                mProgressDialog.hide();
            }

            @Override
            public void onFailure(Throwable t) {
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.retrofit_error_message), 1000);
                mProgressDialog.hide();
            }
        });
    }

    /**
     * Check if WFH can be taken today.
     */
    public void checkWfhAvailability() {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.fetching_leave_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<LeaveTransactionResponse> call = apiService.checkLeaveAvailabilityAndroid(Integer.parseInt(mUserId), Utilities.setTimeToMidnight(Calendar.getInstance().getTime().getTime()), Utilities.setTimeToMidnight(Calendar.getInstance().getTime().getTime()), Constants.LEAVE_TYPE_SICK_LEAVE);
        call.enqueue(new Callback<LeaveTransactionResponse>() {
            @Override
            public void onResponse(Response<LeaveTransactionResponse> response, Retrofit retrofit) {
                Log.i(LOG_TAG, getString(R.string.fetched_wfh_details_successfully));
                mProgressDialog.hide();
                LeaveTransactionResponse leaveTransactionResponse = response.body();
                analyseLeaveTransactionResponse(leaveTransactionResponse);
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                mProgressDialog.hide();
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.retrofit_error_message), 2000);
            }
        });
    }


    /**
     * @param leaveTransactionResponse - The response from the API containing details about WFH availability
     */
    public void analyseLeaveTransactionResponse(LeaveTransactionResponse leaveTransactionResponse) {
        switch (leaveTransactionResponse.getResponseCode()) {
            case Constants.OK:
                AlertDialog.Builder builder = setApplyWfhDialog();
                LayoutInflater inflater = getActivity().getLayoutInflater();
                View dialogView = inflater.inflate(R.layout.dialog_apply_wfh, null);
                builder.setView(dialogView);
                mWfhReasonSpinner = (AppCompatSpinner) dialogView.findViewById(R.id.wfh_reason_spinner);
                mLeaveCommentsEditText = (EditText) dialogView.findViewById(R.id.leave_comments);
                mReasonSpinnerAdapter = new ArrayAdapter<String>(getActivity(), R.layout.support_simple_spinner_dropdown_item, mReasonList);
                mWfhReasonSpinner.setAdapter(mReasonSpinnerAdapter);
                mWfhReasonSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
                    @Override
                    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                        if (mWfhReasonSpinner.getSelectedItem().toString().equals(mReasonMap.get(Constants.WFH_REASON_OTHERS))) {
                            mLeaveCommentsEditText.setVisibility(View.VISIBLE);
                            mLeaveCommentsEditText.setText("");
                        } else {
                            mLeaveCommentsEditText.setVisibility(View.GONE);
                            mLeaveCommentsEditText.setText("");
                        }
                    }

                    @Override
                    public void onNothingSelected(AdapterView<?> parent) {

                    }
                });
                AlertDialog alertDialog = builder.create();
                alertDialog.show();

                break;
            case Constants.DATE_ALREADY_EXISTS:
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.date_already_exists_error), 2000);
                break;
            case Constants.NO_LEAVE_BALANCE:
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.insufficient_leave_balance), 2000);
                break;
        }
    }

    /**
     * Get a list of the WFH applied before.
     */
    public void getWfhList() {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.fetching_wfh_details));
        ApiInterface apiService = ApiClient.getClient().create(ApiInterface.class);
        Call<List<WorkFromHomeModel>> call = apiService.getWorkFromHomeList(Integer.parseInt(mUserId));
        call.enqueue(new Callback<List<WorkFromHomeModel>>() {
            @Override
            public void onResponse(Response<List<WorkFromHomeModel>> response, Retrofit retrofit) {
                Log.i(LOG_TAG, getString(R.string.fetched_wfh_details_successfully));
                getWfhReasonList(mProgressDialog);
                List<WorkFromHomeModel> workFromHomeModelList = response.body();
                if (workFromHomeModelList.isEmpty()) {
                    mPlaceholderLayout.setVisibility(View.VISIBLE);
                    mWfhRecyclerView.setVisibility(View.GONE);
                } else {
                    WfhHistoryAdapter wfhHistoryAdapter = new WfhHistoryAdapter(getActivity(), workFromHomeModelList);
                    mWfhRecyclerView.setAdapter(wfhHistoryAdapter);
                    mPlaceholderLayout.setVisibility(View.GONE);
                    mWfhRecyclerView.setVisibility(View.VISIBLE);
                }
            }

            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message), t);
                mProgressDialog.hide();
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.retrofit_error_message), 2000);
                mPlaceholderLayout.setVisibility(View.VISIBLE);
                mWfhRecyclerView.setVisibility(View.GONE);
            }
        });

    }
}
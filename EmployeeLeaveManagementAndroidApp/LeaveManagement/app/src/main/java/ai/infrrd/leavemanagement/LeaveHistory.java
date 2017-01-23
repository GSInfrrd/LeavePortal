package ai.infrrd.leavemanagement;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.ArrayList;
import java.util.List;

import models.LeaveHistoryModel;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.LeaveHistoryAdapter;
import utilities.SharedPreferenceManager;

/**
 * @author Anuj Dutt
 * Class for creating the fragment for LeaveHistory
 */

public class LeaveHistory extends Fragment {

    String LOG_TAG = "";
    RecyclerView mLeaveHistoryRecyclerView = null;
    private LinearLayoutManager mLayoutManager = null;
    Context mContext = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        LOG_TAG = getClass().getSimpleName();
        View view = inflater.inflate(R.layout.fragment_leave_history,container,false);
        mContext = getActivity();

        mLeaveHistoryRecyclerView = (RecyclerView)view.findViewById(R.id.recycler_view_leave_history);
        mLeaveHistoryRecyclerView.setHasFixedSize(true);
        RecyclerView.LayoutManager layoutManager = new LinearLayoutManager(getActivity());
        mLeaveHistoryRecyclerView.setLayoutManager(layoutManager);



        String userId = SharedPreferenceManager.getPreference(getContext(),getString(R.string.user_preferences),getString(R.string.emp_id),getString(R.string.emp_id));
        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<List<LeaveHistoryModel>> call = apiService.getLeaveDetails(Integer.parseInt(userId));

        call.enqueue(new Callback<List<LeaveHistoryModel>>() {
            @Override
            public void onResponse(Response<List<LeaveHistoryModel>> response, Retrofit retrofit) {
                Log.i(LOG_TAG,"Fetched user leave details");
                List<LeaveHistoryModel> leaveHistoryModelList = response.body();
                LeaveHistoryAdapter leaveHistoryAdapter = new LeaveHistoryAdapter(mContext,leaveHistoryModelList);
                mLeaveHistoryRecyclerView.setAdapter(leaveHistoryAdapter);

            }
            @Override
            public void onFailure(Throwable t) {
                Log.e(LOG_TAG, getString(R.string.retrofit_error_message),t);
            }
        });
        return view;
    }
}

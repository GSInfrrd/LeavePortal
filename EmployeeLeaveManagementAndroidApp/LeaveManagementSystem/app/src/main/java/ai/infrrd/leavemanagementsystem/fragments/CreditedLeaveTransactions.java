package ai.infrrd.leavemanagementsystem.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.LinearLayout;

import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.adapters.LeaveHistoryAdapter;
import ai.infrrd.leavemanagementsystem.models.LeaveHistoryModel;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;

/**
 * Created by Anuj Dutt on 1/29/2017.
 */

public class CreditedLeaveTransactions extends Fragment {

    private String LOG_TAG = "";
    private View mFragmentView = null;
    private RecyclerView mLeaveHistoryRecyclerView = null;
    private LinearLayoutManager mLayoutManager = null;
    private LinearLayout mPlaceholderLayout = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        LOG_TAG = getClass().getSimpleName();
        mFragmentView = inflater.inflate(R.layout.fragment_leave_transactions_credited, container, false);
        initialiseUIComponents();
        fillLeaveDetails();
        return mFragmentView;
    }

    /**
     * Fill leave details of the employees
     */
    public void fillLeaveDetails() {
        DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<LeaveHistoryModel> leaveHistoryModelList = databaseHandler.getAllEmployeeLeaveDetailsByCategory(Constants.LEAVE_TYPE_CREDIT);
        if (leaveHistoryModelList.isEmpty()) {
            mPlaceholderLayout.setVisibility(View.VISIBLE);
            mLeaveHistoryRecyclerView.setVisibility(View.GONE);
        } else {
            LeaveHistoryAdapter leaveHistoryAdapter = new LeaveHistoryAdapter(getActivity(), leaveHistoryModelList);
            mLeaveHistoryRecyclerView.setAdapter(leaveHistoryAdapter);
            mPlaceholderLayout.setVisibility(View.GONE);
            mLeaveHistoryRecyclerView.setVisibility(View.VISIBLE);
        }
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        mPlaceholderLayout = (LinearLayout) mFragmentView.findViewById(R.id.placeholder_layout);
        mLeaveHistoryRecyclerView = (RecyclerView) mFragmentView.findViewById(R.id.recycler_view_leave_history);
        mLeaveHistoryRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(getActivity());
        mLeaveHistoryRecyclerView.setLayoutManager(mLayoutManager);
    }
}

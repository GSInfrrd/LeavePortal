package utilities;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import models.LeaveHistoryModel;

/**
 * Created by Anuj Dutt on 1/9/2017.
 */

public class LeaveHistoryAdapter extends RecyclerView.Adapter<LeaveHistoryAdapter.LeaveHistoryViewHolder> {

    List<LeaveHistoryModel> mLeaveHistoryModel = null;
    Context mContext = null;

    public LeaveHistoryAdapter(Context context, List<LeaveHistoryModel> leaveHistoryModel) {
        this.mLeaveHistoryModel = leaveHistoryModel;
        this.mContext = context;
    }

    public class LeaveHistoryViewHolder extends RecyclerView.ViewHolder {
        public TextView vhLeaveTypeTextView = null;
        public TextView vhLeaveDurationTextView = null;
        public TextView vhNoOfWorkingDays = null;
        public TextView vhLeaveCommentsTextview = null;

        public LeaveHistoryViewHolder(View view) {
            super(view);
            vhLeaveTypeTextView = (TextView) view.findViewById(R.id.textview_leave_type);
            vhLeaveDurationTextView = (TextView) view.findViewById(R.id.textview_leave_duration);
            vhNoOfWorkingDays = (TextView) view.findViewById(R.id.textview_no_of_working_days);
            vhLeaveCommentsTextview = (TextView) view.findViewById(R.id.textview_leave_comments);
        }
    }

    @Override
    public void onBindViewHolder(LeaveHistoryViewHolder holder, int position) {
        holder.vhLeaveTypeTextView.setText(mLeaveHistoryModel.get(position).getLeaveTypeName());
        holder.vhLeaveDurationTextView.setText(Utilities.getFormattedDate(mLeaveHistoryModel.get(position).getFromDate()) + " - " + Utilities.getFormattedDate(mLeaveHistoryModel.get(position).getToDate()));
        holder.vhNoOfWorkingDays.setText(String.valueOf(mLeaveHistoryModel.get(position).getNumberOfWorkingDays()));
        holder.vhLeaveCommentsTextview.setText(String.valueOf(mLeaveHistoryModel.get(position).getEmployeeComment()));

        switch (mLeaveHistoryModel.get(position).getRefStatus()) {
            case Constants.LEAVE_TYPE_APPROVED:
                holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorPrimaryDark));
                break;
            case Constants.LEAVE_TYPE_REJECTED:
                holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorAppliedLeaves));
                break;
            case Constants.LEAVE_TYPE_SUBMITTED:
                holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorSpentLeaves));
                break;
        }
    }

    @Override
    public LeaveHistoryViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.leave_history_recycler_view_item, parent, false);
        LeaveHistoryViewHolder leaveHistoryViewHolder = new LeaveHistoryViewHolder(view);
        return leaveHistoryViewHolder;
    }

    @Override
    public int getItemCount() {
        if (mLeaveHistoryModel != null) {
            Log.i("TAG", String.valueOf(mLeaveHistoryModel.size()));
            return mLeaveHistoryModel.size();
        } else {
            Log.i("TAG", "ZERO");
            return 0;
        }

    }
}

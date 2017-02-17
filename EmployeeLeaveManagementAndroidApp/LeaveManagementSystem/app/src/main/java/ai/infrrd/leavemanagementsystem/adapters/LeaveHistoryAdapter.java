package ai.infrrd.leavemanagementsystem.adapters;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.media.Image;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.LeaveHistoryModel;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;

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
        public TextView vhLeaveStatusTextview = null;
        public TextView vhLeaveAppliedDateTextview = null;
        public ImageView vhTransactionTypeImageview = null;

        public LeaveHistoryViewHolder(View view) {
            super(view);
            vhLeaveTypeTextView = (TextView) view.findViewById(R.id.textview_leave_type);
            vhLeaveDurationTextView = (TextView) view.findViewById(R.id.textview_leave_duration);
            vhNoOfWorkingDays = (TextView) view.findViewById(R.id.textview_no_of_working_days);
            vhLeaveStatusTextview = (TextView) view.findViewById(R.id.textview_leave_status);
            vhLeaveAppliedDateTextview = (TextView) view.findViewById(R.id.textview_leave_submitted_date);
            vhTransactionTypeImageview = (ImageView) view.findViewById(R.id.imageview_transaction_type);
        }
    }

    @Override
    public void onBindViewHolder(LeaveHistoryViewHolder holder, int position) {
        GradientDrawable drawable = (GradientDrawable) holder.vhLeaveTypeTextView.getBackground();
        holder.vhLeaveTypeTextView.setText(mLeaveHistoryModel.get(position).getLeaveTypeName());
        if(mLeaveHistoryModel.get(position).getToDate() != null)
        {
            holder.vhLeaveDurationTextView.setText(Utilities.getFormattedDate(mLeaveHistoryModel.get(position).getFromDate()) + " - " + Utilities.getFormattedDate(mLeaveHistoryModel.get(position).getToDate()));
        }
        else
        {
            holder.vhLeaveDurationTextView.setVisibility(View.GONE);
        }
        holder.vhNoOfWorkingDays.setText(String.valueOf(mLeaveHistoryModel.get(position).getNumberOfWorkingDays()));
        holder.vhLeaveStatusTextview.setText(String.valueOf(mLeaveHistoryModel.get(position).getStatusName()));
        holder.vhLeaveAppliedDateTextview.setText(new SimpleDateFormat("MMM dd, HH:MM").format(mLeaveHistoryModel.get(position).getCreatedDate()).toUpperCase());
        switch (mLeaveHistoryModel.get(position).getRefLeaveType()) {
            case Constants.LEAVE_TYPE_SICK_LEAVE:
                drawable.setColor(mContext.getResources().getColor(R.color.colorSickLeave));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_outgoing_leave);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorSickLeave));
                break;
            case Constants.LEAVE_TYPE_CASUAL_LEAVE:
                drawable.setColor(mContext.getResources().getColor(R.color.colorCasualLeave));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_outgoing_leave);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorCasualLeave));
                break;
            case Constants.LEAVE_TYPE_COMP_OFF:
                drawable.setColor(mContext.getResources().getColor(R.color.colorCompOff));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_outgoing_leave);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorCompOff));
                break;
            case Constants.LEAVE_TYPE_ADVANCED_LEAVE:
                drawable.setColor(mContext.getResources().getColor(R.color.colorAdvancedLeave));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_outgoing_leave);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorAdvancedLeave));
                break;
            case Constants.LEAVE_TYPE_LOP:
                drawable.setColor(mContext.getResources().getColor(R.color.colorLOP));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_outgoing_leave);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorLOP));
                break;
            case Constants.LEAVE_TYPE_REWARD:
                drawable.setColor(mContext.getResources().getColor(R.color.colorSickLeave));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_incoming_leaves);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorSickLeave));
                break;
            case Constants.LEAVE_TYPE_EARNED:
                drawable.setColor(mContext.getResources().getColor(R.color.colorSickLeave));
                holder.vhLeaveTypeTextView.setBackground(drawable);
                holder.vhTransactionTypeImageview.setImageResource(R.drawable.ic_incoming_leaves);
                //holder.vhNoOfWorkingDays.setTextColor(mContext.getResources().getColor(R.color.colorSickLeave));
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
            return mLeaveHistoryModel.size();
        } else {
            return 0;
        }

    }
}

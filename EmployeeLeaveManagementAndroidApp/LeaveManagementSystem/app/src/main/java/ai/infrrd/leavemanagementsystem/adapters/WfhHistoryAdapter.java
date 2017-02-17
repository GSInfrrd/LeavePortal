package ai.infrrd.leavemanagementsystem.adapters;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.text.SimpleDateFormat;
import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.WorkFromHomeModel;

/**
 * Created by Anuj Dutt on 1/30/2017.
 */

public class WfhHistoryAdapter extends RecyclerView.Adapter<WfhHistoryAdapter.WfhHistoryViewHolder> {

    List<WorkFromHomeModel> mWorkFromHomeModel = null;
    Context mContext = null;

    public WfhHistoryAdapter(Context context, List<WorkFromHomeModel> WorkFromHomeModel) {
        this.mWorkFromHomeModel = WorkFromHomeModel;
        this.mContext = context;
    }

    public class WfhHistoryViewHolder extends RecyclerView.ViewHolder {
        public TextView vhWfhReason = null;
        public TextView vhWfhDate = null;
        public TextView vhWfhAppliedDate = null;

        public WfhHistoryViewHolder(View view) {
            super(view);
            vhWfhReason = (TextView) view.findViewById(R.id.textview_wfh_reason);
            vhWfhDate = (TextView) view.findViewById(R.id.textview_wfh_date);
            vhWfhAppliedDate = (TextView) view.findViewById(R.id.textview_wfh_applied_date);
        }
    }

    @Override
    public void onBindViewHolder(WfhHistoryViewHolder holder, int position) {
        holder.vhWfhReason.setText(String.valueOf(mWorkFromHomeModel.get(position).getReason()));
        holder.vhWfhDate.setText(new SimpleDateFormat("dd/MM/yyyy").format(mWorkFromHomeModel.get(position).getDate()).toUpperCase());
        holder.vhWfhAppliedDate.setText(new SimpleDateFormat("MMM dd, yyyy").format(mWorkFromHomeModel.get(position).getCreatedDate()).toUpperCase());
    }

    @Override
    public WfhHistoryViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.wfh_recyclerview_item, parent, false);
        WfhHistoryViewHolder wfhHistoryViewHolder = new WfhHistoryViewHolder(view);
        return wfhHistoryViewHolder;
    }

    @Override
    public int getItemCount() {
        if (mWorkFromHomeModel != null) {
            return mWorkFromHomeModel.size();
        } else {
            return 0;
        }

    }
}

package ai.infrrd.leavemanagementsystem.adapters;

import android.content.Context;
import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

import ai.infrrd.leavemanagementsystem.AddActivity;
import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.EmployeeEducationDetails;
import ai.infrrd.leavemanagementsystem.utilities.Constants;

/**
 * Created by Anuj Dutt on 1/2/2017.
 * Adapter to bind education details to the list
 */

public class EducationAdapter extends RecyclerView.Adapter<EducationAdapter.EducationViewHolder> {

    private List<EmployeeEducationDetails> mEducationDetails = null;
    private boolean mShowEditicon = false;
    private int mLayoutId;
    private Context mContext = null;

    public EducationAdapter(List<EmployeeEducationDetails> educationDetails, boolean showEditIcon, int layoutId, Context context) {
        this.mEducationDetails = educationDetails;
        this.mShowEditicon = showEditIcon;
        this.mLayoutId = layoutId;
        this.mContext = context;
    }

    public class EducationViewHolder extends RecyclerView.ViewHolder {
        public TextView vhDegree = null;
        public TextView vhCollegeName = null;
        public TextView vhDuration = null;
        public TextView vhId = null;
        public ImageView vhEditData = null;

        public EducationViewHolder(View view) {
            super(view);
            vhDegree = (TextView) view.findViewById(R.id.profile_degree);
            vhCollegeName = (TextView) view.findViewById(R.id.profile_college_name);
            vhDuration = (TextView) view.findViewById(R.id.profile_degree_duration);
            vhId = (TextView) view.findViewById(R.id.education_id);
            vhEditData = (ImageView) view.findViewById(R.id.edit_education_data);
        }
    }

    @Override
    public int getItemCount() {
        if (mEducationDetails != null)
            return mEducationDetails.size();
        else
            return 0;
    }

    @Override
    public void onBindViewHolder(EducationAdapter.EducationViewHolder holder, final int position) {
        holder.vhDegree.setText(mEducationDetails.get(position).getDegree());
        holder.vhCollegeName.setText(mEducationDetails.get(position).getInstitution());
        holder.vhDuration.setText(mEducationDetails.get(position).getTimePeriod());
        if (!mShowEditicon) {
            holder.vhEditData.setVisibility(View.GONE);

        } else {
            holder.vhEditData.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Intent intent = new Intent(mContext, AddActivity.class);
                    intent.putExtra("id", mEducationDetails.get(position).getId());
                    intent.putExtra("type", Constants.DETAILS_EDUCATION_DETAILS);
                    mContext.startActivity(intent);
                }
            });

        }
    }

    @Override
    public EducationAdapter.EducationViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(mLayoutId, parent, false);
        EducationAdapter.EducationViewHolder educationViewHolder = new EducationAdapter.EducationViewHolder(view);
        return educationViewHolder;
    }
}

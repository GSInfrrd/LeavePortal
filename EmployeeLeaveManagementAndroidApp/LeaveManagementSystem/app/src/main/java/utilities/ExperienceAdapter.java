package utilities;

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
import models.EmployeeExperienceDetails;

/**
 * @author Anuj Dutt
 *         Adapter to bind experience details to the list
 */

public class ExperienceAdapter extends RecyclerView.Adapter<ExperienceAdapter.ExperienceViewHolder> {

    private List<EmployeeExperienceDetails> mExperienceDetails = null;
    private boolean mShowEditicon = false;
    private int mLayoutId;
    private Context mContext = null;

    public ExperienceAdapter(List<EmployeeExperienceDetails> experienceDetails, boolean showEditIcon, int layoutId, Context context) {
        this.mExperienceDetails = experienceDetails;
        this.mShowEditicon = showEditIcon;
        this.mLayoutId = layoutId;
        this.mContext = context;
    }

    public class ExperienceViewHolder extends RecyclerView.ViewHolder {
        public TextView vhDesignation = null;
        public TextView vhCompanyName = null;
        public TextView vhDuration = null;
        public TextView vhId = null;
        public ImageView vhEditData = null;

        public ExperienceViewHolder(View view) {
            super(view);
            vhDesignation = (TextView) view.findViewById(R.id.profile_designation);
            vhCompanyName = (TextView) view.findViewById(R.id.profile_company_name);
            vhDuration = (TextView) view.findViewById(R.id.profile_experience_duration);
            vhId = (TextView) view.findViewById(R.id.experience_id);
            vhEditData = (ImageView) view.findViewById(R.id.edit_data);
        }
    }

    @Override
    public int getItemCount() {
        if (mExperienceDetails != null)
            return mExperienceDetails.size();
        else
            return 0;
    }

    @Override
    public void onBindViewHolder(ExperienceAdapter.ExperienceViewHolder holder, final int position) {
        holder.vhId.setText(String.valueOf(mExperienceDetails.get(position).getId()));
        holder.vhCompanyName.setText(mExperienceDetails.get(position).getCompany());
        holder.vhDesignation.setText(mExperienceDetails.get(position).getRole());
        holder.vhDuration.setText(mExperienceDetails.get(position).getTimePeriod());
        if (!mShowEditicon) {
            holder.vhEditData.setVisibility(View.GONE);

        } else {
            holder.vhEditData.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Intent intent = new Intent(mContext, AddActivity.class);
                    intent.putExtra("id", mExperienceDetails.get(position).getId());
                    intent.putExtra("type", Constants.DETAILS_EXPERIENCE_DETAILS);
                    mContext.startActivity(intent);
                }
            });

        }
    }

    @Override
    public ExperienceAdapter.ExperienceViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(mLayoutId, parent, false);
        ExperienceViewHolder experienceViewHolder = new ExperienceViewHolder(view);
        return experienceViewHolder;
    }
}

package utilities;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

import ai.infrrd.leavemanagement.R;
import models.EmployeeEducationDetails;

/**
 * Created by E7440 on 1/2/2017.
 */

public class EducationAdapter extends RecyclerView.Adapter<EducationAdapter.EducationViewHolder> {

    private List<EmployeeEducationDetails> mEducationDetails = null;

    public EducationAdapter(List<EmployeeEducationDetails> educationDetails) {
        this.mEducationDetails = educationDetails;
    }

    public class EducationViewHolder extends RecyclerView.ViewHolder {
        public TextView vhDegree = null;
        public TextView vhCollegeName = null;
        public TextView vhDuration = null;

        public EducationViewHolder(View view) {
            super(view);
            vhDegree = (TextView) view.findViewById(R.id.profile_degree);
            vhCollegeName = (TextView) view.findViewById(R.id.profile_college_name);
            vhDuration = (TextView) view.findViewById(R.id.profile_degree_duration);
        }
    }
    @Override
    public int getItemCount() {
        if(mEducationDetails != null)
            return mEducationDetails.size();
        else
            return 0;
    }

    @Override
    public void onBindViewHolder(EducationAdapter.EducationViewHolder holder, int position) {
        holder.vhDegree.setText(mEducationDetails.get(position).getDegree());
        holder.vhCollegeName.setText(mEducationDetails.get(position).getInstitution());
        holder.vhDuration.setText(mEducationDetails.get(position).getTimePeriod());
    }

    @Override
    public EducationAdapter.EducationViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.education_recyclerview_item,parent,false);
        EducationAdapter.EducationViewHolder educationViewHolder = new EducationAdapter.EducationViewHolder(view);
        return educationViewHolder;
    }
}

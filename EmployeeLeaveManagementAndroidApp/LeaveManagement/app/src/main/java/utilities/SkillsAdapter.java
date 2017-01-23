package utilities;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;
import java.util.List;

import ai.infrrd.leavemanagement.R;
import models.EmployeeSkillDetails;

/**
 * Created by Anuj Dutt on 12/29/2016.
 */

public class SkillsAdapter extends RecyclerView.Adapter<SkillsAdapter.SkillsViewHolder> {

    public List<EmployeeSkillDetails> mSkills = null;

    public SkillsAdapter(List<EmployeeSkillDetails> skills) {
        this.mSkills = skills;
    }

    public class SkillsViewHolder extends RecyclerView.ViewHolder
    {
        public TextView mSkills = null;

        public SkillsViewHolder(View itemView) {
            super(itemView);
            mSkills = (TextView) itemView.findViewById(R.id.skills);
        }
    }

    @Override
    public void onBindViewHolder(SkillsViewHolder holder, int position) {
        holder.mSkills.setText(mSkills.get(position).getSkillName());
    }

    @Override
    public int getItemCount() {
        if(mSkills != null)
            return mSkills.size();
        else
            return 0;
    }

    @Override
    public SkillsViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.skills_recycler_view_item,parent,false);
        SkillsViewHolder skillsViewHolder = new SkillsViewHolder(view);
        return skillsViewHolder;
    }
}

package ai.infrrd.leavemanagementsystem.adapters;

import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.EmployeeSkillDetails;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;

/**
 * Created by Anuj Dutt on 12/29/2016.
 */

public class SkillsAdapter extends RecyclerView.Adapter<SkillsAdapter.SkillsViewHolder> {

    public List<EmployeeSkillDetails> mSkillList = null;
    private boolean mIcons = false;
    private int mLayoutId;
    private Context mContext = null;

    public SkillsAdapter(Context mContext, boolean mIcons, int mLayoutId, List<EmployeeSkillDetails> mSkillList) {
        this.mContext = mContext;
        this.mIcons = mIcons;
        this.mLayoutId = mLayoutId;
        this.mSkillList = mSkillList;
    }

    public class SkillsViewHolder extends RecyclerView.ViewHolder
    {
        public TextView mSkills = null;
        public TextView mSkillId = null;
        public ImageView mAddSkill = null;
        public ImageView mCancelSkill = null;

        public SkillsViewHolder(View itemView) {
            super(itemView);
            mSkills = (TextView) itemView.findViewById(R.id.skills);
            mSkillId = (TextView) itemView.findViewById(R.id.skill_id);
            mAddSkill = (ImageView) itemView.findViewById(R.id.add_skill_imageview);
            mCancelSkill = (ImageView) itemView.findViewById(R.id.cancel_skill_imageview);
        }
    }

    @Override
    public void onBindViewHolder(SkillsViewHolder holder, final int position) {
        holder.mSkills.setText(mSkillList.get(position).getSkillName().trim());
        if(mIcons)
        {
           // holder.mSkillId.setText(mSkillList.get(position).getId());
            if(mSkillList.get(position).isSelected())
            {
                holder.mCancelSkill.setVisibility(View.VISIBLE);
                holder.mAddSkill.setVisibility(View.GONE);

                holder.mCancelSkill.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        DatabaseHandler databaseHandler = new DatabaseHandler(mContext);
                        databaseHandler.deleteAllEmployeeSkillDetails();
                        mSkillList.get(position).setSelected(false);
                        databaseHandler.addEmployeeSkillsList(mSkillList);
                        updateRecyclerView(mSkillList);
                    }
                });
                holder.mAddSkill.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        //
                    }
                });
            }
            else{
                holder.mCancelSkill.setVisibility(View.GONE);
                holder.mAddSkill.setVisibility(View.VISIBLE);
                holder.mCancelSkill.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        //
                    }
                });
                holder.mAddSkill.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        DatabaseHandler databaseHandler = new DatabaseHandler(mContext);
                        databaseHandler.deleteAllEmployeeSkillDetails();
                        mSkillList.get(position).setSelected(true);
                        databaseHandler.addEmployeeSkillsList(mSkillList);
                        updateRecyclerView(mSkillList);
                    }
                });

            }
        }
    }

    @Override
    public int getItemCount() {
        if(mSkillList != null)
            return mSkillList.size();
        else
            return 0;
    }

    @Override
    public SkillsViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(mLayoutId, parent, false);
        SkillsViewHolder skillsViewHolder = new SkillsViewHolder(view);
        return skillsViewHolder;
    }

    public void updateRecyclerView(List<EmployeeSkillDetails> employeeSkillDetailsList)
    {
        DatabaseHandler databaseHandler = new DatabaseHandler(mContext);
        mSkillList = employeeSkillDetailsList;
        notifyDataSetChanged();
    }
}

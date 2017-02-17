package ai.infrrd.leavemanagementsystem.adapters;

import android.content.Context;
import android.widget.ArrayAdapter;

import java.util.List;

import ai.infrrd.leavemanagementsystem.models.WorkFromHomeModel;

/**
 * Created by Anuj Dutt on 1/23/2017.
 */

public class WorkFromHomeReasonAdapter extends ArrayAdapter{

    public List<WorkFromHomeModel> mWfhModelList = null;
    private Context mContext = null;

    public WorkFromHomeReasonAdapter(Context context, int resource, int textViewResourceId, List objects, Context mContext, List<WorkFromHomeModel> mWfhModelList) {
        super(context, resource, textViewResourceId, objects);
        this.mContext = mContext;
        this.mWfhModelList = mWfhModelList;
    }
}

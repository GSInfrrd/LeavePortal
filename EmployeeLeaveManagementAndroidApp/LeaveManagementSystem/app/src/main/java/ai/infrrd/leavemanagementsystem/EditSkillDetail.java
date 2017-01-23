package ai.infrrd.leavemanagementsystem;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;

import java.util.ArrayList;
import java.util.List;

import models.EmployeeSkillDetails;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;
import utilities.DatabaseHandler;
import utilities.SharedPreferenceManager;
import utilities.SkillsAdapter;
import utilities.Utilities;

/**
 * Created by Anuj Dutt on 1/23/2017.
 */

public class EditSkillDetail extends Fragment {

    private View mFragment = null;
    private RecyclerView mEditSkillsRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        mFragment = inflater.inflate(R.layout.fragment_edit_skill_details, container, false);
        setHasOptionsMenu(true);
        mEditSkillsRecyclerView = (RecyclerView) mFragment.findViewById(R.id.edit_skills_recycler_view);
        mEditSkillsRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(getActivity(), LinearLayoutManager.HORIZONTAL, false);
        mEditSkillsRecyclerView.setLayoutManager(mLayoutManager);
        setSkillsRecyclerView();
        return mFragment;
    }

    public void setSkillsRecyclerView() {
        DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeSkillDetails> employeeSkillDetailsList = databaseHandler.getAllEmployeeSkillDetails();
        SkillsAdapter skillsAdapter = new SkillsAdapter(getActivity(), true, R.layout.edit_skills_recyclerview_item, employeeSkillDetailsList);
        mEditSkillsRecyclerView.setAdapter(skillsAdapter);
    }

    public void saveSkillDetails() {
        DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeSkillDetails> employeeSkillDetailsList = databaseHandler.getAllEmployeeSkillDetails();
        List<EmployeeSkillDetails> employeeSkillDetails = new ArrayList<EmployeeSkillDetails>();
        for (EmployeeSkillDetails employeeSkillDetail : employeeSkillDetailsList) {
            if (employeeSkillDetail.isSelected()) {
                employeeSkillDetails.add(employeeSkillDetail);
            }
        }
        String userId = SharedPreferenceManager.getPreference(getActivity(), getString(R.string.user_preferences), getString(R.string.emp_id), getString(R.string.emp_id));
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<Boolean> call = apiInterface.saveSkillDetails(employeeSkillDetails, Integer.parseInt(userId));
        final ProgressDialog progressDialog = new ProgressDialog(getActivity(),
                R.style.AppTheme_Dark_Dialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(getString(R.string.saving_your_skills_details));
        progressDialog.show();
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                progressDialog.hide();
                Utilities.showInfoSnackbar(mFragment, getString(R.string.skill_details_saved), 2000);
                Intent intent = new Intent(getActivity(), ProfileActivity.class);
                startActivity(intent);
            }

            @Override
            public void onFailure(Throwable t) {
                progressDialog.hide();
                Utilities.showInfoSnackbar(mFragment, getString(R.string.retrofit_error_message), 2000);
            }
        });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            saveSkillDetails();
        }
        return true;
    }
}

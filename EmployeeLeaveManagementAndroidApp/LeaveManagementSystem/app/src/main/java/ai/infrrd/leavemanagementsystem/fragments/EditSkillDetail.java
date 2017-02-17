package ai.infrrd.leavemanagementsystem.fragments;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
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

import ai.infrrd.leavemanagementsystem.ProfileActivity;
import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.adapters.SkillsAdapter;
import ai.infrrd.leavemanagementsystem.models.EmployeeSkillDetails;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/23/2017.
 */

public class EditSkillDetail extends Fragment {
    private String LOG_TAG = "";
    private View mFragment = null;
    private RecyclerView mEditSkillsRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;
    private ProgressDialog mProgressDialog = null;
    private String mUserId = "";

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        mFragment = inflater.inflate(R.layout.fragment_edit_skill_details, container, false);
        initialiseUIComponents();
        setSkillsRecyclerView();
        return mFragment;
    }

    /**
     * Shows skills available in a list.
     */
    public void setSkillsRecyclerView() {
        DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeSkillDetails> employeeSkillDetailsList = databaseHandler.getAllEmployeeSkillDetails();
        SkillsAdapter skillsAdapter = new SkillsAdapter(getActivity(), true, R.layout.edit_skills_recyclerview_item, employeeSkillDetailsList);
        mEditSkillsRecyclerView.setAdapter(skillsAdapter);
    }

    /**
     * Extract data entered by the user.
     */
    public void extractUserEnteredData() {
        DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        List<EmployeeSkillDetails> employeeSkillDetailsList = databaseHandler.getAllEmployeeSkillDetails();
        List<EmployeeSkillDetails> employeeSkillDetails = new ArrayList<EmployeeSkillDetails>();
        for (EmployeeSkillDetails employeeSkillDetail : employeeSkillDetailsList) {
            if (employeeSkillDetail.isSelected()) {
                employeeSkillDetails.add(employeeSkillDetail);
            }
        }
        savePersonalDetails(employeeSkillDetails);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            extractUserEnteredData();
        }
        return super.onOptionsItemSelected(item);
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        setHasOptionsMenu(true);
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mEditSkillsRecyclerView = (RecyclerView) mFragment.findViewById(R.id.edit_skills_recycler_view);
        mEditSkillsRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(getActivity(), LinearLayoutManager.HORIZONTAL, false);
        mEditSkillsRecyclerView.setLayoutManager(mLayoutManager);
    }

    /**
     * Save personal data in the DB
     *
     * @param employeeSkillDetails - Data entered by user.
     */
    public void savePersonalDetails(List<EmployeeSkillDetails> employeeSkillDetails) {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.saving_your_skills_details));
        mProgressDialog.show();
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<Boolean> call = apiInterface.saveSkillDetails(employeeSkillDetails, Integer.parseInt(mUserId));
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                mProgressDialog.hide();
                Utilities.showInfoSnackbar(mFragment, getString(R.string.skill_details_saved), 2000);
                Intent intent = new Intent(getActivity(), ProfileActivity.class);
                startActivity(intent);
            }

            @Override
            public void onFailure(Throwable t) {
                mProgressDialog.hide();
                ConstructAlertDialogs.errorAlertDialog(getActivity(), getString(R.string.retrofit_error_message), false);
            }
        });
    }
}

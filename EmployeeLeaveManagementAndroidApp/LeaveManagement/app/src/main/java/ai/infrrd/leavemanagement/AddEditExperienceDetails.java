package ai.infrrd.leavemanagement;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import models.EmployeeExperienceDetails;
import utilities.DatabaseHandler;

/**
 * Created by Anuj Dutt on 1/10/2017.
 * Class to load the fragment for adding new/ altering existing experience details.
 */

public class AddEditExperienceDetails extends Fragment {
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_edit_profile_experience_details,container,false);
        Bundle bundle = this.getArguments();
        DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        EmployeeExperienceDetails employeeExperienceDetails = databaseHandler.getEmployeeExperienceDetailsById(bundle.getInt("id",0));

        return view;
    }
}

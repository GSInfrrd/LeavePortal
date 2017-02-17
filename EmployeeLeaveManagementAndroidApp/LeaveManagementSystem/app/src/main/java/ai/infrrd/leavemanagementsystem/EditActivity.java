package ai.infrrd.leavemanagementsystem;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.Menu;
import android.view.MenuItem;

import java.util.ArrayList;
import java.util.List;

import ai.infrrd.leavemanagementsystem.adapters.EducationAdapter;
import ai.infrrd.leavemanagementsystem.adapters.ExperienceAdapter;
import ai.infrrd.leavemanagementsystem.models.EmployeeEducationDetails;
import ai.infrrd.leavemanagementsystem.models.EmployeeExperienceDetails;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;

public class EditActivity extends AppCompatActivity {

    private RecyclerView mEditRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;
    private int mViewType;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_edit);
        mEditRecyclerView = (RecyclerView) findViewById(R.id.recycler_view_edit);
        mEditRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(EditActivity.this);
        mEditRecyclerView.setLayoutManager(mLayoutManager);
        Intent intent = getIntent();

        DatabaseHandler databaseHandler = new DatabaseHandler(this);
        switch (intent.getIntExtra("viewId", 0)) {
            case R.id.edit_experience:

                List<EmployeeExperienceDetails> employeeExperienceDetailsList = new ArrayList<EmployeeExperienceDetails>();
                employeeExperienceDetailsList = databaseHandler.getAllEmployeeExperienceDetails();
                getSupportActionBar().setTitle(getString(R.string.edit_experience));
                ExperienceAdapter experienceAdapter = new ExperienceAdapter(employeeExperienceDetailsList, true, R.layout.edit_experience_recyclerview_item, EditActivity.this);
                mEditRecyclerView.setAdapter(experienceAdapter);
                mViewType = Constants.DETAILS_EXPERIENCE_DETAILS;
                break;

            case R.id.edit_education:

                List<EmployeeEducationDetails> employeeEducationDetailsList = new ArrayList<EmployeeEducationDetails>();
                employeeEducationDetailsList = databaseHandler.getAllEmployeeEducationDetails();
                getSupportActionBar().setTitle(getString(R.string.edit_education));
                EducationAdapter educationAdapter = new EducationAdapter(employeeEducationDetailsList, true, R.layout.edit_education_recyclerview_item, EditActivity.this);
                mEditRecyclerView.setAdapter(educationAdapter);
                mViewType = Constants.DETAILS_EDUCATION_DETAILS;
                break;
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_edit_profile, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.action_add) {
            Intent intent = new Intent(EditActivity.this, AddActivity.class);
            intent.putExtra("type", mViewType);
            startActivity(intent);
        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
    }
}
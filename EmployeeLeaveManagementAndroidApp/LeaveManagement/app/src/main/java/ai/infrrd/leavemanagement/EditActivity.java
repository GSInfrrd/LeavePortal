package ai.infrrd.leavemanagement;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.Menu;
import android.view.MenuItem;

import java.util.ArrayList;
import java.util.List;

import models.EmployeeExperienceDetails;
import utilities.DatabaseHandler;
import utilities.ExperienceAdapter;

public class EditActivity extends AppCompatActivity {

    RecyclerView mEditRecyclerView = null;
    private RecyclerView.LayoutManager mLayoutManager = null;

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
                int x = employeeExperienceDetailsList.size();
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
            return true;
        }

        return super.onOptionsItemSelected(item);
    }
}
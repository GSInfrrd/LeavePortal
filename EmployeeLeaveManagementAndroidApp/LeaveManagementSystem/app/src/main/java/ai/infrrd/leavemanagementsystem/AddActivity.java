package ai.infrrd.leavemanagementsystem;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;

import utilities.Constants;

public class AddActivity extends AppCompatActivity {

    private int mDetailType;
    private int mDetailId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_add);
        Intent intent = getIntent();
        mDetailType = intent.getIntExtra("type", 0);
        mDetailId = intent.getIntExtra("id", 0);
        android.support.v4.app.Fragment selectedFragment = null;
        FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
        Bundle bundle = new Bundle();
        switch (mDetailType) {
            case Constants.DETAILS_EXPERIENCE_DETAILS:
                selectedFragment = new EditExperienceDetails();
                bundle.putInt("id", mDetailId);
                selectedFragment.setArguments(bundle);
                getSupportActionBar().setTitle(getString(R.string.experience));
                break;
            case Constants.DETAILS_EDUCATION_DETAILS:
                selectedFragment = new EditEducationDetails();
                bundle.putInt("id", mDetailId);
                selectedFragment.setArguments(bundle);
                getSupportActionBar().setTitle(getString(R.string.education));
                break;
            case Constants.DETAILS_PERSONAL_DETAILS:
                selectedFragment = new EditPersonalDetails();
                selectedFragment.setArguments(bundle);
                getSupportActionBar().setTitle(getString(R.string.about));
                break;
            case Constants.DETAILS_SKILLS_DETAILS:
                selectedFragment = new EditSkillDetail();
                selectedFragment.setArguments(bundle);
                getSupportActionBar().setTitle(getString(R.string.skills));
                break;
        }
        fragmentTransaction.replace(R.id.content_frame_add_activity, selectedFragment);
        fragmentTransaction.commit();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_add_activity, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.action_cancel_item) {
            Intent intent = new Intent(AddActivity.this, ProfileActivity.class);
            startActivity(intent);
        }
        if (id == R.id.action_save_item) {
            //
        }
        return super.onOptionsItemSelected(item);
    }
}

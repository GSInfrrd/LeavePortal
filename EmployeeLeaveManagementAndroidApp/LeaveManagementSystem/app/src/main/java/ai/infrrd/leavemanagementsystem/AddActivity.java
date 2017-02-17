package ai.infrrd.leavemanagementsystem;

import android.content.Intent;
import android.os.Bundle;
import android.support.v4.app.FragmentTransaction;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;

import ai.infrrd.leavemanagementsystem.fragments.EditEducationDetails;
import ai.infrrd.leavemanagementsystem.fragments.EditExperienceDetails;
import ai.infrrd.leavemanagementsystem.fragments.EditPersonalDetails;
import ai.infrrd.leavemanagementsystem.fragments.EditSkillDetail;
import ai.infrrd.leavemanagementsystem.utilities.Constants;

public class AddActivity extends AppCompatActivity {

    private static final String TAG = AddActivity.class.getSimpleName();
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
        fragmentTransaction.replace(R.id.content_frame_add_activity, selectedFragment, "abcd");
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

    @Override
    public void onBackPressed() {
        Intent intent = new Intent(AddActivity.this, ProfileActivity.class);
        startActivity(intent);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        Log.i("LOG_TAG", "Calling activity onActivityResult");
        super.onActivityResult(requestCode, resultCode, data);
//        Fragment fragmentById = getSupportFragmentManager().findFragmentByTag("abcd");
//        if(fragmentById == null){
//            Log.e(TAG, "Fragment is null");
//        }
//        else{
//            Log.e(TAG, "Fragment is not null");
//        }
//        try {
//            Log.d(TAG, "Casting now");
//            EditPersonalDetails editPersonalDetails = (EditPersonalDetails) fragmentById;
//            Log.d(TAG, "Calling onActivity result of fragment");
//            editPersonalDetails.onActivityResult(requestCode, resultCode, data);
//        } catch (Exception ex) {
//            Log.e(TAG, "Error casting some shit");
//            ex.printStackTrace();
//        }
    }
}

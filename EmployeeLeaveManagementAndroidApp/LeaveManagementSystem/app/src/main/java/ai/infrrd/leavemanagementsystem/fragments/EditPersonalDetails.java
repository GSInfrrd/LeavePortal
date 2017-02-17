package ai.infrrd.leavemanagementsystem.fragments;

import android.Manifest;
import android.app.Activity;
import android.app.AlertDialog;
import android.app.DatePickerDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.content.pm.ResolveInfo;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.os.Environment;
import android.provider.MediaStore;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.support.v4.content.FileProvider;
import android.support.v7.widget.AppCompatSpinner;
import android.util.Base64;
import android.util.Log;
import android.util.Patterns;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.DatePicker;
import android.widget.EditText;

import java.io.File;
import java.io.IOException;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

import ai.infrrd.leavemanagementsystem.ProfileActivity;
import ai.infrrd.leavemanagementsystem.R;
import ai.infrrd.leavemanagementsystem.models.EmployeeDetailsModel;
import ai.infrrd.leavemanagementsystem.models.EmployeePersonalDetails;
import ai.infrrd.leavemanagementsystem.rest.ApiClient;
import ai.infrrd.leavemanagementsystem.rest.ApiInterface;
import ai.infrrd.leavemanagementsystem.utilities.Constants;
import ai.infrrd.leavemanagementsystem.utilities.ConstructAlertDialogs;
import ai.infrrd.leavemanagementsystem.utilities.DatabaseHandler;
import ai.infrrd.leavemanagementsystem.utilities.Utilities;
import de.hdodenhof.circleimageview.CircleImageView;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/17/2017.
 * <p>
 * Class to return fragment to edit personal details
 */

public class EditPersonalDetails extends Fragment {
    private String LOG_TAG = null;
    private String mUserId = "";
    private String mCurrentPhotoPath;
    private static final int MY_PERMISSIONS_REQUEST_CAMERA = 101;
    //private static final int REQUEST_IMAGE_CAPTURE = 102;
    private static final int MY_PERMISSIONS_REQUEST_EXTERNAL_STORAGE = 103;
    private static final int REQUEST_EXTERNAL_STORAGE = 104;
    private static final int MY_PERMISSIONS_REQUEST_CAMERA_EXT_STORAGE = 105;
    private static final int REQUEST_IMAGE_CROP = 106;

    static final int REQUEST_IMAGE_CAPTURE = 1;
    static final int REQUEST_IMAGE_PICK = 1;
    private View mFragmentView = null;
    private EditText mFirstNameEditText = null;
    private EditText mLastNameEditText = null;
    private EditText mDobEditText = null;
    private EditText mPhoneNumberEditText = null;
    private EditText mCityEditText = null;
    private EditText mCountryEditText = null;
    private EditText mTwitterEditText = null;
    private EditText mFacebookEditText = null;
    private EditText mGooglePlusEditText = null;
    private CircleImageView mEmployeeImage = null;
    private ProgressDialog mProgressDialog = null;
    private AppCompatSpinner mChooseUploadMethodSpinner = null;
    private String mUserImage = null;
    private List<String> mPermissions = null;
    private Uri mImageUri = null;
    private Fragment mFragment = this;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        mFragmentView = inflater.inflate(R.layout.fragment_edit_personal_details, container, false);
        initialiseUIComponents();
        setPassedValues();
        checkForPermissions();
        return mFragmentView;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.action_save_item) {
            extractDataFromUIForSaving();
        }
        return super.onOptionsItemSelected(item);
    }

    /**
     * Prepopulates the stored employee data
     */

    public void setPassedValues() {
        final DatabaseHandler databaseHandler = new DatabaseHandler(getActivity());
        EmployeePersonalDetails employeePersonalDetails = databaseHandler.getEmployeePersonalDetails();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
        mFirstNameEditText.setText(employeePersonalDetails.getFirstName());
        mLastNameEditText.setText(employeePersonalDetails.getLastName());
        mDobEditText.setText(simpleDateFormat.format(employeePersonalDetails.getDateOfBirth()));
        mPhoneNumberEditText.setText(employeePersonalDetails.getPhoneNumber());
        mCityEditText.setText(employeePersonalDetails.getCity());
        mCountryEditText.setText(employeePersonalDetails.getCountry());
        mTwitterEditText.setText(employeePersonalDetails.getTwitter());
        mFacebookEditText.setText(employeePersonalDetails.getFacebook());
        mGooglePlusEditText.setText(employeePersonalDetails.getGooglePlus());
    }

    public class dateSetListener implements android.app.DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker datePicker, int year, int monthOfYear, int dayOfMonth) {
            Calendar date = Calendar.getInstance();
            SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
            date.set(year, monthOfYear, dayOfMonth, 0, 0, 0);
            mDobEditText.setText(simpleDateFormat.format(date.getTime()));
        }
    }

    /**
     * Validate user input.
     *
     * @param firstName
     * @param lastName
     * @param dob
     * @param twitter
     * @param facebook
     * @param googlePlus
     * @return
     */
    public boolean validate(String firstName, String lastName, String dob, String twitter, String facebook, String googlePlus) {
        boolean valid = true;

        if (firstName.isEmpty()) {
            mFirstNameEditText.setError(getString(R.string.please_enter_first_name));
            valid = false;
        } else {
            mFirstNameEditText.setError(null);
        }

        if (lastName.isEmpty()) {
            mLastNameEditText.setError(getString(R.string.please_enter_last_name));
            valid = false;
        } else {
            mLastNameEditText.setError(null);
        }

        if (dob.isEmpty()) {
            mDobEditText.setError(getString(R.string.please_enter_last_name));
            valid = false;
        } else {
            mDobEditText.setError(null);
        }

        if (!twitter.isEmpty()) {
            if (!Patterns.WEB_URL.matcher(twitter).matches()) {
                mTwitterEditText.setError(getString(R.string.please_enter_valid_url));
                valid = false;
            } else {
                mTwitterEditText.setError(null);
            }
        }
        if (!facebook.isEmpty()) {
            if (!Patterns.WEB_URL.matcher(facebook).matches()) {
                mFacebookEditText.setError(getString(R.string.please_enter_valid_url));
                valid = false;
            } else {
                mFacebookEditText.setError(null);
            }
        }
        if (!googlePlus.isEmpty()) {
            if (!Patterns.WEB_URL.matcher(googlePlus).matches()) {
                mGooglePlusEditText.setError(getString(R.string.please_enter_valid_url));
                valid = false;
            } else {
                mGooglePlusEditText.setError(null);
            }
        }

        return valid;
    }

    /**
     * Get data entered by user.
     */
    public void extractDataFromUIForSaving() {
        EmployeeDetailsModel employeeDetailsModel = new EmployeeDetailsModel();
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");
        if (validate(mFirstNameEditText.getText().toString(), mLastNameEditText.getText().toString(), mDobEditText.getText().toString(), mTwitterEditText.getText().toString(), mFacebookEditText.getText().toString(), mGooglePlusEditText.getText().toString())) {
            employeeDetailsModel.setId(Integer.parseInt(mUserId));
            employeeDetailsModel.setFirstName(mFirstNameEditText.getText().toString());
            employeeDetailsModel.setLastName(mLastNameEditText.getText().toString());
            try {
                employeeDetailsModel.setDateOfBirth(simpleDateFormat.parse(mDobEditText.getText().toString()));
            } catch (ParseException e) {
                e.printStackTrace();
            }
            employeeDetailsModel.setTelephone(mPhoneNumberEditText.getText().toString());
            employeeDetailsModel.setCity(mCityEditText.getText().toString());
            employeeDetailsModel.setCountry(mCountryEditText.getText().toString());
            employeeDetailsModel.setTwitterLink(mTwitterEditText.getText().toString());
            employeeDetailsModel.setFacebookLink(mFacebookEditText.getText().toString());
            employeeDetailsModel.setGooglePlusLink(mGooglePlusEditText.getText().toString());
            savePersonalDetails(employeeDetailsModel);
        }
    }

    /**
     * Initialise UI components.
     */
    public void initialiseUIComponents() {
        setHasOptionsMenu(true);
        SharedPreferences userPreferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_USER, Context.MODE_PRIVATE);
        LOG_TAG = getClass().getSimpleName();
        mUserId = userPreferences.getString(Constants.PreferenceConstants.PREFS_EMPLOYEE_ID, "0");
        mFirstNameEditText = (EditText) mFragmentView.findViewById(R.id.first_name_edittext);
        mLastNameEditText = (EditText) mFragmentView.findViewById(R.id.last_name_edittext);
        mDobEditText = (EditText) mFragmentView.findViewById(R.id.dob_edittext);
        mPhoneNumberEditText = (EditText) mFragmentView.findViewById(R.id.phone_number_edittext);
        mCityEditText = (EditText) mFragmentView.findViewById(R.id.city_edittext);
        mCountryEditText = (EditText) mFragmentView.findViewById(R.id.country_edittext);
        mTwitterEditText = (EditText) mFragmentView.findViewById(R.id.twitter_edittext);
        mFacebookEditText = (EditText) mFragmentView.findViewById(R.id.facebook_edittext);
        mGooglePlusEditText = (EditText) mFragmentView.findViewById(R.id.googleplus_edittext);
        mEmployeeImage = (CircleImageView) mFragmentView.findViewById(R.id.profile_image);
        mChooseUploadMethodSpinner = (AppCompatSpinner) mFragmentView.findViewById(R.id.choose_upload_method_spinner);
        mUserImage = userPreferences.getString(Constants.PreferenceConstants.PREFS_USER_IMAGE, null);
        mPermissions = new ArrayList<String>();
        if (mUserImage != null) {
            byte[] imageByteArray = Base64.decode(mUserImage, Base64.DEFAULT);
//            Glide.with(getActivity())
//                    .load(imageByteArray)
//                    .asBitmap()
//                    .placeholder(R.drawable.default_image).fitCenter()
//                    .into(mEmployeeImage);
            //           mEmployeeImage.setImageBitmap();
        }
        String[] chooseUploadChoices = new String[]{getActivity().getString(R.string.choose_from_gallery), getActivity().getString(R.string.take_a_photo)};
        ArrayAdapter<String> arrayAdapter = new ArrayAdapter<String>(getActivity(), R.layout.support_simple_spinner_dropdown_item, chooseUploadChoices);
        mChooseUploadMethodSpinner.setAdapter(arrayAdapter);
        mChooseUploadMethodSpinner.setSelection(0, false);
        setListeners();
    }

    /**
     * Set listeners for relevant UI components.
     */
    public void setListeners() {
        mDobEditText.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                Calendar now = Calendar.getInstance();
                DatePickerDialog datePickerDialog = new DatePickerDialog(getActivity(), new dateSetListener(),
                        now.get(Calendar.YEAR),
                        now.get(Calendar.MONTH),
                        now.get(Calendar.DAY_OF_MONTH));
                datePickerDialog.getDatePicker().setMaxDate(Calendar.getInstance().getTime().getTime());
                datePickerDialog.show();
            }
        });
        mDobEditText.setFocusable(false);
        mEmployeeImage.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Log.i(LOG_TAG, "Click on image");
                //mChooseUploadMethodSpinner.performClick();

            }
        });
        mChooseUploadMethodSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                Log.i(LOG_TAG, String.valueOf(position));
                switch (position) {
                    case 0:
                        Log.i(LOG_TAG, String.valueOf(position));
                        dispatchTakePictureIntent();
                        break;
                    case 1:
                        Log.i(LOG_TAG, String.valueOf(position));
                        dispatchTakePictureIntent();
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                Log.i(LOG_TAG, "Nothing selected");
            }
        });


    }

    /**
     * Save personal data in the DB
     *
     * @param employeeDetailsModel - Data entered by user.
     */
    public void savePersonalDetails(EmployeeDetailsModel employeeDetailsModel) {
        mProgressDialog = Utilities.showProgressDialog(getActivity(), getString(R.string.saving_your_personal_details));
        mProgressDialog.show();
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
        Call<Boolean> call = apiInterface.savePersonalDetails(employeeDetailsModel);
        call.enqueue(new Callback<Boolean>() {
            @Override
            public void onResponse(Response<Boolean> response, Retrofit retrofit) {
                mProgressDialog.hide();
                Utilities.showInfoSnackbar(mFragmentView, getString(R.string.personal_details_saved), 2000);
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

    /**
     * Check for permission.
     * <p>
     * If present - Proceed with full functionality.
     * <p>
     * If not present - Ask user for permissions.
     */
    public void checkForPermissions() {
        SharedPreferences preferences = getActivity().getSharedPreferences(Constants.PreferenceConstants.PREFS_NAME_PERMISSION, Context.MODE_PRIVATE);
        preferences.edit().putBoolean(Constants.PreferenceConstants.PREFS_PERM_DENIED, false).apply();
        if (!checkForPreviousPermissions()) {
            Log.i(LOG_TAG, "Permissions previously denied/not present");
            preferences.edit().putBoolean(Constants.PreferenceConstants.PREFS_PERM_DENIED, true).apply();
        }
        if (preferences.getBoolean(Constants.PreferenceConstants.PREFS_PERM_DENIED, true)) {
            askUserForPermissions();
        } else {
            Log.i(LOG_TAG, "Permissions already present");
        }
    }

    /**
     * Check for previously given persmissions
     */
    public boolean checkForPreviousPermissions() {
        Log.i(LOG_TAG, "checkForPreviousPermissions");
        mPermissions.removeAll(mPermissions);
        if (ContextCompat.checkSelfPermission(getActivity(), Manifest.permission.CAMERA) != PackageManager.PERMISSION_GRANTED) {
            mPermissions.add(Manifest.permission.CAMERA);
        }
        if (ContextCompat.checkSelfPermission(getActivity(), Manifest.permission.WRITE_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED) {
            mPermissions.add(Manifest.permission.WRITE_EXTERNAL_STORAGE);
        }
        if (ContextCompat.checkSelfPermission(getActivity(), Manifest.permission.READ_EXTERNAL_STORAGE) != PackageManager.PERMISSION_GRANTED) {
            mPermissions.add(Manifest.permission.READ_EXTERNAL_STORAGE);
        }
        return !(mPermissions.size() > 0);
    }

    /**
     * Ask user for the permissions
     */
    public void askUserForPermissions() {
        Log.i(LOG_TAG, "askUserForPermissions");
        if (mPermissions.size() > 0) {
            requestPermissions(mPermissions.toArray(new String[mPermissions.size()]), MY_PERMISSIONS_REQUEST_CAMERA_EXT_STORAGE);
        }
    }

    @Override
    public void onRequestPermissionsResult(int requestCode, @NonNull String[] permissions, @NonNull int[] grantResults) {
        Log.i(LOG_TAG, "Return from taking permission");
        switch (requestCode) {
            case MY_PERMISSIONS_REQUEST_CAMERA_EXT_STORAGE:
                if (grantResults.length > 0) {
                    boolean isCameraGranted = (grantResults[0] == PackageManager.PERMISSION_GRANTED);
                    boolean isExternalStorageWriteGranted = (grantResults[1] == PackageManager.PERMISSION_GRANTED);
                    boolean isExternalStorageReadGranted = (grantResults[2] == PackageManager.PERMISSION_GRANTED);
                    if (isCameraGranted && isExternalStorageWriteGranted && isExternalStorageReadGranted) {
                        Log.i(LOG_TAG, "Permissions granted for taking and storing photo");
                    } else {
                        Log.i(LOG_TAG, "Permissions denied for taking and storing photo");
                        if (ActivityCompat.shouldShowRequestPermissionRationale(getActivity(), Manifest.permission.CAMERA) || ActivityCompat.shouldShowRequestPermissionRationale(getActivity(), Manifest.permission.WRITE_EXTERNAL_STORAGE) || ActivityCompat.shouldShowRequestPermissionRationale(getActivity(), Manifest.permission.READ_EXTERNAL_STORAGE)) {
                            showRationaleForGivenString(getActivity().getString(R.string.camera_external_storage_permission_rationale), MY_PERMISSIONS_REQUEST_CAMERA_EXT_STORAGE);
                        }
                    }
                }
                break;
        }
    }


    /**
     * Show the rationale for a given String
     */
    private void showRationaleForGivenString(String inputString, final int permission) {
        Log.d(LOG_TAG, "showRationaleForGivenString + " + inputString);
        new AlertDialog.Builder(getActivity())
                .setTitle("Permission Required")
                .setMessage(inputString)
                .setPositiveButton("RE-TRY", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        switch (permission) {
                            case MY_PERMISSIONS_REQUEST_CAMERA_EXT_STORAGE:
                                ActivityCompat.requestPermissions(getActivity(),
                                        new String[]{Manifest.permission.CAMERA, Manifest.permission.WRITE_EXTERNAL_STORAGE, Manifest.permission.READ_EXTERNAL_STORAGE}, MY_PERMISSIONS_REQUEST_CAMERA_EXT_STORAGE);

                                dialog.dismiss();
                                break;
                            default:
                                dialog.dismiss();

                        }
                        // continue with delete
                    }
                })
                .setNegativeButton("I'M SURE", new DialogInterface.OnClickListener() {
                    public void onClick(DialogInterface dialog, int which) {
                        // do nothing
                        dialog.dismiss();

                    }
                })
                .setCancelable(false)
                .show();
    }


    public void cropImageTakenFromDevice() {
        Log.e(LOG_TAG, "cropImageTakenFromDevice");
        Log.e(LOG_TAG, mImageUri.toString());
        Intent cropIntent = new Intent("com.android.camera.action.CROP");
        List<ResolveInfo> resInfoList = getActivity().getPackageManager().queryIntentActivities(cropIntent, PackageManager.MATCH_DEFAULT_ONLY);
        for (ResolveInfo resolveInfo : resInfoList) {
            String packageName = resolveInfo.activityInfo.packageName;
            Log.e(LOG_TAG, packageName);
            getActivity().grantUriPermission(packageName, mImageUri, Intent.FLAG_GRANT_WRITE_URI_PERMISSION | Intent.FLAG_GRANT_READ_URI_PERMISSION);
        }
        cropIntent.setDataAndType(mImageUri, "image/*");
        cropIntent.putExtra("crop", "true");
        cropIntent.putExtra("aspectX", 1);
        cropIntent.putExtra("aspectY", 1);
        cropIntent.putExtra("outputX", 256);
        cropIntent.putExtra("outputY", 256);
        cropIntent.putExtra("return-data", true);
        cropIntent.putExtra(MediaStore.EXTRA_OUTPUT, mImageUri);
        startActivityForResult(cropIntent, REQUEST_IMAGE_CROP);
    }


    private void dispatchTakePictureIntent() {
        Intent takePictureIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);
        if (takePictureIntent.resolveActivity(getActivity().getPackageManager()) != null) {
            File photoFile = null;
            try {
                photoFile = Utilities.createEmptyImageFile(getActivity());
                mCurrentPhotoPath = photoFile.getAbsolutePath();
            } catch (IOException ex) {
                // Error occurred while creating the File
                ex.printStackTrace();
            }

            // Continue only if the File was successfully created
            if (photoFile != null) {
                mImageUri = FileProvider.getUriForFile(getActivity(), getActivity().getApplicationContext().getPackageName() + ".provider", photoFile);
                Log.i(LOG_TAG, mImageUri.toString());
                takePictureIntent.putExtra(MediaStore.EXTRA_OUTPUT, mImageUri);
                takePictureIntent.putExtra("crop", true);
                mFragment.startActivityForResult(takePictureIntent, REQUEST_IMAGE_CAPTURE);
                Log.i(LOG_TAG, getActivity().getExternalFilesDir(Environment.DIRECTORY_PICTURES).getAbsolutePath());
            }
        }
    }

    private void dispatchPickFromGalleryIntent() {
        Intent pickPictureIntent = new Intent(Intent.ACTION_GET_CONTENT);
        pickPictureIntent.setType("image/*");
        if (pickPictureIntent.resolveActivity(getActivity().getPackageManager()) != null) {
            startActivityForResult(pickPictureIntent, REQUEST_IMAGE_PICK);
        }
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (requestCode == REQUEST_IMAGE_CAPTURE && resultCode == Activity.RESULT_OK) {
            Bundle extras = data.getExtras();
            Bitmap imageBitmap = (Bitmap) extras.get("data");
            mEmployeeImage.setImageBitmap(imageBitmap);
        }
        if (requestCode == REQUEST_IMAGE_PICK && resultCode == Activity.RESULT_OK) {
            Bundle extras = data.getExtras();
            Bitmap imageBitmap = (Bitmap) extras.get("data");
            mEmployeeImage.setImageBitmap(imageBitmap);
        }
    }
}
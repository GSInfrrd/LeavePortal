package ai.infrrd.leavemanagementsystem.utilities;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.Environment;
import android.support.design.widget.Snackbar;
import android.util.Log;
import android.view.View;

import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

import ai.infrrd.leavemanagementsystem.R;

/**
 * Created by Anuj Dutt on 1/13/2017.
 * <p>
 * Contains commonly used utility methods.
 */

public class Utilities {

    private static String LOG_TAG = "";

    /**
     * Give ordinal value of an integer number.
     *
     * @param day - Integer to be converted
     * @return - Ordinal value
     */
    public static String getDayNumberSuffix(int day) {

        LOG_TAG = "getDayNumberSuffix";
        Log.i(LOG_TAG, "Entering method");
        if (day >= 11 && day <= 13) {
            return String.valueOf(day) + "th";
        }
        switch (day % 10) {
            case 1:
                return String.valueOf(day) + "st";
            case 2:
                return String.valueOf(day) + "nd";
            case 3:
                return String.valueOf(day) + "rd";
            default:
                return String.valueOf(day) + "th";
        }
    }

    /**
     * Show snackbar on the screen
     *
     * @param view     - Parent view
     * @param message  - Message to be shown in the snackbar
     * @param duration - The duration for which the message is to be shown
     */
    public static void showInfoSnackbar(View view, String message, int duration) {
        LOG_TAG = "showInfoSnackbar";
        Log.i(LOG_TAG, "Entering method");
        Snackbar.make(view, message, duration).show();
    }

    /**
     * Change format of date to dd/MM/yy.
     *
     * @param inputDate
     * @return Fomatted date
     */
    public static String getFormattedDate(Date inputDate) {
        LOG_TAG = "getFormattedDate";
        Log.i(LOG_TAG, "Entering method");
        String dateInString = "";
        return new SimpleDateFormat("dd/MM/yy", Locale.ENGLISH).format(inputDate).toString().toUpperCase();
    }

    /**
     * Set the time of day to midnight for date inputted.
     *
     * @param inputDate - Input date in milliseconds from 1970
     * @return - Date in milliseconds from 1970 with time of day set to 00:00:00
     */
    public static long setTimeToMidnight(long inputDate) {
        LOG_TAG = "setTimeToMidnight";
        Log.i(LOG_TAG, "Entering method");
        Calendar calendar = Calendar.getInstance();

        calendar.setTimeInMillis(inputDate);
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND, 0);

        return calendar.getTime().getTime();
    }

    /**
     * Show progress dialog with passed message.
     *
     * @param context - context associated with dialog
     * @param message - message to be shown
     * @return - return the progress dialog
     */
    public static ProgressDialog showProgressDialog(Context context, String message) {
        ProgressDialog progressDialog = new ProgressDialog(context, R.style.AppTheme_Dark_Dialog);
        progressDialog.setIndeterminate(true);
        progressDialog.setMessage(message);
        progressDialog.show();
        return progressDialog;
    }

    /**
     * Creates an empty image file.
     */
    public static File createEmptyImageFile(Context context) throws IOException {
        // Create an image file name
        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
        String imageFileName = "JPEG_" + timeStamp + "_";
        File storageDir = context.getExternalFilesDir(Environment.DIRECTORY_PICTURES);
        File imageFile = File.createTempFile(
                imageFileName,  /* prefix */
                ".jpg",         /* suffix */
                storageDir      /* directory */
        );
        return imageFile;
    }
}

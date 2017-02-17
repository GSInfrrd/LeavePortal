package ai.infrrd.leavemanagementsystem.utilities;

import android.app.Activity;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.AlertDialog;

import ai.infrrd.leavemanagementsystem.R;

/**
 * Created by Anuj Dutt on 2/1/2017.
 * Returns inflated alert dialogs for use across the application.
 */

public class ConstructAlertDialogs {
    /**
     * Construct an AlertDialog to show retry events.
     * @param context - Context to be used.
     * @param message - Error message to be shown.
     * @param intent - Intent of the activity to be reloaded.
     * @return - AlertDialog inflated.
     */

    public static AlertDialog retryAlertDialog(final Context context, String message, final Intent intent)
    {
        AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder.setMessage(message);
        builder.setCancelable(false);
        builder.setPositiveButton(context.getString(R.string.retry), new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                ((Activity)context).finish();
                context.startActivity(intent);
            }
        });
        AlertDialog alertDialog = builder.create();
        return alertDialog;
    }

    /**
     * Construct an AlertDialog to show error events.
     * @param context - Context to be used.
     * @param message - Error message to be shown.
     * @param toFinishActivity - If current activity should be restarted or not.
     * @return - AlertDialog inflated.
     */

    public static AlertDialog errorAlertDialog(final Context context, String message, final boolean toFinishActivity)
    {
        final AlertDialog.Builder builder = new AlertDialog.Builder(context);
        builder.setMessage(message);
        builder.setCancelable(false);
        builder.setPositiveButton(context.getString(R.string.okay), new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                if(toFinishActivity)
                    ((Activity)context).finish();
                else
                    dialog.cancel();
            }
        });
        AlertDialog alertDialog = builder.create();
        alertDialog.show();
        return alertDialog;
    }
}

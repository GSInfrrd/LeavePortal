package ai.infrrd.leavemanagementsystem.utilities;

import android.content.Context;
import android.content.SharedPreferences;

/**
 * Created by Anuj Dutt on 12/26/2016.
 */

public class SharedPreferenceManager {

    public static void addPreference(Context context, String preferenceName, String key, String value) {
        SharedPreferences sharedpreferences = context.getSharedPreferences(preferenceName, Context.MODE_PRIVATE);
        SharedPreferences.Editor editor = sharedpreferences.edit();
        editor.putString(key, value);
        editor.apply();
    }

    public static String getPreference(Context context, String preferenceName, String key, String defaultValue) {
        SharedPreferences sharedpreferences = context.getSharedPreferences(preferenceName, Context.MODE_PRIVATE);
        return sharedpreferences.getString(key, defaultValue);
    }
}
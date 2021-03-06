package ai.infrrd.leavemanagementsystem.utilities;

import android.util.Log;

import com.google.gson.JsonDeserializationContext;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonElement;

import java.lang.reflect.Type;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;

/**
 * @author Anuj Dutt
 *         A TypeAdapter to deserailize DateTime received from API into Date.
 */

public class DateDeserializer implements JsonDeserializer<Date> {

    @Override
    public Date deserialize(JsonElement element, Type type, JsonDeserializationContext context) {
        String date = element.getAsString();

        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss", Locale.ENGLISH);

        try {
            return format.parse(date);
        } catch (ParseException exp) {
            Log.e(getClass().getSimpleName(), "ParseException", exp);
            return null;
        }
    }
}

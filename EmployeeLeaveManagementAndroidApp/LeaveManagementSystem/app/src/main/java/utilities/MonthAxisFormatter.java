package utilities;

import com.github.mikephil.charting.components.AxisBase;
import com.github.mikephil.charting.formatter.IAxisValueFormatter;

/**
 * Created by Anuj Dutt on 1/9/2017.
 * Sets the x axis to represent months.
 */

public class MonthAxisFormatter implements IAxisValueFormatter {

    protected String[] mMonths = new String[]{
            "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    };

    @Override
    public String getFormattedValue(float value, AxisBase axis) {
        return mMonths[(int) value];
    }
}

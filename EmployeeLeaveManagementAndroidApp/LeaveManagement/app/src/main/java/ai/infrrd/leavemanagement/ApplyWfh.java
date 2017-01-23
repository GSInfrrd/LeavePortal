package ai.infrrd.leavemanagement;

import android.app.DatePickerDialog;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.Transformation;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.Calendar;
import java.util.List;

import models.HolidayModel;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/3/2017.
 *
 * Loads the WFH fragment into the navigation drawer content
 */

public class ApplyWfh extends Fragment{

    private View view = null;

    private ImageView mImgCompOff = null;
    private EditText startDate, endDate;
    private int selectedStartYear;

    private int selectedStartDay;
    private int selectedStartMonth;
    private int selectedEndDay;
    private int selectedEndMonth;
    private int selectedEndYear;

    int initialHeight;
    int actualHeight;

    private DatePickerDialog SecD, firstD;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        view = inflater.inflate(R.layout.fragment_apply_wfh, container, false);


        ApiInterface apiService =
                ApiClient.getClient().create(ApiInterface.class);

        Call<List<HolidayModel>> call = apiService.getHolidayList();

        call.enqueue(new Callback<List<HolidayModel>>() {

            @Override
            public void onResponse(Response<List<HolidayModel>> response, Retrofit retrofit) {
                List<HolidayModel> holidays = response.body();
                Log.d("TAG", "Number of holidays received: " + holidays.size());
            }

            @Override
            public void onFailure(Throwable t) {
                Log.d("TAG", "Number of holidayModels received:");
            }
        });





        startDate = (EditText) view.findViewById(R.id.editText1);
        endDate = (EditText) view.findViewById(R.id.editText2);


        final Calendar c = Calendar.getInstance();
        selectedEndDay = selectedStartDay = c.get(Calendar.DAY_OF_MONTH);
        selectedEndMonth = selectedStartMonth = c.get(Calendar.MONTH);
        selectedEndYear = selectedStartYear = c.get(Calendar.YEAR);
        SecD = new DatePickerDialog(getActivity(), new SecondDate(),
                selectedEndYear, selectedEndMonth, selectedEndDay);

        firstD = new DatePickerDialog(getActivity(), new FirstDate(),
                selectedStartYear, selectedStartMonth, selectedStartDay);
        startDate.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub

                firstD.show();

            }
        });

        endDate.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                // TODO Auto-generated method stub

                SecD.show();

            }
        });


        mImgCompOff = (ImageView) view.findViewById(R.id.img_comp_off);

        mImgCompOff.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                mImgCompOff.animate().scaleX(1.3f);
                mImgCompOff.animate().scaleY(1.3f);
//                Ani a=new Ani();
//                a.setDuration(2);
//                mImgCompOff.startAnimation(a);
            }
        });

        final Animation in = new AlphaAnimation(0.0f, 1.0f);
        in.setDuration(500);

        final TextView txt = (TextView) view.findViewById(R.id.working_days);
        txt.setOnClickListener( new View.OnClickListener() {
            public void onClick(View v) {

                txt.startAnimation(in);

            }
        });

        return view;
    }


    class FirstDate implements DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker view, int year, int monthOfYear,
                              int dayOfMonth) {
            // Doing thing with first Date pick Dialog

            selectedStartYear = year;
            selectedStartMonth = monthOfYear;
            selectedStartDay = dayOfMonth;

            startDate.setText((selectedStartMonth + 1) + "/" + selectedStartDay
                    + "/" + selectedStartYear);

            // set minimum date of end datepicker
            Calendar c2 = Calendar.getInstance();
            c2.set(year, monthOfYear, dayOfMonth);

            SecD.getDatePicker().setMinDate(c2.getTime().getTime());

        }

    }

    class SecondDate implements DatePickerDialog.OnDateSetListener {
        @Override
        public void onDateSet(DatePicker view, int year, int monthOfYear,
                              int dayOfMonth) {
            // Doing thing with second Date Picker Dialog

            selectedEndYear = year;
            selectedEndMonth = monthOfYear;
            selectedEndDay = dayOfMonth;

            endDate.setText((selectedEndMonth + 1) + "/" + selectedEndDay + "/"
                    + selectedEndYear);
        }

    }


    class Ani extends Animation
    {
        public Ani() {
        }

        @Override
        protected void applyTransformation(float interpolatedTime, Transformation t) {
            int newHeight;

            newHeight = (int)(mImgCompOff.getHeight() + (mImgCompOff.getHeight() * interpolatedTime));

            mImgCompOff.setMinimumHeight(newHeight);
            mImgCompOff.requestLayout();
        }

        @Override
        public void initialize(int width, int height, int parentWidth, int parentHeight) {
            super.initialize(width, height, parentWidth, parentHeight);
            initialHeight = actualHeight;
        }

        @Override
        public boolean willChangeBounds() {
            return true;
        }
    };

}

package ai.infrrd.leavemanagementsystem;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.AppCompatSpinner;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.List;

import models.WorkFromHomeModel;
import rest.ApiClient;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.Callback;
import retrofit.Response;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/18/2017.
 * <p>
 * Loads the WFH fragment into the navigation drawer content
 */

public class ApplyWorkFromHome extends Fragment{

    private View mFragmentView = null;
    private AppCompatSpinner mwfhReasonSpinner = null;
    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {

        mFragmentView = inflater.inflate(R.layout.fragment_apply_wfh,container,false);
        mwfhReasonSpinner = (AppCompatSpinner) mFragmentView.findViewById(R.id.wfh_reason_spinner);
        ApiInterface apiInterface = ApiClient.getClient().create(ApiInterface.class);
//        Call<List<WorkFromHomeModel>> call =  apiInterface.getWorkFromHomeReasonsList();
//        call.enqueue(new Callback<List<WorkFromHomeModel>>() {
//            @Override
//            public void onResponse(Response<List<WorkFromHomeModel>> response, Retrofit retrofit) {
//                List<WorkFromHomeModel> workFromHomeModelList = response.body();
//                List<String> reasonList
//            }
//
//            @Override
//            public void onFailure(Throwable t) {
//
//            }
//        }));
        return mFragmentView;
    }
}

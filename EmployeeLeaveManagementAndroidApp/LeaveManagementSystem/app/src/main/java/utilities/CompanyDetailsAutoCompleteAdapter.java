package utilities;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.Filter;
import android.widget.Filterable;
import android.widget.TextView;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import ai.infrrd.leavemanagementsystem.R;
import models.CompanyDetailsResponse;
import rest.ApiInterface;
import retrofit.Call;
import retrofit.GsonConverterFactory;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/10/2017.
 * Adapter to populate suggestions for company in adding experience details.
 */

public class CompanyDetailsAutoCompleteAdapter extends BaseAdapter implements Filterable {

    private static final int MAX_RESULTS = 10;
    private Context mContext;
    private List<CompanyDetailsResponse> resultList = new ArrayList<CompanyDetailsResponse>();

    public CompanyDetailsAutoCompleteAdapter(Context mContext) {
        this.mContext = mContext;
    }

    @Override
    public int getCount() {
        return resultList.size();
    }

    @Override
    public CompanyDetailsResponse getItem(int index) {
        return resultList.get(index);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        if (convertView == null) {
            LayoutInflater inflater = (LayoutInflater) mContext
                    .getSystemService(Context.LAYOUT_INFLATER_SERVICE);
            convertView = inflater.inflate(R.layout.simple_two_line_item_dropdown, parent, false);
        }
        ((TextView) convertView.findViewById(R.id.text1)).setText(getItem(position).getName());
        ((TextView) convertView.findViewById(R.id.text2)).setText(getItem(position).getDomain());
        return convertView;
    }

    @Override
    public Filter getFilter() {
        Filter filter = new Filter() {
            @Override
            protected FilterResults performFiltering(CharSequence constraint) {
                FilterResults filterResults = new FilterResults();
                if (constraint != null) {
                    List<CompanyDetailsResponse> companyDetailsResponses = findCompanies(mContext, constraint.toString());

                    // Assign the data to the FilterResults
                    filterResults.values = companyDetailsResponses;
                    filterResults.count = companyDetailsResponses.size();
                }
                return filterResults;
            }

            @Override
            protected void publishResults(CharSequence constraint, FilterResults results) {
                if (results != null && results.count > 0) {
                    resultList = (List<CompanyDetailsResponse>) results.values;
                    notifyDataSetChanged();
                } else {
                    notifyDataSetInvalidated();
                }
            }
        };
        return filter;
    }

    private List<CompanyDetailsResponse> findCompanies(Context context, String companyName) {

        List<CompanyDetailsResponse> companyDetailsResponses = new ArrayList<CompanyDetailsResponse>();

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(mContext.getString(R.string.api_get_company_details))
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        ApiInterface apiService =
                retrofit.create(ApiInterface.class);

        Call<List<CompanyDetailsResponse>> call = apiService.getCompanyDetails(companyName);

        try {
            companyDetailsResponses = call.execute().body();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return companyDetailsResponses;
    }
}
package ai.infrrd.leavemanagementsystem.rest;

import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import retrofit.GsonConverterFactory;
import retrofit.Retrofit;

/**
 * Created by Anuj Dutt on 1/3/2017.
 * <p>
 * API ai.infrrd.leavemanagementsystem.rest client
 */

public class ApiClient {

    public static final String BASE_URL = "http://ec2-52-32-62-145.us-west-2.compute.amazonaws.com:8089/api/";
    private static Retrofit retrofit = null;

    public static Retrofit getClient() {
        if (retrofit == null) {
            Gson gson = new GsonBuilder()
                    .setDateFormat("yyyy-MM-dd'T'HH:mm:ss")
                    .create();
            retrofit = new Retrofit.Builder()
                    .baseUrl(BASE_URL)
                    .addConverterFactory(GsonConverterFactory.create(gson))
                    .build();
        }
        return retrofit;
    }
}

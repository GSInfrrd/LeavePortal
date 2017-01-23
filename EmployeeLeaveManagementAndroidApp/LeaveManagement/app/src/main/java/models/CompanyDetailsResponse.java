package models;

/**
 * Created by Anuj Dutt on 1/10/2017.
 * Stores company details response when fetching company details.
 */

public class CompanyDetailsResponse {

    private String domain;
    private String logo;
    private String name;

    public String getDomain() {
        return domain;
    }

    public void setDomain(String domain) {
        this.domain = domain;
    }

    public String getLogo() {
        return logo;
    }

    public void setLogo(String logo) {
        this.logo = logo;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}

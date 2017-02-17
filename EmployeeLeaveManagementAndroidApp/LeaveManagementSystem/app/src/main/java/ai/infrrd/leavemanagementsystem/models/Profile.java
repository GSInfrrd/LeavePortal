package ai.infrrd.leavemanagementsystem.models;

import com.google.gson.annotations.SerializedName;

import java.util.Date;

/**
 * Created by Anuj Dutt on 12/25/2016.
 */

public class Profile {
    @SerializedName("Id")
    private int id;
    @SerializedName("UserName")
    private String userName;
    @SerializedName("Password")
    private String password;
    @SerializedName("Lastlogin")
    private Date lastLogin;
    @SerializedName("RefEmployeeId")
    private int refEmployeeId;
    @SerializedName("CreatedDate")
    private Date createdDate;
    @SerializedName("ModifiedDate")
    private Date modifiedDate;
    @SerializedName("RefRoleId")
    private int refRoleId;
    @SerializedName("Imagepath")
    private String imagePath;
    @SerializedName("DateOfJoining")
    private Date dateOfJoining;

    public Date getCreatedDate() {
        return createdDate;
    }

    public void setCreatedDate(Date createdDate) {
        this.createdDate = createdDate;
    }

    public Date getDateOfJoining() {
        return dateOfJoining;
    }

    public void setDateOfJoining(Date dateOfJoining) {
        this.dateOfJoining = dateOfJoining;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getImagePath() {
        return imagePath;
    }

    public void setImagePath(String imagePath) {
        this.imagePath = imagePath;
    }

    public Date getLastLogin() {
        return lastLogin;
    }

    public void setLastLogin(Date lastLogin) {
        this.lastLogin = lastLogin;
    }

    public Date getModifiedDate() {
        return modifiedDate;
    }

    public void setModifiedDate(Date modifiedDate) {
        this.modifiedDate = modifiedDate;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public int getRefEmployeeId() {
        return refEmployeeId;
    }

    public void setRefEmployeeId(int refEmployeeId) {
        this.refEmployeeId = refEmployeeId;
    }

    public int getRefRoleId() {
        return refRoleId;
    }

    public void setRefRoleId(int refRoleId) {
        this.refRoleId = refRoleId;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }
}

package ai.infrrd.leavemanagementsystem.models;

import com.google.gson.annotations.SerializedName;

/**
 * Created by Anuj Dutt on 1/10/2017.
 * Model class to hold the employee skills.
 */

public class EmployeeSkillDetails {

    @SerializedName("Id")
    private int id;

    @SerializedName("RefEmployeeId")
    private int refEmployeeId;

    @SerializedName("SkillName")
    private String skillName;

    @SerializedName("IsSelected")
    private boolean isSelected;

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public int getRefEmployeeId() {
        return refEmployeeId;
    }

    public void setRefEmployeeId(int refEmployeeId) {
        this.refEmployeeId = refEmployeeId;
    }

    public String getSkillName() {
        return skillName;
    }

    public void setSkillName(String skillName) {
        this.skillName = skillName;
    }

    public boolean isSelected() {
        return isSelected;
    }

    public void setSelected(boolean selected) {
        isSelected = selected;
    }
}

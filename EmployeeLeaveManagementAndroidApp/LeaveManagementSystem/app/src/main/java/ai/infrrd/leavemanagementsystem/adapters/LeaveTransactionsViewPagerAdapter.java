package ai.infrrd.leavemanagementsystem.adapters;

import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentStatePagerAdapter;

import ai.infrrd.leavemanagementsystem.fragments.AllLeaveTransactions;
import ai.infrrd.leavemanagementsystem.fragments.CreditedLeaveTransactions;
import ai.infrrd.leavemanagementsystem.fragments.SpentLeaveTransactions;

/**
 * Created by Anuj Dutt on 1/26/2017.
 * An adapter to bind relevant fragments to tab selected in leave history.
 */

public class LeaveTransactionsViewPagerAdapter extends FragmentStatePagerAdapter {

    private int mTabCount;

    public LeaveTransactionsViewPagerAdapter(FragmentManager fm, int mTabCount) {
        super(fm);
        this.mTabCount = mTabCount;
    }

    @Override
    public Fragment getItem(int position) {
        AllLeaveTransactions allLeaveTransactions;
        switch (position) {
            case 0:
                allLeaveTransactions = new AllLeaveTransactions();
                return allLeaveTransactions;
            case 1:
                SpentLeaveTransactions spentLeaveTransactions = new SpentLeaveTransactions();
                return spentLeaveTransactions;
            case 2:
                CreditedLeaveTransactions creditedLeaveTransactions = new CreditedLeaveTransactions();
                return creditedLeaveTransactions;
            default:
                allLeaveTransactions = new AllLeaveTransactions();
                return allLeaveTransactions;
        }
    }

    @Override
    public int getCount() {
        return mTabCount;
    }

}

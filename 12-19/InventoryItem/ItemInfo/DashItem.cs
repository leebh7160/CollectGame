using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DashItem : ItemList
{
    private int dashValue = 0;

    public DashItem(int dashvalue)
    {
        SetItemStatus(dashvalue);
    }

    private void SetItemStatus(int type)
    {
        switch (type)
        {
            case (int)AllGameItem.dash_LV1:
                dashValue = 1;
                break;
            case (int)AllGameItem.dash_LV2:
                dashValue = 2;
                break;
            case (int)AllGameItem.dash_LV3:
                dashValue = 3;
                break;
        }
    }

    protected override int getDashValue()
    {
        return dashValue;
    }
}

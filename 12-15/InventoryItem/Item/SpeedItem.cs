using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : ItemList
{
    private int speedValue = 0;

    public SpeedItem(int type)
    {
        SetItemStatus(type);
    }

    private void SetItemStatus(int type)
    {
        switch(type)
        {
            case (int)AllGameItem.speed_LV1:
                speedValue = 1;
                break;
            case (int)AllGameItem.speed_LV2:
                speedValue = 2;
                break;
            case (int)AllGameItem.speed_LV3:
                speedValue = 3;
                break;
        }
    }


    protected override int getSpeedValue()
    {
        return speedValue;
    }
}

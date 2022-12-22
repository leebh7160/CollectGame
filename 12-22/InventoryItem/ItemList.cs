using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;

enum AllGameItem
{
    speed_LV1 = 1001, speed_LV2 = 1002, speed_LV3 = 1003,
    jump_LV1 = 2001, jump_LV2 = 2002, jump_LV3 = 2003,
    dash_LV1 = 3001, dash_LV2 = 3002, dash_LV3 = 3003,
};

public class ItemList
{
    public ItemList()
    {
    }

    protected virtual int getSpeedValue() { return 0; }
    protected virtual int getJumpValue() { return 0; }
    protected virtual int getDashValue() { return 0; }

    internal int getItemStatus(int type)
    {
        switch (type)
        {
            case 1001:
            case 1002:
            case 1003:
                return getSpeedValue();
            case 2001:
            case 2002:
            case 2003:
                return getJumpValue();
            case 3001:
            case 3002:
            case 3003:
                return getDashValue();
            default:
                return 0;
        }
    }
}

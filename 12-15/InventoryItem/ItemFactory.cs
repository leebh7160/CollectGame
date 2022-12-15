using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory
{
    private int speedItemValue = 0;

    public ItemList GetItemValue(int type)
    {
        switch (type)
        {
            case 1001:
            case 1002:
            case 1003:
                return new SpeedItem(type);
            case 2001:
            case 2002:
            case 2003:
                return new JumpItem(type);
            case 3001:
            case 3002:
            case 3003:
                return new DashItem(type);
            default:
                return null;
        }
    }
}

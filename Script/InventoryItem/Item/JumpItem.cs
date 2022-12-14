using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : ItemList
{
    private int jumpValue = 0;

    public JumpItem(int type)
    {
        SetItemStatus(type);
    }

    private void SetItemStatus(int type)
    {
        switch (type)
        {
            case (int)AllGameItem.jump_LV1:
                jumpValue = 1;
                break;
            case (int)AllGameItem.jump_LV2:
                jumpValue = 2;
                break;
            case (int)AllGameItem.jump_LV3:
                jumpValue = 3;
                break;
        }
    }


    protected override int getJumpValue()
    {
        return jumpValue;
    }
}

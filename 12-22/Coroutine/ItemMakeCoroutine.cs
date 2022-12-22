using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class ItemMakeCoroutine : GameManager
{
    internal static IEnumerator ItemMake(Transform selecteditem, Transform objectparent, Sprite itemimage, int itemvalue)
    {
        Transform instantOBJ    = null;
        ItemObject itemOBJ      = null;
        Vector2 rand_position   = Item_DinamicSetting();
        int Value               = item_value(itemvalue);

        instantOBJ = Instantiate(selecteditem, rand_position, Quaternion.identity, objectparent);
        itemOBJ = instantOBJ.GetComponent<ItemObject>();
        itemOBJ.SetItemImage(itemimage);
        itemOBJ.SetItemValue(Value);

        yield return null;
    }

    private static Vector2 Item_DinamicSetting() //아이템 만들어진 것 실시간 세팅
    {
        Vector2 return_Vector2 = new Vector2();
        float randX = 0f;
        float randY = 0f;

        randX = UnityEngine.Random.Range(-23, 23);
        randY = Item_floar();

        return_Vector2 = new Vector2(randX, randY);

        return return_Vector2;
    }

    private static float Item_floar()
    {
        int floar = UnityEngine.Random.Range(0, 5);

        switch(floar)
        {
            case 0:
                return 1f;
            case 1:
                return 7.5f;
            case 2:
                return 12.5f;
            case 3:
                return 17.5f;
            case 4:
                return 22.5f;
            case 5:
                return 27.5f;
            default:
                return 1f;
        }
    }

    private static int item_value(int value)
    {
        switch (value)
        {
            case 0:
                return 1001;
            case 1:
                return 2001;
            case 2:
                return 3001;
            default:
                return 1001;
        }
    }

}


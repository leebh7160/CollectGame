using System;
using System.Collections;
using UnityEngine;

class ItemMakeCoroutine : GameManager
{
    internal static IEnumerator ItemMake(Transform selecteditem, Transform objectparent)
    {
        Vector2 rand_position = Item_DinamicSetting();

        Instantiate(selecteditem, rand_position, Quaternion.identity, objectparent);

        yield return null;
    }
}


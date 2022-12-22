using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private int itemvalue;
    private SpriteRenderer itemImage;

    internal void SetItemValue(int value)
    {
        itemvalue = value;
    }
    internal int GetItemValue()
    {
        return itemvalue;
    }

    internal void SetItemImage(Sprite itemimage)
    {
        itemImage = this.gameObject.GetComponent<SpriteRenderer>();
        itemImage.sprite = itemimage;
    }

    internal Sprite GetItemImage()
    {
        return itemImage.sprite;
    }

    internal void ItemObjectActive(bool activeCheck)
    {
        this.gameObject.SetActive(activeCheck);

        if(activeCheck == false)
            Destroy(this.gameObject);
    }
}

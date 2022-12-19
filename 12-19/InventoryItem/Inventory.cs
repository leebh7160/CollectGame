using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //팩토리패턴 itemcode
    private ItemFactory itemFactory = new ItemFactory();
    private Transform ItemSlot;

    [SerializeField]
    GameObject dragableitem;

    private void Awake()
    {
        ItemSlot = this.transform.GetChild(0);
    }

    #region 아이템 정보 받아 반환
    internal bool Inven_ItemFull()//인벤토리 비었는지 확인
    {
        bool returnbool = false;
        if (InvenControl_SearchNullSlot() == null)
            returnbool = true;

        return returnbool;
    }

    public List<int> Inven_GetItemData(int type, Sprite imageindex)//아이템 정보 받기
    {
        int itemLocation = 0;
        List<int> itemStatDataList = new List<int>(3) { 0, 0, 0 }; //0=speed 1=jump 2=dash
        itemLocation                    = ItemLocationCheck(type);
        itemStatDataList[itemLocation]  = Inven_MatchItem(type);

        Inven_MatchItemImage(type, itemStatDataList[itemLocation], imageindex);//아이템 인벤토리로 객체화

        //Debug.Log("리스트 속도 : " + itemStatDataList[0] + "점프 : " + itemStatDataList[1] + "대시 : " + itemStatDataList[2]);
        return itemStatDataList;
    }

    private int Inven_MatchItem(int type)//아이템 대조
    {
        ItemList OBJitemstatus = itemFactory.GetItemValue(type);

        return OBJitemstatus.getItemStatus(type);
    }

    private int ItemLocationCheck(int temp)//타입에 맞는 리스트 자리 반환
    {
        string tempString = temp.ToString().Substring(0, 1);
        switch (tempString)
        {
            case "1":
                return 0;
            case "2":
                return 1;
            case "3":
                return 2;
            default:
                return -1;
        }
    }

    private void Inven_MatchItemImage(int itemvalue, int itemstatus, Sprite imageindex)//아이템 인벤토리로 객체화
    {
        Transform instantParent     = InvenControl_SearchNullSlot();
        if (instantParent == null)
            return;

        GameObject itemObject       = null;
        DragableItem dragitemStatus = null;
        Image dragitemImage         = null;

        itemObject              = Instantiate(dragableitem, instantParent);
        dragitemStatus          = itemObject.GetComponent<DragableItem>();
        dragitemStatus.Set_ItemValue(itemvalue);
        dragitemStatus.Set_ItemStatuas(itemstatus);

        dragitemImage           = itemObject.GetComponent<Image>();
        dragitemImage.sprite = imageindex;
        dragitemImage.color     = Color.white;
    }
    #endregion

    #region 인벤토리 조작
    private Transform InvenControl_SearchNullSlot()//인벤토리 비었는지 확인
    {
        Transform returnObject = null;

        for(int i = 0; i < ItemSlot.childCount; i++)
        {
            if (ItemSlot.GetChild(i).childCount == 0)
            {
                returnObject = ItemSlot.GetChild(i);
                break;
            }
        }

        return returnObject;
    }
    #endregion
}

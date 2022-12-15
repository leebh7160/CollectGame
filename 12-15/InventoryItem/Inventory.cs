using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    //팩토리패턴 itemcode
    ItemFactory itemFactory = new ItemFactory();

    private int speedStatus = 1;
    private int jumpStatus = 1;
    private int dashStatus = 1;

    #region
    #endregion

    #region 아이템 정보 받아 반환
    public List<int> Inven_GetItemData(int type)//아이템 정보 받기

    {
        int itemLocation = 0;
        List<int> itemStatDataList = new List<int>(3) { 0, 0, 0 }; //0=speed 1=jump 2=dash

        itemLocation                    = ItemLocationCheck(type);
        itemStatDataList[itemLocation]  = Inven_MatchItem(type);

        Inven_MatchItemImage(type);//아이템 객체화

        return itemStatDataList;
    }

    private int Inven_MatchItem(int type)//아이템 대조
    {
        ItemList OBJitemstatus = itemFactory.GetItemValue(type);

        return OBJitemstatus.getItemStatus(type);
    }

    private void Inven_MatchItemImage(int type)//아이템 이미지 객체화
    {

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
    #endregion

    #region 인벤토리 조작


    #endregion
}

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    //���丮���� itemcode
    ItemFactory itemFactory = new ItemFactory();

    private int speedStatus = 1;
    private int jumpStatus = 1;
    private int dashStatus = 1;

    #region
    #endregion

    #region ������ ���� �޾� ��ȯ
    public List<int> Inven_GetItemData(int type)//������ ���� �ޱ�

    {
        int itemLocation = 0;
        List<int> itemStatDataList = new List<int>(3) { 0, 0, 0 }; //0=speed 1=jump 2=dash

        itemLocation                    = ItemLocationCheck(type);
        itemStatDataList[itemLocation]  = Inven_MatchItem(type);

        Inven_MatchItemImage(type);//������ ��üȭ

        return itemStatDataList;
    }

    private int Inven_MatchItem(int type)//������ ����
    {
        ItemList OBJitemstatus = itemFactory.GetItemValue(type);

        return OBJitemstatus.getItemStatus(type);
    }

    private void Inven_MatchItemImage(int type)//������ �̹��� ��üȭ
    {

    }

    private int ItemLocationCheck(int temp)//Ÿ�Կ� �´� ����Ʈ �ڸ� ��ȯ
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

    #region �κ��丮 ����


    #endregion
}

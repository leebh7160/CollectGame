using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    //���丮���� itemcode
    private ItemFactory itemFactory = new ItemFactory();
    private Transform ItemSlot;

    [SerializeField]
    List<Sprite> itemImage = new List<Sprite> ();
    [SerializeField]
    GameObject dragableitem;

    private int speedStatus = 1;
    private int jumpStatus = 1;
    private int dashStatus = 1;

    private void Start()
    {
        ItemSlot = this.transform.GetChild(0);
    }




    #region ������ ���� �޾� ��ȯ
    internal bool Inven_ItemFull()
    {
        bool returnbool = false;
        if (InvenControl_SearchNullSlot() == null)
            returnbool = true;

        return returnbool;
    }

    public List<int> Inven_GetItemData(int type)//������ ���� �ޱ�
    {
        int itemLocation = 0;
        List<int> itemStatDataList = new List<int>(3) { 0, 0, 0 }; //0=speed 1=jump 2=dash

        itemLocation                    = ItemLocationCheck(type);
        itemStatDataList[itemLocation]  = Inven_MatchItem(type);

        Inven_MatchItemImage(type, itemStatDataList[itemLocation]);//������ �κ��丮�� ��üȭ

        return itemStatDataList;
    }

    private int Inven_MatchItem(int type)//������ ����
    {
        ItemList OBJitemstatus = itemFactory.GetItemValue(type);

        return OBJitemstatus.getItemStatus(type);
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

    private void Inven_MatchItemImage(int itemvalue, int itemstatus)//������ �κ��丮�� ��üȭ
    {
        Transform instantParent = InvenControl_SearchNullSlot();
        if (instantParent == null)
            return;

        GameObject itemObject   = null;
        DragableItem dragitemStatus = null;

        itemObject = Instantiate(dragableitem, instantParent);
        dragitemStatus = itemObject.GetComponent<DragableItem>();
        dragitemStatus.Set_ItemValue(itemvalue);
        dragitemStatus.Set_ItemStatuas(itemstatus);
    }
    #endregion

    #region �κ��丮 ����
    private Transform InvenControl_SearchNullSlot()//�κ��丮 ������� Ȯ��
    {
        Transform returnObject = null;

        for(int i = 0; i < ItemSlot.childCount; i++)
        {
            if (ItemSlot.GetChild(i).childCount == 0)
                returnObject = ItemSlot.GetChild(i);
        }

        return returnObject;
    }
    #endregion
}

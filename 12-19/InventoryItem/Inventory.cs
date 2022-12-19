using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //���丮���� itemcode
    private ItemFactory itemFactory = new ItemFactory();
    private Transform ItemSlot;

    [SerializeField]
    GameObject dragableitem;

    private void Awake()
    {
        ItemSlot = this.transform.GetChild(0);
    }

    #region ������ ���� �޾� ��ȯ
    internal bool Inven_ItemFull()//�κ��丮 ������� Ȯ��
    {
        bool returnbool = false;
        if (InvenControl_SearchNullSlot() == null)
            returnbool = true;

        return returnbool;
    }

    public List<int> Inven_GetItemData(int type, Sprite imageindex)//������ ���� �ޱ�
    {
        int itemLocation = 0;
        List<int> itemStatDataList = new List<int>(3) { 0, 0, 0 }; //0=speed 1=jump 2=dash
        itemLocation                    = ItemLocationCheck(type);
        itemStatDataList[itemLocation]  = Inven_MatchItem(type);

        Inven_MatchItemImage(type, itemStatDataList[itemLocation], imageindex);//������ �κ��丮�� ��üȭ

        //Debug.Log("����Ʈ �ӵ� : " + itemStatDataList[0] + "���� : " + itemStatDataList[1] + "��� : " + itemStatDataList[2]);
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

    private void Inven_MatchItemImage(int itemvalue, int itemstatus, Sprite imageindex)//������ �κ��丮�� ��üȭ
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

    #region �κ��丮 ����
    private Transform InvenControl_SearchNullSlot()//�κ��丮 ������� Ȯ��
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

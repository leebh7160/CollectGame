using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Transform obj_speedItem;
    [SerializeField]
    private Transform obj_jumpItem;
    [SerializeField]
    private Transform obj_dashItem;

    [SerializeField]
    private Transform itemObjectParent;

    private static int item_MaxCount           = 9;  //�� ������ ����
    private int itemMakeCurrentCount    = 0; //���� ������ ����

    private int gameScoreSave = 0;


    void Start()
    {
        Gameinit();
    }

    void Update()
    {
        float timecheck = 0;
        timecheck += Time.deltaTime;

        if (itemMakeCurrentCount < item_MaxCount && timecheck > 5)
        {
            timecheck = 0;
            Item_MakeCoroutine();
        }
    }

    private void Gameinit()//���� ���� �� �ʱ�ȭ
    {
        itemMakeCurrentCount = 0;
    }

    internal void Game_ScoreSave()//���� ���� ����
    {

    }

    internal void Game_TimeCheck() //���� �ð� Ȯ��
    {

    }

    internal void Game_End() //���� ����
    {

    }

    #region ������
    protected static Vector2 Item_DinamicSetting() //������ ������� �� �ǽð� ����
    {
        Vector2 return_Vector2 = new Vector2();
        return return_Vector2;
    }

    protected Transform Item_Choice() //������ ���� ����
    {
        int randnumber = UnityEngine.Random.Range(0, 4);

        switch (randnumber)
        {
            case 0:
                return obj_speedItem;
            case 1:
                return obj_jumpItem;
            case 2:
                return obj_dashItem;
            default:
                return obj_speedItem;
        }
    }

    private void Item_MakeCoroutine() //������ �ڷ�ƾ ����
    {
        Transform selecteditem = Item_Choice();

        StartCoroutine(ItemMakeCoroutine.ItemMake(selecteditem, itemObjectParent));
    }


    #endregion
}

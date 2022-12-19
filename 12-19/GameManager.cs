using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private List<Sprite> itemImage = new List<Sprite>();
    [SerializeField]
    private Transform obj_Item;

    [SerializeField]
    private Transform itemObjectParent;

    private static int item_MaxCount     = 9;  //�� ������ ����
    private int itemMakeCurrentCount     = 0; //���� ������ ����

    float timecheck = 0;

    private int gameScoreSave = 0;


    void Start()
    {
        Gameinit();
    }

    void Update()
    {
        timecheck += Time.deltaTime;

        if (itemMakeCurrentCount < item_MaxCount &&timecheck > 3.5f)
        {
            timecheck = 0;
            Item_MakeCoroutine();
            itemMakeCurrentCount += 1;
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

    private int Item_Choice() //������ ���� ����
    {
        int randnumber = UnityEngine.Random.Range(0, 3);
        return randnumber;
    }

    private void Item_MakeCoroutine() //������ ��ü ���� �� ��ŭ�� ����
    {
        int itemlocation = Item_Choice();


        StartCoroutine(ItemMakeCoroutine.ItemMake(obj_Item, itemObjectParent, itemImage[itemlocation], itemlocation));
    }

    internal void Item_MaxCountCheck()//
    {
        itemMakeCurrentCount -= 1;
    }

    internal void Item_ScoreCheck(int index)
    {

    }


    #endregion
}

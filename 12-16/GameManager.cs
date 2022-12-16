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

    private static int item_MaxCount           = 9;  //총 아이템 갯수
    private int itemMakeCurrentCount    = 0; //현재 아이템 갯수

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

    private void Gameinit()//게임 실행 시 초기화
    {
        itemMakeCurrentCount = 0;
    }

    internal void Game_ScoreSave()//게임 점수 저장
    {

    }

    internal void Game_TimeCheck() //게임 시간 확인
    {

    }

    internal void Game_End() //게임 종료
    {

    }

    #region 아이템
    protected static Vector2 Item_DinamicSetting() //아이템 만들어진 것 실시간 세팅
    {
        Vector2 return_Vector2 = new Vector2();
        return return_Vector2;
    }

    protected Transform Item_Choice() //아이템 랜덤 선택
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

    private void Item_MakeCoroutine() //아이템 코루틴 실행
    {
        Transform selecteditem = Item_Choice();

        StartCoroutine(ItemMakeCoroutine.ItemMake(selecteditem, itemObjectParent));
    }


    #endregion
}

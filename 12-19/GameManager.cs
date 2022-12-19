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

    private static int item_MaxCount     = 9;  //총 아이템 갯수
    private int itemMakeCurrentCount     = 0; //현재 아이템 갯수

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

    private int Item_Choice() //아이템 랜덤 선택
    {
        int randnumber = UnityEngine.Random.Range(0, 3);
        return randnumber;
    }

    private void Item_MakeCoroutine() //아이템 객체 일정 수 만큼만 생성
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

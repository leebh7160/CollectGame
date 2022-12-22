using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Character character;

    [SerializeField]
    private List<Sprite> itemImage = new List<Sprite>();
    [SerializeField]
    private Transform obj_Item;
    [SerializeField]
    private Transform itemObjectParent;

    private GameUI gameui;

    private int score = 0;

    private static int item_MaxCount     = 9;  //총 아이템 갯수
    private int itemMakeCurrentCount     = 0; //현재 아이템 갯수

    private float timecheck = 0;

    private bool isgamestart = false;

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
        gameui = GetComponent<GameUI>();
        gameui.GetGameManager(this);
        itemMakeCurrentCount = 0;
        character.CharacterRestart();
        ItemDeleteAll();
    }

    internal void GameReplay()
    {
        Game_Pause();
        itemMakeCurrentCount = 0; //현재 아이템 갯수
        timecheck = 0;
        gameScoreSave = 0;
        character.CharacterRestart();
        ItemDeleteAll();
    }

    internal void Game_ScoreSave()//게임 점수 저장
    {

    }

    internal void Game_TimeCheck() //게임 시간 확인
    {

    }

    internal void Game_Pause()//플레이어 정지
    {
        isgamestart = false;
        character.CharacterPause();
    }

    internal void Game_Play()//플레이 시작
    {
        isgamestart = true;
        character.CharacterPlay();
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

        if(isgamestart == true)
            StartCoroutine(ItemMakeCoroutine.ItemMake(obj_Item, itemObjectParent, itemImage[itemlocation], itemlocation));
    }

    internal void Item_MaxCountCheck()//아이템 생성 체크
    {
        itemMakeCurrentCount -= 1;
    }

    internal void Item_ScoreCheck(int score)//점수 아이템이 필요할 것 같다.
    {
        gameui.Game_ScoreUI(score);
    }

    private void ItemDeleteAll()
    {
        if (itemObjectParent == null)
            return;

        Debug.Log("??");
        for (int i = 0; i < itemObjectParent.childCount; i++)
        {
            Destroy(itemObjectParent.GetChild(i).gameObject);
        }
    }


    #endregion
}

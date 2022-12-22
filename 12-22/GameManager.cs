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

    private static int item_MaxCount     = 9;  //�� ������ ����
    private int itemMakeCurrentCount     = 0; //���� ������ ����

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

    private void Gameinit()//���� ���� �� �ʱ�ȭ
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
        itemMakeCurrentCount = 0; //���� ������ ����
        timecheck = 0;
        gameScoreSave = 0;
        character.CharacterRestart();
        ItemDeleteAll();
    }

    internal void Game_ScoreSave()//���� ���� ����
    {

    }

    internal void Game_TimeCheck() //���� �ð� Ȯ��
    {

    }

    internal void Game_Pause()//�÷��̾� ����
    {
        isgamestart = false;
        character.CharacterPause();
    }

    internal void Game_Play()//�÷��� ����
    {
        isgamestart = true;
        character.CharacterPlay();
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

        if(isgamestart == true)
            StartCoroutine(ItemMakeCoroutine.ItemMake(obj_Item, itemObjectParent, itemImage[itemlocation], itemlocation));
    }

    internal void Item_MaxCountCheck()//������ ���� üũ
    {
        itemMakeCurrentCount -= 1;
    }

    internal void Item_ScoreCheck(int score)//���� �������� �ʿ��� �� ����.
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

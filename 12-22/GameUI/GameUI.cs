using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText;
    [SerializeField]
    private TextMeshProUGUI playTimeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    [SerializeField]
    private Transform backgroundImage;

    [SerializeField]
    private List<Transform> startUIObject = new List<Transform>();
    [SerializeField]
    private List<Transform> endUIObject = new List<Transform>();

    private bool isGameStart = false;

    private float playTimer_Limit = 10;
    private float playTimer_Current = 0;

    private int playScore_Current = 0;

    //==========================================타이머
    private bool isGameTimerStart = false;
    private float gameStartTimer_Limit = 3;
    private float gameStartTimer_Check = 3;
    private float gameStartTimer_FontTime = 10;
    private string gameStartTimer_TextCheck = "0";
    //==========================================타이머^^

    GameManager gameManager;

    public void GetGameManager(GameManager _gamemanager)
    {
        gameManager = _gamemanager;
    }

    void FixedUpdate()
    {
        if (isGameTimerStart == true)
            Game_StartTimer();
        else
            Game_Pause();

        if(isGameStart == true)
        {
            Game_CheckTime();
        }
    }

    private void UIInit()
    {
        isGameStart = false;
        playTimer_Limit = 60;
        playTimer_Current = 0;

        playScore_Current = 0;

        //==========================================타이머
        isGameTimerStart = false;
        gameStartTimer_Limit = 3;
        gameStartTimer_Check = 3;
        gameStartTimer_FontTime = 10;
        gameStartTimer_TextCheck = "0";
        //==========================================타이머^^
    }

    private void Game_CheckTime()
    {
        if (isGameStart == false)
            return;
        if(playTimer_Current >= playTimer_Limit)
        {
            isGameStart = false;
            Game_ShowEndTitle();
        }

        playTimer_Current += Time.deltaTime;
        playTimeText.text = "Time : " + playTimer_Current.ToString("F0");
    }

    internal void Game_ScoreUI(int score)//게임 점수
    {
        scoreText.text = "Score : " + score.ToString();
    }

    #region UI버튼
    public void StartButton()
    {
        Game_ShowStartTitle();
    }

    public void EndButton()
    {
        Game_HideTitleObj(endUIObject);
        Game_Replay();
    }
    #endregion

    #region 화면 표시
    private void Game_ShowStartTitle()//게임 시작 화면 표시
    {
        isGameTimerStart = true;
        Game_HideTitleObj(startUIObject);
    }

    private void Game_ShowEndTitle()//게임 종료 화면 표시
    {
        isGameStart = false;
        Game_ShowTitleObj(endUIObject);
    }

    private void Game_ShowTitleObj(List<Transform> showlist)//리스트 표시 작업
    {
        for (int i = 0; i < showlist.Count; i++)
            showlist[i].gameObject.SetActive(true);
    }

    private void Game_HideTitleObj(List<Transform> hidelist)//리스트 숨기기 작업
    {
        for (int i = 0; i < hidelist.Count; i++)
            hidelist[i].gameObject.SetActive(false);
    }

    #endregion

    private void Game_Replay()//게임 재시작
    {
        Game_ShowTitleObj(startUIObject);
        UIInit();
        gameManager.GameReplay();
    }

    private void Game_Pause()//일시정지
    {
        if(isGameStart == false)
            gameManager.Game_Pause();
    }

    private void Game_Play()//시작
    {
        if (isGameStart == true)
        gameManager.Game_Play();
    }

    private void Game_StartTimer()//게임 시작 시 타이머
    {
        timerText.gameObject.SetActive(true);
        gameStartTimer_Check -= Time.deltaTime;
        timerText.text = Mathf.Ceil(gameStartTimer_Check).ToString("F0");

        if (gameStartTimer_Check > 0)
        {
            if (gameStartTimer_TextCheck != timerText.text)
            {
                gameStartTimer_TextCheck = timerText.text;
                timerText.fontSize = 300;
                gameStartTimer_FontTime = 10;
            }
            gameStartTimer_FontTime -= Time.deltaTime * 10;
            timerText.fontSize = (gameStartTimer_FontTime) / 100 * 10000;

            isGameStart = false;
            Game_Pause();
        }

        if (gameStartTimer_Check < 0)
        {
            timerText.text = "Start!";
            timerText.fontSize = 300;
            isGameStart = true;
            Game_Play();
        }
        if (gameStartTimer_Check < -1)
        {
            timerText.gameObject.SetActive(false);
            isGameTimerStart = false;
            gameStartTimer_Check = gameStartTimer_Limit;
        }
    }

}

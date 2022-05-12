using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    // 시네머신
    public PlayableDirector playableDirector;

    public GameObject GameOverUI;   // 게임 오버 UI
    public GameObject GameEndUI;    // 게임 종료 UI

    public bool gameOver = false;   // 게임 오버 판단
    public bool gameEnd = false;    // 게임 종료 판단
    public bool onFire = false;     // 사격 판단

    void Awake()
    {
        Application.targetFrameRate = 60;   // 60f으로 고정

        // 싱글톤 패턴
        if (null == instance)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // 싱글톤 패턴
    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    // 게임 시작시 초기 셋팅
    public void InitGame()
    {
        gameOver = false;
        gameEnd = false;
        GameOverUI.SetActive(false);
    }

    // 게임 퍼즈
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    // 게임 퍼즈 종료
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    // 게임 재시작
    public void RestartGame()
    {
        Time.timeScale = 1;
    }

    // 게임 오버
    public void GameOver()
    {
        gameOver = true;

        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }

    // 게임 종료
    public void GameEnd()
    {
        gameEnd = true;

        GameEndUI.SetActive(true);
    }

    // 보스 컷신
    public void BossCut()
    {
        playableDirector.Play();
    }
}

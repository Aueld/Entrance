using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public PlayableDirector playableDirector;
    public GameObject GameOverUI;
    public GameObject GameEndUI;

    public bool gameOver = false;
    public bool gameEnd = false;
    public bool onFire = false;

    void Awake()
    {
        Application.targetFrameRate = 60;

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

    public void InitGame()
    {
        gameOver = false;
        gameEnd = false;
        GameOverUI.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOver = true;

        Time.timeScale = 0;
        GameOverUI.SetActive(true);
    }

    public void GameEnd()
    {
        gameEnd = true;

        GameEndUI.SetActive(true);
    }

    public void BossCut()
    {
        playableDirector.Play();
    }
}

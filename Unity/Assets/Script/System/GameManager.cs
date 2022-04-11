using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    //private int level = 1;
    //private bool gameOver = false;

    public GameObject GameOverUI;
    public GameObject GameEndUI;
    public bool gameOver = false;
    public bool gameEnd = false;

    void Awake()
    {
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
}

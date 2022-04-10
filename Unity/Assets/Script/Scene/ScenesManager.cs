using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void OnClickStartButton()
    {
        LoadSceneController.LoadScene("GameHomeScene");
    }

    public void OnClickLoadGame()
    {

    }

    public void ReTry()
    {
        GameManager.Instance.RestartGame();
        LoadSceneController.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    // 씬의 전체적인 흐름 관리

    public void OnClickStartButton()
    {
        // 타이틀에서 버튼 누를시 마을로 이동
        LoadSceneController.LoadScene("GamePrologueScene");
    }

    public void OnClickLoadGame()
    {

    }

    public void ReTry()
    {
        // 재시도 버튼 누를시 해당 씬 처음부터 다시 시작
        GameManager.Instance.RestartGame();
        LoadSceneController.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickExit()
    {
        // 게임 종료
        Application.Quit();
    }
}
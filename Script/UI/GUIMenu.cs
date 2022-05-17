using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMenu : MonoBehaviour
{
    private bool check = false;

    // 메뉴 캔버스
    public GameObject canvas;

    void Start()
    {
        // 게임 시작시
        GameManager.Instance.InitGame();
        canvas.SetActive(false);
    }

    void Update()
    {
        // Esc 입력시
        if (Input.GetKeyDown(KeyCode.Escape) && !check)
        {
            canvas.SetActive(true);
            check = true;
            GameManager.Instance.PauseGame();   // 게임 정지
            
        } else if(Input.GetKeyDown(KeyCode.Escape) && check)
        {
            canvas.SetActive(false);
            check = false;
            GameManager.Instance.ContinueGame();// 게임 재개
        }
    }
}

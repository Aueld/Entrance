using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GUIMenu : MonoBehaviour
{
    private bool check = false;
    private bool mapCheck = false;

    // 메뉴 캔버스
    public GameObject canvas;
    public GameObject minimap;
    public GameObject bigmap;

    void Start()
    {
        try {
            // 게임 시작시
            GameManager.Instance.InitGame();
            canvas.SetActive(false);
            bigmap.SetActive(false);
        } catch (Exception e) { }
    }

    void Update()
    {
        try
        {
            if (Input.GetKeyDown(KeyCode.M) && !mapCheck)
            {
                bigmap.SetActive(true);
                minimap.SetActive(false);
                mapCheck = true;
            }
            else if (Input.GetKeyDown(KeyCode.M) && mapCheck)
            {
                bigmap.SetActive(false);
                minimap.SetActive(true);
                mapCheck = false;
            }

            // Esc 입력시
            if (Input.GetKeyDown(KeyCode.Escape) && !check)
            {
                minimap.SetActive(false);
                canvas.SetActive(true);
                check = true;
                GameManager.Instance.PauseGame();   // 게임 정지

            }
            else if (Input.GetKeyDown(KeyCode.Escape) && check)
            {
                if (mapCheck)
                {
                    bigmap.SetActive(false);
                    mapCheck = false;
                }
                minimap.SetActive(true);
                canvas.SetActive(false);
                check = false;
                GameManager.Instance.ContinueGame();// 게임 재개
            }
        } catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
    }
}

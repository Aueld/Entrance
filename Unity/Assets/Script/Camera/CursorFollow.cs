using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : ControlManager
{
    private void Start()
    {
        // 게임 시작시 커서가 밖으로 나가지 않게 설정
        Cursor.lockState = CursorLockMode.Confined;

        StartCoroutine(Updater());
    }
    private IEnumerator Updater()
    {
        while (true)
        {
            // 마우스의 위치를 받아와서 커서UI가 위치하게 설정
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.LookAt(mouse);

            yield return wait;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTime : ControlManager
{
    // 불릿타임 캔버스
    private Canvas canvas;
    private Scrollbar timeBar;

    private void Awake()
    {
        // 불릿타임 체크용
        check = false;

        // 컴포넌트 설정
        canvas = GetComponent<Canvas>();
        timeBar = canvas.GetComponentInChildren<Scrollbar>();

        timeBar.transform.position = new Vector2(960, 1200);
    }

    private void Update()
    {
        // 불릿 타임이 사용중이지 않을때 Q 를 눌렀을때
        if (Input.GetKeyDown(KeyCode.Q) && !check)
        {
            check = true;

            Timer();
        }
        else
            return;
    }

    public void Timer()
    {
        // 시간 느려지게
        Time.timeScale = 0.4f;

        StartCoroutine(CrTimer());
    }

    private IEnumerator CrTimer()
    {
        // 타이머 위치 변경
        timeBar.transform.position = new Vector2(960, 900);
        
        // 값 설정
        timeBar.size = 1f;
        
        float timer = 5f;
        
        // 남은 시간이 0이 될때까지
        while (timer > 0f)
        {
            // 0에 근접했을때 탈출
            if(timer < 0.1f)
            {
                timer = 0f;
                timeBar.size = 0f;

                // 타이머 안보이는 위치로 변경
                timeBar.transform.position = new Vector2(960, 1200);
                // 불릿타임 사용 가능하게 값 변경
                check = false;

                // 타임스케일 정상화
                Time.timeScale = 1f;

                break;
            }

            // 시간과 타이머 줄어들게 설정
            timer -= 0.1f;
            timeBar.size -= 0.02f;

            yield return wait;
        }
    }
}

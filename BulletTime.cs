using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletTime : ControlManager
{
    private Canvas canvas;
    private Scrollbar timeBar;

    private void Awake()
    {
        check = false;

        canvas = GetComponent<Canvas>();
        timeBar = canvas.GetComponentInChildren<Scrollbar>();

        timeBar.transform.position = new Vector2(960, 1200);
    }

    private void Update()
    {
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
        Time.timeScale = 0.4f;

        StartCoroutine(CrTimer());
    }

    private IEnumerator CrTimer()
    {
        timeBar.transform.position = new Vector2(960, 900);
        timeBar.size = 1f;
        float timer = 5f;
        while (timer > 0f)
        {
            if(timer < 0.1f)
            {
                timer = 0f;
                timeBar.size = 0f;

                timeBar.transform.position = new Vector2(960, 1200);
                check = false;

                Time.timeScale = 1f;

                break;
            }
            timer -= 0.1f;
            timeBar.size -= 0.02f;

            yield return wait;
        }
    }
}

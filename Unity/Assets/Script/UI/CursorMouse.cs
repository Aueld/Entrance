using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorMouse : ControlManager
{
    public GameObject bullet;   // 불릿 오브젝트
    public Sprite[] change;     // 스프라이트 변경

    private Shot shot;          // Shot CS
    private SpriteRenderer now; // 현재 스프라이트
    private Quaternion reRot;   // 회전 값
    private float rotateSpeed;  // 회전 속도

    void Start()
    {
        rotateSpeed = 0f;
        reRot = new Quaternion(0, 0, 0, 0);
        now = GetComponent<SpriteRenderer>();
        shot = bullet.GetComponent<Shot>();

        StartCoroutine(Updater());
        StartCoroutine(ChangeScale());
    }

    private IEnumerator Updater()
    {
        while (true)
        {
            // 마우스 위치값
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouse;

            // 마우스 입력시 커서 회전
            if (Input.GetMouseButton(0) && shot.bullet > 0)
            {
                now.sprite = change[0];

                if (rotateSpeed < 1200)
                    rotateSpeed += 10;
            }
            else if (now.sprite != change[1])
                now.sprite = change[1];
            
            else if (rotateSpeed > 15)
                rotateSpeed -= 20;

            yield return wait;
        }
    }

    // 재장전
    public void Reloading()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        //rotateSpeed = 0f;
        while(true)
        {
            // 회전 속도가 일정 이하가 되면 회전값 멈춤
            if (rotateSpeed < 10f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }

            /* { 더미데이터. 
            //if (transform.rotation.z > 359f || transform.rotation.z < -359f)
            //{
            //    transform.rotation = Quaternion.Euler(0, 0, 0);
            //    break;
            //}
            //transform.Rotate(0, 0, 0.5f, Space.Self);
            } */

            yield return wait;
        }
    }

    private IEnumerator ChangeScale()
    {
        while (true)
        {
            // 회전
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);

            yield return wait;
        }
    }
}

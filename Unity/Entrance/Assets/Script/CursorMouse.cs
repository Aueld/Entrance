using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorMouse : ControlManager
{
    public GameObject bullet;
    public Sprite[] change;

    private Shot shot;
    private SpriteRenderer now;
    private Quaternion reRot;
    private float rotateSpeed;

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
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mouse;

            if (Input.GetMouseButton(0) && shot.bullet > 0)
            {
                now.sprite = change[0];

                if (rotateSpeed < 7200)
                    rotateSpeed += 60;
            }
            else if (now.sprite != change[1])
                now.sprite = change[1];
            
            else if (rotateSpeed > 15)
                rotateSpeed -= 10;

            yield return wait;
        }
    }

    public void Reloading()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        //rotateSpeed = 0f;
        while(true)
        {
            if (rotateSpeed < 10f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            }
            //if (transform.rotation.z > 359f || transform.rotation.z < -359f)
            //{
            //    transform.rotation = Quaternion.Euler(0, 0, 0);
            //    break;
            //}
            //transform.Rotate(0, 0, 0.5f, Space.Self);

            yield return wait;
        }
    }

    private IEnumerator ChangeScale()
    {
        while (true)
        {
            transform.Rotate(0, 0, Time.deltaTime * rotateSpeed, Space.Self);

            yield return wait;
        }
    }
}

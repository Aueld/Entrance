using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : BulletMove
{
    public GameObject target;

    private float offsetZ = -5f;

    private float DelayTime = 1.5f;

    private Vector3 FixedPos { get; set; }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameDengeonScene")
            Invoke("Loading", 1f);

        StartCoroutine(Updater());
    }

    private void Loading()
    {
        transform.position = target.transform.position;
    }

    private IEnumerator Updater()
    {
        while (true)
        {

            // 마우스 위치
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            FixedPos = new Vector3(
                (target.transform.position.x + mouse.x) / 2,
                (target.transform.position.y + mouse.y) / 2,
                target.transform.position.z + offsetZ);

            transform.position = Vector3.Lerp(transform.position, FixedPos, Time.fixedDeltaTime * DelayTime);
            //yield return new WaitForSecondsRealtime(0.01f);
            yield return wait;
        }
    }
}


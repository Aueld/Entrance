using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : BulletMove
{
    // 플레이어 위치
    public GameObject target;

    // 고정 z값
    private float offsetZ = -5f;
    
    // 화면 이동 딜레이 시간
    private float DelayTime = 1.5f;

    // 계산된 현재 위치
    private Vector3 FixedPos { get; set; }

    private void Start()
    {
        // 던전 진입시 입장 딜레이 => 맵 생성 및 플레이어 이동 카메라 이동 로딩
        if (SceneManager.GetActiveScene().name == "GameDengeonScene")
            Invoke("Loading", 1f);

        StartCoroutine(Updater());
    }

    private void Loading()
    {
        // 카메라의 위치 변경
        transform.position = target.transform.position;
    }

    private IEnumerator Updater()
    {
        while (true)
        {

            // 마우스의 화면상 위치 기록
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // 마우스와 플레이어 중간의 값에 카메라 위치하게
            FixedPos = new Vector3(
                (target.transform.position.x + mouse.x) / 2,
                (target.transform.position.y + mouse.y) / 2,
                target.transform.position.z + offsetZ);

            // Vector.Lerp 로 움직이게 설정
            transform.position = Vector3.Lerp(transform.position, FixedPos, Time.fixedDeltaTime * DelayTime);
            //yield return new WaitForSecondsRealtime(0.01f);
            
            yield return wait;
        }
    }
}


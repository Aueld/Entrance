using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : Unit
{
    // 플레이어의 위치에 존재하는 보이지 않는 축의 중심값 설정
    public GameObject player;

    void Start()
    {
        StartCoroutine(Updater());
    }

    private IEnumerator Updater()
    {
        // 축 중심값 설정
        while (true)
        {
            transform.position = player.transform.position;
            yield return Wait;
        }
    }
}

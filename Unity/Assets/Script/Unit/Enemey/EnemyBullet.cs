using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : ShotPatterns
{
    private void Start()
    {
        parent = GameObject.FindWithTag("Pool");
        Player = GameObject.FindGameObjectWithTag("Player");
        // 오브젝트 풀링 구현
        for (int i = 0; i < maxBullet; i++)
        {
            bullets.Add(Instantiate(obj, transform.position, Quaternion.identity));
            bullets[i].transform.parent = parent.transform;
            bullets[i].SetActive(false);
        }

        time = 0;
    }

    void Update()
    {
        time++;
        if (time > 2000)
        {
            time = 0;
            SearchPlayer();
        }
    }
}

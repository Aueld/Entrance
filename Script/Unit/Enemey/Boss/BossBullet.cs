using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : ShotPatterns
{


    private void Start()
    {
        parent = GameObject.FindWithTag("Pool");
        Player = GameObject.FindGameObjectWithTag("Player");
        audioSource = GetComponent<AudioSource>();

        // ������Ʈ Ǯ�� ����
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
        if (GameManager.Instance.gameOver || GameManager.Instance.gameEnd)
            return;

        time++;
        if (time > 200)
        {
            time = 0;
            SearchPlayerWithBoss();
        }
    }
}

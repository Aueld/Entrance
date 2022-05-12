using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiMik : MonoBehaviour
{
    private WaitForSeconds Wait = new WaitForSeconds(0.001f);

    // 플레이어 오브젝트
    public GameObject player;

    private bool check;

    private void Start()
    {
        check = true;

        StartCoroutine(Updater());
    }


    private IEnumerator Updater()
    {
        while (check)
        {
            if (player.transform.position.y > 8)    // 플레이어 오브젝트로부터 일정 거리 이상 떨어질 때
            {
                check = false;

                // 게임오브젝트 비활성화
                gameObject.SetActive(false);

            }
            yield return Wait;
        }
    }
}

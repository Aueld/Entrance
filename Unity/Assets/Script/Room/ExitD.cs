using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitD : MonoBehaviour
{
    // 던전 출구에 플레이어가 도달했을때
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 보스방으로 이동
        if(collision.transform.tag == "Player")
            LoadSceneController.LoadScene("GameBoss01Scene");
    
        // GameManager에 값 추가해서 게임 진행도에 따라 새로운 보스방에 갈 수 있게 설정할 예정
        //
        //
        //
    }
}

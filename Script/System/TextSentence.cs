using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TextSentence : MonoBehaviour
{
    public string[] sentences;                  // 텍스트 센텐스
    public Transform chatTr;                    // 채팅박스 위치
    public GameObject chatBoxPrefab;            // 채팅박스 프리팹

    public GameObject Canvas;                   // 채팅박스 캔버스
    public PlayableDirector playableDirector;   // 시네머신

    private GameObject go;                      // 생성된 채팅박스

    private bool check = false;

    public void TalkNpc()
    {
        check = true;
        go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }

    // 플레이어 감지시 채팅박스 출력
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !check)
        {
            TalkNpc();

            Canvas.SetActive(true);
            playableDirector.Play();
        }
    }

    // 플레이어가 멀어질 시 생성된 채팅박스 삭제
    private void OnTriggerExit2D(Collider2D collision)
    {
        Canvas.SetActive(false);
        check = false;
        Destroy(go);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSentence : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    private GameObject go;

    private bool check = false;

    public void TalkNpc()
    {
        check = true;
        go = Instantiate(chatBoxPrefab);
        go.GetComponent<ChatSystem>().Ondialogue(sentences, chatTr);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !check)
            TalkNpc();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        check = false;
        Destroy(go);
    }
}

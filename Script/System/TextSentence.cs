using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TextSentence : MonoBehaviour
{
    public string[] sentences;
    public Transform chatTr;
    public GameObject chatBoxPrefab;

    public GameObject Canvas;
    public PlayableDirector playableDirector;

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
        if (collision.tag == "Player" && !check)
        {
            TalkNpc();

            Canvas.SetActive(true);
            playableDirector.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Canvas.SetActive(false);
        check = false;
        Destroy(go);
    }
}

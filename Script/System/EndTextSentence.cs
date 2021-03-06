using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTextSentence : MonoBehaviour
{
    public string[] sentences;
    public GameObject EndCanvas;
    public GameObject EndUI;

    private void Update()
    {
        // 게임 종료시
        if (EndCanvas.activeSelf && GameManager.Instance.gameEnd)
        {
            GameManager.Instance.gameEnd = false;
            EndUI.GetComponent<EndingTextSystem>().Ondialogue(sentences);
        }
    }
}

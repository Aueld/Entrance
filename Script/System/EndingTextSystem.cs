using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingTextSystem: MonoBehaviour
{
    public Queue<string> sentences; // 대사 큐
    public string currentSentence;  // TMP
    public Text text;               // text

    public void Ondialogue(string[] lines)
    {
        // string 큐 초기화
        sentences = new Queue<string>();
        sentences.Clear();

        foreach (var line in lines)
        {
            // 한 큐 ( 한 문장 ) 씩 출력
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow());
    }

    IEnumerator DialogueFlow()
    {
        yield return null;

        // 출력 될 string 큐가 남았을 경우
        while(sentences.Count > 0)
        {
            // 다음 내용 출력
            currentSentence = sentences.Dequeue();
            text.text = currentSentence;

            // 5초간 기다림
            yield return new WaitForSeconds(3f);
        }

        // 5초간 기다림
        yield return new WaitForSeconds(3f);

        // 메인화면으로
        LoadSceneController.LoadScene("mainStartScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingTextSystem: MonoBehaviour
{
    public Queue<string> sentences;
    public string currentSentence;
    public Text text;

    public void Ondialogue(string[] lines)
    {
        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow());
    }

    IEnumerator DialogueFlow()
    {
        yield return null;

        while(sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();
            text.text = currentSentence;

            yield return new WaitForSeconds(5f);
        }


        yield return new WaitForSeconds(5f);

        LoadSceneController.LoadScene("mainStartScene");

    }
}

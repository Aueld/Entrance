using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    // 대사 큐
    public Queue<string> sentences;
    public string currentSentence;
    public TextMeshPro textMesh;
    public GameObject quad;

    public void Ondialogue(string[] lines, Transform pos)
    {
        transform.position = pos.position;

        sentences = new Queue<string>();
        sentences.Clear();
        foreach (var line in lines)
        {
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(pos));
    }

    IEnumerator DialogueFlow(Transform pos)
    {
        yield return null;

        while(sentences.Count > 0)
        {
            currentSentence = sentences.Dequeue();
            textMesh.text = currentSentence;

            float x = textMesh.preferredWidth;
            x = (x > 3) ? 3 : x + 0.3f;

            quad.transform.localScale = new Vector2(x, textMesh.preferredHeight + 0.3f);

            transform.position = new Vector2(pos.position.x, pos.position.y + textMesh.preferredHeight / 2);

            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}

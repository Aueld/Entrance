<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences; // 대사 큐
    public TextMeshPro textMesh;    // TMP
    public GameObject quad;         // quad

    public string currentSentence;

    public void Ondialogue(string[] lines, Transform pos)
    {
        // 대사 창의 위치
        transform.position = pos.position;

        // string 큐 초기화
        sentences = new Queue<string>();
        sentences.Clear();

        foreach (var line in lines)
        {
            // 한 큐 ( 한 문장 ) 씩 출력
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(pos));
    }

    IEnumerator DialogueFlow(Transform pos)
    {
        yield return null;

        // 출력 될 strig 큐가 남았을 경우
        while(sentences.Count > 0)
        {
            // 다음 내용 출력
            currentSentence = sentences.Dequeue();
            textMesh.text = currentSentence;

            // 대사 배경 크기 조정
            float x = textMesh.preferredWidth;
            x = (x > 3) ? 3 : x + 0.3f;

            // 위치 조정
            quad.transform.localScale = new Vector2(x, textMesh.preferredHeight + 0.3f);

            transform.position = new Vector2(pos.position.x, pos.position.y + textMesh.preferredHeight / 2);

            // 3초마다 반복
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatSystem : MonoBehaviour
{
    public Queue<string> sentences; // 대사 큐
    public TextMeshPro textMesh;    // TMP
    public GameObject quad;         // quad

    public string currentSentence;

    public void Ondialogue(string[] lines, Transform pos)
    {
        // 대사 창의 위치
        transform.position = pos.position;

        // string 큐 초기화
        sentences = new Queue<string>();
        sentences.Clear();

        foreach (var line in lines)
        {
            // 한 큐 ( 한 문장 ) 씩 출력
            sentences.Enqueue(line);
        }
        StartCoroutine(DialogueFlow(pos));
    }

    IEnumerator DialogueFlow(Transform pos)
    {
        yield return null;

        // 출력 될 strig 큐가 남았을 경우
        while(sentences.Count > 0)
        {
            // 다음 내용 출력
            currentSentence = sentences.Dequeue();
            textMesh.text = currentSentence;

            // 대사 배경 크기 조정
            float x = textMesh.preferredWidth;
            x = (x > 3) ? 3 : x + 0.3f;

            // 위치 조정
            quad.transform.localScale = new Vector2(x, textMesh.preferredHeight + 0.3f);

            transform.position = new Vector2(pos.position.x, pos.position.y + textMesh.preferredHeight / 2);

            // 3초마다 반복
            yield return new WaitForSeconds(3f);
        }
        Destroy(gameObject);
    }
}
>>>>>>> Stashed changes

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inter: MonoBehaviour
{
    [SerializeField] Camera cam;

    private static Dictionary<string, TalkData[]> DialogueDictionary =
                 new Dictionary<string, TalkData[]>();

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // 대화 정보 가져오기
                TalkData[] talkDatas = hit.transform.GetComponent<Dialogue>().GetObjectDialogue();
                // 대사가 null이 아니면 대사 출력
                if (talkDatas != null) DebugDialogue(talkDatas); ;

            }
        }
    }

    // 대화 정보 출력하는 함수
    void DebugDialogue(TalkData[] talkDatas)
    {
        for (int i = 0; i < talkDatas.Length; i++)
        {
            // 캐릭터 이름 출력
            Debug.Log(talkDatas[i].name);
            // 대사들 출력
            foreach (string context in talkDatas[i].contexts)
                Debug.Log(context);
        }
    }

    public static TalkData[] GetDialogue(string eventName)
    {
        // 키에 매칭되는 값이 있으면 true 없으면 false
        if (DialogueDictionary.ContainsKey(eventName))
            return DialogueDictionary[eventName];
        else
        {
            // 경고 출력하고 null 반환
            Debug.LogWarning("찾을 수 없는 이벤트 이름 : " + eventName);
            return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneController : MonoBehaviour
{
    // 정적 생성
    static string nextScene;

    // 인스펙터에서 접근이 가능하게 설정
    [SerializeField] Image progressBar;

    private void Start()
    {
        StartCoroutine(LoadSceneProcess());
    }

    // 씬 전환시 로딩씬이 나오고 다음씬으로 넘어가게 설정
    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadScene");
    }

    // 로딩 프로세스
    private IEnumerator LoadSceneProcess()
    {
        yield return null;

        // 비동기적인 연산을 위한 코루틴
        // 비동기 장면 전환 // 모든 정보를 메모리로 가져오기 전까지 기다림
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        // 장면 즉시 활성화시킬것인이 혀용 여부
        op.allowSceneActivation = false;
        
        float timer = 0.0f;
        
        // 동작이 준비되었는가
        while (!op.isDone) {
            yield return null;
            
            timer += Time.deltaTime;

            // 프로그래스바 차오르게 설정
            if (op.progress < 0.9f) {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                if (progressBar.fillAmount >= op.progress) {
                    timer = 0f;
                }
            }
            // 완료시 프로그래스 채우고 탈출
            else {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f) {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}

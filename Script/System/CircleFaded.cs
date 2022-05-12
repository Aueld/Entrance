<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFaded : MonoBehaviour
{
    //Wait 객체가 하나만 생기게 구현
    private static WaitForSeconds Wait = new WaitForSeconds(0.001f);

    [SerializeField]
    float speed = 0.3f;

    // 정중앙이 될 타겟 오브젝트
    GameObject target;

    // 쉐이더 메테리얼
    Material runtimeMaterial;

    void Awake()
    {
        this.runtimeMaterial = Instantiate(GetComponent<Image>().material);
        GetComponent<Image>().material = this.runtimeMaterial;
        target = GameObject.FindWithTag("Player");                  // 타겟을 플레이어로 지정

        StartCoroutine(Faded());
    }

    private IEnumerator Faded()
    {

        //transform.position = target.transform.position;

        Rect rect = GetComponent<Image>().rectTransform.rect;
        float halfWidth = rect.width * 2;

        float radius = Mathf.Abs(Mathf.Sin(this.speed) * halfWidth);

        //float time = 0f;

        // 원 지름 10
        radius = 10f;

        this.runtimeMaterial.SetFloat("_Width", rect.width);
        this.runtimeMaterial.SetFloat("_Height", rect.height);
        this.runtimeMaterial.SetFloat("_Radius", radius);

        while (radius > 0)
        {
            // 타겟을 계속 따라가게
            transform.position = target.transform.position;
            //            radius = Mathf.Abs(Mathf.Sin(Time.deltaTime * this.speed) * halfWidth);
            //radius = (Mathf.Sin(Time.deltaTime * this.speed) * halfWidth);


            //time += 0.0001f;

            yield return Wait;
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFaded : MonoBehaviour
{
    //Wait 객체가 하나만 생기게 구현
    private static WaitForSeconds Wait = new WaitForSeconds(0.001f);

    [SerializeField]
    float speed = 0.3f;

    // 정중앙이 될 타겟 오브젝트
    GameObject target;

    // 쉐이더 메테리얼
    Material runtimeMaterial;

    void Awake()
    {
        this.runtimeMaterial = Instantiate(GetComponent<Image>().material);
        GetComponent<Image>().material = this.runtimeMaterial;
        target = GameObject.FindWithTag("Player");                  // 타겟을 플레이어로 지정

        StartCoroutine(Faded());
    }

    private IEnumerator Faded()
    {

        //transform.position = target.transform.position;

        Rect rect = GetComponent<Image>().rectTransform.rect;
        float halfWidth = rect.width * 2;

        float radius = Mathf.Abs(Mathf.Sin(this.speed) * halfWidth);

        //float time = 0f;

        // 원 지름 10
        radius = 10f;

        this.runtimeMaterial.SetFloat("_Width", rect.width);
        this.runtimeMaterial.SetFloat("_Height", rect.height);
        this.runtimeMaterial.SetFloat("_Radius", radius);

        while (radius > 0)
        {
            // 타겟을 계속 따라가게
            transform.position = target.transform.position;
            //            radius = Mathf.Abs(Mathf.Sin(Time.deltaTime * this.speed) * halfWidth);
            //radius = (Mathf.Sin(Time.deltaTime * this.speed) * halfWidth);


            //time += 0.0001f;

            yield return Wait;
        }
    }
}
>>>>>>> Stashed changes

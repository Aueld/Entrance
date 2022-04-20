using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleFaded : MonoBehaviour
{
    private static WaitForSeconds Wait = new WaitForSeconds(0.001f);

    [SerializeField]
    float speed = 0.3f;

    GameObject target;

    Material runtimeMaterial;

    void Awake()
    {
        this.runtimeMaterial = Instantiate(GetComponent<Image>().material);
        GetComponent<Image>().material = this.runtimeMaterial;
        target = GameObject.FindWithTag("Player");


        StartCoroutine(Faded());
    }

    private IEnumerator Faded()
    {

        //transform.position = target.transform.position;

        Rect rect = GetComponent<Image>().rectTransform.rect;
        float halfWidth = rect.width * 2;

        float radius = Mathf.Abs(Mathf.Sin(this.speed) * halfWidth);

        //float time = 0f;


        radius = 10f;


        this.runtimeMaterial.SetFloat("_Width", rect.width);
        this.runtimeMaterial.SetFloat("_Height", rect.height);
        this.runtimeMaterial.SetFloat("_Radius", radius);

        while (radius > 0)
        {

            transform.position = target.transform.position;
            //            radius = Mathf.Abs(Mathf.Sin(Time.deltaTime * this.speed) * halfWidth);
            //radius = (Mathf.Sin(Time.deltaTime * this.speed) * halfWidth);


            //time += 0.0001f;

            yield return Wait;
        }
    }
}

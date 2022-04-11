using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : ControlManager
{
    private static WaitForSeconds lon = new WaitForSeconds(0.2f);

    public GameObject parent;
    public GameObject obj;

    public int maxBullet = 200;
    public int bullet;

    private List<GameObject> bullets = new List<GameObject>();
    private GameObject pin;

    void Start()
    {
        pin = GameObject.FindWithTag("Gun");

        // 오브젝트 풀링 구현
        for (int i = 0; i < maxBullet; i++)
        {

            bullets.Add(Instantiate(obj, transform.position, Quaternion.AngleAxis(GetAngle(transform.position, mouse), Vector3.forward))); //.transform.parent = parent.transform; // transform.rotation);// );
            
            bullets[i].transform.parent = parent.transform;
            bullets[i].SetActive(false);

        }
        bullet = 30;
        StartCoroutine(Updater());
    }

    private IEnumerator Updater()
    {


        while (true)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (bullet > 0)
            {
                if (GameManager.Instance.gameOver || GameManager.Instance.gameEnd)
                    yield break;

                if (Input.GetMouseButton(0))
                {
                    pin.GetComponent<SoundManager>().PlayEff();
                    Debug.Log("탄소모 : " + bullet);
                    bullet--;

                    for (int i = 0; i < maxBullet; i++)
                    {
                        if (!bullets[i].activeSelf)
                        {
                            bullets[i].transform.position = transform.position;
                            bullets[i].transform.rotation = Quaternion.AngleAxis(GetAngle(transform.position, mouse), Vector3.forward);
                            bullets[i].SetActive(true);
                            break;
                        }
                    }

                    //Instantiate(obj, transform.position, Quaternion.AngleAxis(GetAngle(transform.position, mouse), Vector3.forward)).transform.parent = parent.transform; // transform.rotation);// );
                    //yield return fire;
                    yield return lon;
                }
            }

            yield return wait;
        }
    }
}

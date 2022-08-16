using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shot : ControlManager
{
    private static WaitForSeconds lon = new WaitForSeconds(0.2f);

    public GameObject parent;
    public GameObject obj;

    public int maxBullet = 200;
    public int bullet;
    public Text bulletCount;

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

                // 탄 발사
                if (Input.GetMouseButton(0))
                {
                    // 발사 중
                    GameManager.Instance.onFire = true;

                    pin.GetComponent<SoundManager>().PlayEff();
                    Debug.Log("탄소모 : " + bullet);
                    bullet--;
                    
                    bulletCount.text = bullet + " / 30";

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
                else
                    GameManager.Instance.onFire = false;
            }
            else
                GameManager.Instance.onFire = false;

            yield return wait;
        }
    }
}

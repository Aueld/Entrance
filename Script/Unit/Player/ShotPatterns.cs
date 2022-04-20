using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPatterns : MonoBehaviour
{
    private static WaitForSeconds Wait = new WaitForSeconds(0.1f);

    public Transform Center;
    //public GameObject bullet;

    public bool ShotOn;

    protected GameObject parent;
    public GameObject obj;

    public int maxBullet = 512;

    protected List<GameObject> bullets = new List<GameObject>();

    protected GameObject Player;
    private float shortDis;

    private float rot_Speed;
    protected int time;

    private int rand;

    public float rot = 0f;
    public int Vertex = 3;
    public float sup = 3;

    public float Speed = 3;//speed

    private int m;
    private float a;
    private float phi;
    private List<float> v = new List<float>();
    private List<float> xx = new List<float>();

    public AudioSource audioSource;

    private GameObject cameraCut;
    private bool cutCheck = false;

    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        ShotOn = false;
        rot_Speed = 2f;
        parent = GameObject.FindWithTag("Pool");
        Player = GameObject.FindGameObjectWithTag("Player");

        cameraCut = Camera.main.gameObject;
    }

    public void SearchPlayer()
    {

        shortDis = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        //Debug.Log(shortDis);

        if (shortDis < 5f)
        {
            //Center = Player.transform;

            ShotOn = true;
            rand = Random.Range(1, 10);
            if(rand < 5)
            {
                //ShapeInit();
                //ShapeShot();
                ShotOn = false;
            }
            else if(rand > 5 && rand < 8)
            {
                GotoShot();
            }
            else if(rand > 8)
            {
                SpinShot();
            }

            audioSource.Play();
        }
    }

    public void SearchPlayerWithBoss()
    {
        shortDis = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        //Debug.Log(shortDis);

        // 보스와의 거리
        if (shortDis < 15f && !cutCheck)
        {
            cutCheck = true;
            GameManager.Instance.BossCut();
        }
        // 보스 - 플레이어 인식 거리
        if (shortDis < 20f)
        {
            //Center = Player.transform;

            ShotOn = true;
            rand = Random.Range(1, 10);
            if (rand < 4)
            {
                //SpinShot();
                ShapeInit();
                ShapeShot();
            }
            else if (rand > 4 && rand < 7)
            {
                CircleShot();
            }
            else if (rand > 7 && rand < 8)
            {
                CircleShot();
                GotoShot();
            }
            else if (rand > 8)
            {
                SpinShot();
            }
            audioSource.Play();
        }
    }

    public void GotoShot()
    {
        if (!ShotOn)
            return;

        for (int i = 0; i < maxBullet; i++)
        {
            if (!bullets[i].activeSelf)
            {
                bullets[i].transform.position = transform.position;
                bullets[i].transform.rotation = Center.rotation;
                bullets[i].SetActive(true);
                break;
            }
        }

        ShotOn = false;
    }

    public void SpinShot()
    {
        StartCoroutine(CSpinShot());
    }

    void ShapeInit()
    {
        v.Clear();
        xx.Clear();

        Vertex = Random.Range(3, 7);

        m = (int)Mathf.Floor(sup / 2);
        a = 2 * Mathf.Sin(Mathf.PI / Vertex);
        phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;
        v.Add(0);
        xx.Add(0);

        for (int i = 1; i <= m; i++)
        {
            v.Add(Mathf.Sqrt(sup * sup - 2 * a * Mathf.Cos(phi) * i * sup + a * a * i * i));
        }

        for (int i = 1; i <= m; i++)
        {
            xx.Add(Mathf.Rad2Deg * (Mathf.Asin(a * Mathf.Sin(phi) * i / v[i])));
        }
    }

    public void ShapeShot()
    {

        var dir = rot;

        for (int r = 0; r < Vertex; r++)
        {
            for (int j = 1; j <= m; j++)
            {
                #region

                for (int i = 0; i < maxBullet; i++)
                {
                    if (!bullets[i].activeSelf)
                    {
                        bullets[i].SetActive(true);
                        bullets[i].transform.position = transform.position;
                        bullets[i].transform.rotation = Quaternion.Euler(0, 0, dir + xx[j]);

                        bullets[i].GetComponent<EBulletMove>().speed = v[j] * Speed / sup;
                        break;
                    }
                }

                #endregion

                #region 

                for (int i = 0; i < maxBullet; i++)
                {
                    if (!bullets[i].activeSelf)
                    {
                        bullets[i].SetActive(true);
                        bullets[i].transform.position = transform.position;
                        bullets[i].transform.rotation = Quaternion.Euler(0, 0, dir - xx[j]);

                        bullets[i].GetComponent<EBulletMove>().speed = v[j] * Speed / sup;
                        break;
                    }
                }

                #endregion

                #region

                for (int i = 0; i < maxBullet; i++)
                {
                    if (!bullets[i].activeSelf)
                    {
                        bullets[i].SetActive(true);
                        bullets[i].transform.position = transform.position;
                        bullets[i].transform.rotation = Quaternion.Euler(0, 0, dir);

                        bullets[i].GetComponent<EBulletMove>().speed = Speed;
                        break;
                    }
                }


                #endregion

                dir += 360 / Vertex;
            }
        }

        ShotOn = false;
    }

    void CircleShot()
    {
        for (int j = 0; j < 360; j += 13)
        {
            for (int i = 0; i < maxBullet; i++)
            {
                if (!bullets[i].activeSelf)
                {
                    bullets[i].GetComponent<EBulletMove>().speed = 3f;
                    bullets[i].transform.position = transform.position;
                    bullets[i].transform.rotation = Quaternion.Euler(0, 0, j);
                    bullets[i].SetActive(true);
                    break;
                }
                else
                    continue;
            }
        }
        ShotOn = false;
    }

    private IEnumerator CSpinShot()
    {
        int bulletNum = 10;
        while (ShotOn && bulletNum > 0)
        {
            bulletNum--;

            if(bulletNum < 2)
            {
                ShotOn = false;
                break;
            }
            transform.Rotate(Vector3.forward * rot_Speed * 100);


            for (int i = 0; i < maxBullet; i++)
            {
                if (!bullets[i].activeSelf)
                {
                    bullets[i].transform.position = transform.position;
                    bullets[i].transform.rotation = Center.rotation;
                    bullets[i].SetActive(true);
                    break;
                }
            }

            yield return Wait;
        }
    }
}

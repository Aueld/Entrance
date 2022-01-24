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

    //초기 중심 : 회전 되는 방향
    public float rot = 0f;
    public int Vertex = 3;
    public float sup = 3;

    //스피드
    public float Speed = 3;//speed

    //기타 데이터들
    int m;
    float a;
    float phi;
    List<float> v = new List<float>();
    List<float> xx = new List<float>();

    protected AudioSource audioSource;

    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
        ShotOn = false;
        rot_Speed = 2f;
        parent = GameObject.FindWithTag("Pool");
        Player = GameObject.FindGameObjectWithTag("Player");
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

        if (shortDis < 10f)
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
        //요소들이 들어 있을 수 있으니 초기화 하기전에 Clear한다.
        v.Clear();
        xx.Clear();

        Vertex = Random.Range(3, 7);

        //데이터 초기화
        m = (int)Mathf.Floor(sup / 2);
        a = 2 * Mathf.Sin(Mathf.PI / Vertex);
        phi = ((Mathf.PI / 2f) * (Vertex - 2f)) / Vertex;
        v.Add(0);
        xx.Add(0);

        for (int i = 1; i <= m; i++)
        {
            //list.Insert(위치,요소) -> 해당 위치에 값을 집어넣습니다.
            v.Add(Mathf.Sqrt(sup * sup - 2 * a * Mathf.Cos(phi) * i * sup + a * a * i * i));
        }

        for (int i = 1; i <= m; i++)
        {
            xx.Add(Mathf.Rad2Deg * (Mathf.Asin(a * Mathf.Sin(phi) * i / v[i])));
        }
    }

    public void ShapeShot()
    {

        //rot값에 영향을 주지 않도록 별도로 dir값을 선언하였다.
        var dir = rot;

        //꼭짓점 수 만큼 실행
        for (int r = 0; r < Vertex; r++)
        {
            for (int j = 1; j <= m; j++)
            {
                #region //1차 생성

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

                #region //2차 생성

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

                #region //3차 생성

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

                //모양을 완성한다.
                dir += 360 / Vertex;
            }
        }

        ShotOn = false;
    }

    void CircleShot()
    {
        //360번 반복
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

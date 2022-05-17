using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Enemy : Unit
{
    public GameObject GB_HP_Bar;            // 체력바 오브젝트
    public Image HPimg;                     // 체력바 이미지

    private GameObject player;              // 플레이어

    private GlitchEffect glitchEffect;      // 글리치 이펙트
    private CircleCollider2D hitBox;        // 유닛 히트 박스
    private Animator ani;
    private SpriteRenderer spriteRenderer;

    private Vector2 SPos;                   // 유닛 생성 초기 위치

    private float DelayTime = 5f;

    private bool playerCheck = false;       // 플레이어 판단
    private bool wallCheck = false;         // 벽 판단

    void Start()
    {
        //GB_HP_Bar.SetActive(false);

        SPos = transform.position;

        Check = false;
        player = GameObject.FindWithTag("Player");
        glitchEffect = Camera.main.GetComponent<GlitchEffect>();
        hitBox = GetComponent<CircleCollider2D>();
        ani = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // 유닛 활성화 시
    private void OnEnable()
    {
        GB_HP_Bar.SetActive(false);

        SPos = transform.position;

        HP = 3;
        
        HPimg.fillAmount = HP / 3;

        Check = false;
        try {
            if (gameObject.activeSelf)
                hitBox.enabled = true;
        }
        catch { }
    }

    void Update()
    {
        //if (GetDistance(transform.position.x, transform.position.y, player.transform.position.x, player.transform.position.y) < 1)
            //Debug.Log("감지");

        if (!playerCheck) // 자동 패트롤 무빙
        {
            if (!Check)
                StartCoroutine(WaitMove());
        }
        else
        {

        }
    }

    public override void Hit()
    {
        //Debug.Log("근접 공격!, 적 남은 체력 : " + HP);
        // 피격시
        HP--;

        HPimg.fillAmount = HP / 3f;

        if (HP < 1)
        {
            StartCoroutine(Glitch());
        }

        StartCoroutine(SeeHPBar());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Walls")
        {
            wallCheck = true;
            transform.position = Vector2.Lerp(transform.position, SPos, Time.deltaTime);
        }

        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("HIT");
            Hit();

        }
    }

    protected override float GetDistance(float x1, float y1, float x2, float y2)
    {
        float width = x2 - x1;
        float height = y2 - y1;

        float distance = width * width + height * height;
        distance = Mathf.Sqrt(distance);

        return distance;
    }

    protected override void UnitLR()
    {
        // 좌우 반전
        if (transform.position.x > Pos.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

        //transform.localScale = new Vector2(size * LR, size);
    }

    private IEnumerator PosMove()
    {
        ani.SetBool("Move", true);
        float time = 3f;

        RandX = UnityEngine.Random.Range(-1.5f, 1.5f);
        RandY = UnityEngine.Random.Range(-1.5f, 1.5f);
        Rand = UnityEngine.Random.Range(-2, 1);

        if (Rand == -2)
            Rand = -1;
        if (Rand == 0)
            Rand = 1;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 3.0f)
            Pos = new Vector2(player.transform.position.x + RandX * Rand, player.transform.position.y + RandY * Rand);
         else
            Pos = SPos + new Vector2(RandX * Rand, RandY * Rand);


        // 좌우 반전
        UnitLR();

        while (Check)
        {
            if (time < 0)
            {
                Check = false;
                break;
            }


            if (!wallCheck)
            {
                transform.position = Vector2.Lerp(transform.position, Pos, Time.deltaTime * DelayTime);
            }
            else
            {
                wallCheck = false;
                Check = false;
                break;
            }

            time -= 0.1f;
            yield return Wait;

        }
    }

    private IEnumerator WaitMove()
    {
        ani.SetBool("Move", false);
        Check = true;
        yield return new WaitForSeconds((float)UnityEngine.Random.Range(2, 5));

        StartCoroutine(PosMove());
    }

    private IEnumerator SeeHPBar()
    {
        GB_HP_Bar.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        GB_HP_Bar.SetActive(false);
    }

    protected override IEnumerator Glitch()
    {
        hitBox.enabled = false;

        glitchEffect.flipIntensity = 0.55f;
        glitchEffect.colorIntensity = 0.45f;

        yield return new WaitForSeconds(0.5f);

        glitchEffect.flipIntensity = 0f;
        glitchEffect.colorIntensity = 0f;

        gameObject.SetActive(false);

        //Destroy(gameObject);
    }

    //public void CoinMove()
    //{
    //    dir = (playerpos.position - transform.position).normalized;
    //    acceleration = 0.2f;
    //    velocity = (velocity + acceleration * Time.deltaTime);
    //    float distance = Vector3.Distance(playerpos.position, transform.position);


    //    if (distance <= 3.0f)
    //    {
    //        transform.position = new Vector3(transform.position.x + (dir.x * velocity),
    //                                               transform.position.y,
    //                                                 transform.position.z + (dir.z * velocity));
    //    }
    //    else
    //    {
    //        velocity = 0.0f;
    //    }
    //}
}

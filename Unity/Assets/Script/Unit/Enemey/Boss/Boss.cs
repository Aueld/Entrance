using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Boss : Unit
{
    public GameObject GB_HP_Bar;
    public Image HPimg;

    private GameObject player;

    private GlitchEffect glitchEffect;
    private CircleCollider2D hitBox;
    private SpriteRenderer spriteRenderer;

    private float DelayTime = 5f;

    private bool playerCheck = false;


    void Start()
    {
        Check = false;
        player = GameObject.FindWithTag("Player");
        glitchEffect = Camera.main.GetComponent<GlitchEffect>();
        hitBox = GetComponent<CircleCollider2D>();
        GB_HP_Bar.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        HP = 125;

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

        if (!playerCheck)
        {
            //if (!Check)
                //StartCoroutine(WaitMove());
        }
        else
        {

        }
    }

    public override void Hit()
    {
        //Debug.Log("근접 공격!, 적 남은 체력 : " + HP);
        HP--;


        HPimg.fillAmount = HP / 125f;

        if (HP < 1)
        {
            GameManager.Instance.GameEnd();


            StartCoroutine(Glitch());

        }
        StartCoroutine(SeeHPBar());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            //Debug.Log("HIT");
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
        if (transform.position.x > Pos.x)
            spriteRenderer.flipX = true;
        else
            spriteRenderer.flipX = false;

    }

    private IEnumerator PosMove()
    {
        float time = 0.5f;

        RandX = UnityEngine.Random.Range(-5f, 5f);
        RandY = UnityEngine.Random.Range(-5f, 5f);
        Rand = UnityEngine.Random.Range(-2, 1);

        if (Rand == -2)
            Rand = -1;
        if (Rand == 0)
            Rand = 1;

        Pos = new Vector2(player.transform.position.x + RandX * Rand, player.transform.position.y + RandY * Rand);

        UnitLR();

        while (Check)
        {
            if (time < 0)
            {
                Check = false;
                break;
            }


            transform.position = Vector2.Lerp(transform.position, Pos, Time.deltaTime * DelayTime);

            time -= 0.1f;
            yield return Wait;

        }
    }

    private IEnumerator WaitMove()
    {
        Check = true;
        yield return new WaitForSeconds(5f);

        StartCoroutine(PosMove());
    }
    private IEnumerator SeeHPBar()
    {
        GB_HP_Bar.SetActive(true);
        yield return new WaitForSeconds(50f);
        //GB_HP_Bar.SetActive(false);
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

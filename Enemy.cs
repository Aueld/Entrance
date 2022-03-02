using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : Unit
{

    public GameObject player;

    private GlitchEffect glitchEffect;
    private CircleCollider2D hitBox;

    private float DelayTime = 5f;

    private bool playerCheck = false;

    void Start()
    {
        Check = false;
        glitchEffect = Camera.main.GetComponent<GlitchEffect>();
        hitBox = GetComponent<CircleCollider2D>();

    }

    private void OnEnable()
    {
        HP = 3;

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
        HP--;
        if (HP < 1)
        {
            StartCoroutine(Glitch());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

    protected override void UnitLR(int size)
    {
        if (transform.position.x > Pos.x)
            LR = -1;
        else
            LR = 1;

        transform.localScale = new Vector2(size * LR, size);
    }

    private IEnumerator PosMove()
    {
        float time = 3f;

        RandX = UnityEngine.Random.Range(-5f, 5f);
        RandY = UnityEngine.Random.Range(-5f, 5f);
        Rand = UnityEngine.Random.Range(-2, 1);

        if (Rand == -2)
            Rand = -1;
        if (Rand == 0)
            Rand = 1;

        Pos = new Vector2(RandX * Rand, RandY * Rand);

        UnitLR(5);

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

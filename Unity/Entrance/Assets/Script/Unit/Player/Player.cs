using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : PlayerSetting
{
    private List<GameObject> Enemy;
    private GameObject targetEnemy;

    private Enemy enemy;
    private float shortDis;
    private float distance;
    private bool coolTime = false;

    private void Update()
    {
        Move();
    }
    private void FixedUpdate()
    {
        FixedMove();
    }

    private void FixedMove()
    {
        h = Input.GetAxis("Horizontal") * Speed;
        v = Input.GetAxis("Vertical") * Speed;

        transform.position += new Vector3(h, v, 0);
    }

    protected override void Move()
    {
        if (Input.GetKeyDown(KeyCode.Space) && MoveJudment() && !rollCheck)
        {
            StartCoroutine(CrtRoll());
        }


        KeyState();

        if (v == 0 && h == 0)
            Animate((AniState)lastState);

        else if (v <= 0)
            HorizontalSet(h, (int)AniState.Front, (int)AniState.FrontSide, 3);

        else if (v > 0)
            HorizontalSet(h, (int)AniState.Back, (int)AniState.BackSide, 3);
    }

    protected override void KeyDown_E()
    {

        StartCoroutine(MeleeAttack());

        Enemy = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        shortDis = Vector3.Distance(gameObject.transform.position, Enemy[0].transform.position);

        if (shortDis < 5f)
        {
            targetEnemy = Enemy[0];

            foreach (GameObject found in Enemy)
            {
                distance = Vector3.Distance(gameObject.transform.position, found.transform.position);
                if (distance < shortDis)
                {
                    targetEnemy = found;
                    enemy = targetEnemy.GetComponent<Enemy>();
                    enemy.Hit();
                    shortDis = distance;
                }
            }
            Debug.Log(targetEnemy.name + " 와의 거리 : " + shortDis);
        }
    }

    protected override void KeyDown_R()
    {
        pin.GetComponent<SoundManager>().Reloading();

        Debug.Log("재장전 30발");
        shot.bullet = 30;
        cursorMouse.Reloading();
    }

    public override void Hit()
    {
        if (Invincible)
            return;

        if (HP > 0)
        {
            //Debug.Log("플레이어 HiT!!");
            HP--;
        }
        if(HP <= 0)
        {
            GameManager.Instance.GameOver();
            //Debug.Log("플레이어 Finish");
        }
    }
    
    private void KeyState()
    {
        KeyDown();
        KeyUp();
    }

    private void HorizontalSet(float h2, int state1, int state2, int size)
    {
        if (h2 == 0)
        {
            Animate((AniState)state1);
            lastState = state1;
        }
        else if (h2 > 0 || h2 < 0)
        {
            Animate((AniState)state2);
            lastState = state2;
        }

        UnitLR(size);
    }

    protected override void UnitLR(int size)
    {
        if (h < 0)
            LR = -1;
        else
            LR = 1;
        
        transform.localScale = new Vector2(size * LR, size);
    }

    private void Animate(AniState state)
    {
        animator.SetInteger("State", (int)state);
    }

    private bool MoveJudment()
    {
        if (Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical") > 0)
            return true;
        else
            return false;
    }

    private IEnumerator CrtRoll()
    {
        Invincible = true;
        animator.SetBool("Roll", true);
        rollCheck = true;

        roll.GetComponent<Image>().fillAmount = 0;

        float time = 2.5f;
        float minTime;
        float cool;

        cool = time;

        Speed *= 2;
        minTime = 0.003f / time;

        while (time > 0f)
        {
            if (time < 0.1f)
            {
                Invincible = false;
                animator.SetBool("Roll", false);
                rollCheck = false;
                Speed = 0.08f;

                spriteRenderer.color = color;
                break;
            }

            roll.GetComponent<Image>().fillAmount = (1.0f / time);
            //yield return new WaitForFixedUpdate();

            time -= 0.1f;
            Speed -= minTime;

            yield return Wait;
        }
    }



    private IEnumerator Invincibility()
    {
        int time = 100;

        spriteRenderer.color = color;
        while (Invincible && time > 0)
        {
            time--;

            if (!rollCheck)
            {
                if (time % 50 > 25)
                    spriteRenderer.color = new Color(1, 1, 1, 0.8f);
                else
                    spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            }

            if (time < 5)
            {
                spriteRenderer.color = color;
                Invincible = false;
                break;
            }

            yield return Wait;
        }
    }

    protected override IEnumerator Glitch()
    {

        glitchEffect.flipIntensity = 0.55f;
        glitchEffect.colorIntensity = 0.45f;

        yield return new WaitForSeconds(0.5f);

        glitchEffect.flipIntensity = 0f;
        glitchEffect.colorIntensity = 0f;

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Invincible)
            return;

        if (collision.gameObject.layer == 3)
        {
            //Destroy(collision.gameObject);
            collision.gameObject.SetActive(false);
            Hit();

            Invincible = true;
            StartCoroutine(Glitch());
            StartCoroutine(Invincibility());
        }

        if (collision.gameObject.layer == 6)
        {
            LoadSceneController.LoadScene("GameDengeonScene");
            //SceneManager.LoadScene("GameDengeonScene");
        }
    }
}

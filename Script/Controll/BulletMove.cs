using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove: ControlManager
{
    // 불릿 애니메이션
    private Animator animator;

    // 불릿 속도
    private float speed;

    void Start()
    {
        // 불릿 스피드 설정
        speed = 15f;

        // 불릿 애니메이터 설정
        animator = GetComponent<Animator>();
        transform.localScale *= -1;
    }

    // 활성화시 실행
    private void OnEnable()
    {
        check = false;

        StartCoroutine(Updater());
    }

    private IEnumerator Updater()
    {
        while (true)
        {
            // 체크 = true 탈출, 비활성화시 연산을 멈추게 하기 위해서
            if (check)
                break;

            // 오른쪽 방향으로 speed 값 만큼 전진
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            //if (transform.position.magnitude > 30f)
                //this.gameObject.SetActive(false);

                //Destroy(gameObject);

            yield return wait;
        }
    }

    // 충돌 체크 TriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽 충돌시
        if (collision.gameObject.name == "Walls" || collision.gameObject.tag == "WALL")
        {
            // check값 설정 후 Bool값 셋팅 후 딜레이
            SetAnimator();
        }

        // 적 충돌 시
        if (collision.gameObject.tag == "Enemy")
        {
            SetAnimator();
        }
    }

    private IEnumerator Delay()
    {
        //check = false;
        yield return waitOne;

        // 비활성화
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }

    private void SetAnimator()
    {
        // While문 탈출
        check = true;

        // 소멸 애니메이션 트리거
        animator.SetBool("HIT", true);

        // 소멸하는 이펙트를 보여주기 위한 딜레이
        StartCoroutine(Delay());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletMove : MonoBehaviour {

    private static WaitForSeconds waitOne = new WaitForSeconds(0.5f);
    public float speed = 3f;        // 속도

    private Animator animator;
    private bool check;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // 활성화시
    private void OnEnable()
    {
        check = false;
    }

    private void Update()
    {
        if (check)  // 비활성화시 멈춤
            return;

        //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 벽에 닿았을때
        if (collision.gameObject.name == "Walls" || collision.gameObject.tag == "WALL")
        {
            check = true;
            animator.SetBool("HIT", true);
            StartCoroutine(Delay());
        }
    }

    // 벽에 닿았을때 비활성화 되기 전 애니메이션이 재생되기 위한 딜레이 시간
    private IEnumerator Delay()
    {
        //check = false;
        yield return waitOne;

        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

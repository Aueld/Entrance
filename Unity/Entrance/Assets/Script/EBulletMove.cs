using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBulletMove : MonoBehaviour {

    private static WaitForSeconds waitOne = new WaitForSeconds(0.5f);
    public float speed = 3f;

    private Animator animator;
    private bool check;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        check = false;
    }

    private void Update()
    {
        if (check)
            return;

        //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Walls")
        {
            check = true;
            animator.SetBool("HIT", true);
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        //check = false;
        yield return waitOne;

        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}

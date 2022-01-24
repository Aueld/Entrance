using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove: ControlManager
{
    private Animator animator;

    private float speed;

    void Start()
    {
        speed = 40f;

        animator = GetComponent<Animator>();
        transform.localScale *= -1;
    }

    private void OnEnable()
    {
        check = false;


        StartCoroutine(Updater());
    }

    private IEnumerator Updater()
    {
        while (true)
        {
            if (check)
                break;

            transform.Translate(Vector2.right * speed * Time.deltaTime);



            //if (transform.position.magnitude > 30f)
                //this.gameObject.SetActive(false);

                //Destroy(gameObject);

            yield return wait;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "WALL")
        {
            check = true;

            animator.SetBool("HIT", true);
            StartCoroutine(Delay());
        }
        

        if (collision.gameObject.tag == "Enemy")
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

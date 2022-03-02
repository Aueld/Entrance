using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiMik : MonoBehaviour
{
    public GameObject player;

    private bool check;

    private void Start()
    {
        check = true;

        StartCoroutine(Updater());
    }


    private IEnumerator Updater()
    {
        while (check)
        {
            if (player.transform.position.y > 8)
            {
                check = false;

                gameObject.SetActive(false);

            }
            yield return new WaitForSeconds(0.001f);
        }
    }
}

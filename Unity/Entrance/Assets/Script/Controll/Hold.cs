using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold : Unit
{
    public GameObject player;

    void Start()
    {
        StartCoroutine(Updater());
    }

    private IEnumerator Updater()
    {
        while (true)
        {
            transform.position = player.transform.position;
            yield return Wait;
        }
    }
}

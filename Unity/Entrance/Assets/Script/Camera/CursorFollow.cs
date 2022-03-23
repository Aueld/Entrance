using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : ControlManager
{
    void Start()
    {
        StartCoroutine(Updater());
    }
    private IEnumerator Updater()
    {
        while (true)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.LookAt(mouse);

            yield return wait;
        }
    }
}

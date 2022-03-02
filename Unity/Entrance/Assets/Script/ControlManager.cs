using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    protected static WaitForSeconds wait = new WaitForSeconds(0.01f);
    protected static WaitForSeconds waitOne = new WaitForSeconds(0.5f);
    protected static WaitForSeconds fire = new WaitForSeconds(0.2f);

    protected Vector2 mouse;
    protected bool check;

    protected float GetAngle(Vector3 start, Vector3 end)
    {
        Vector3 v2 = end - start;
        return Mathf.Atan2(v2.y, v2.x) * Mathf.Rad2Deg;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum AniState
    {
        Front,
        Back,
        FrontSide,
        BackSide
    }

    protected static WaitForSeconds Wait = new WaitForSeconds(0.01f);

    protected Animator animator;

    protected Vector2 Pos { get; set; }
    protected Vector3 Pos3D { get; set; }

    protected float PosX { get; set; }
    protected float PosY { get; set; }
    protected float PosZ { get; set; }
    protected int LR { get; set; }

    protected float RandX { get; set; }
    protected float RandY { get; set; }
    protected float Rand { get; set; }

    protected float Speed { get; set; }

    protected bool Check { get; set; }
    protected bool Invincible { get; set; }

    protected virtual void Move() { }
    public virtual void Hit() { }
    protected virtual void UnitLR(int size) { }
    protected virtual float GetDistance(float x1, float y1, float x2, float y2) { return 0; }
    protected virtual IEnumerator Glitch() { yield return Wait; }

    public int HP { get; set; }
}

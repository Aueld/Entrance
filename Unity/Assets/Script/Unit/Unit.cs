using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    protected enum AniState     // 애니메이션 상태
    {
        Front,
        Back,
        FrontSide,
        BackSide
    }

    protected static WaitForSeconds Wait = new WaitForSeconds(0.01f);

    protected Animator animator;

    protected Vector2 Pos { get; set; }     // 2D 위치 포지션
    protected Vector3 Pos3D { get; set; }   // 3D 위치 포지션

    protected float PosX { get; set; }      // X
    protected float PosY { get; set; }      // Y
    protected float PosZ { get; set; }      // Z
    protected int LR { get; set; }          // 좌우 판정

    protected float RandX { get; set; }     // Rand X
    protected float RandY { get; set; }     // Rand Y
    protected float Rand { get; set; }      // Rand

    protected float Speed { get; set; }     // 속도

    protected bool Check { get; set; }      // 판단용 Bool
    protected bool Invincible { get; set; } // 무적 판단

    protected virtual void Move() { }       // 이동 가상함수
    public virtual void Hit() { }           // 피격 가상함수
    protected virtual void UnitLR() { } // 좌우 판단 가상함수
    protected virtual float GetDistance(float x1, float y1, float x2, float y2) { return 0; }   // 거리 판정 가상 함수
    protected virtual IEnumerator Glitch() { yield return Wait; }                               // 글리치 이펙트 가상 코루틴

    public int HP { get; set; }                                                                 // 유닛의 체력
}

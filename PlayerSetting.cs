using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerSetting : Unit
{

    protected Shot shot;
    protected CursorMouse cursorMouse;
    protected GameObject pin;

    protected GlitchEffect glitchEffect;

    protected Dictionary<KeyCode, Action> keyDictionary;

    protected Dictionary<KeyCode, Action> keyDictionaryUp;
    protected bool rollCheck { get; set; }
    protected bool[] LRCheck = { false, false };
    protected float h { get; set; }
    protected float v { get; set; }
    protected int lastState { get; set; }

    protected SpriteRenderer spriteRenderer;
    protected Color color;

    protected void Awake()
    {
        rollCheck = false;

        Speed = 0.01f;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = new Color(1, 1, 1, 1);
    }

    protected void Start()
    {
        HP = 4;
        Invincible = false;
        shot = GameObject.FindGameObjectWithTag("Gun").GetComponentInChildren<Shot>();
        cursorMouse = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorMouse>();
        pin = GameObject.FindWithTag("Gun");

        keyDictionary = new Dictionary<KeyCode, Action>
        {
            { KeyCode.W, KeyDown_W },
            { KeyCode.S, KeyDown_S },
            { KeyCode.A, KeyDown_A },
            { KeyCode.D, KeyDown_D },
            { KeyCode.E, KeyDown_E },
            { KeyCode.R, KeyDown_R }
        };

        glitchEffect = Camera.main.GetComponent<GlitchEffect>();
    }

    protected void KeyDown_W()
    {
        animator.SetBool("RunBack", true);
    }

    protected void KeyDown_S()
    {
        animator.SetBool("RunFront", true);
    }

    protected void KeyDown_A()
    {
        LRCheck[0] = true;
        animator.SetBool("RunSide", true);
    }

    protected void KeyDown_D()
    {
        LRCheck[1] = true;
        animator.SetBool("RunSide", true);
    }

    protected virtual void KeyDown_E() {}

    protected virtual void KeyDown_R() {}

    protected void KeyDown()
    {
        if (Input.anyKey)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }

    protected void KeyUp()
    {
        if (Input.GetKeyUp(KeyCode.W))
            animator.SetBool("RunBack", false);

        if (Input.GetKeyUp(KeyCode.S))
            animator.SetBool("RunFront", false);

        if (Input.GetKeyUp(KeyCode.A))
            LRCheck[0] = false;

        if (Input.GetKeyUp(KeyCode.D))
            LRCheck[1] = false;

        if(!LRCheck[0] && !LRCheck[1])
            animator.SetBool("RunSide", false);
        
    }

    protected IEnumerator MeleeAttack()
    {
        animator.SetBool("MeleeAttack", true);
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("MeleeAttack", false);
    }
}

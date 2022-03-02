using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPImage : MonoBehaviour
{
    private static WaitForSeconds wait = new WaitForSeconds(0.001f);

    public Sprite[] sprite;

    private Image image;
    private Player player;
    private int oldHp;

    private void Start()
    {
        image = GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        oldHp = 4;
        SetImage(oldHp);

        StartCoroutine(Updater());
    }

    private IEnumerator Updater()
    {
        while (true)
        {
            if (player.HP < oldHp)
            {
                Change();
            }
            if (player.HP > oldHp)
            {
                if(oldHp < 0)
                    yield return wait;

                Change();
            }
            yield return wait;
        }
    }

    public void Change()
    {
        oldHp = player.HP;
        SetImage(oldHp);
    }

    private void SetImage(int hp)
    {
        image.sprite = sprite[hp];
    }
}

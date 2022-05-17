using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPImage : MonoBehaviour
{
    private static WaitForSeconds wait = new WaitForSeconds(0.001f);

    // 체력 스프라이트
    public Sprite[] sprite;

    private Image image;
    private Player player;  // 플레이어
    private int oldHp;      // 변경되기 전 체력

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
            // 체력 변경이 있을때 이미지 변경
            if (player.HP < oldHp)
            {
                Change();
            }
            if (player.HP > oldHp)
            {
                // 체력이 0 이하로 내려갈때 이미지 변경 안되게 설정
                if(oldHp < 0)
                    yield return wait;

                Change();
            }
            yield return wait;
        }
    }

    public void Change()
    {
        // 체력만큼 체력 이미지 표시
        oldHp = player.HP;
        SetImage(oldHp);
    }

    private void SetImage(int hp)
    {
        // 이미지 전달
        image.sprite = sprite[hp];
    }
}

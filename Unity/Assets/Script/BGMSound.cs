using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSound : MonoBehaviour
{
    AudioSource BGM;        // 음향 파일
    public Slider slider;   // 음향 조절 바

    private void Start()
    {
        BGM = GetComponent<AudioSource>();
    }

    public void BGMControl()
    {
        // 슬라이드 값에 대입
        BGM.volume = slider.value;
    }
}

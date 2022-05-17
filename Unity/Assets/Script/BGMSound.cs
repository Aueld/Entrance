using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSound : MonoBehaviour
{
    AudioSource BGM;
    public Slider slider;

    private void Start()
    {
        BGM = GetComponent<AudioSource>();
    }

    public void BGMControl()
    {
        BGM.volume = slider.value;
    }
}

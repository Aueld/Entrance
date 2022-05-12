using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // 오디오 소스
    private AudioSource audioSource;
    private AudioSource audioSource2;

    // 오디오 파일
    public GameObject audioo;

    // 오디오 클립
    public AudioClip Shot;
    public AudioClip Reload;

    // 최저음, 최고음
    public float lowPitRange;
    public float highPitRange;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource2 = audioo.GetComponent<AudioSource>();
        lowPitRange = .55f;
        highPitRange = .75f;
    }
    
    // 이펙트 효과음 재생
    public void PlayEff()
    {
        float randomPitch = Random.Range(lowPitRange, highPitRange);

        audioSource.volume = 0.7f;

        audioSource.pitch = randomPitch;
        audioSource.clip = Shot;
        audioSource.Play();
    }

    // 재장전 효과음 재생
    public void Reloading()
    {
        float randomPitch = Random.Range(lowPitRange, highPitRange);

        audioSource2.volume = 0.35f;

        audioSource2.pitch = randomPitch;
        audioSource2.clip = Reload;
        audioSource2.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource audioSource2;

    public GameObject audio;

    public AudioClip Shot;
    public AudioClip Reload;

    public float lowPitRange;
    public float highPitRange;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource2 = audio.GetComponent<AudioSource>();
        lowPitRange = .55f;
        highPitRange = .75f;
    }
    
    public void PlayEff()
    {
        float randomPitch = Random.Range(lowPitRange, highPitRange);

        audioSource.volume = 0.7f;

        audioSource.pitch = randomPitch;
        audioSource.clip = Shot;
        audioSource.Play();
    }

    public void Reloading()
    {
        float randomPitch = Random.Range(lowPitRange, highPitRange);

        audioSource2.volume = 0.35f;

        audioSource2.pitch = randomPitch;
        audioSource2.clip = Reload;
        audioSource2.Play();
    }
}

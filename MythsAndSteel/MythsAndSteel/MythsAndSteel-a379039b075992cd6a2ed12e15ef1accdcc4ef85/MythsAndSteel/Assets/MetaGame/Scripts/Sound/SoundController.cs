using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoSingleton<SoundController>
{
    AudioMixer AudioMixer;
    [SerializeField] AudioSource _Source;
    public AudioSource Source => _Source;

    public void PlaySound(AudioClip SoundPlay)
    {
        AudioClip tolpay = SoundPlay;
        _Source.clip = tolpay;

        _Source.Play();
    }
}

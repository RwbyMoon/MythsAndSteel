using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoSingleton<SoundController>
{
    AudioMixer AudioMixer;
    [SerializeField] List<AudioSource> _Source = new List<AudioSource>();
    public List<AudioSource> Source => _Source;
    [SerializeField] List<AudioClip> _audioClip = new List<AudioClip>();
    public List<AudioClip> AudioClips => _audioClip;


    public void PlaySound(AudioClip SoundPlay, string log = null)
    {
        //Debug.Log("Son lancé"); 
        for (int i = 0; i < _Source.Count; i++) 
        {
            //Debug.Log("Uraken");
            if (!_Source[i].isPlaying)
            {

                _Source[i].clip = SoundPlay;
                if (log == null) log = SoundPlay.name;
                Debug.Log(log);

                if (_Source[i].clip != null)
                {
                    _Source[i].Play();
                }
                else Debug.Log("No sound");
                break;
            }
        }
    }


    private void Start()
    {
        if(_audioClip[1] != null) PlaySound(_audioClip[1]);
    }
    public void ValidationSon()
    {
        PlaySound(_audioClip[12]);
    }

    public void UINav()
    {
        PlaySound(_audioClip[13]);
    }


    public void nextPhaseSound()
    {
        PlaySound(_audioClip[0]);
    }

}

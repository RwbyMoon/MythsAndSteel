using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLaunch : MonoBehaviour
{
   public AudioSource Musicsource;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("MusicSplash") == 1)
        {
            Musicsource.enabled = true;
            Musicsource.Play();
        }
    }


}

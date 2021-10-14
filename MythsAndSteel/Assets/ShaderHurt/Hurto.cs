using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurto : MonoBehaviour
{
    public SpriteRenderer Renderer;
  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayFeedback();
            
            
        }

    
    }

    void PlayFeedback()
    {
        Renderer.material.SetFloat("_HitTime", Time.time);
   
      

    }


    
}

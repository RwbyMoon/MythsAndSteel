using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingUngrowingSclae : MonoBehaviour
{
    Vector3 CurrentScale;
    [SerializeField] private Vector3 ScaleChanging;
    bool isPositiveGain = true;
    byte AccumulationLight;
    public void Start()
    {
        CurrentScale = transform.localScale;
    }
    public void Update()
    {
        
        if (isPositiveGain == true)
        {
            CurrentScale.x += ScaleChanging.x;
            CurrentScale.y += ScaleChanging.y;
            AccumulationLight += 1;
        }

        if (isPositiveGain == false)
        {
            CurrentScale.x -= ScaleChanging.x;
            CurrentScale.y -= ScaleChanging.y;
            AccumulationLight += 1;
        }

        if (AccumulationLight == 150)
        {
            isPositiveGain = !isPositiveGain;
            AccumulationLight = 0;
        }
        transform.localScale = CurrentScale;
    }

}   

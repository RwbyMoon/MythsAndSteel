using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>{
    private static T _instance;
    public static T Instance => _instance;

    private void Awake(){
        if(_instance == null) 
        {
            _instance = (T)this;

        }
    }
}

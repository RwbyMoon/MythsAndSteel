using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenfortBtnUI : MonoBehaviour
{
    [SerializeField] private GameObject _imgBtn = null;
    
    public void ShowCanvas(){
        _imgBtn.SetActive(true);
    }

    public void HideCanvas()
    {
        _imgBtn.SetActive(false);
    }
}

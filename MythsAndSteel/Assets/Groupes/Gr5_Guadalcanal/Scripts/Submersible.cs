using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submersible : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnitScript>().Submersible = true;
    }
}

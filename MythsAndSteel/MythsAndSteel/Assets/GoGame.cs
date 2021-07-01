using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoGame : MonoBehaviour
{
    // Start is called before the first frame update
void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.SetInt("Avertissement", 1);
            PlayerPrefs.SetInt("Volume", 1);
            SceneManager.LoadScene(1);
        }
    }


}

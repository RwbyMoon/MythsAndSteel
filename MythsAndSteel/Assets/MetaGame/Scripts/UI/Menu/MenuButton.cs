using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject _campagneCanvas;
    [SerializeField] GameObject _menuCanvas;

    /// <summary>
    /// Change de scène
    /// </summary>

    public void ChangeScene(int sceneToLoad)
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }

    /// <summary>
    /// Quitte le jeu
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// ouvre le menu campagne
    /// </summary>
    public void LoadCampagneMenu()
    {
        _campagneCanvas.SetActive(true);
        _menuCanvas.SetActive(false);
  
    }

    /// <summary>
    /// Quitte le menu campagne
    /// </summary>
    public void QuitCampagneMenu()
    {
        _campagneCanvas.SetActive(false);
        _menuCanvas.SetActive(true);
    }

    //reload la scene
    public void ReloadScene()
    {
     
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
    }



}
    
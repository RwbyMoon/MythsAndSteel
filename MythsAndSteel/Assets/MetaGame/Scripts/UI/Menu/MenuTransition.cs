using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
Ce Script permet d'afficher ou d'enlever les Options en appuyant sur Echap. 
Il fait apparaitre une autre sc�ne qui va se superposer � la sc�ne principal.
 */

[CreateAssetMenu(menuName = "META/Transition Menu")]
public class MenuTransition : ScriptableObject
{
    /// <summary>
    /// Active une sc�ne
    /// </summary>
 
    public void LoadScene(int sceneId)
    {
        SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
    }
    public void LoadSceneOptionInGame(int sceneId)
    {
       GameManager.Instance.menuOptionOuvert = true;
        SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);

    }
    public void DesactivMenuOptionInGame(int sceneId)
    {
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
        Scene[] eho = SceneManager.GetAllScenes();
        if (eho[0].buildIndex != 1)
        {

            GameManager.Instance.menuOptionOuvert = false;
        }
      
        SceneManager.UnloadSceneAsync(sceneId);
    }
    /// <summary>
    /// D�sactive une sc�ne
    /// </summary>
    /// <param name="sceneId"></param>
    public void DesactivMenu(int sceneId)
    {
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
        SceneManager.UnloadSceneAsync(sceneId);
    }
}

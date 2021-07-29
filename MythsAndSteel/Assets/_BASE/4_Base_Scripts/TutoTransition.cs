using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[CreateAssetMenu(menuName = "META/Transition Tuto")]
public class TutoTransition : ScriptableObject
{
    /// <summary>
    /// Active une scène
    /// </summary>
    public void LoadSceneTutoriel(int sceneId)
    {
        GameManager.Instance.menuTutorielOuvert = true;
        SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Additive);
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
    }

    public void DesactivMenuTutoriel(int sceneId)
    {
        SoundController.Instance.PlaySound(SoundController.Instance.AudioClips[13]);
        Scene[] eho = SceneManager.GetAllScenes();
        if (eho[0].buildIndex != 1)
        {

            GameManager.Instance.menuTutorielOuvert = false;
        }

        SceneManager.UnloadSceneAsync(sceneId);
    }
}
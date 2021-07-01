using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseOverUI : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSpawn = null;
    [SerializeField] private float _timeToWait = 0.3f;

    private void Start(){
        _objectToSpawn.SetActive(false);
    }

    /// <summary>
    /// Quand la souris passe sur l'UI
    /// </summary>
    public void StartOver(){
        if(!_objectToSpawn.activeSelf)
        StartCoroutine(ShowObject());
    }

    /// <summary>
    /// Quand la souris sort de l'UI
    /// </summary>
    public void StopOver(){
        StopAllCoroutines();
        _objectToSpawn.SetActive(false);
    }

    /// <summary>
    /// Attend avant d'afficher l'objet
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowObject(){
        yield return new WaitForSeconds(_timeToWait);
        _objectToSpawn.SetActive(true);
    }
}

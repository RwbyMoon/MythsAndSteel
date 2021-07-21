using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blitzkrieg : Capacity
{
    public AudioClip ActivUp;
    public AudioSource audioSource;
    public override void StartCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = true;
        int ressourcePlayer = GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.J1Infos.Ressource : PlayerScript.Instance.J2Infos.Ressource;
        if (ressourcePlayer >= Capacity1Cost)
        {
            List<GameObject> tile = new List<GameObject>();


            Debug.Log("oui");
            GameManager.Instance._eventCall += EndCpty;
            GameManager.Instance._eventCallCancel += StopCpty;
            Debug.Log("oui");
            
            if (PlayerPrefs.GetInt("Avertissement") == 0)
            {
                GameManager.Instance._eventCall();

            }
            UIInstance.Instance.ShowValidationPanel("Blitzkrieg!", "Voulez-vous vraiment acquerir deux activations supplémentaire ce tour ?");
        }
        base.StartCpty();

    }

    public override void StopCpty()
    {
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
        GameManager.Instance.StopEventModeTile();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().StopCapacity(true);
        base.StopCpty();
    }

    public override void EndCpty()
    {
        audioSource.PlayOneShot(ActivUp, 1f);
         Player player = GameManager.Instance.IsPlayerRedTurn ? PlayerScript.Instance.J1Infos : PlayerScript.Instance.J2Infos;
        Debug.Log("oui");
        player.Ressource -= Capacity1Cost;
        player.ActivationLeft += 2;
        UIInstance.Instance.UpdateRessourceLeft();
        UIInstance.Instance.UpdateActivationLeft();
        Debug.Log("Inchallah");
        GameManager.Instance._eventCall -= EndCpty;
        GetComponent<UnitScript>().EndCapacity();
        base.EndCpty();
        GameManager.Instance.TileChooseList.Clear();
        GetComponent<UnitScript>().IsActifNotConsumeAction = false;
        GetComponent<Animator>().SetBool("Blitzkrieg", true);
        StartCoroutine(WaitEndAnim());
    }

    IEnumerator WaitEndAnim()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + 1.5f);
        GetComponent<Animator>().SetBool("Blitzkrieg", false);
    }
}

